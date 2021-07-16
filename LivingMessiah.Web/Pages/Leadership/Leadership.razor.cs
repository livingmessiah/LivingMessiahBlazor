using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.Leadership
{
	public partial class Leadership
	{
		[Inject]
		protected ILeadershipService Svc { get; set; }

		protected IList<Domain.Person> People { get; set; }
		//protected List<Domain.Person> People;

		protected override Task OnInitializedAsync()
		{
			People = Svc.LoadPeople();
			return base.OnInitializedAsync();
		}

	}
}
