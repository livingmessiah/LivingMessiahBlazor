using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Index;

//ToDo delete this code behind and use @inject
public partial class Index
{
		[Inject] private IState<IndexState>? State { get; set; }
}
