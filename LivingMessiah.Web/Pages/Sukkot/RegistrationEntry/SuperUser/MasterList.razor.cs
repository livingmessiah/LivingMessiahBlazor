using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Shared;
//using Blazored.Modal.Services;
using Fluxor;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

public partial class MasterList
{
	[Inject] public ILogger<MasterList>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }

	//[CascadingParameter] IModalService Modal { get; set; } = default!;

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(MasterList) + "!" + nameof(OnInitialized)));
		if (State!.Value.vwSuperUserList is null)
		{
			Dispatcher!.Dispatch(new Get_List_Action());
		}
		base.OnInitialized();
	}

	private async Task ReturnedCrud(CrudAndIdArgs args)
	{
		Logger!.LogDebug(string.Format("inside: {0}; args.Crud.Name: {1}; icon: {2}; id: {3}"
			, nameof(MasterList) + "!" + nameof(ReturnedCrud), args.Crud.Name, args.Crud.Icon, args.Id));

		switch (args.Crud.Name)
		{
			case nameof(Enums.Crud.Add):
				Dispatcher!.Dispatch(new Add_Action(args.EMail)); //, RegistrationSteps.Enums.Status.StartRegistration.Value
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				break;

			case nameof(Enums.Crud.Edit):
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				Dispatcher!.Dispatch(new Get_Item_Action(args.Id, Enums.FormMode.Edit));
				break;

			case nameof(Enums.Crud.Display):
				//Dispatcher!.Dispatch(new Get_Item_Action(args.Id, Enums.FormMode.Display));
				Dispatcher!.Dispatch(new Get_DisplayItem_Action(args.Id));
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				break;

			case nameof(Enums.Crud.Delete):
				await Task.Delay(0); // ToDo: Delete this
				//if (await IsModalConfirmed(args.Id) == true)
				//{
				//	Dispatcher!.Dispatch(new Delete_Action(args.Id));
				//	Dispatcher!.Dispatch(new Get_List_Action());
				//}

				break;

			case "Repopulate":
				Dispatcher!.Dispatch(new Get_List_Action());
				break;
		}
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