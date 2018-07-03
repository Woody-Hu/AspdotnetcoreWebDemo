using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.DAO
{
    [AutoMongoDBContextAttribue(IfAsThisType = true)]
    public class TestMongoDBContext : BaseMongoDBContext
    {
        protected override void Prepare(ref string useContectionString)
        {
            useContectionString = "mongodb://localhost:27017";
        }
    }
}
