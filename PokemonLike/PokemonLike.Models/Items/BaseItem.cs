using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLike.Models.Items
{
    public class BaseItem : IIdentifiable, INamed
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BaseItem(int id, string name) 
        {
            Id = id;
            Name = name;
        }

    }
}
