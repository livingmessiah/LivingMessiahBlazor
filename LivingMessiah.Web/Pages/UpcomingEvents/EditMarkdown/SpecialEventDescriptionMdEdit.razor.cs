using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace LivingMessiah.Web.Pages.UpcomingEvents.EditMarkdown;

public partial class SpecialEventDescriptionMdEdit
{
		[Parameter]
		public int Id { get; set; }

		[Inject]
		public ILogger<SpecialEventDescriptionMdEdit> Logger { get; set; }

		[Inject]
		public IUpcomingEventsRepository db { get; set; }


		private EditMarkdownVM VM = new EditMarkdownVM();

		protected override async Task OnInitializedAsync()
		{
				try
				{
						Logger.LogDebug(string.Format("Inside {0} Id:{1}", nameof(SpecialEventDescriptionMdEdit) + "!" + nameof(OnInitializedAsync), Id));

						VM = await db.GetDescription(Id);
						if (VM is null)
						{
								DatabaseWarning = true;
								DatabaseWarningMsg = $"{nameof(SpecialEventDescriptionMdEdit)} NOT FOUND";
						}
				}
				catch (Exception ex)
				{
						DatabaseError = true;
						DatabaseErrorMsg = $"Error reading database";
						Logger.LogError(ex, $"...{DatabaseErrorMsg}");
				}
		}

		private bool HasRowBeenUpdated { get; set; } = false;
		private void CloseDialog()
		{
				this.HasRowBeenUpdated = false;
		}

		protected async Task ValidSubmit(EditContext context)
		{
				Logger.LogDebug(string.Format("Inside {0}", nameof(SpecialEventDescriptionMdEdit) + "!" + nameof(ValidSubmit)));
				int rows = 0;
				EditMarkdownVM vm = (EditMarkdownVM)context.Model;
				try
				{
						rows = await db.UpdateDescription(vm.Id, vm.Description);
						DatabaseInformation = true;
						DatabaseInformationMsg = $"Description Updated";
						HasRowBeenUpdated = true;
				}
				catch (Exception ex)
				{
						DatabaseError = true;
						DatabaseErrorMsg = $"Error updating database";
						Logger.LogError(ex, $"...{DatabaseErrorMsg}");
				}
				Logger.LogDebug(string.Format("...rows {0}", rows));
		}

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

}
