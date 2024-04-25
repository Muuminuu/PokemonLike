using PokemonLike.Models;
using PokemonLike.Models.Monsters;
using PokemonLike.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLike.Console
{
    public class DisplayManager : IDisplayManager
    {
        public void DisplayTitle(string title)
        {
            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Green;

            System.Console.WriteLine("===================================================================\n\t\t\t"
                + title
                + "\n===================================================================");
            System.Console.ForegroundColor = currentColor;
        }

        public void DisplayMessage(string message) 
        {
            System.Console.WriteLine(message);
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public void DisplayBattleState(BasePlayer activePlayer, BasePlayer targetPlayer)
        {
            Clear();
            // ---------------------------------------------------
            //                      Game Title
            // ---------------------------------------------------
            //                                      Ai Player2
            //                              Charmander (Lv1)
            //                              [========= ] 25/30    
            // >> Muuminuu
            //      Bulbasaur (Lv 1)
            //      [=====     ] 13/26
            // ----------------------------------------------------
            DisplayTitle("PokemonLike Battle");
            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Green;
            DisplayMessage(string.Format(
                "{0,65}\n" +
                "{1,65}\n" +
                "{2,65}\n" +

                "{3}\n" +
                "{4}\n" +
                "{5}\n" +
                "===================================================================\n",
                targetPlayer.Name,
                $"{targetPlayer.CurrentPokemon?.Name} (Lv{targetPlayer.CurrentPokemon?.Level})",
                GetHealthBar(targetPlayer.CurrentPokemon),
                
                activePlayer.Name,
                $"{activePlayer.CurrentPokemon?.Name} (Lv{activePlayer.CurrentPokemon?.Level}",
                GetHealthBar(activePlayer.CurrentPokemon)));

            System.Console.ForegroundColor = currentColor;
        }

        private string GetHealthBar(BattleMonster? monster)
        {
            if (monster == null)
            {
                return "[          ] --/--";
            }
            // [==========] 30/30
            var hpPercent = (double)monster.CurrentHP / (double)monster.HP;
            var segments = (int)(10 * hpPercent);
            var sb = new StringBuilder("[");
            for (int i = 0; i < segments; i++) 
            {
                sb.Append("=");
            }
            for (int j = segments; j < 10; j++)
            {
                sb.Append(" ");
            }
            sb.Append($"] {monster.CurrentHP}/{monster.HP}");
            return sb.ToString();
        }
    }
}
