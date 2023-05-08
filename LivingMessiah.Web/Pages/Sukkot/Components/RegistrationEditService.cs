using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Services;
using LivingMessiah.Web.Pages.Sukkot.Enums;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;
using LivingMessiah.Web.Pages.Sukkot.Services;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using static LivingMessiah.Web.Links.Store;
using LivingMessiah.Web.Pages.Admin.AudioVisual.Services;

namespace LivingMessiah.Web.Pages.Sukkot.Components;

public interface IRegistrationEditService
{
	string ExceptionMessage { get; set; }

	Task<RegistrationVM> GetById(int id);                                                                 
	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(RegistrationVM registration);        
	Task<(int RowsAffected, int SprocReturnValue, string ReturnMsg)> Update(RegistrationVM registration); 
}

public class RegistrationEditService : IRegistrationEditService
{
	#region Constructor and DI
	private readonly IRegistrationEditRepository db;
	private readonly ILogger Logger;
	private readonly ISecurityClaimsService SvcClaims;

	public RegistrationEditService(
		IRegistrationEditRepository repo,
		ILogger<IRegistrationEditService> logger,
		ISecurityClaimsService securityClaimsService)
	{
		db = repo;
		Logger = logger;
		SvcClaims = securityClaimsService;
	}
	#endregion

	public string ExceptionMessage { get; set; } = "";

	public async Task<RegistrationVM> GetById(int id)
	{
		Logger.LogInformation($"Inside {nameof(RegistrationEditService)}!{nameof(GetById)}, id={id}");
		RegistrationVM VM = new();
		try
		{
			VM = await db.GetById(id);
			string email = await SvcClaims.GetEmail();
			VM.Status = Status.FromValue(VM.StatusId);

			var tuple = Helper.GetAttendanceDatesArray(VM.AttendanceBitwise);
			VM.AttendanceDateList = tuple.week1;
			VM.AttendanceDateList2ndMonth = tuple.week2;

			if (await SvcClaims.IsUserAuthoirized(email) == false)
			{
				ExceptionMessage = $"...logged in user:{email} lacks authority for to see content of id={id}";  // /EMail:{RegistrationEditPOCO.EMail}
				Logger.LogWarning(ExceptionMessage);
				throw new UserNotAuthoirizedException(ExceptionMessage);
			}
			else
			{
				return VM;
			}
		}
		catch (Exception ex)
		{
			ExceptionMessage = $"Inside {nameof(GetById)}";
			Logger.LogError(ex, ExceptionMessage, id);
			ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(ExceptionMessage);
		}
	}


	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> Create(RegistrationVM registrationVM)
	{
		Logger.LogInformation($"Inside {nameof(IRegistrationEditService)}!{nameof(Create)}; calling {nameof(db.Create)}");
		try
		{
			registrationVM.Status = Status.Payment;
			registrationVM.AttendanceBitwise = GetDaysBitwise(registrationVM.AttendanceDateList!, registrationVM.AttendanceDateList2ndMonth!, Enums.DateRangeType.Attendance);

			var sprocTuple = await db.Create(DTO_From_VM_To_DB(registrationVM));

			int newId = sprocTuple.Item1;
			int sprocReturnValue = sprocTuple.Item2;
			string returnMsg = sprocTuple.Item3;

			return (newId, sprocReturnValue, returnMsg);
		}
		catch (Exception ex)
		{
			ExceptionMessage = $"...Error calling {nameof(db.Create)} (presumably)";
			Logger.LogError(ex, ExceptionMessage);

			// Note, the UI should NOT display this detailed Exception Message, unless maybe if Env.IsDevelopment
			ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(ExceptionMessage);
		}
	}

	public async Task<(int RowsAffected, int SprocReturnValue, string ReturnMsg)> Update(RegistrationVM registrationVM)
	{
		Logger.LogInformation($"Inside {nameof(IRegistrationEditService)}!{nameof(Update)}; calling {nameof(db.Update)}");
		try
		{
			var sprocTuple = await db.Update(DTO_From_VM_To_DB(registrationVM));
			Logger.LogInformation($"Registration updated for {registrationVM.FamilyName}/{registrationVM.EMail}");

			int rowsAffected = sprocTuple.Item1; ;
			int sprocReturnValue = sprocTuple.Item2;
			string returnMsg = sprocTuple.Item3;
			return (rowsAffected, sprocReturnValue, returnMsg);
		}
		catch (Exception ex)
		{
			ExceptionMessage = $"...Error calling {nameof(db.Update)} (presumably)";
			Logger.LogError(ex, ExceptionMessage);
			ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
			throw new InvalidOperationException(ExceptionMessage);
		}
	}

	private RegistrationEditPOCO DTO_From_VM_To_DB(RegistrationVM registration)
	{
		RegistrationEditPOCO poco = new RegistrationEditPOCO
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
			StatusId = registration.Status!.Value,
			AttendanceBitwise = GetDaysBitwise(registration.AttendanceDateList!, registration.AttendanceDateList2ndMonth!, Enums.DateRangeType.Attendance),
			LmmDonation = registration.LmmDonation,
			Avatar = registration.Avatar,
			Notes = GetNotesScrubbed(registration.Notes!)
		};

		Logger.LogDebug(string.Format("...Inside RegistrationEditPOCO [2], poco.AttendanceBitwise: {0}", poco.AttendanceBitwise));

		return poco;
	}

	private int GetDaysBitwise(DateTime[] selectedDateArray, DateTime[] selectedDateArray2ndMonth, Enums.DateRangeType dateRangeType)
	{
		if (selectedDateArray is null || selectedDateArray.Length == 0) { return 0; }

		int bitwise = 0;
		AttendanceDate? attendanceDate;

		foreach (var item in selectedDateArray)
		{
			attendanceDate = AttendanceDate.List.Where(w => w.Date == item).SingleOrDefault();
			if (attendanceDate is not null)
			{
				bitwise += attendanceDate.Value;
			}
			else
			{
				ExceptionMessage = $"...Acceptance Date:{item.ToShortDateString()} is out of range; range is {DateRangeType.Attendance.Range.Min.ToShortDateString()} to {DateRangeType.Attendance.Range.Max.ToShortDateString()}";
				Logger.LogWarning(ExceptionMessage);
				//throw new RegistratationException(ExceptionMessage);
			}
		}

		if (dateRangeType.HasSecondMonth)
		{
			foreach (var item in selectedDateArray2ndMonth)
			{
				attendanceDate = AttendanceDate.List.Where(w => w.Date == item).SingleOrDefault();
				if (attendanceDate is not null)
				{
					bitwise += attendanceDate.Value;
				}
				else
				{
					ExceptionMessage = $"...Acceptance Date:{item.ToShortDateString()} is out of range; range is {DateRangeType.Attendance.Range.Min.ToShortDateString()} to {DateRangeType.Attendance.Range.Max.ToShortDateString()}";
					Logger.LogWarning(ExceptionMessage);
					//throw new RegistratationException(ExceptionMessage);
				}
			}
		}

		return bitwise;
	}

	public string GetNotesScrubbed(string notes)
	{
		if (!string.IsNullOrEmpty(notes))
		{
			return notes.Replace("\"", string.Empty).Replace("'", string.Empty);
		}
		else
		{
			return notes;
		}
	}

}