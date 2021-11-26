# Grid Events

https://www.syncfusion.com/forums/159289/sfgrid-doesnt-update-after-edit-or-delete-with-dapper

From your code example we found that you have not enabled PrimaryKey column in Grid. 
CRUD operation in Grid will take place based on PrimaryKey column whose value is unique. 
IsPrimaryKey property of GridColumns is used to define the primary key. 
So kindly define the IsPrimaryKey property to any one of the available column whose value is unique.  

https://blazor.syncfusion.com/documentation/datagrid/editing#event-trace-while-editing
The Editing functionalities can be performed based upon the primary key value of the selected row. 
If PrimaryKey is not defined in the datagrid, then edit or delete action take places in the first row.

While editing operation is getting executed the following events will be notified,

`OnActionBegin`
`OnActionComplete`

In both these events the type of editing operation is returned in the RequestType parameter of the event arguments. In addition to this, the event arguments also return the edited row data.

The RequestType values for the editing operations are listed in the below table,

RequestType	OnActionBegin											OnActionComplete
----------- -------------------------------- ------------------------------
BeginEdit		Before editing operation begins		After editing operation is completed
Add					Before add operation begins				After add operation is completed
Delete			Before delete operation begins		After delete operation is completed
Save				Before save operation begins			After save operation is completed
Cancel			Before cancel operation begins		After cancel operation is completed

```html
@using Syncfusion.Blazor.Grids

<SfGrid DataSource="@Orders" AllowPaging="true" 
    Toolbar="@(new List<string>() { "Add", "Edit", "Delete", "Cancel", "Update" })" Height="315">
    <GridEditSettings AllowAdding="true" AllowEditing="true" AllowDeleting="true"></GridEditSettings>
    <GridEvents OnActionBegin="ActionBegin" OnActionComplete="ActionComplete" TValue="Order"></GridEvents>
    <GridColumns>
        <GridColumn Field=@nameof(Order.OrderID) HeaderText="Order ID" IsPrimaryKey="true" Width="120"></GridColumn>
        <GridColumn Field=@nameof(Order.CustomerID) HeaderText="Customer Name" Width="120"></GridColumn>
        <GridColumn Field=@nameof(Order.OrderDate) HeaderText=" Order Date" EditType="EditType.DatePickerEdit" Format="d" TextAlign="TextAlign.Right" Width="130" Type="ColumnType.Date"></GridColumn>
        <GridColumn Field=@nameof(Order.Freight) HeaderText="Freight" Format="C2" TextAlign="TextAlign.Right" Width="120"></GridColumn>
    </GridColumns>
</SfGrid>
```

```csharp
@code{
    public List<Order> Orders { get; set; }

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

    public void ActionBegin(ActionEventArgs<Order> args)
    {
        if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
        {
            // Triggers before editing operation starts
        }
        else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
        {
            // Triggers before add operation starts
        }
        else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Cancel)
        {
            // Triggers before cancel operation starts
        }
        else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
        {
            // Triggers before save operation starts
        }
        else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
        {
            // Triggers before delete operation starts
        }
    }

    public void ActionComplete(ActionEventArgs<Order> args)
    {
        if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
        {
            // Triggers once editing operation completes
        }
        else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
        {
            // Triggers once add operation completes
        }
        else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Cancel)
        {
            // Triggers once cancel operation completes
        }
        else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
        {
            // Triggers once save operation completes
        }
        else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
        {
            // Triggers once delete operation completes
        }
    }
}
```