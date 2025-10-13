using Application.Features.Customers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Rules;

public class CustomerBusinessRules:BaseBusinessRules
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerBusinessRules(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CheckIfCustomerIdExists(int id)
    {
        bool result = await _customerRepository.AnyAsync(c => c.Id == id);

        if (!result)
            throw new NotFoundException(CustomerMessages.CustomerNotFoundOrAlreadyDeleted);
    }
}
