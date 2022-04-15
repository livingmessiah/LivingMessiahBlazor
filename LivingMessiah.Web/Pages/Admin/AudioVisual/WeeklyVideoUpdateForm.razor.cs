using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

[Authorize(Roles = Roles.AdminOrAudiovisual)]
public partial class WeeklyVideoUpdateForm 
{

}
