using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Unity.Common.ObjectPool
{
    public interface IPoolable
    {
        void Create();
        void Destroy();
    }
}
