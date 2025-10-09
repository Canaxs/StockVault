using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ShipmentRepository : EfRepositoryBase<Shipment, int, BaseDbContext>, IShipmentRepository
{
    public ShipmentRepository(BaseDbContext baseDbContext) : base(baseDbContext) { }
}
