using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace LivingMessiah.Web.Pages.BlazorSyncFusion.RichTextEditor
{
	public partial class Form_Validation
	{
		private class FormModel
		{
			[Required]
			[MinLength(20, ErrorMessage = "Please enter at least 20 characters based on HTML.")]
			public string Description { get; set; }
		}

		private FormModel Model = new FormModel();
		private bool Visible { get; set; } = false;

		private void CloseDialog()
		{
			this.Visible = false;
		}

		private void ResetHandler()
		{
			Model = new FormModel();
		}

		private void ValidSubmit(EditContext context)
		{
			FormModel contextModel = (FormModel)context.Model;
			if (contextModel.Description != "<p><br></p>" && contextModel.Description.Length > 19)
			{
				Model = new FormModel();
				this.Visible = true;
			}
		}
	}
}
