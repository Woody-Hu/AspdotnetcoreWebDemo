using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Utility
{
    [AttributeUsage(AttributeTargets.Parameter,AllowMultiple = false,Inherited = false)]
    public class CacheKeyAttribute:Attribute
    {

        
    }
}
