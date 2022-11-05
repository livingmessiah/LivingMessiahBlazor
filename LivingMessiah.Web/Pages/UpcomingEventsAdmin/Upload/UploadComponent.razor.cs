using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace LivingMessiah.Web.Pages.UpcomingEventsAdmin.Upload;

public partial class UploadComponent
{

	[Parameter] public int Id { get; set; }

	//ToDo: clean this up and make more generic
	//private const string UlClass = "list-inline";
	//private const string LiClass = "list-inline-item";
	private const string UlClass = "";
	private const string LiClass = "";

	private const string SaveUrl = "https://aspnetmvc.syncfusion.com/services/api/uploadbox/Save";
	private const string RemoveUrl = "https://aspnetmvc.syncfusion.com/services/api/uploadbox/Remove";

	public void OnFileRemove(RemovingEventArgs args)
	{
		args.PostRawFile = false;
	}
}

/*
https://www.syncfusion.com/forums/160405/sfuploader-working-with-azure-storage-blobs
https://www.eugenechiang.com/2021/03/13/syncfusion-blazor-sfuploader-with-azure-storage/
https://search.brave.com/search?q=SfUploader&source=desktop
 */