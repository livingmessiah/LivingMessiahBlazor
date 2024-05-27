namespace LivingMessiah.Web.Features.Admin.Video.GraphicFileIsNull;


public record BlobRecord (int ShabbatWeekId, int WeeklyVideoTypeId, string? YouTubeId, string? GraphicFile); // FN: 1

/*
public record BlobRecord
{
public int ShabbatWeekId { get; set; }     // FN2
public int WeeklyVideoTypeId { get; set; } // FN2
public string? YouTubeId { get; set; }
public string? GraphicFile { get; set; }

}

## FN1: These four types are init-only properties and cnce assigned, their values cannot be changed

To do this in "long form" you would use `init` e.g...

```
public record AddEditState
{
	public Enums.FormMode? FormMode { get; init; }
	// ...
```

see also `Response_Message_Action.cs` (Features\Admin\Video\Enums\) for another example

## FN2: Note these two int's make up the unique index for the Sql Server table `dbo.WeeklyVideo`
 `IX_WeeklyVideo_Unique ON dbo.WeeklyVideo ShabbatWeekId ASC,	WeeklyVideoTypeId ASC`

I need an example in Blazor where a record is passed as a Parameter to components

*/
