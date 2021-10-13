using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;  // ToDo: Move this to LivingMessiah.Web.Data
using Domain = LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data
{
	public interface IRegistrationRepository
	{
		string BaseSqlDump { get; }
		Task<List<Domain.Registration>> GetAll();  //BaseRegistrationSortSmartEnum sort
		Task<RegistrationPOCO> GetPocoById(int id);
		Task<Tuple<int, int, string>> Create(RegistrationPOCO registration);
		Task<int> Update(RegistrationPOCO registration);
		
		/*
		Task<Domain.Registration> ById(int id);
		Task<vwRegistrationShell> ByEmail(string email);

		Task<int> Delete(int id);
		Task<List<Domain.Notes>> GetNotes(BaseRegistrationSortSmartEnum sort);
		*/
	}

	public class RegistrationRepository : BaseRepositoryAsync, IRegistrationRepository
	{
		const int ViolationInUniqueIndex = 2601;

		public RegistrationRepository(IConfiguration config, ILogger<RegistrationRepository> logger) : base(config, logger)
		{
		}

		public string BaseSqlDump
		{
			get { return base.SqlDump; }
		}

		//BaseDonationStatusFilterSmartEnum filter, string sortAndOrder
		public async Task<List<Domain.Registration>> GetAll()
		{
			//base.Parms = new DynamicParameters(new { DonationStatus = filter.Value });

			//string sortField = sort switch
			//{
			//	BaseRegistrationSortSmartEnum.ById => "LocationEnum, Id",
			//	BaseRegistrationSortSmartEnum.ByLastName => "LocationEnum, FamilyName",
			//	BaseRegistrationSortSmartEnum.ByFirstName => "LocationEnum, FirstName",
			//	_ => "Id",
			//};


			base.Sql = $@"
SELECT Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone
, Adults, ChildBig, ChildSmall
, CampId -- Offsite, Tent, RV Hookup, Cabin/BH, RV DryCamp
, LocationEnum AS LocationInt
, StatusId
, AttendanceBitwise, LodgingDaysBitwise
, Notes
, Sukkot.udfLodgingDatesConcat(Id) AS LodgingDatesCSV
, Sukkot.udfAttendanceDatesConcat(Id) AS AttendanceDatesCSV
--, AssignedLodging, LmmDonation, WillHelpWithMeals, Avitar
FROM Sukkot.Registration
ORDER BY FirstName
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<Domain.Registration>(sql: base.Sql);  //, param: base.Parms
				return rows.ToList();
			});
		}

		// ById NOT USED
		/*
		public async Task<Domain.Registration> ById(int id)
		{
			base.Parms = new DynamicParameters(new { Id = id });
			base.Sql = $@"
SELECT Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone
, Adults, ChildBig, ChildSmall
, CampId 
, LocationEnum AS LocationInt
, StatusId
, AttendanceBitwise, LodgingDaysBitwise
, Notes
FROM Sukkot.Registration
WHERE Id = @id
";

			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<Domain.Registration>(sql: base.Sql, param: base.Parms);
				return rows.SingleOrDefault();
			});
		}
		*/




		public async Task<RegistrationPOCO> GetPocoById(int id)
		{
			base.Sql = $@"
SELECT TOP 1 
Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone, Adults, ChildBig, ChildSmall
, LocationEnum, CampId AS CampTypeEnum, StatusId AS StatusEnum
, AttendanceBitwise, LodgingDaysBitwise, AssignedLodging, LmmDonation, WillHelpWithMeals, Notes, Avitar
, Sukkot.udfLodgingDatesConcat(Id) AS LodgingDatesCSV
, Sukkot.udfAttendanceDatesConcat(Id) AS AttendanceDatesCSV
FROM Sukkot.Registration WHERE Id = {id}";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<RegistrationPOCO>(sql: base.Sql);
				return rows.SingleOrDefault();
			});
		}

		public async Task<Tuple<int, int, string>> Create(RegistrationPOCO registration)
		{
			base.Sql = "Sukkot.stpRegistrationInsert";
			base.Parms = new DynamicParameters(new
			{
				FamilyName = registration.FamilyName,
				FirstName = registration.FirstName,
				SpouseName = registration.SpouseName,
				OtherNames = registration.OtherNames,
				Email = registration.EMail,
				Phone = registration.Phone,
				Adults = registration.Adults,
				ChildBig = registration.ChildBig,
				ChildSmall = registration.ChildSmall,

				/*
				LocationEnum = registration.LocationSmartEnum,
				CampId = registration.CampTypeSmartEnum, 
				StatusId = registration.StatusSmartEnum, 
				*/
				LocationEnum = registration.LocationEnum,
				CampId = registration.CampId, 
				StatusId = registration.StatusId, 

				AttendanceBitwise = registration.AttendanceBitwise,
				LodgingDaysBitwise = registration.LodgingDaysBitwise,

				AssignedLodging = "",
				LmmDonation = 0,
				WillHelpWithMeals = 0,
				Avitar = registration.Avitar,
				Notes = registration.Notes,
			});;

			base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			base.Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

			int NewId = 0;
			int SprocReturnValue = 0;
			string ReturnMsg = "";

			return await WithConnectionAsync(async connection =>
			{
				base.log.LogDebug($"Inside {nameof(RegistrationRepository)}!{nameof(Create)}; About to execute sql:{base.Sql}");
				var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
				SprocReturnValue = base.Parms.Get<int>("ReturnValue");
				int? x = base.Parms.Get<int?>("NewId");
				if (x==null)
				{
					if (SprocReturnValue == ViolationInUniqueIndex)
					{
						ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
						base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
					}
					else
					{
						ReturnMsg = $"Database call falied; registration.EMail: {@registration.EMail}; SprocReturnValue: {SprocReturnValue}";
						base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
					}
				}
				else
				{
					NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
					ReturnMsg = $"Registration created for {registration.FamilyName}/{registration.EMail}; NewId={NewId}";
					base.log.LogDebug($"...Return NewId:{NewId}");
				}

				return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

			});
		}

		public async Task<int> Update(RegistrationPOCO registration)
		{
			/*
			LocationEnum = registration.LocationSmartEnum,
			CampId = registration.CampTypeSmartEnum, 
			StatusId = registration.StatusSmartEnum, 
			*/

			base.Sql = $@"
UPDATE Sukkot.Registration SET 
	FamilyName = N'{registration.FamilyName}',
	FirstName = N'{registration.FirstName}',
	SpouseName = N'{registration.SpouseName}',
	OtherNames = N'{registration.OtherNames}',
	EMail = N'{registration.EMail}',
	Phone = N'{registration.Phone}',
	Adults = {registration.Adults},
	ChildBig = {registration.ChildBig},
	ChildSmall = {registration.ChildSmall},

	AttendanceBitwise = {registration.AttendanceBitwise},
	LodgingDaysBitwise = {registration.LodgingDaysBitwise},

	LocationEnum = registration.LocationEnum,
	CampId = registration.CampId, 
	StatusId = registration.StatusId, 

	WillHelpWithMeals = {registration.WillHelpWithMealsToInt}, 
	LmmDonation = {registration.LmmDonation},
	AssignedLodging = N'{registration.AssignedLodging}',
	Notes = N'{registration.NotesScrubbed}',
	Avitar = N'{registration.Avitar}'
WHERE Id = {registration.Id};
";
			return await WithConnectionAsync(async connection =>
			{
				var count = await connection.ExecuteAsync(sql: base.Sql);
				return count;
			});
		}

			//base.Parms = new DynamicParameters(new {
			//	RegistrationId = donation.RegistrationId,
			//	CreateDate = donation.CreateDate
			//});		 
		

		/*
		public async Task<int> Delete(int id)
		{
			base.Sql = "Sukkot.stpRegistrationDelete";
			base.Parms = new DynamicParameters(new { RegistrationId = id });
			return await WithConnectionAsync(async connection =>
			{
				var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
				//if (affectedrows < 0) { throw new Exception($"Registration NOT Deleted"); }
				return affectedrows;
			});
		}

		*/


		/*

		public async Task<List<Domain.Notes>> GetNotes(BaseRegistrationSortSmartEnum sort)
		{
			string sortField = (sort == BaseRegistrationSortSmartEnum.ByLastName) ? "FamilyName" : "Id";

			base.Sql = $@"
SELECT TOP 500 Id, FirstName, FamilyName, Notes AS UserNotes, AssignedLodging, CampCD, Phone, EMail
FROM Sukkot.vwRegistration
WHERE Notes IS NOT NULL AND TRIM(Notes) <> ''
ORDER BY {sortField}
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<Domain.Notes>(sql: base.Sql, param: base.Parms);
				return rows.ToList();
			});
		}
		*/

	}
}
