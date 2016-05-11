using commonSenseASP.Patterns;
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
        public virtual Expected<Task<List<T>>> GetPaginated(int page, int limit) {
            //var lastTimeStamp = lastSeenObject.Timestamp;
            return Expected<Task<List<T>>>.Success(dataCollection.Find(x => true).Skip(calculateCurrentPage(page, limit)).Limit(limit).ToListAsync());
        }

        public int calculateCurrentPage(int page , int limit) {
            return (page - 1) * limit;
        }

        public virtual Expected<Task<long>> GetCount() {
            return Expected<Task<long>>.Success(dataCollection.CountAsync(x => true));
        }

        public virtual Expected<Task<ReplaceOneResult>> TryUpdate(T toInsert) {
            if (toInsert == null) {
                return Expected<Task<ReplaceOneResult>>.Failed(new InvalidOperationException("Object to insert is null"));
            }
            return Expected<Task<ReplaceOneResult>>.Success(dataCollection.ReplaceOneAsync((document => document.id == toInsert.id), toInsert));
        }

        public virtual Expected<Task> TryCreate(T toInsert) {
            if (toInsert == null) {
                return Expected<Task>.Failed(new InvalidOperationException("Object to create is null"));
            }
            return Expected<Task>.Success(dataCollection.InsertOneAsync(toInsert));
        }

        public virtual Expected<Task<T>> TryDelete(T toDelete) {
            if (toDelete == null) {
                return Expected<Task<T>>.Failed(new InvalidOperationException("Object to delete is null"));
            }
            return TryDelete(toDelete.id);
        }

        public virtual Expected<Task<T>> TryDelete(ObjectId id) {
            if (id == ObjectId.Empty) {
                return Expected<Task<T>>.Failed(new InvalidOperationException("Object to delete is empty"));
            }
            return Expected<Task<T>>.Success(dataCollection.FindOneAndDeleteAsync((document => document.id == id)));
        }


        public virtual Expected<Task<T>> FindById(ObjectId id) {
            var result = dataCollection.Find(document => document.id == id);
            return Expected<Task<T>>.Success(result.FirstOrDefaultAsync());
        }

        public virtual Expected<Task<T>> FindById(string id) {
            ObjectId objId;
            if (ObjectId.TryParse(id, out objId)) {
                return FindById(objId);
            } else {
                return Expected<Task<T>>.Failed(new InvalidOperationException($"the supplied id {id} is invalid"));
            }
        }
    }
}
