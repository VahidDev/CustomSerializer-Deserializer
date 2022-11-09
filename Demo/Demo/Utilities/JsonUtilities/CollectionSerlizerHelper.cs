using System.Collections.Generic;
using AutoMapper;
using Demo.CustomJsonConverter.Serializer;

namespace Demo.Utilities.JsonUtilities
{
    public class CollectionSerlizerHelper
    {
        public static string SerializeCollection<T,D>(IList<T>list,IMapper mapper)
        {
            //T is Entity type parameter D is Dto
            string json = "";

            foreach (T item in list)
            {
                json += CustomSerialier.Serialize<D>(mapper.Map<D>(item));
                json += ',';
            }

            json = json.Remove(json.Length - 1, 1);

            return json;
        }
    }
}
