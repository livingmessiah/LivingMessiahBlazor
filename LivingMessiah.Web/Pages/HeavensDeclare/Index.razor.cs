using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.HeavensDeclare
{
	public partial class Index
	{
		protected List<Video> Videos;

		protected override void OnInitialized()
		{
			Videos = new List<Video>();
			Videos = Data.GetAll();
		}

	}
}
