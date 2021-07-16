using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Leadership
{
	public partial class ScrollSpy
	{
		[Parameter]
		public IList<Domain.Person> People { get; set; }
	}
}
