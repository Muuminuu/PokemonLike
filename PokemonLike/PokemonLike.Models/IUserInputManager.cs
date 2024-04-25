using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLike.Models
{
    public interface IUserInputManager
    {
        int GetUserSelection(string prompt, int min, int max);
    }
}
