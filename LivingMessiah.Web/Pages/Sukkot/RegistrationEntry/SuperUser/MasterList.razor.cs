using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Shared;
using Fluxor;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Reflection;
using Blazored.Modal.Services;
using Blazored.Modal;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.Detail;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

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
		if (State!.Value.vwSuperUserList is null)
		{
			Dispatcher!.Dispatch(new Get_List_Action());
		}
		base.OnInitialized();
	}

	private async Task ReturnedCrud(CrudAndIdArgs args)
	{
		string inside = $"inside {nameof(MasterList) + "!" + nameof(ReturnedCrud)}; args.Crud.Name: {args.Crud.Name}";
		Logger!.LogDebug(string.Format("{0}", inside));

		switch (args.Crud.Name)
		{
			case nameof(Enums.Crud.AddRegistration):
				Dispatcher!.Dispatch(new Add_Registration_Action(args.EMail)); //, RegistrationSteps.Enums.Status.StartRegistration.Value
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				break;

			case nameof(Enums.Crud.Edit):
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				Dispatcher!.Dispatch(new Get_EditItem_Action(args.Id, Enums.FormMode.Edit));
				break;

			case nameof(Enums.Crud.Display):
				Dispatcher!.Dispatch(new Get_DisplayItem_Action(args.Id));
				Dispatcher!.Dispatch(new Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				break;

			case nameof(Enums.Crud.DeleteRegistration):
				if (await IsModalConfirmed(args.Id, "Registration") == true)
				{
					Dispatcher!.Dispatch(new Delete_Action(args.Id));
					Dispatcher!.Dispatch(new Get_List_Action());
				}
				break;

			case nameof(Enums.Crud.DeleteHRA):
				if (await IsModalConfirmed(args.Id, "House Rules Agreement (HRA)") == true)
				{
					Dispatcher!.Dispatch(new Delete_HRA_Action(args.Id));
					Dispatcher!.Dispatch(new Get_List_Action());
				}
				break;

			case nameof(Enums.Crud.Repopulate):
				Dispatcher!.Dispatch(new Get_List_Action());
				break;

			default:
				// ToDo: maybe just log this
				Dispatcher!.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"{args.Crud.Name} unknown!!!; {inside}"));
				break;
		}
	}


	private async Task<bool> IsModalConfirmed(int id, string title)
	{
		var parameters = new ModalParameters { { nameof(ConfirmDeleteModal.Message), $"{title} Id: {id}" } };
		var modal = Modal.Show<ConfirmDeleteModal>("Confirmation Required", parameters);
		var result = await modal.Result;
		return result.Confirmed;
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