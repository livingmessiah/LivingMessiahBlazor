namespace LivingMessiah.Web.Pages.Sukkot.CreateEdit;

public class UI
{
		public string Title { get; set; }
		public string Handler { get; set; }
		public string DatabaseCommand { get; set; }
		public bool EditMode { get; set; }

		public SukkotEnums.CRUD CRUD { get; set; }

		public UI(SukkotEnums.CRUD crudType)
		{
				CRUD = crudType;

				switch (crudType)
				{
						case SukkotEnums.CRUD.Add:
								EditMode = false;
								Title = "Add";
								Handler = "Create";
								//Handler = "CreateSingle"; Handler2 = "CreateAddAnother";
								DatabaseCommand = "Create Registration";
								break;

						case SukkotEnums.CRUD.Edit:
								EditMode = true;
								Title = "Edit";
								Handler = "Edit";
								DatabaseCommand = "Update";
								break;

						default:
								EditMode = true;
								Title = "Edit";
								Handler = "CreateEdit";
								DatabaseCommand = "Update";

								break;
				}
		}

		public override string ToString()
		{
				string x = $"U ==> Title: {Title}, Handler: {Handler}, EditMode: {EditMode}";
				return x;
		}

}
