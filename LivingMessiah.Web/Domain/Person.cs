using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LivingMessiah.Web.Domain;

public enum OfficeEnum
{
		//All = 0,
		CongregationLeader = 1,
		BeitDin1 = 2,
		BeitDin2 = 3,
		BeitDin3 = 4,
		Elder1 = 5,
		Elder2 = 6,
		Elder3 = 7
		//, MusicLeader = 7
}

public class Person
{
		public int RowCount { get; set; }
		public string OfficeEnumString { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public string ImageFile { get; set; }
		public string IconFile { get; set; }
		public string Email { get; set; }
		public string Title { get; set; }
		public string BioFile { get; set; } // Markdown file
		public string IdXs
		{
				get { return Id + "Xs"; }
		}

		public string CollapseClass
		{
				get
				{
						return RowCount == 1 ? "collapse show " : "collapse ";
				}
		}

		public OfficeEnum OfficeEnum
		{
				get
				{
						Enum.TryParse(OfficeEnumString, out OfficeEnum newEnum);
						return newEnum;
				}
		}
}

