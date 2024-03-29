﻿using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using LivingMessiah.Web.Shared;
using LivingMessiah.Web.Features.Sukkot.ManageRegistration.Enums;
using HRA_State = LivingMessiah.Web.Features.Sukkot.ManageRegistration.HRA;
using ParentState = LivingMessiah.Web.Features.Sukkot.ManageRegistration.Index;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.MasterDetail;

public partial class ActionButtonGroup
{
	[Inject] public ILogger<ActionButtonGroup>? Logger { get; set; }
	//[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	[Parameter] public string? EMail { get; set; } 
	[Parameter] public int Id { get; set; }
	[Parameter] public int IdHra { get; set; }
	[Parameter] public string? FullName { get; set; }
	[Parameter] public int DonationRowCount { get; set; }

	[Parameter, EditorRequired] public int StatusId { get; set; }
	[CascadingParameter] IModalService Modal { get; set; } = default!;

	private async Task ReturnedCrud(CrudAndIdArgs args)
	{
		string inside = $"inside {nameof(ActionButtonGroup) + "!" + nameof(ReturnedCrud)}; args.Crud.Name: {args.Crud.Name}";
		Logger!.LogDebug(string.Format("{0}", inside));

		switch (args.Crud.Name)
		{
			case nameof(Enums.Crud.AddRegistration):
				Dispatcher!.Dispatch(new Registrant.Add_Registration_Action(args.EMail));
				Dispatcher!.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.AddEditForm));
				Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Detail_Action(args.Crud.Text, args.Crud!.Icon, args.Crud!.Color, args.Id));
				Dispatcher!.Dispatch(new ParentState.Set_DetailPageHeader_Action("Email", args.EMail));
				break;

			case nameof(Enums.Crud.Edit):
				Dispatcher!.Dispatch(new Registrant.Get_Action(args.Id, Enums.FormMode.Edit));
				Dispatcher!.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.AddEditForm));
				Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				Dispatcher!.Dispatch(new ParentState.Set_DetailPageHeader_Action("Name", args.FullName));
				break;

			case nameof(Enums.Crud.Display):
				Dispatcher!.Dispatch(new Detail.Get_Action(args.Id));
				Dispatcher!.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.DisplayCard));
				Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Detail_Action(args.Crud.Name, args.Crud!.Icon, args.Crud!.Color, args.Id));
				Dispatcher!.Dispatch(new ParentState.Set_DetailPageHeader_Action("Name", args.FullName));
				break;

			case nameof(Enums.Crud.DeleteRegistration):

				if (await IsModalConfirmed("Registration", "Name", args.FullName) == true)
				{
					Dispatcher!.Dispatch(new HRA_State.Delete_Registration_Action(args.Id));
					Dispatcher!.Dispatch(new GetAll_Action());
				}
				break;

			case nameof(Enums.Crud.DeleteHRA):
				if (await IsModalConfirmed("HRA", "e-mail", args.EMail) == true)
				{
					Dispatcher!.Dispatch(new HRA_State.Delete_HRA_Action(args.Id));
					Dispatcher!.Dispatch(new GetAll_Action());
				}
				break;

			case nameof(Enums.Crud.Donation):
				Dispatcher!.Dispatch(new Donations.Form_Prep_Action(args.Id, args.FullName));
				Dispatcher!.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.DonationForm));
				Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Detail_Action(args.Crud.Text, args.Crud!.Icon, args.Crud!.Color, args.Id));
				Dispatcher!.Dispatch(new ParentState.Set_DetailPageHeader_Action("Name", args.FullName));
				break;

			default:
				// ToDo: maybe just log this
				Dispatcher!.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"{args.Crud.Name} unknown!!!; {inside}"));
				break;
		}

	}

	private async Task<bool> IsModalConfirmed(string title, string label, string value)
	{
		var parameters = new ModalParameters { { nameof(ConfirmDeleteModal.Message), $"{title} for {label}: {value}" } };
		var modal = Modal.Show<ConfirmDeleteModal>("Confirmation Required", parameters);
		var result = await modal.Result;
		return result.Confirmed;
	}

}
