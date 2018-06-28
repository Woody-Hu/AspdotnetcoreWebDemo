using AutoEFContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Entity
{
    [AutoEntity]
    public class User
    {
        public int Id { set; get; }

        public string Name { set; get; }
    }
}
