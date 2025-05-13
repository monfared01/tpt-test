namespace Sales.Application.Common.Interface
{
    public interface ICurrentUserAccessor
    {
        public Guid UserId { get; }
    }
}
