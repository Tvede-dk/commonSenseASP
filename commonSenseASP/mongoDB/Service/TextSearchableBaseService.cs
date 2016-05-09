using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace commonSenseASP.mongoDB.Service {
    public abstract class TextSearchableBaseService<T> : BaseService<T> where T : BaseModel {
        public Task<List<T>> GetPaginatedTextSearch(string textSearch, int page, int limit) {
            var filter = GetTextSearchFilter(textSearch);
            return dataCollection.Find(filter).Skip(calculateCurrentPage(page, limit)).Limit(limit).ToListAsync();
        }

        public Task<long> GetPagniatedTextSearchCount(string textSearch, int page, int limit) {
            var filter = GetTextSearchFilter(textSearch);
            return dataCollection.Find(filter).CountAsync();
        }


        public abstract Expression<Func<T, bool>> GetTextSearchFilter(string textSearch);
    }
}
