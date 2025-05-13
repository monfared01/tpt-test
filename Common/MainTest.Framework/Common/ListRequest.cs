using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MainTest.Framework.Common
{
    public abstract class ListRequest
    {
        protected int MaxPageSize = 100;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? OrderBy { get; set; } = "Id";
        public bool OrderDescending { get; set; } = false;
        
        public string OrderByString() => OrderBy + (OrderDescending ? " desc" : "");        
        public int RowSkip() => (PageIndex - 1)  * PageSize;
    }
}
