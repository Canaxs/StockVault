using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<UpdatedCustomerResponse>,ICacheRemoverRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CompanyName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }

    public string? CacheKey => "";

    public bool BypassCache => false;

    public string? CacheGroupKey => "GetCustomers";

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdatedCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, CustomerBusinessRules customerBusinessRules)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<UpdatedCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CheckIfCustomerIdExists(request.Id);

            Customer customer = await _customerRepository.GetAsync(predicate: c => c.Id == request.Id);

            if (!string.IsNullOrWhiteSpace(request.Name) && !string.Equals(customer?.Name, request.Name, StringComparison.Ordinal))
                customer.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.CompanyName) && !string.Equals(customer?.CompanyName, request.CompanyName, StringComparison.Ordinal))
                customer.CompanyName = request.CompanyName;

            if (!string.IsNullOrWhiteSpace(request.Address) && !string.Equals(customer?.Address, request.Address, StringComparison.Ordinal))
                customer.Address = request.Address;

            if (!string.IsNullOrWhiteSpace(request.City) && !string.Equals(customer?.City, request.City, StringComparison.Ordinal))
                customer.City = request.City;

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && !string.Equals(customer?.PhoneNumber, request.PhoneNumber, StringComparison.Ordinal))
                customer.PhoneNumber = request.PhoneNumber;


            await _customerRepository.UpdateAsync(customer);

            return _mapper.Map<UpdatedCustomerResponse>(customer);
        }
    }
}
