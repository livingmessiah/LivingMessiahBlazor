using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using LivingMessiah.Web.Shared;
using LivingMessiah.Web.Features.Admin.Video.Enums;
using ParentState = LivingMessiah.Web.Features.Admin.Video.Index;

using System;
//using LivingMessiah.Web.Features.Admin.Video.Models;

namespace LivingMessiah.Web.Features.Admin.Video.MasterDetail;

public partial class ActionButtonGroup
{
	[Inject] public ILogger<ActionButtonGroup>? Logger { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	[Parameter, EditorRequired] public Models.YouTubeFeed? YouTubeFeed { get; set; }

	[CascadingParameter] IModalService Modal { get; set; } = default!;

	private async Task ReturnedCrud(CrudAndIdArgs args)
	{
		string inside = $"inside Admin.Video.MasterDetail!{nameof(ActionButtonGroup) + "!" + nameof(ReturnedCrud)}; args.Crud.Name: {args.Crud.Name}";
		Logger!.LogDebug(string.Format("{0}", inside));

		switch (args.Crud.Name)
		{
			case nameof(Enums.Crud.Add):
				Dispatcher!.Dispatch(new AddEdit.DB_Populate_ShabbatWeekList());
				Dispatcher!.Dispatch(new AddEdit.Load_YouTubeFeed_Action(args.YouTubeFeed!)); 
				Dispatcher!.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.AddEditForm));
				Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Detail_Action(args.Crud, args.YouTubeFeed!.Id_Zero_If_Null));  
				Dispatcher!.Dispatch(new ParentState.Set_DetailPageHeader_Action("Publish Date", args.YouTubeFeed!.PublishDate.Date.ToShortDateString()));
				break;

			case nameof(Enums.Crud.Edit):
				Dispatcher!.Dispatch(new AddEdit.DB_Populate_ShabbatWeekList());
				Dispatcher!.Dispatch(new AddEdit.DB_Get_Action(args.YouTubeFeed!.Id_Zero_If_Null, Enums.FormMode.Edit)); 
				
				Dispatcher!.Dispatch(new ParentState.Set_VisibleComponent_Action(VisibleComponent.AddEditForm));
				Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Detail_Action(args.Crud, args.YouTubeFeed!.Id_Zero_If_Null));
				Dispatcher!.Dispatch(new ParentState.Set_DetailPageHeader_Action("Publish Date", args.YouTubeFeed!.PublishDate.Date.ToShortDateString()));
				break;


			case nameof(Enums.Crud.Delete):

				if (await IsModalConfirmed("Video", "Title", args.YouTubeFeed!.Title ?? "???") == true)
				{
					Dispatcher!.Dispatch(new AddEdit.DB_Delete_Action(args.YouTubeFeed.Id_Zero_If_Null));  ////args.Id ?? 0

				//ToDo create Dispatch that updates the in memory list
				//Dispatcher!.Dispatch(new GetAll_Action());
				}
				break;

			default:
				// ToDo: maybe just log this
				Dispatcher!.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"{args.Crud.Name} unknown!!!; {inside}"));
				break;
		}

	}

	private async Task<bool> IsModalConfirmed(string title, string label, string value)
	{
		var parameters = new ModalParameters { { nameof(ConfirmDeleteModal.Message), $"{title}: {value}" } }; //for {label}
		var modal = Modal.Show<ConfirmDeleteModal>("Confirmation Required", parameters);
		var result = await modal.Result;
		return result.Confirmed;
	}

}
