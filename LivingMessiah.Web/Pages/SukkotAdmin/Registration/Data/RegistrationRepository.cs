using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using static LivingMessiah.Web.Pages.Sukkot.Constants.SqlServer;
using LivingMessiah.Web.Pages.SukkotAdmin.Data;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;

public interface IRegistrationRepository
{
	string BaseSqlDump { get; }
	Task<List<Domain.Registration>> GetAll();  
	Task<RegistrationPOCO> GetPocoById(int id);
	Task<Tuple<int, int, string>> Create(RegistrationPOCO registration);
	Task<Tuple<int, int, string>> Update(RegistrationPOCO registration);
	Task<List<RegistrationLookup>> PopulateRegistrationLookup();
	Task<Sukkot.Components.RegistrationVM> GetByIdVer2(int id);
}


public class RegistrationRepository : BaseRepositoryAsync, IRegistrationRepository
{
	public RegistrationRepository(IConfiguration config, ILogger<RegistrationRepository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump; }
	}


	public async Task<Sukkot.Components.RegistrationVM> GetByIdVer2(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
--DECLARE @Id int = 32
SELECT TOP 1 
Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone, Adults, ChildBig, ChildSmall
, StatusId
, AttendanceBitwise, LmmDonation, Notes, Avatar
, Sukkot.udfAttendanceDatesConcat(Id) AS AttendanceDatesCSV
FROM Sukkot.Registration 
WHERE Id = @Id
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Sukkot.Components.RegistrationVM>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault();
		});
	}

	public async Task<List<RegistrationLookup>> PopulateRegistrationLookup()
	{
		base.Sql = $@"
SELECT Id AS ID, Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Text
FROM Sukkot.Registration
ORDER BY FirstName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationLookup>(base.Sql);
			return rows.ToList();
		});
	}

	public async Task<List<Domain.Registration>> GetAll()
	{
		base.Sql = $@"
SELECT Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone
, Adults, ChildBig, ChildSmall
, StatusId
, AttendanceBitwise
, Notes
, Sukkot.udfAttendanceDatesConcat(Id) AS AttendanceDatesCSV
--, LmmDonation, Avatar
FROM Sukkot.Registration
ORDER BY FirstName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Domain.Registration>(sql: base.Sql);  //, param: base.Parms
					return rows.ToList();
		});
	}

	public async Task<RegistrationPOCO> GetPocoById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
--DECLARE @id int=4
SELECT TOP 1 
Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone, Adults, ChildBig, ChildSmall
, StatusId
, AttendanceBitwise, LmmDonation, Notes, Avatar
, Sukkot.udfAttendanceDatesConcat(Id) AS AttendanceDatesCSV
FROM Sukkot.Registration WHERE Id = @Id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationPOCO>(sql: base.Sql, param: base.Parms);
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
			StatusId = registration.StatusId,
			AttendanceBitwise = registration.AttendanceBitwise,
			LmmDonation = 0,
			Avatar = registration.Avatar,
			Notes = registration.Notes,
		});

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(RegistrationRepository)}!{nameof(Create)}, {nameof(registration.EMail)}:{registration.EMail}; a about to execute SPROC: {base.Sql}");
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>(ReturnValueName);
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (SprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; registration.EMail: {@registration.EMail}; SprocReturnValue: {SprocReturnValue}";
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

	public async Task<Tuple<int, int, string>> Update(RegistrationPOCO registration)
	{
		base.Sql = "Sukkot.stpRegistrationUpdate";
		base.Parms = new DynamicParameters(new
		{
			Id = registration.Id,
			FamilyName = registration.FamilyName,
			FirstName = registration.FirstName,
			SpouseName = registration.SpouseName,
			OtherNames = registration.OtherNames,
			EMail = registration.EMail,
			Phone = registration.Phone,
			Adults = registration.Adults,
			ChildBig = registration.ChildBig,
			ChildSmall = registration.ChildSmall,
			AttendanceBitwise = registration.AttendanceBitwise,
			StatusId = registration.StatusId,
			LmmDonation = registration.LmmDonation,
			Notes = registration.NotesScrubbed,
			Avatar = registration.Avatar
		});

		base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(RegistrationRepository)}!{nameof(Update)}, {nameof(registration.Id)}:{registration.Id}; about to execute SPROC: { base.Sql}");
			RowsAffected = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>(ReturnValueName);

			if (SprocReturnValue != ReturnValueOk)
			{
				if (SprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					ReturnMsg = $"Database call did not update the record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
				else
				{
					ReturnMsg = $"Database call falied; {nameof(registration.Id)}:{registration.Id}, {nameof(registration.EMail)}:{registration.EMail}; SprocReturnValue: {SprocReturnValue}";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
			}
			else
			{
				ReturnMsg = $"Registration updated for {registration.FamilyName}/{registration.EMail}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);
		});
	}
}
