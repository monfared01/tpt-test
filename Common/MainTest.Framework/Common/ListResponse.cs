using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.Framework.Common
{
    public class ListResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRowCount { get; set; }
    }
}
