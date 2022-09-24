using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.LinkSmartEnums;

namespace LivingMessiah.Web.Pages.Parasha;

public partial class MenuBar
{
	[Parameter, EditorRequired]
	public ParashaLink ParashaEnum { get; set; } 

	string GetDisabled(ParashaLink currentParashaEnum) 
	{
		return ParashaEnum == currentParashaEnum ? " disabled" : "";
	}

	string GetActive(ParashaLink currentParashaEnum)
	{
		return ParashaEnum == currentParashaEnum ? " active" : "";
	}

}
