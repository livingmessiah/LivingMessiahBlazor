using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.MasterDetail;

public partial class MasterList
{
	[Inject] public ILogger<MasterList>? Logger { get; set; }
	[Inject] private IState<MasterDetailState>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(MasterList) + "!" + nameof(OnInitialized)));
		if (State!.Value.RegistrationList is null)
		{
			Logger!.LogDebug(string.Format("...Call {0} because RegistrationList is null", nameof(GetAll_Action)));
			Dispatcher!.Dispatch(new GetAll_Action());
		}
		base.OnInitialized();
	}

}
