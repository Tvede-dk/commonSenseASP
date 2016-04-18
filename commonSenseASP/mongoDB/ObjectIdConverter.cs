using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commonSenseASP.mongoDB {
    public class ObjectIdConverter : JsonConverter {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            serializer.Serialize(writer, value.ToString());

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return ObjectId.Parse(reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType) {
            return typeof(ObjectId).IsAssignableFrom(objectType);
            //return true;
        }


    }
}
