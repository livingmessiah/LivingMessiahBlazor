﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivingMessiah.Web.Features.Parasha.Data;
using LivingMessiah.Web.Features.Parasha.ListByBook;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Features.Parasha.Services;

public interface IParashaService
{
	string UserInterfaceMessage { get; set; }
	Task<CurrentParasha?> GetCurrentParasha();
	Task<List<Parashot>> GetParashotByBookId(int bookId);
}

public class ParashaService : IParashaService
{
	public string UserInterfaceMessage { get; set; } = "";
	private string LogExceptionMessage { get; set; } = "";

	#region Constructor and DI
	private readonly IRepository db;
	private readonly ILogger Logger;

	public ParashaService(
		IRepository Repository, ILogger<ParashaService> logger)
	{
		db = Repository;
		Logger = logger;
	}
	#endregion

	public async Task<CurrentParasha?> GetCurrentParasha()
	{
		UserInterfaceMessage = "";
		Logger.LogDebug(string.Format("Inside {0}"
			, nameof(ParashaService) + "!" + nameof(GetCurrentParasha)));

		CurrentParasha? vm = null;

		try
		{
			vm = await db.GetCurrentParasha();

			if (vm is null)
			{
				UserInterfaceMessage = "Current Parasha Record NOT Found";
				Logger.LogWarning(string.Format("Inside {0} id:{1}"
					, nameof(ParashaService) + "!" + nameof(db.GetCurrentParasha), UserInterfaceMessage));
			}
		}

		catch 
		{
			UserInterfaceMessage += "An invalid operation occurred, contact your administrator";
			Logger.LogError(string.Format("  Inside catch of {0}; after calling {1}"
				, nameof(ParashaService) + "!" + nameof(GetCurrentParasha)
				, nameof(db) + "!" + (nameof(db.GetCurrentParasha) )));
		}
		return vm;
	}

	public async Task<List<Parashot>> GetParashotByBookId(int bookId)
	{
		UserInterfaceMessage = "";
		Logger.LogDebug(string.Format("Inside {0}, bookId: {1}"
			, nameof(ParashaService) + "!" + nameof(db.GetParashotByBookId), bookId));
		
		var vm = new List<Parashot>(); //IReadOnlyList<Parashot> Parashot = await db.GetParashotByBookId(bookId);
		try
		{
			vm = await db.GetParashotByBookId(bookId);
			if (vm is null || !vm.Any())
			{
				UserInterfaceMessage = "Parashot list NOT Found";
				Logger.LogWarning(string.Format("Inside {0} id:{1}"
					, nameof(ParashaService) + "!" + nameof(db.GetParashotByBookId), UserInterfaceMessage));
				//throw new ParashotListNotFoundException(UserInterfaceMessage);
			}
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(GetParashotByBookId)}, db.{nameof(db.GetParashotByBookId)}";
			Logger.LogError(ex, LogExceptionMessage);
			UserInterfaceMessage += "An invalid operation occurred, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}
		return vm!;
	}


	#region CustomExceptions Classes

	/*
	 
	ToDo: If I throw this exception than `GetParashotByBookId`, then `catch (Exception ex)` will also be called and I don't want that.
	 
	public class CurrentParashaNotFoundException : Exception
	{
		public CurrentParashaNotFoundException()
		{
		}
		public CurrentParashaNotFoundException(string message)
				: base(message)
		{
		}
		public CurrentParashaNotFoundException(string message, Exception inner)
				: base(message, inner)
		{
		}
	}

	
	public class ParashotListNotFoundException : Exception
	{
		public ParashotListNotFoundException()
		{
		}
		public ParashotListNotFoundException(string message)
				: base(message)
		{
		}
		public ParashotListNotFoundException(string message, Exception inner)
				: base(message, inner)
		{
		}
	}
	*/
}

#endregion

// Ignore Spelling: Parashot