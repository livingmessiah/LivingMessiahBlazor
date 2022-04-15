namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

//ToDo see if I can implement this instead of WeeklyVideoClass
public record WeeklyVideoRecord
{
	public int WeeklyVideoTypeId { get; init; }
	public int ShabbatWeekId { get; init; }
	// [Parameter]
	//public string YouTubeId { get; init; } 
	//public string Title { get; init; } 
}

/*
dbo.WeeklyVideo
- Id = PK
- WeeklyVideoTypeId = WeeklyVideoTypeEnum
- Book and Chapter are NOT NULL
- IX_WeeklyVideo_Unique: ShabbatWeekId ASC, WeeklyVideoTypeId ASC

See 
- LivingMessiah.Web\Pages\Admin\AudioVisual\Components\WeeklyVideos\UpdateGridViewModel.cs
- LivingMessiah.Web\Pages\Admin\AudioVisual\WeeklyVideoTypeEnum.cs

https://search.brave.com/search?q=model+bind+record+types&source=desktop
https://www.learmoreseekmore.com/2020/11/dotnet5-mvc-model-binding-validation-with-record-types.html

namespace Sample.MvcRecords.Models
{
  public record Profile(string FirstName, string LastName, string Email);
}
 

using System.ComponentModel.DataAnnotations;
namespace Sample.MvcRecords.Models
{
  public record Profile([Required]string FirstName, [Required]string LastName, [EmailAddress][Required]string Email);   
}

*/