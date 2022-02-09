using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.Leadership;

public partial class IndexXsAccordion
{
		[Parameter]
		public IList<Domain.Person> People { get; set; }

}
