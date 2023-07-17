using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.Detail;

public partial class Display
{
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	DisplayVM? DisplayVM => State!.Value.DisplayVM;

	void CancelActionHandler()
	{
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}
}
