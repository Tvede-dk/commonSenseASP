using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commonSenseASP.mongoDB
{
    public abstract class BaseModel
    {
        public ObjectId id { get; set; }
    }
}
