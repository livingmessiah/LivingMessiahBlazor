using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser;

public partial class SimpleMasterList
{
	[Inject] public ILogger<SimpleMasterList>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(SimpleMasterList) + "!" + nameof(OnInitialized)));
		if (State!.Value.vwSuperUserList is null)
		{
			Dispatcher!.Dispatch(new Get_List_Action());
		}
		base.OnInitialized();
	}

	private string GetCardHeader(string fullName, string email)
	{
		if (string.IsNullOrEmpty(fullName))
		{
			return $"<h4><b>Email</b>: {email}</h4>";
		}
		else
		{
			return $"<h4><b>Name</b>: {fullName}</h4><h5><b>Email</b>: {email}</h5>";
		}
	}
}
