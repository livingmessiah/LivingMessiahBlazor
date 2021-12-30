﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.Hosting;

// C:\Users\JohnM\source\repos\syncfusion\blazor-samples\Program.cs

namespace LivingMessiah.Web.Pages.BlazorSyncFusion
{
	public class SampleBlazorDemo
	{
    public class SampleListType
    {
      public List<SampleListType> SourceData { get; set; }
      public string Name { get; set; }
      [JsonConverter(typeof(StringEnumConverter))]
      public SampleType Type { get; set; }
      public List<Sample> Samples { get; set; }
      public string DemoPath { get; set; }
      public string Category { get; set; }
      public string InfoTooltip { get; set; }
    }

    public class SampleList
    {
      public string Name { get; set; }
      public string Directory { get; set; }
      public string Category { get; set; }
      [JsonConverter(typeof(StringEnumConverter))]
      public SampleType Type { get; set; }
      public List<Sample> Samples { get; set; } = new List<Sample>();
      public string ControllerName { get; set; }
      public string DemoPath { get; set; }
      public bool IsPreview { get; set; }
      public string CustomDocLink { get; set; }
      public bool IsHideFromHomePageList { get; set; }
      public string InfoTooltip { get; set; }
    }

    public class Sample
    {
      public string Name { get; set; }
      public string Directory { get; set; }
      public string Category { get; set; }
      public string FileName { get; set; }
      public string Url { get; set; }
      public string MappingSampleName { get; set; }
      public string MetaTitle { get; set; }
      public string MetaDescription { get; set; }
      public string HeaderText { get; set; }
      public List<SourceCollection> SourceFiles { get; set; } = new List<SourceCollection>();
      [JsonConverter(typeof(StringEnumConverter))]
      public SampleType Type { get; set; }
    }

    public class SourceCollection
    {
      public string FileName { get; set; }
      public string Id { get; set; }
    }

    internal static class SampleBrowser
    {
      public static List<SampleList> SampleList { get; set; } = new List<SampleList>();
      internal static List<string> SampleUrls = new List<string>();
      
      //ToDo: don't know where this is from
      //internal static SampleConfig Config { get; set; } = new SampleConfig();
      
      internal static List<string> PreLoadFiles = new List<string>()
        {
            "styles/common/fonts/open-sans-700.woff2",
            "styles/common/fonts/open-sans-regular.woff2",
        };
    }

    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SampleType
    {
      [EnumMember(Value = "none")]
      None,
      [EnumMember(Value = "new")]
      New,
      [EnumMember(Value = "updated")]
      Updated,
      [EnumMember(Value = "preview")]
      Preview
    }

  }
}
