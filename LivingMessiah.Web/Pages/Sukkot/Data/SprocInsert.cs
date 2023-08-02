namespace LivingMessiah.Web.Pages.Sukkot.Data;

public record SprocInsert(int NewId, int SprocReturnValue, string ReturnMsg);

// Ignore Spelling: Sproc

/*
public record Product(int ProductId, string Name, int Cost);
public record Receipt(int ReceiptId, int Payment);
public record Order(int ProductId, int Payment);

public enum PlaceOrderError
{
    DoesntExist,
    InsufficientFunds
} 


IOrder.Service.cs

using OneOf;
using OneOf.Types;

namespace API;

public interface IOrdersService
{
    OneOf<Receipt, PlaceOrderError> PlaceOrder(Order order);
    OneOf<Product, NotFound> FindProduct(OneOf<string, int> productNameOrId);
}

*/