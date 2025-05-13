using System.Linq.Expressions;

namespace MainTest.Framework.Common
{
    public interface IHasExpressionFilter<T>
    {
        Expression<Func<T, bool>> GetExpression();
    }
}
