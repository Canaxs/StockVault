using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WarehouseRepository : EfRepositoryBase<Warehouse, int, BaseDbContext>, IWarehouseRepository
{
    public WarehouseRepository(BaseDbContext baseDbContext) : base(baseDbContext) { }
}
