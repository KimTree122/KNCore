using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Comm.DataSwitch
{
    public class JSonHelper
    {
        public static List<T> JsonToList<T>(string jsonlist)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(jsonlist);
            }
            catch (Exception)
            {
                return default(List<T>);
            }
        }

        public static T JsonToObject<T>(string jsonobject)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonobject);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
