namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations;

public static class SyncFusionToolbar
{
	public static class Excel
	{
		public const string Arg = "ExcelExport";
		public const string ArgId = "Grid_excelexport";
	}
	public static class Csv
	{
		public const string Arg = "CsvExport";
		public const string ArgId = "Grid_csvexport";
	}
	public static class Pdf
	{
		public const string Arg = "PdfExport";
		public const string ArgId = "Grid_pdfexport";
	}
	public static class Print
	{
		public const string Arg = "Print";
		public const string ArgId = "Grid_print";
	}
}

public static class SyncFusionToolbarCRUD
{
	public static class Add
	{
		public const string Arg = "Add";
		public const string ArgId = "Grid_add";
	}
	public static class Edit
	{
		public const string Arg = "Edit";
		public const string ArgId = "Grid_edit";
	}
	public static class Delete
	{
		public const string Arg = "Delete";
		public const string ArgId = "Grid_delete";
	}
	public static class Update
	{
		public const string Arg = "Update";
		public const string ArgId = "Grid_update";
	}
	public static class Cancel
	{
		public const string Arg = "Cancel";
		public const string ArgId = "Grid_cancel";
	}

	//public const string All = Add.Arg + '"' + ", " + '"' + Edit.Arg + "", "" + Delete.Arg + "", "" + Update.Arg + "", "" + Cancel.Arg + "";
	//public const string All = "'Add', 'Edit', 'Delete', 'Update', 'Cancel'";
}

public static class SqlServer
{
	public const int ReturnValueOk = 0;
	public const int ReturnValueViolationInUniqueIndex = 2601;
	public const string ReturnValueName = "ReturnValue";
	public const string ReturnValueParm = "@ReturnValue";
}
