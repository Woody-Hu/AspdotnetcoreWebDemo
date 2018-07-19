using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Utility;
using WebDemo.Config;

namespace WebDemo.DAO
{
    [AutoMongoDBContextAttribue(IfAsThisType = true)]
    public class MongoDBContext : BaseMongoDBContext
    {
        protected override void Prepare(ref string useContectionString)
        {
            MongoDBConfig tempConfig = ConfigPacker.GetConfigPacker().GetConfig<MongoDBConfig>();
            useContectionString = tempConfig.ConnectStr;
        }
    }
}
