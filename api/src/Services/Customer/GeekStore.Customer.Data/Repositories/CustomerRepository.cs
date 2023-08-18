﻿using GeekStore.Core.Data;
using GeekStore.Customer.Data.Context;
using GeekStore.Customer.Domain.Customers.Repositories;

namespace GeekStore.Customer.Data.Repositories
{
    public class CustomerRepository : Repository<CustomerDataContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerDataContext context) : base(context)
        {
        }
    }
}
