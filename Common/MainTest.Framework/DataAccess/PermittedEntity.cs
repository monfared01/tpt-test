using MainTest.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.Framework.DataAccess
{
    public class PermittedEntity
    {
        public int EntityType { get; set; }
        public CrudMethods Method { get; set; }
    }
}
