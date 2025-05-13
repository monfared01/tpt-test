using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.Framework.Extensions
{
    public static class RepositoryFilterExtenstion
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> firstFilter, Expression<Func<T, bool>> secondFilter)
        {
            // Parameter for the combined expression
            var parameter = Expression.Parameter(typeof(T));

            // Replace parameter in first and second filters with a common parameter
            var firstBody = Expression.Invoke(firstFilter, parameter);
            var secondBody = Expression.Invoke(secondFilter, parameter);

            // Combine the two filters using Expression.AndAlso (logical AND)
            var combinedBody = Expression.AndAlso(firstBody, secondBody);

            // Create a new lambda expression with the combined body
            return Expression.Lambda<Func<T, bool>>(combinedBody, parameter);
        }

        // Combine two expressions with OR
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> firstFilter, Expression<Func<T, bool>> secondFilter)
        {
            var parameter = Expression.Parameter(typeof(T));

            var firstBody = Expression.Invoke(firstFilter, parameter);
            var secondBody = Expression.Invoke(secondFilter, parameter);

            // Combine the two filters using Expression.OrElse (logical OR)
            var combinedBody = Expression.OrElse(firstBody, secondBody);

            return Expression.Lambda<Func<T, bool>>(combinedBody, parameter);
        }
    }
}
