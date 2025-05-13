using MainTest.Framework.DataAccess;

namespace Sales.Application.Common.Persistence.Interfaces;

public interface IOrderRepository : IGenericRepository<Domain.Order>
{
}
