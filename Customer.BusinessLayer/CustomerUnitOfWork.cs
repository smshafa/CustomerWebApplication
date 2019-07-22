using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.DataLayer;

namespace Customer.BusinessLayer
{
    public class CustomerUnitOfWork
    {
        private CustomerContext _dbContext = new CustomerContext();

        public Type TheType { get; set; }

        public GenericRepository<TEntityType> GetRepoInstance<TEntityType>() where TEntityType : class
        {
            return new GenericRepository<TEntityType>(_dbContext);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
