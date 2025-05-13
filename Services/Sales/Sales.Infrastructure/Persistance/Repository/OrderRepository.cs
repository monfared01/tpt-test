using MainTest.Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Common.Persistence.Interfaces;

namespace Sales.Infrastructure.Persistance.Repository;

public class OrderRepository : GenericRepository<Domain.Order>, IOrderRepository
{
    public OrderRepository(DbContext context) : base(context) 
    {
        
    }
}
