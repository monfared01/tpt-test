using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainTest.Framework.Entity
{
    public interface ISecureEntity : IEntity
    {
        public int EntityType { get; }
    }
}
