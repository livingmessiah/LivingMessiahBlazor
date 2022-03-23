using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.BlazorSyncFusion.Grid;
public partial class GridSearch : ComponentBase
{
	public List<Order> Orders { get; set; }

	//If you don't include GridSearchSettings Fields, it searches all fields
	public string[] InitSearchField = (new string[] { "CustomerID" });
	public List<string> Tool       = (new List<string>() { "Search" });

	protected override void OnInitialized()
	{
		Orders = Enumerable.Range(1, 75).Select(x => new Order()
		{
			OrderID = 1000 + x,
			CustomerID = (new string[] { "ALFKI", "ANANTR", "ANTON", "BLONP", "BOLID" })[new Random().Next(5)],
			Freight = 2.1 * x,
			OrderDate = DateTime.Now.AddDays(-x),
		}).ToList();
	}


	public class Order
	{
		public int? OrderID { get; set; }
		public string CustomerID { get; set; }
		public DateTime? OrderDate { get; set; }
		public double? Freight { get; set; }
	}

	/*
	To search datagrid records from an external button, invoke the Search method.	
	public void SearchBtnHandler()
	{
		this.DefaultGrid.Search("1001");
	}
	*/
}
