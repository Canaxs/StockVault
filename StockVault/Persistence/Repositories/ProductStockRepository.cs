using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductStockRepository : EfRepositoryBase<ProductStock, int, BaseDbContext>, IProductStockRepository
{
    public ProductStockRepository(BaseDbContext baseDbContext) : base(baseDbContext) { }
}
