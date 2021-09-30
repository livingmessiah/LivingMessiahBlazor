﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SukkotApi.Data;
using Microsoft.Extensions.Logging;
using SukkotApi.Domain;
using SukkotApi.Domain.Registrations.Enums;

namespace LivingMessiah.Web.Services
{
	public interface ISukkotAdminService
	{
		string ExceptionMessage { get; set; }

		Task<List<vwRegistration>> GetAll(RegistrationSortEnum sort);
		Task<List<Notes>> GetNotes(RegistrationSortEnum sort);

		Task<int> LogErrorTest();
		Task<List<zvwErrorLog>> GetzvwErrorLog();
		Task<int> EmptyErrorLog();

		Task<int> MealTicketPunchInsert(MealTicketPunchLog mealTicketPunchLog);

		Task<List<vwLodgingDaysAll>> GetLodgingDaysAll();
		Task<List<vwLodgingDetail>> GetvwLodgingDetail();
	}

	public class SukkotAdminService : ISukkotAdminService
	{
		#region Constructor and DI
		private readonly ISukkotAdminRepository db;
		private readonly ILogger log;
		private readonly ISecurityClaimsService svcClaims;

		public SukkotAdminService(
			ISukkotAdminRepository dbRepository, ILogger<SukkotAdminService> logger, ISecurityClaimsService serviceClaims)
		{
			db = dbRepository;
			log = logger;
			svcClaims = serviceClaims;
		}
		#endregion

		public string ExceptionMessage { get; set; } = "";

		public async Task<List<vwRegistration>> GetAll(RegistrationSortEnum sort)
		{
			var vm = new List<vwRegistration>();
			//var profiler = MiniProfiler.Current;
			try
			{
				//using (profiler.Step($"profiling {nameof(db.GetAll)}"))
				//{
				vm = await db.GetAll(sort);
				//}
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetAll)}, {nameof(db.GetAll)}";
				log.LogError(ex, ExceptionMessage, sort);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return vm;
		}

		public async Task<List<Notes>> GetNotes(RegistrationSortEnum sort)
		{
			var vm = new List<Notes>();
			try
			{
				vm = await db.GetNotes(sort);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetNotes)}, {nameof(db.GetNotes)}";
				log.LogError(ex, ExceptionMessage, sort);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return vm;
		}

		public async Task<List<vwLodgingDaysAll>> GetLodgingDaysAll()
		{
			var vm = new List<vwLodgingDaysAll>();
			try
			{
				vm = await db.GetLodgingDaysAll();
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetLodgingDaysAll)}, {nameof(db.GetLodgingDaysAll)}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return vm;
		}

		public async Task<List<vwLodgingDetail>> GetvwLodgingDetail()
		{
			var vm = new List<vwLodgingDetail>();
			try
			{
				vm = await db.GetvwLodgingDetail();
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetvwLodgingDetail)}, {nameof(db.GetvwLodgingDetail)}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return vm;
		}

		public async Task<int> LogErrorTest()
		{
			int count = 0;
			try
			{
				count = await db.LogErrorTest();
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(LogErrorTest)}, {nameof(db.LogErrorTest)}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}

		public async Task<List<zvwErrorLog>> GetzvwErrorLog()
		{
			var vm = new List<zvwErrorLog>();
			try
			{
				vm = await db.GetzvwErrorLog();
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetzvwErrorLog)}, {nameof(db.GetzvwErrorLog)}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return vm;
		}

		public async Task<int> EmptyErrorLog()
		{
			int count = 0;
			try
			{
				count = await db.EmptyErrorLog();
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(EmptyErrorLog)}, {nameof(db.EmptyErrorLog)}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}


		public async Task<int> MealTicketPunchInsert(MealTicketPunchLog mealTicketPunchLog)
		{
			int count = 0;
			try
			{
				count = await db.MealTicketPunchInsert(mealTicketPunchLog);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(MealTicketPunchInsert)}, {nameof(db.MealTicketPunchInsert)}";
				log.LogError(ex, ExceptionMessage); // , donation.ToString()
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}

	}
}
