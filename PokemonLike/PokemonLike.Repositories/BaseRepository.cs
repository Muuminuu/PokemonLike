using System;
using PokemonLike.Models;
using PokemonLike.Models.Moves;
using PokemonLike.Utilities;

namespace PokemonLike.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IIdentifiable
    {
        protected abstract IList<T> Items {  get; }

        public T? Get(int id)
        {
            var item = Items.Where(m => m.Id == id).FirstOrDefault();
            return item.Clone();
        }

        public IList<T> GetAll()
        {
            return Items;
        }

        public int Save(T item, bool allowUpdate = true)
        {
            var existing = Get(item.Id);
            if (item.Id > 0 && existing != null)
            {
                // we have a match, item is existing
                if (allowUpdate)
                {
                    // update existing item
                    Items[Items.IndexOf(existing)] = item;
                    return item.Id;
                }
                return -1;
            }
            // add new item
            item.Id = Items.OrderBy(m => m.Id).Last().Id + 1;
            Items.Add(item);
            return item.Id;
        }

        public bool Delete(T item)
            => Delete(item.Id);

        public bool Delete(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
                return Items.Remove(item);
            return false;
        }
    }
}
