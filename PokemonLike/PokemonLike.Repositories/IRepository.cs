using PokemonLike.Models;
using System;

namespace PokemonLike.Repositories
{
    public interface IRepository<T> where T : class, IIdentifiable
    {
        T? Get(int id);

        IList<T> GetAll();

        /// <summary>
        /// save an item by create or update if allowUpdate is true. Otherwise it will return -1 if the item already exists. 
        /// </summary>
        /// ^<param name="item"></param>
        /// <param name="allowUpdate"></param>
        /// <returns></returns>



        int Save(T item, bool allowUpdate = true);

        bool Delete(T item);
        bool Delete(int id);
    }
}
