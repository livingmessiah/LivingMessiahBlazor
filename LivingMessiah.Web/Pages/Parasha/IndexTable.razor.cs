using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Domain;
using System;
using Microsoft.Extensions.Logging;

using ParashaDomain = LivingMessiah.Domain.Parasha.Queries;
using Microsoft.Extensions.Caching.Memory;

using CacheSettings = LivingMessiah.Web.Settings.Constants.ParashaIndexTableTupleCache;
using Component = LivingMessiah.Web.Components.Names;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class IndexTable
	{
		[Inject] public ILogger<IndexTable> Logger { get; set; }
		[Inject] private LivingMessiah.Data.IShabbatWeekRepository db { get; set; }
		[Inject] public IMemoryCache Cache { get; set; }

		protected IReadOnlyList<ParashaDomain.ParashaList> ParashaList;
		protected ParashaDomain.BibleBook BibleBook;
		protected Tuple<ParashaDomain.BibleBook, List<ParashaDomain.ParashaList>> ParashaListTuple;

		[Parameter]
		public bool IsXsOrSm { get; set; }

		[Parameter]
		public int BookId { get; set; } = 0;

		protected string Colspan;
		protected int prevGregorianYear = 0;

		protected string CachedMsg { get; set; }
		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside Component: {0}, Class!Method: {1}; BookId{2}"
				, Component.ParashaIndexTable, nameof(IndexTable) + "!" + nameof(OnInitializedAsync), BookId));
			
			Colspan = (!IsXsOrSm) ? "8" : "6";			

			CachedMsg = "";
			ParashaListTuple = Cache.Get<Tuple<ParashaDomain.BibleBook, List<ParashaDomain.ParashaList>>>(CacheSettings.Key);

			if (ParashaListTuple is null)
			{
				Logger.LogDebug(string.Format("...ParashaListTuple is null"));
				try
				{
					ParashaListTuple = await db.GetParashotByBookId(BookId);
					if (ParashaListTuple is not null)
					{
						//CachedMsg = "Data gotten from DATABASE";
						Logger.LogDebug(string.Format("... Data gotten from DATABASE"));
						BibleBook = ParashaListTuple.Item1;  
						ParashaList = ParashaListTuple.Item2;
						Cache.Set(CacheSettings.Key, ParashaListTuple, TimeSpan.FromMinutes(CacheSettings.FromMinutes));
					}
					else
					{
						DatabaseWarning = true;
						DatabaseWarningMsg = $"{nameof(ParashaListTuple)} NOT FOUND";
					}
				}
				catch (Exception ex)
				{
					DatabaseError = true;
					DatabaseErrorMsg = $"Error reading database";
					Logger.LogError(ex, string.Format("...Exception, DatabaseErrorMsg: {0}", DatabaseErrorMsg));
				}
			}
			else
			{
				//CachedMsg = "...Attempting to extract objects from chached tuple";
				Logger.LogDebug(string.Format("...Attempting to extract objects from chached tuple"));
				try
				{
					BibleBook = ParashaListTuple.Item1;
					ParashaList = ParashaListTuple.Item2;
					//CachedMsg = "Data gotten from CACHE";
					Logger.LogDebug(string.Format("... Data gotten from CACHE"));
				}
				catch (Exception ex)
				{
					DatabaseError = true;
					DatabaseErrorMsg = $"Error reading database";
					Logger.LogError(ex, string.Format("...Exception, DatabaseErrorMsg: {0}", DatabaseErrorMsg));
				}

			}

			//Logger.LogDebug(string.Format("...BOT BibleBook {0} null, ParashaList {1} null"
			//	, BibleBook == null ? "is" : "is NOT"
			//	, ParashaList == null ? "is" : "is NOT"));

		}

		public static string CurrentReadDateTextFormat(DateTime readDate)
		{
			DateTime compareDate = DateTime.Today;
			if (readDate >= compareDate & readDate <= compareDate.AddDays(6))
			{
				return "text-danger";
				//<span class='badge-danger'>@Title</span>
			}
			else
			{
				return "";
			}
		}

		public static string MyHebrewBibleParashaUrl(int id, string url)
		{
			string url2 = !String.IsNullOrEmpty(url) ? url : "";
			return "https://myhebrewbible.com/Parasha/Triennial/LivingMessiah/" + id.ToString() + "?slug=" + url2;
		}

		#region ErrorHandling
		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		#endregion

	}
}
