using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLike.Utilities
{
    public static class ObjectExtensions
    {
        public static T? Clone<T>(this T item)
            {
                var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);
            }
    }
}
