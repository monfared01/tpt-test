using MainTest.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.Framework.DataAccess
{    
    
    public interface IHasSecureRepository
    {
        public List<string> Roles { get; set; }
        public ICollection<PermittedEntity> GetPermitedEntities(ICollection<string> roles);
    }
}
