

namespace LivingMessiah.Web.Pages.Admin.Video.Index;

// 1. Action
public record Set_VisibleComponent_Action(Enums.VisibleComponent VisibleComponent);
public record Set_PageHeader_For_Index_Action(PageHeaderVM PageHeaderVM);  
public record Set_PageHeader_For_Detail_Action(Enums.Crud Crud, int Id);  //string Title, string Icon, string Color
public record Set_DetailPageHeader_Action(string Label, string Value);
public record Set_DetailPageHeader_Empty_Action();

// 2. State
public record IndexState
{
	public Enums.VisibleComponent? VisibleComponent { get; init; }
	public PageHeaderVM? PageHeaderVM { get; init; }
	public DetailPageHeaderVM? DetailPageHeaderVM { get; init; }
}

// 3. Feature
public class FeatureImplementation : Feature<IndexState>
{
	public override string GetName() => Constants.FluxorStores.Index;

	protected override IndexState GetInitialState()
	{
		return new IndexState
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
			PageHeaderVM = Constants.GetPageHeaderForIndexVM(),
		};
	}
}


// 4. Reducers
public static class Reducers
{
	[ReducerMethod]
	public static IndexState On_Set_VisibleComponent(
		IndexState state, Set_VisibleComponent_Action action)
	{
		return state with { VisibleComponent = action.VisibleComponent };
	}


	[ReducerMethod]
	public static IndexState On_Set_PageHeader_For_Index(
	IndexState state, Set_PageHeader_For_Index_Action action)
	{
		return state with
		{
			VisibleComponent = Enums.VisibleComponent.MasterList,
			PageHeaderVM = Constants.GetPageHeaderForIndexVM()
		};
	}


	[ReducerMethod]
	public static IndexState On_Set_PageHeader_For_Detail(
		IndexState state, Set_PageHeader_For_Detail_Action action)
	{
		return state with
		{
			// For Title, if Add, use Crud.Text, if  Edit, use Crud.Name
			PageHeaderVM = new PageHeaderVM { Title = action.Crud.Name, Icon = action.Crud.Icon, Color = action.Crud.Color, Id = action.Id }
		};
	}

	[ReducerMethod]
	public static IndexState On_Set_DetailPageHeader(
		IndexState state, Set_DetailPageHeader_Action action)
	{
		return state with
		{
			DetailPageHeaderVM = new DetailPageHeaderVM { Label = action.Label, Value = action.Value }
		};
	}

	[ReducerMethod]
	public static IndexState On_Set_DetailPageHeader_Empty(IndexState state, Set_DetailPageHeader_Empty_Action action)
	{
		return state with
		{
			DetailPageHeaderVM = null
		};
	}

	//

}

// 5. Effects
