using Microsoft.AspNetCore.Components;
using MasterDetailState = LivingMessiah.Web.Features.Admin.Video.MasterDetail;
using ParentState = LivingMessiah.Web.Features.Admin.Video.Index;

namespace LivingMessiah.Web.Features.Admin.Video.WeeklyVideos;

public partial class Index
{
	[Inject] private IState<MasterDetailState.MasterDetailState>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	void Edit_ButtonClick(int id)
	{
		Dispatcher!.Dispatch(new AddEdit.DB_Populate_ShabbatWeekList());
		Dispatcher!.Dispatch(new AddEdit.DB_Get_Action(id, Enums.FormMode.Edit));
		Dispatcher!.Dispatch(new ParentState.Set_VisibleComponent_Action(Enums.VisibleComponent.AddEditForm));
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Detail_Action(Enums.Crud.Edit, id));  //args.Crud
		Dispatcher!.Dispatch(new ParentState.Set_DetailPageHeader_Action("Publish Date", "I do not know!!"));
	}

}
