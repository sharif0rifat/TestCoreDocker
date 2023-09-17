using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestCoreDockerService.Helper
{
    public static class ObjectExtension
    {
        public static bool IsNotNull(this object  obj)
            {
            if (obj == null)
                return false;
            else
                return true;
        }
    }
}
