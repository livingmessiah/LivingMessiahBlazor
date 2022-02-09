using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using LivingMessiah.Web.Domain;
using System;

namespace LivingMessiah.Web.Pages.Leadership;

public partial class Office
{
		[Parameter]
		public Person Person { get; set; }
}
