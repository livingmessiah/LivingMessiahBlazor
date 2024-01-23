using System.Collections.Generic;
using System.Linq;
using LivingMessiah.Web.Enums;
using LivingMessiah.Web.Services;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.SampleCode.Syncfusion_SfDropDownList;

public partial class BibleBookChapterDDL
{
		[Inject] ISmartEnumServiceForSfDropDownList? svcDDL { get; set; }

		public string? SelectedValue;
		public int SelectedId;

		public int CurrentLastChapter = 150;

		protected List<DropDownListVM> DataSource => svcDDL!.GetBibleBooksVM().ToList();  // ** ToDo: Update**

		public void OnChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DropDownListVM> args)
		{
				int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
				SelectedId = i;
				CurrentLastChapter = BibleBook.FromValue(SelectedId).LastChapter;
		}
}
