using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace LivingMessiah.Web.Pages.BlazorExamples;

public partial class EventHandler
{
		//[Parameter]
		//public int MyProperty { get; set; }

		private string currentHeading = "Initial heading";
		private string newHeading;
		private string checkedMessage = "Not changed yet";

		//private void UpdateHeading()
		//{
		//	currentHeading = $"{newHeading}!!!";
		//}

		private async Task UpdateHeading()
		{
				await Task.Delay(2000);
				currentHeading = $"{newHeading}!!!";
		}

		private void CheckChanged()
		{
				checkedMessage = $"Last changed at {DateTime.Now}";
		}


		//protected string heading = "Initial Heading!!!";
		//public string heading { get; set; } = "Initial Heading!!!";

		private string heading2 = "Select a button to learn its position";
		private void UpdateHeading(MouseEventArgs e, int buttonNumber)
		{
				heading2 = $"Selected #{buttonNumber} at {e.ClientX}:{e.ClientY}";
		}

}

// is only necessary if the event type is used in the method.