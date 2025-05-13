using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Common.Interface
{
    public interface ICurrentUserAccessor
    {
        public Guid UserId { get; }
    }
}
