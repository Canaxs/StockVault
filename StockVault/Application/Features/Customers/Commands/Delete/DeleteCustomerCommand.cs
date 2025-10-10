using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.Delete;

public class DeleteCustomerCommand:IRequest<DeletedCustomerResponse>
{
    public int Id { get; set; }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeletedCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, CustomerBusinessRules customerBusinessRules)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<DeletedCustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CheckIfCustomerIdExists(request.Id);

            Customer customer = await _customerRepository.GetAsync(predicate: c => c.Id == request.Id);

            await _customerRepository.DeleteAsync(customer);

            return _mapper.Map<DeletedCustomerResponse>(customer);
        }
    }
}
