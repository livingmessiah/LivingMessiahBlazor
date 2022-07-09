using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using static LivingMessiah.Web.Pages.SqlServer;
using Syncfusion.Blazor.RichTextEditor;
using Markdig;

namespace LivingMessiah.Web.Pages.UpcomingEvents;

//[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class NonKeyDateCRUD
{
		[Inject]
		public IUpcomingEventsRepository db { get; set; }

		[Inject]
		public ILogger<NonKeyDateCRUD> Logger { get; set; }


		public NonKeyDateCrudVM NonKeyDateCrudVM { get; set; } = new NonKeyDateCrudVM();


		private string Title = "Add Upcoming Event";


		protected async Task HandleValidSubmit()
		{
				Logger.LogDebug(string.Format("Inside {0}", nameof(NonKeyDateCRUD) + "!" + nameof(HandleValidSubmit)));
				try
				{
						NonKeyDateCrudVM.Id = 0;

						//public async Task<Tuple<int, int, string>> Create(RegistrationVM registrationVM)

						var sprocTuple = await db.Create(NonKeyDateCrudVM);
						//var sprocTuple = await db.Create(DTO_From_VM_To_DB(registrationVM));
						//return sprocTuple;

						if (sprocTuple.Item1 != 0)
						{
								DatabaseInformation = true;
								DatabaseInformationMsg = $"{sprocTuple.Item3}";
								NonKeyDateCrudVM = new NonKeyDateCrudVM();
						}
						else
						{
								if (sprocTuple.Item2 == ReturnValueViolationInUniqueIndex)
								{
										DatabaseWarning = true;
										DatabaseWarningMsg = sprocTuple.Item3;
								}
								else
								{
										DatabaseError = true;
										DatabaseErrorMsg = sprocTuple.Item3;
								}
						}

				}
				catch (Exception)
				{
						DatabaseError = true;
						DatabaseErrorMsg = "Error adding to database";
				}


		}

		private string Message = string.Empty;
		private void OnInvalidSubmit()
		{
				Message = string.Empty;
		}

		private void InitializeVM()
		{
				NonKeyDateCrudVM.EventDate = DateTime.Now;
				NonKeyDateCrudVM.YearId = DateTime.Now.Year;
				NonKeyDateCrudVM.EventTypeEnum = KeyDates.Enums.EventTypeEnum.GuestSpeaker;
		}


		private void OnValueChange(Syncfusion.Blazor.RichTextEditor.ChangeEventArgs args)
		{
				if (args.Value == null)
				{
						this.HtmlValue = null;
				}
				else
				{
						this.HtmlValue = Markdig.Markdown.ToHtml(args.Value, Pipeline);
				}
		}

		private bool IsPreview { get; set; }
		private string HtmlValue { get; set; }
		private MarkdownPipeline Pipeline { get; set; }
		/*
				private string MarkdownValue { get; set; } = @"The sample is added to showcase **markdown editing**.

		Type or edit the content and apply formatting to view markdown formatted content.

		We can add our own custom formation syntax for the Markdown formation, [sample link](https://blazor.syncfusion.com/demos/rich-text-editor/markdown-custom-format).

		The third-party library **Marked** is used in this sample to convert markdown into HTML content.";
		*/
		private void PreviewClick()
		{
				this.IsPreview = true;
		}

		private void CodeClick()
		{
				this.IsPreview = false;
		}

		private List<ToolbarItemModel> Items = new List<ToolbarItemModel>() {
				new ToolbarItemModel() { Name = "code", TooltipText = "Code View" },
		};

		private List<ToolbarItemModel> Tools = new List<ToolbarItemModel>()
		{
			new ToolbarItemModel() { Command = ToolbarCommand.Bold },
			new ToolbarItemModel() { Command = ToolbarCommand.Italic },
			new ToolbarItemModel() { Command = ToolbarCommand.Underline },
			new ToolbarItemModel() { Command = ToolbarCommand.Separator },
			new ToolbarItemModel() { Command = ToolbarCommand.Formats },
			new ToolbarItemModel() { Command = ToolbarCommand.Alignments },
			new ToolbarItemModel() { Command = ToolbarCommand.Separator },
			new ToolbarItemModel() { Command = ToolbarCommand.CreateLink },
			new ToolbarItemModel() { Command = ToolbarCommand.Image },
			new ToolbarItemModel() { Command = ToolbarCommand.CreateTable },
			new ToolbarItemModel() { Command = ToolbarCommand.Separator },
			new ToolbarItemModel() { Command = ToolbarCommand.SourceCode },
			new ToolbarItemModel() { Command = ToolbarCommand.Separator },
			new ToolbarItemModel() { Command = ToolbarCommand.Undo },
			new ToolbarItemModel() { Command = ToolbarCommand.Redo }
		};

		private Dictionary<string, string> ListSyntax { get; set; } = new Dictionary<string, string>(){
				{ "OL", "1., 2., 3." }
		};

		#region ErrorHandling

		private void InitializeErrorHandling()
		{
				DatabaseInformationMsg = "";
				DatabaseInformation = false;
				DatabaseWarningMsg = "";
				DatabaseWarning = false;
				DatabaseErrorMsg = "";
				DatabaseError = false;
		}

		protected bool DatabaseInformation = false;
		protected string DatabaseInformationMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
		protected string DatabaseErrorMsg { get; set; }
		#endregion


} // class 

