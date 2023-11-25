using Microsoft.AspNetCore.Server.IIS.Core;

namespace ikem23_wapi
{
    public static class Globals
    {
        public readonly static string FHIRServerUri = "https://fhir.mfiuni6ctmu7.workload-nonprod-fhiraas.isccloud.io";
        public readonly static string FHIRServerApiKey = "W1ZFtBlnwAarkA8KGhPLd1ozRxp7jzD13hPtosS4";
    }

    public static class Extensions
    {
        public static TVal Get<TKey, TVal>(this Dictionary<TKey, TVal> dic, TKey key)
        {
            if(dic == null) return default(TVal);
            else if(!dic.ContainsKey(key)) return default(TVal);
            else return dic[key];
        }
    }
}
