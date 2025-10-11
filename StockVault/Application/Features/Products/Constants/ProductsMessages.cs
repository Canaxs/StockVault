using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Constants;

public class ProductsMessages
{
    public const string ProductNameExists = "Product name exists";
    public const string ProductNameNotFound = "No record found with the product name";
    public const string ProductNotFoundOrAlreadyDeleted = "Product not found or already deleted.";
    public const string ProductHasStockCannotBeDeleted = "This product cannot be deleted as it still has stock.";
}
