using System.Linq;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.BlazorSyncFusion.Shared //  BlazorDemos.Shared 
;

/// <summary>
/// The injectable service class used to handle common functionalities all over the application.
/// </summary>
public class SampleService
{
		/// <summary>
		/// Specifies the app is rendering in device or not.
		/// </summary>
		public bool IsDevice { get; set; }
		/// <summary>
		/// Specifies spinner component reference.
		/// </summary>
		public SpinnerComponent Spinner { get; set; }
		/// <summary>
		/// Specifies the current component name.
		/// </summary>
		public string ComponentName { get; set; }
		/// <summary>
		/// Specifies the current sample details.
		/// </summary>

		/*
Error	CS0246	
The type or namespace name 'SampleList' could not be found 
public LivingMessiah.Web.Pages.BlazorSyncFusion.Shared.SampleService. SampleInfo { get; set; }    
     */
		//    public Sample SampleInfo { get; set; }

		//    /// <summary>
		//    /// Specifies the meta data component reference.
		//    /// </summary>
		//    public SampleMetaData MetaData { get; set; }
		//        /// <summary>
		//        /// Specifies the very first sample url.
		//        /// </summary>
		//        public string FirstSampleUrl { get; set; }
		//        /// <summary>
		//        /// Specifies the last sample url.
		//        /// </summary>
		//        public string LastSampleUrl { get; set; }

		//        /// <summary>
		//        /// Specifies the image url starts path.
		//        /// </summary>
		//        public string ImagePath { get; set; }
		//        /// <summary>
		//        /// Specifies the showcase image url starts path.
		//        /// </summary>
		//        public string ShowCaseImagePath { get; set; }
		//        /// <summary>
		//        /// Specifies the home page loaded or not.
		//        /// </summary>
		//        public bool IsHomeLoaded { get; set; }
		//        /// <summary>
		//        /// Specifies the demo page loaded or not.
		//        /// </summary>
		//        public bool IsDemoLoaded { get; set; }

		//        public SampleService()
		//        {
		//#if DEBUG
		//            ImagePath = "./images/common/";
		//            ShowCaseImagePath = "./images/showcase/";
		//#else
		//            ImagePath = "https://cdn.syncfusion.com/blazor/images/demos/";
		//            ShowCaseImagePath = "https://cdn.syncfusion.com/blazor/images/showcase/";
		//#endif
		//        }

		// Updates the SampleInfo and ComponentName based on current loaded uri.
		//internal void Update(NavigationManager urlHelper)
}  // public class SampleService
