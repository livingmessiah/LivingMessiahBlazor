using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Shared;
using Blazored.Modal;
using Blazored.Modal.Services;
using Fluxor;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class MasterList
{
	[Inject] public ILogger<MasterList>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	[Parameter, EditorRequired] public bool IsXsOrSm { get; set; }

	[CascadingParameter] IModalService Modal { get; set; } = default!;

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(MasterList) + "!" + nameof(OnInitialized)));
		if (State!.Value.SpecialEventList is null)
		{
			Dispatcher!.Dispatch(new Get_List_Action());
		}
		base.OnInitialized();
	}

	private async Task ReturnedCrud(CrudAndIdArgs args)
	{
		Logger!.LogDebug(string.Format("inside: {0}; args.Crud.Name: {1}; icon: {2}; id: {3}"
			, nameof(MasterList), args.Crud.Name, args.Crud.Icon, args.Id));

		switch (args.Crud.Name)
		{
			case nameof(Enums.Crud.Add):
				Dispatcher!.Dispatch(new Add_Action());
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				break;

			case nameof(Enums.Crud.Edit):
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				Dispatcher!.Dispatch(new Get_Item_Action(args.Id, Enums.FormMode.Edit));
				break;

			case nameof(Enums.Crud.Display):
				Dispatcher!.Dispatch(new Get_Item_Action(args.Id, Enums.FormMode.Display));
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				break;

			case nameof(Enums.Crud.Delete):
				if (await IsModalConfirmed(args.Id) == true)
				{
					Dispatcher!.Dispatch(new Delete_Action(args.Id));
					Dispatcher!.Dispatch(new Get_List_Action());
				}
				break;

			case "Repopulate":
				//Dispatcher!.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Calling {nameof(Get_List_Action)}"));
				Dispatcher!.Dispatch(new Get_List_Action());
				//Dispatcher!.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Called {nameof(Get_List_Action)}"));
				break;
		}
	}


	private async Task<bool> IsModalConfirmed(int id)
	{
		var parameters = new ModalParameters { { nameof(ConfirmDeleteModal.Message), $"Special Event Id: {id}" } };
		var modal = Modal.Show<ConfirmDeleteModal>("Confirmation Required", parameters);
		var result = await modal.Result;
		return result.Confirmed;

	}


}