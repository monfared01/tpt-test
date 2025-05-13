using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.Framework.Common
{
    public interface IHasExpressionFilter<T>
    {
        Expression<Func<T, bool>> GetExpression();
    }
}
