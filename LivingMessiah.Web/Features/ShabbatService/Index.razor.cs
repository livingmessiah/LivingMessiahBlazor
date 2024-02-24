using LivingMessiah.Web.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace LivingMessiah.Web.Features.ShabbatService;

public partial class Index
{
	[Inject] public IOptions<AppSettings>? AppSettings { get; set; }
	protected bool _isPrinterFriendly = false;

	protected string? _printMsg;
	protected string? _oppositeIcon;
	protected string? _oppositeToggleMsg;

	public bool _ShowSpanish { get; set; } = false;
	public bool LoadQuickly { get; set; }

	protected override void OnInitialized()
	{
		LoadQuickly = AppSettings!.Value.ShabbatServiceLoadQuickly;
		SetPrintButton();
	}

	protected void LanguageSelectionChanged(bool showSpanish)
	{
		_ShowSpanish = showSpanish;
	}

	protected void PrintFriendly_Button_Click()
	{
		_isPrinterFriendly = !_isPrinterFriendly; // Toggle _isPrinterFriendly
		SetPrintButton();
		StateHasChanged();
	}

	protected void SetPrintButton()
	{
		if (_isPrinterFriendly)
		{
			_printMsg = _ShowSpanish ? "Impresión" : "Print";
			_oppositeIcon = "<i class='far fa-arrow-alt-circle-left'></i>";
			_oppositeToggleMsg = _ShowSpanish ? "Mostrar imagenes" : "Show Images";
		}
		else
		{
			_printMsg = "";
			_oppositeIcon = "<i class='fas fa-print'></i>";
			_oppositeToggleMsg = _ShowSpanish ? "Amistoso de impresión" : "Print Friendly";
		}

	}

}