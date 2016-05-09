using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace commonSenseASP.mongoDB {
    [BsonIgnoreExtraElements]
    public abstract class BaseModel {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId id { get; set; }
    }
}
