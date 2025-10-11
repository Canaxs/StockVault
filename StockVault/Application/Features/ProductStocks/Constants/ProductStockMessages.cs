using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductStocks.Constants;

public class ProductStockMessages
{
    public const string ProductAlreadyInWarehouse = "This product is already available in this warehouse.";
    public const string ProductStockNotExist = "Product stock does not exist.";
    public const string NotEnoughSpaceForStock = "Not enough space in the warehouse for this quantity.";
}
