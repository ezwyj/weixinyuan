using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Util
{
    public class Serializer
    {
        public static string ToJson<T>(T obj)
        {
            

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj); 
        }

        public static T ToObject<T>(string json)
        {
           
            T obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
