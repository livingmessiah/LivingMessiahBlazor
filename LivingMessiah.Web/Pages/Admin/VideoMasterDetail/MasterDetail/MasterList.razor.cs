using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.Admin.VideoMasterDetail.MasterDetail;

public partial class MasterList
{
	[Inject] public ILogger<MasterList>? Logger { get; set; }
	[Inject] private IState<MasterDetailState>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(MasterList) + "!" + nameof(OnInitialized)));
		if (State!.Value.WeekVideoList is null)
		{
			Logger!.LogDebug(string.Format("...Call {0} because WeekVideoList is null", nameof(Get_Action)));
			Dispatcher!.Dispatch(new Get_Action());
		}
		base.OnInitialized();
	}

}
