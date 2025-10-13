using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListShipmentSummary;

public class GetShipmentSummaryByProductIdQuery:IRequest<GetShipmentSummaryByProductIdResponse>
{
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetShipmentSummaryByProductIdQueryHandler : IRequestHandler<GetShipmentSummaryByProductIdQuery, GetShipmentSummaryByProductIdResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetShipmentSummaryByProductIdQueryHandler(IShipmentRepository shipmentRepository,ProductBusinessRules productBusinessRules)
        {
            _shipmentRepository = shipmentRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetShipmentSummaryByProductIdResponse> Handle(GetShipmentSummaryByProductIdQuery request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductShouldExistWhenRequested(request.Id);

            GetShipmentSummaryByProductIdResponse? getShipmentSummaryByProductIdResponse = await _shipmentRepository.GetProjectedAsync(
                predicate: s => s.ProductId == request.Id
                        && s.DeliveryStatus != Domain.Enums.DeliveryStatus.Failed
                        && (!request.StartDate.HasValue || s.CreatedDate >= request.StartDate.Value)
                        && (!request.EndDate.HasValue || s.CreatedDate <= request.EndDate.Value),
                include: q => q.Include(s => s.Product),
                groupBy: q => q
                .GroupBy(s => s.ProductId)
                .Select(g => new GetShipmentSummaryByProductIdResponse
                {
                    Id = g.FirstOrDefault().Product.Id,
                    Name = g.FirstOrDefault().Product.Name,
                    Price = g.FirstOrDefault().Product.Price,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalPrice = g.Sum(x => x.Quantity * x.Product.Price)
                }),
                cancellationToken: cancellationToken);

            return getShipmentSummaryByProductIdResponse;
        }
    }
}
