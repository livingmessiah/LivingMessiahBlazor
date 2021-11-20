﻿using LivingMessiah.Web.Pages.KeyDates.Enums;
using System;

namespace LivingMessiah.Web.Pages.KeyDates.Queries
{
	public class UpcomingEvent
	{
		public DateTime EventDate { get; set; }  //[DateTime]
		public EventTypeEnum EventTypeEnum { get; set; }
		public DateTypeEnum DateTypeEnum { get; set; }    
		public int EnumId { get; set; }     
		public int DaysDiff { get; set; }
		public string DaysDiffDescr { get; set; }
		public string Title { get; set; }
		public string SubTitle { get; set; } 
		public string ImageUrl { get; set; }  
		public string YouTubeId { get; set; }
		public string WebsiteUrl { get; set; }  
		public string WebsiteDescr { get; set; } 
		public string Description { get; set; }  // ToDo: md?, probably going to be Component Body

		public string DaysAhead
		{
			get
			{
				if (DaysDiffDescr != null)
				{
					if (DaysDiff >= 0)
					{
						return DaysDiff + ' ' + DaysDiffDescr;
					}
					else
					{
						return DaysDiff + ' ' + DaysDiffDescr;
					}
				}
				else
				{
					return "?";
				}

			}
		}

		public override string ToString()
		{
			return $"EventDate: {EventDate}, EventTypeEnum: {EventTypeEnum}, DateTypeEnum: {DateTypeEnum}";
			//RowNum: {RowNum}, Year: {YearId}, DateId: {DateId}, 
		}

		public string EventDay()
		{
			return EventDate.Day.ToString();
		}

		public string EventYear()
		{
			return EventDate.Year.ToString();
		}

		public string EventMonth()
		{
			return EventDate.ToString("MMMM");
		}

	}


}