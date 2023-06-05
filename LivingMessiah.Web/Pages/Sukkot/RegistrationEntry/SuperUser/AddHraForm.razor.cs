using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;


namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

public partial class AddHraForm
{
	[Inject] public ILogger<AddHraForm>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	public HouseRulesAgreementVM VM { get; set; } = new HouseRulesAgreementVM();

	protected void Add_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(AddHraForm) + "!" + nameof(Add_ButtonClick)));
		Dispatcher!.Dispatch(new Add_HRA_Action(VM.EMail, GetLocalTimeZone()));
		
		//Dispatcher!.Dispatch(new Get_List_Action());
		//Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}
}

