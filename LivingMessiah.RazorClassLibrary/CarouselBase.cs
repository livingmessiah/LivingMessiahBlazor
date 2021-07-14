using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace LivingMessiah.RazorClassLibrary
{
	public class CarouselBase : ComponentBase
	{

    [Parameter]
    public string id { get; set; }

    [Parameter]
    public List<ImageFile> imageset { get; set; }

    [Parameter]
    public string name { get; set; }

    [Parameter]
    public string CssClass { get; set; }

    [Parameter]
    public string ImageClass { get; set; }

    [Parameter]
    public int? AutoScrollInterval { get; set; }

    public delegate void CarouselItemClicked(object sender, int index);

    [Parameter]
    public CarouselItemClicked OnCarouselItemClicked { get; set; }

    [Parameter]
    public bool ShowNavigation { get; set; } = true;

    protected int _carouselRenderIndex = -1;
    protected int _activeImageIndex = 0;
    private Timer _scrollTimer = null;

    protected override async Task OnParametersSetAsync()
    {
      if ((this.AutoScrollInterval ?? 0) > 0)
      {

        int scrollMilliseconds = ((int)(this.AutoScrollInterval)) * 1000;

        _scrollTimer?.Stop();

        if (_scrollTimer == null)
        {
          _scrollTimer = new Timer();
          _scrollTimer.Elapsed += (o, e) =>
          {

            _activeImageIndex += 1;

            if (_activeImageIndex > (imageset?.Count - 1 ?? 0))
            {
              _activeImageIndex = 0;
            }

            this.InvokeAsync(() =>
            {
              this.StateHasChanged();
            });
          };
        }

        _scrollTimer.Interval = scrollMilliseconds;
        _scrollTimer?.Start();
      }
      else
      {
        _scrollTimer?.Stop();
      }
    }

    protected void OnNextClicked()
    {
      _carouselRenderIndex = -1;
      _activeImageIndex += 1;
      this.StateHasChanged();
    }

    protected void OnPreviousClicked()
    {
      _carouselRenderIndex = -1;
      _activeImageIndex -= 1;
      this.StateHasChanged();
    }

    protected void SetActiveImageIndex(int newIndex)
    {
      _carouselRenderIndex = -1;
      _activeImageIndex = newIndex;

      this.StateHasChanged();
    }

    protected string GetClasses()
    {
      string imageStateClass = "hidden";

      _carouselRenderIndex++;

      if (_carouselRenderIndex == _activeImageIndex)
      {
        imageStateClass = null;
      }

      imageStateClass += OnCarouselItemClicked != null ? " blazor-carousel-pointer" : "";
      return imageStateClass.Trim();

    }

    protected string GetIndicatorState()
    {
      string imageStateClass = "";

      _carouselRenderIndex++;

      if (_carouselRenderIndex == _activeImageIndex)
      {
        imageStateClass = "blazor-carousel-indicator-active";
      }

      return imageStateClass;
    }

    protected string GetImageSource(ImageFile imageFile)
    {
      string imageSrc = imageFile.Url;

      if (string.IsNullOrEmpty(imageFile.Url) && imageFile.FileContent?.Length > 0)
      {
        imageSrc = imageFile.Base64Image;
      }

      return imageSrc;
    }

    protected string ResetRenderCounter()
    {

      _carouselRenderIndex = -1;
      return null;
    }


  }

}
