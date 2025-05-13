using MainTest.Framework.DataAccess;

namespace Sales.Application.Common.Persistence.Interfaces
{
    public interface ISalesUnitOfWork :IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
    }
}
