using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using Stripe;

namespace SukkotPaymentApp;

public static class SukkotPaymentMonitor //static
{
	[FunctionName("AddSukkotPaymentToDb")]
	public static async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
			ILogger log)
	{
		log.LogInformation(string.Format("Inside {0}, a {1} type of function, that does {2}"
			, "SukkotPaymentMonitor", "HTTP post trigger", "Sukkot payment monitoring from e.g. Stripe.com"));

		string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
		var data = JsonConvert.DeserializeObject<Rootobject>(requestBody);

		Donation donation = new Donation
		{
			RegistrationId = 4,
			Amount = data._object.amount_captured,
			Notes = "Statement descriptor: " + data._object.calculated_statement_descriptor + ", is paid: " + data._object.paid,
			ReferenceId = "object.id: " + data._object.id,
			CreateDate = DateTime.UtcNow,
			CreatedBy = "billing email: " + data._object.billing_details.email // data._object.charges.data.am
		};


		var connectionString = Environment.GetEnvironmentVariable("SukkotConnection");

		#region Dapper
		string Sql = "Sukkot.stpDonationInsert ";

		DynamicParameters Parms = new DynamicParameters(new
		{
			RegistrationId = donation.RegistrationId,
			Amount = donation.Amount,
			Notes = donation.Notes,
			ReferenceId = donation.ReferenceId,
			CreatedBy = donation.CreatedBy,
			CreateDate = donation.CreateDate
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		using (var connection = new SqlConnection(connectionString))
		{
			await connection.OpenAsync();
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				log.LogWarning($"NewId is null; returning as 0; Check dbo.ErrorLog for FK_Donation_Registration conflict Error; donation.RegistrationId: {donation.RegistrationId}");
				//return 0;
			}
			else
			{
				int NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				log.LogDebug($"Return NewId:{NewId}");
				//return NewId;
			}

		}
		#endregion

		//log.LogInformation(requestBody);

		return new OkResult();
	}
}

#region charge.succeeded


public class Rootobject
{
	public Object _object { get; set; }
}

public class Object
{
	public string id { get; set; }
	public string _object { get; set; }
	public int amount { get; set; }
	public int amount_captured { get; set; }
	public int amount_refunded { get; set; }
	public object application { get; set; }
	public object application_fee { get; set; }
	public object application_fee_amount { get; set; }
	public string balance_transaction { get; set; }
	public Billing_Details billing_details { get; set; }
	public string calculated_statement_descriptor { get; set; }
	public bool captured { get; set; }
	public int created { get; set; }
	public string currency { get; set; }
	public object customer { get; set; }
	public object description { get; set; }
	public object destination { get; set; }
	public object dispute { get; set; }
	public bool disputed { get; set; }
	public object failure_balance_transaction { get; set; }
	public object failure_code { get; set; }
	public object failure_message { get; set; }
	public Fraud_Details fraud_details { get; set; }
	public object invoice { get; set; }
	public bool livemode { get; set; }
	public Metadata metadata { get; set; }
	public object on_behalf_of { get; set; }
	public object order { get; set; }
	public Outcome outcome { get; set; }
	public bool paid { get; set; }
	public string payment_intent { get; set; }
	public string payment_method { get; set; }
	public Payment_Method_Details payment_method_details { get; set; }
	public object receipt_email { get; set; }
	public object receipt_number { get; set; }
	public string receipt_url { get; set; }
	public bool refunded { get; set; }
	public Refunds refunds { get; set; }
	public object review { get; set; }
	public object shipping { get; set; }
	public object source { get; set; }
	public object source_transfer { get; set; }
	public object statement_descriptor { get; set; }
	public object statement_descriptor_suffix { get; set; }
	public string status { get; set; }
	public object transfer_data { get; set; }
	public object transfer_group { get; set; }
}

public class Billing_Details
{
	public Address address { get; set; }
	public string email { get; set; }
	public string name { get; set; }
	public object phone { get; set; }
}

public class Address
{
	public object city { get; set; }
	public string country { get; set; }
	public object line1 { get; set; }
	public object line2 { get; set; }
	public string postal_code { get; set; }
	public object state { get; set; }
}

public class Fraud_Details
{
}

public class Metadata
{
}

public class Outcome
{
	public string network_status { get; set; }
	public object reason { get; set; }
	public string risk_level { get; set; }
	public int risk_score { get; set; }
	public string seller_message { get; set; }
	public string type { get; set; }
}

public class Payment_Method_Details
{
	public Card card { get; set; }
	public string type { get; set; }
}

public class Card
{
	public string brand { get; set; }
	public Checks checks { get; set; }
	public string country { get; set; }
	public int exp_month { get; set; }
	public int exp_year { get; set; }
	public string fingerprint { get; set; }
	public string funding { get; set; }
	public object installments { get; set; }
	public string last4 { get; set; }
	public object mandate { get; set; }
	public string network { get; set; }
	public object three_d_secure { get; set; }
	public object wallet { get; set; }
}

public class Checks
{
	public object address_line1_check { get; set; }
	public string address_postal_code_check { get; set; }
	public string cvc_check { get; set; }
}

public class Refunds
{
	public string _object { get; set; }
	public object[] data { get; set; }
	public bool has_more { get; set; }
	public int total_count { get; set; }
	public string url { get; set; }
}


#endregion