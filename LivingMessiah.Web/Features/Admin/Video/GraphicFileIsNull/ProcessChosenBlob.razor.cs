using LivingMessiah.Web.Features.Admin.Video.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.Admin.Video.GraphicFileIsNull;
public partial class ProcessChosenBlob
{
	[Parameter, EditorRequired] public string? YouTubeId { get; set; }
	[Parameter, EditorRequired] public string? BlobFileName { get; set; }
	[Parameter, EditorRequired] public int ShabbatWeekId { get; set; }

	[Parameter, EditorRequired] public EventCallback<bool> OnProcessCompleted { get; set; }  // <string>

	protected string? _YouTubeId;
	protected string? _BlobFileName;
	protected int _ShabbatWeekId;

	protected override void OnParametersSet()
	{
		_YouTubeId = YouTubeId;
		_BlobFileName = BlobFileName;
		_ShabbatWeekId = ShabbatWeekId;
	}

	private async Task ButtonClicked()
	{
		YouTubeAndFileVM vm = new();
		vm.YouTubeId = _YouTubeId;
		vm.GraphicFile = _BlobFileName;
		vm.ShabbatWeekId = _ShabbatWeekId;
		vm.WeeklyVideoTypeId = Enums.WeeklyVideoType.MainServiceEnglish.Value;
		try
		{
			var DatabaseResult = await db!.WeeklyVideoUpdateGraphicFile(vm).ContinueWith(t => new SprocTuple(t.Result.Item1, t.Result.Item2, t.Result.Item3));

			if (DatabaseResult.ReturnValue == 0)  //if (sprocTuple.Item2 == 0)
			{
				Logger!.LogInformation("{Class}!{Method}; {Command} successfully updated {GraphicFile} for Key1 {ShabbatWeekId} and Key2 {WeeklyVideoTypeId}  ", 
					nameof(ProcessChosenBlob), nameof(ButtonClicked), nameof(db.WeeklyVideoUpdateGraphicFile), vm.GraphicFile, vm.ShabbatWeekId, vm.WeeklyVideoTypeId);
				Toast!.ShowSuccess($"Graphic File Updated! ReturnValue: {DatabaseResult.ReturnValue}; ReturnMsg{DatabaseResult.ReturnMsg}");
			}
			else
			{
				Toast!.ShowWarning($"{nameof(ProcessChosenBlob)}!{nameof(ButtonClicked)} Clicked; ReturnMsg{DatabaseResult.ReturnMsg}");
			}

		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "{Class}!{Method}; {Command}", nameof(ProcessChosenBlob), nameof(ButtonClicked), nameof(db.WeeklyVideoUpdateGraphicFile));
			Toast!.ShowError($"{Global.ToastShowError} {nameof(ProcessChosenBlob)}, {nameof(ButtonClicked)}");
		}

		finally
		{
			await OnProcessCompleted.InvokeAsync(false); 
		}

	}

	private void CancelClicked()
	{
		OnProcessCompleted.InvokeAsync(true);
	}

}