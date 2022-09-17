using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Threading.Tasks;

namespace SukkotPaymentApp;

internal interface ICommands
{
	string BaseSqlDump { get; }
	Task<int> InsertRegistrationDonation(Donation donation);
}

internal class Commands : BaseRepositoryAsync, ICommands
{
	public Commands(IConfiguration config, ILogger<Commands> logger) : base(config, logger)
	{

	}

	public string BaseSqlDump
	{
		get { return base.SqlDump; }
	}

	public async Task<int> InsertRegistrationDonation(Donation donation)
	{
		base.Sql = "Sukkot.stpDonationInsert ";
		base.Parms = new DynamicParameters(new
		{
			RegistrationId = donation.RegistrationId,
			Amount = donation.Amount,
			Notes = donation.Notes,
			ReferenceId = donation.ReferenceId,
			CreatedBy = donation.CreatedBy,   // email,
			CreateDate = donation.CreateDate
		});

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		base.log.LogDebug($"Inside {nameof(Commands)}!{nameof(Commands)}!{nameof(InsertRegistrationDonation)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: CommandType.StoredProcedure);
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				base.log.LogWarning($"NewId is null; returning as 0; Check dbo.ErrorLog for FK_Donation_Registration conflict Error; donation.RegistrationId: {donation.RegistrationId}");
				return 0;
			}
			else
			{
				int NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				base.log.LogDebug($"Return NewId:{NewId}");
				return NewId;
			}
		});
	}

}
