using PokemonLike.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLike.Models
{
    public interface IDisplayManager
    {
        void DisplayMessage(string message);
        void DisplayTitle(string title);
        void DisplayBattleState(BasePlayer activePlayer, BasePlayer targetPlayer);
    }
}
