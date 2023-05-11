using Microsoft.AspNetCore.Components;
//using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.HouseRulesAgreement;

public partial class Index
{
	public bool RefreshHraList { get; set; }

	public string?	Email { get; set; }

	protected override void OnInitialized()
	{
		RefreshHraList = true; // make the list be created first time
	}

	void RecordAddedHandler(bool recordAdded) // 
	{
		//if (recordAdded)
		//{
		RefreshHraList = recordAdded;
		//}
	}


	void ClickHandler(string email)
	{
		Email = email;
	}


	/*
	void EmailChosenHandler(string email) 
	{
		Email = email;
	}
	*/

}
