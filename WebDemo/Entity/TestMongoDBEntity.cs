using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Entity
{

    public class TestMongoDBEntity: DefaultEntity
    {
        public string Name { set; get; }

        public int Value { set; get; }
    }
}
