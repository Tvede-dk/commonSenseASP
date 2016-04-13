using System;
using System.Collections.Generic;
using System.Text;

public static class DictonaryUtil {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    /// <param name="dict"></param>
    /// <param name="lookup"></param>
    /// <returns></returns>
    public static T GetSafe<T, K>(this Dictionary<K, T> dict, K lookup) {

        if (dict != null && lookup != null) {
            if (dict.ContainsKey(lookup)) {
                return dict[lookup];
            }
        }
        return default(T);
    }
}