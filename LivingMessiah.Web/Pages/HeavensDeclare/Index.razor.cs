using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.HeavensDeclare;

public partial class Index
{
	protected List<VideoRecord>? VideoRecords;

	protected override void OnInitialized()
	{
		VideoRecords = new List<VideoRecord>();
	}

}
