using System;
using System.Text;
using PokemonLike.Models.Monsters;

namespace PokemonLike.Models.Players
{
    public abstract class BasePlayer
    {

        protected IDisplayManager DisplayManager;
        public string Name { get; set; }

        public BasePlayer(string name, IDisplayManager displayManager) 
        { 
            Name = name; 
            DisplayManager = displayManager;
        }

        public virtual IList<BattleMonster>? Party { get; set; }

        public Monsters.BattleMonster? CurrentPokemon => Party?.Where(p => p.HP > 0)?.FirstOrDefault();

        public virtual string ListParty()
        {
            if (Party == null)
                return $"{Name} is out of Battle Monsters";
            
            var sb = new StringBuilder();
            foreach (var monster in Party)
            {
                sb.AppendLine($"{monster.Name} (Lv. {monster.Level})");
            }
            return sb.ToString();
        }

        public virtual ITurnResult? StartTurn(BasePlayer targetPlayer)
        {
            DisplayManager.DisplayBattleState(this, targetPlayer);
            return TurnResult.Default;
        }
    }
}
