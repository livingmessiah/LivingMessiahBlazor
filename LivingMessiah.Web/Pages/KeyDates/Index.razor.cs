using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Services;
using LivingMessiah.Web.Pages.KeyDates.Enums;

using Syncfusion.Blazor.DropDowns;
using System.Linq;

using LinkLogin = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.KeyDates;

public partial class Index
{
		[Inject]
		ISmartEnumServiceForSfDropDownList svcDDL { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		#region DropDownList

		// ToDo: not referenced, wanted to convert the UI (Index.razor) to a modal
		protected List<DropDownListVM> DataSource => svcDDL.GetKeyDateYearVM().ToList();

		public string SelectedValue = BaseKeyDateYearSmartEnum.Current.Name;
		public int SelectedId = BaseKeyDateYearSmartEnum.Current.Value;
		public int SelectedYear = BaseKeyDateYearSmartEnum.Current.Year;

		// ToDo: not referenced, wanted to convert the UI (Index.razor) to a modal
		public void OnChange(ChangeEventArgs<string, DropDownListVM> args)
		{
				int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
				SelectedId = i;
				SelectedYear = BaseKeyDateYearSmartEnum.FromValue(SelectedId).Year;
		}
		#endregion

		//ToDo: have it return to this Edit tab
		void RedirectToLoginClick(string returnUrl)
		{
				NavigationManager.NavigateTo($"{LinkLogin.Login}?returnUrl={returnUrl}", true);
		}

}
