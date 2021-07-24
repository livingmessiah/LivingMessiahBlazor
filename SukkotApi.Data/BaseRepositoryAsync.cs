using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SukkotApi.Data
{
	public abstract class BaseRepositoryAsync
	{
		const string configationConnectionKey = "ConnectionStrings:Sukkot";

		private readonly IConfiguration config;
		protected readonly ILogger log;
		protected BaseRepositoryAsync(IConfiguration config, ILogger<BaseRepositoryAsync> logger)
		{
			this.config = config;
			this.log = logger;
		}

		/// <summary>
		/// This method is responsible for ensuring that the connection is opened and closed safely and also ensures that we are always using an asynchronous connection.
		/// We open and close the connection with each method since SQL is going to manage our connection pooling and optimize this for us anyway
		/// We'll use a delegate here that matches a method that takes an argument of type IDbConnection and returns a Task of type T.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="getData">Delegate that matches a method that takes an argument of type IDbConnection and returns a Task of type T</param>
		/// <returns>Task of type T - we'll be using this to build and execute our query</returns>
		protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> getData)
		{
			string connectionString = config[configationConnectionKey];
			string errMsg = "";

			try
			{
				if (string.IsNullOrEmpty(connectionString))
				{
					string err = $"Inside {GetType().FullName}.{nameof(WithConnectionAsync)}; Connection string is null or empty.  configationConnectionKey={configationConnectionKey}";
					throw new ArgumentException(err);
				}

				using (var connect = new SqlConnection(connectionString))
				{
					await connect.OpenAsync();
					return await getData(connect);
				}
			}
			catch (TimeoutException ex)
			{
				errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} experienced a Sql Timeout <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
				log.LogError(ex, errMsg);
				throw new Exception(errMsg, ex);
			}
			catch (SqlException ex)
			{
				errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} experienced a Sql Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
				log.LogError(ex, errMsg);
				if (ex.Message != null) { errMsg += "<br /> ex.Message:" + ex.Message;	}
				throw new Exception(errMsg, ex);
			}
			catch (InvalidOperationException ex)
			{
				errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} experienced a Invalid Operation Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
				log.LogError(ex, errMsg);
				throw new Exception(errMsg, ex);
			}
			catch (Exception ex)
			{
				errMsg = $"{GetType().FullName}.{nameof(WithConnectionAsync)} Generic Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
				log.LogError(ex, errMsg);
				throw new Exception(errMsg, ex);
			}
		}

		public string Sql { get; set; }
		public DynamicParameters Parms { get; set; }  // using Dapper; Note, only place dependent on Dapper


		public string SqlDump
		{
			get
			{
				string s = "";
				s = Sql ?? "SQL IS NULL";
				if (Parms != null)
				{
					/* See Notes in LivingMessiah.Data!BaseRepositoryAsync*/
				}
				return s;
			}
		}


	}
}

