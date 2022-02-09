using System;
using System.Collections.Generic;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.BlazorSyncFusion.Shared;

public static class SampleUtilsVer2
{
		#region common
		public const string SPACE = " ";
		public const string CONTENT = "sf-content";
		public const string VISIBLE = "sf-visible";
		public const string HIDDEN = "sf-hidden";
		public const string DISPLAY_NONE = "sb-hide";
		public const string COMPONENTS_HIDE = "sb-components-hide";
		public const string ACTIVE_CLASS = "active";

		#endregion

		#region SideBarComponent
		public const string SIDEBAR_CLASS = "sf-sidebar";
		public const string LEFT_CLASS = "sf-sidebar-left";
		public const string RIGHT_CLASS = "sf-sidebar-right";
		public const string RIGHT_PANE = "sf-sidebar-right-pane";
		public const string RIGHT_PANE_COLLAPSE = "sf-sidebar-right-pane-collapse";
		public const string RIGHT_PANE_EXPAND = "sf-sidebar-right-pane-expand";
		#endregion

		#region ListComponent
		public const string LIST_CLASS = "sf-list";
		public const string LIST_UL_CLASS = "sf-list-ul";
		public const string LIST_LI_CLASS = "sf-list-li";
		public const string LIST_LI_GROUP_CLASS = "sf-list-group-li";
		public const string LIST_ACTIVE = "sf-list-li-active";
		#endregion

		#region Spinner
		public const string MODAL_CLASS = "sf-modal";
		#endregion

		#region TreeComponent
		public const string TREE_CLASS = "sf-tree";
		public const string TREE_FULL_ROW = "sf-tree-full-row";
		public const string TREE_TEXT_CONTENT = "sf-tree-text-content";
		public const string TREE_PARENT = "sf-tree-parent";
		public const string TREE_PARENT_LI = "sf-tree-parent-li";
		public const string TREE_EXPAND_ICON = "sf-tree-expand-icon";
		public const string TREE_COLLAPSE_ICON = "sf-tree-collapse-icon";
		public const string TREE_TEXT = "sf-tree-text";
		public const string TREE_ACTIVE = "sf-tree-active";
		public const string TREE_HIDE = "sf-tree-hide";
		#endregion

		#region CarouselComponent
		public const string CAROUSEL = "sf-carousel";
		public const string CAROUSEL_CONTAINER = "sf-carousel-container";
		public const string CAROUSEL_TRANSITION = "sf-carousel-transition";
		public const string CAROUSEL_ITEM = "sf-carousel-item";
		public const string CAROUSEL_SELECTED = "sf-carousel-selected";
		public const string CAROUSEL_PREVIOUS_BUTTON = "sf-carousel-prev-button sb-icons";
		public const string CAROUSEL_NEXT_BUTTON = "sf-carousel-next-button sb-icons";
		public const string CAROUSEL_IMAGE = "sf-carousel-image";
		public const string CAROUSEL_HEADER = "sf-carousel-header";
		public const string CAROUSEL_CONTENT = "sf-carousel-content";
		public const string CAROUSEL_CLONE = "sf-carousel-clone";
		public const string CAROUSEL_CONTENT_AREA = "sf-carousel-content-area";
		public const string CAROUSEL_PROGRESS = "sf-carousel-progress";
		#endregion

		#region SearchComponent
		public const string SEARCH_CONTAINER = "sf-search-container";
		public const string SEARCH_POPUP = "sf-search-popup";
		public const string SEARCH_INPUT = "sf-search-input";
		public const string SEARCH_NO_DATA = "sf-search-no-data";
		public const string CLEAR_ICON = "sb-icons sf-clear-icon";
		public const string SEARCH_ICON = "sb-icons sf-search-icon";
		public const string SEARCH_OVERLAY = "sb-search-overlay";
		public const string SEARCH_KEY_NAV = "sf-key-nav";
		#endregion

		#region AdStripComponent
		public const string AD_CONTAINER = "sb-ad-container";
		public const string AD_CONTENT = "sb-ad-content-area";
		public const string AD_HEADER = "sb-ad-header";
		public const string AD_POINTS_DIV = "sb-ad-points-div";
		public const string AD_POINT_DIV = "sb-ad-point-div";
		public const string AD_POINT_TICK = "sb-ad-img-div sb-icons sb-ad-tick";
		public const string AD_POINT_TEXT = "sb-ad-point-text";
		public const string AD_LINK = "sb-ad-link";
		public const string AD_TRY = "sb-ad-try-area";
		#endregion

		#region HomePage
		public const string HEADER_LOGO = "header-logo";
		public const string SYNCFUSION_LOGO = "sf-header-logo";
		public const string MOBILE_SEARCH_CONTAINER = "mobile-search-container";
		#endregion

		#region Preferences
		public const string DEFAULT_MODE = "mouse";
		public const string PREFERENCES_POPUP_CLASS = "sf-preferences-popup";
		public const string PREFERENCES_TOUCH = "sf-preference-btn sf-preference-touch-btn";
		public const string PREFERENCES_MOUSE = "sf-preference-btn sf-preference-mouse-btn";
		#endregion

		#region HeaderComponent
		public const string HEADER_SEARCH_CLASS = "sb-search-btn";
		public const string HEADER_PREFERENCES_CLASS = "sf-preferences-button";
		#endregion

		#region DropdownComponent
		public const string DROPDOWN_POPUP = "sf-dropdown-popup";
		#endregion

		/// <summary>
		/// Add a class to the existing string content.
		/// </summary>
		/// <param name="prevClass">Previous class list in string format.</param>
		/// <param name="className">Class name needs to be added in the string content.</param>
		/// <returns>Returns class string.</returns>
		internal static string AddClass(string prevClass, string className)
		{
				var finalClass = string.IsNullOrEmpty(prevClass) ? string.Empty : prevClass.Trim();
				finalClass = finalClass.Contains(className) ? finalClass : finalClass + SampleUtilsVer2.SPACE + className;
				return finalClass;
		}

		/// <summary>
		/// Remove a class from the existing string content.
		/// </summary>
		/// <param name="prevClass">Previous class list in string format.</param>
		/// <param name="className">Class name needs to be removed in the string content.</param>
		/// <returns>Returns class string.</returns>
		public static string RemoveClass(string prevClass, string className)
		{
				var finalClass = string.IsNullOrEmpty(prevClass) ? string.Empty : prevClass.Trim();
				finalClass = finalClass.Contains(className) ? prevClass.Replace(className, string.Empty) : finalClass;
				return finalClass;
		}

}
