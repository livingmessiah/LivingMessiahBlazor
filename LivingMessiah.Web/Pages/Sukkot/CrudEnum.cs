using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public enum CrudEnum
	{
		Add = 1,
		Edit = 2,
		Delete = 3
		//, Read = 4
		//, AddAndAddAnother=5
	}

	public class CrudLocal
	{
		public static List<CrudLocal> All { get; } = new List<CrudLocal>();

		public static CrudLocal Add { get; } = new CrudLocal(CrudEnum.Add, "Add", "Add_ButtonClick", "Create Registration", false, "btn btn-success", "fas fa-save");
		public static CrudLocal Edit { get; } = new CrudLocal(CrudEnum.Edit, "Edit", "Edit_ButtonClick", "Update", true, "btn btn-primary", "fas fa-pencil-alt");
		public static CrudLocal Delete { get; } = new CrudLocal(CrudEnum.Delete, "Delete", "Delete_ButtonClick", "Delete Registration", true, "btn btn-danger", "fas fa-times");

		public CrudEnum CrudEnum { get; private set; }
		//public int Id { get; private set; }
		public string Title { get; private set; }
		public string OnClickEvent { get; private set; }
		public string DatabaseCommand { get; private set; }
		public bool IsEditMode { get; private set; }
		public string ButtonColor { get; private set; }
		public string Icon { get; set; }

		private CrudLocal(CrudEnum crudEnum, string title, string onClickEvent, string databaseCommand, bool isEditMode, string buttonColor, string icon)
		{
			CrudEnum = crudEnum;
			//Id = id;  , int id
			Title = title;
			OnClickEvent = onClickEvent;
			DatabaseCommand = databaseCommand;
			ButtonColor = buttonColor;
			Icon = icon;
			IsEditMode = isEditMode;
			All.Add(this);
		}

		public static CrudLocal FromEnum(CrudEnum enumValue)
		{
			return All.SingleOrDefault(r => r.CrudEnum == enumValue);
		}

		/*
		public static Crud FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}


		public static Season FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}
		*/


	} // class Season
} // namespace
