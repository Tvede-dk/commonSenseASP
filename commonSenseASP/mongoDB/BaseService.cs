using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commonSenseASP.mongoDB {
    public abstract class BaseService<T> where T : BaseModel {
         public abstract IMongoCollection<T> dataCollection { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastSeenObject"></param>
        /// <param name="limit"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public Task<List<T>> GetPaginated(int page, int limit) {
            //var lastTimeStamp = lastSeenObject.Timestamp;
            return dataCollection.Find(x => true).Skip((page - 1) * limit).Limit(limit).ToListAsync();
        }

        public Task<long> GetCount() {
            return dataCollection.CountAsync(x => true);
        }

        public Task<ReplaceOneResult> TryUpdate(T toInsert) {
            return dataCollection.ReplaceOneAsync((document => document.id == toInsert.id), toInsert);
        }

        public Task TryCreate(T toInsert) {
            return dataCollection.InsertOneAsync(toInsert);
        }

        public Task<T> TryDelete(T toDelete) {
            return dataCollection.FindOneAndDeleteAsync((document => document.id == toDelete.id));
        }

        public  Task<T> FindById(ObjectId id) {
            var result = dataCollection.Find(document => document.id == id);
            return result.FirstOrDefaultAsync();
        }

        public Task<T> FindById(string id) {
            ObjectId objId;
            if ( ObjectId.TryParse(id, out objId)) {
                return FindById(objId);
            }
            return Task.FromResult<T>(null);
        }
    }
}
