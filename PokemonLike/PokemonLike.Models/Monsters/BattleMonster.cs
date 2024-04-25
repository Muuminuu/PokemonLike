using System;
using System.Text;
using PokemonLike.Models.Moves;

namespace PokemonLike.Models.Monsters
{
    public class BattleMonster : IIdentifiable, INamed
    {
        /// <summary>
        /// Pokemon id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// CurrentLevel
        /// </summary>
        public int Level { get; set; } = 1;

        /// <summary>
        /// Type of the pokemon
        /// </summary>
        public MonsterType Type { get; set; }

        /// <summary>
        /// It's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Max HealthPoint
        /// </summary>
        public int HP { get; set; }

        /// <summary>
        /// Actual HP
        /// </summary>
        public int CurrentHP { get; set; }

        /// <summary>
        /// Attack power Physical & Special
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// Defense power Physical & Special
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// Percent chance of landing a critical hit. Default is 6.25%.
        /// </summary>
        public double CritChance { get; set; }

        /// <summary>
        /// Determine initiative (who's starting the battle)
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Up to 4 moves that the monster knows
        /// </summary>
        ///-- TODO: Add controls/restrictions for learning moves.
        ///-- NOTE: when learning a 5th move, it actually needs to replace on of the existing 4 moves.
        public virtual IList<BattleMove> MoveList { get; private set; }
        public StatusEffect ActiveStatus { get; private set; }

        public BattleMonster(int id, string name, MonsterType type, int hP, int attack, int defense, int speed)
        {
            Id = id;
            Type = type;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            HP = hP;
            CurrentHP = hP;
            Attack = attack;
            Defense = defense;
            Speed = speed;
            
            MoveList = new List<BattleMove> { };
        }

        public string ListMoves()
        {
            if (MoveList == null)
                throw new Exception("Move List is NULL. This shouldn't happen, ever");

            var sb = new StringBuilder();
            var index = 0;
            foreach (var move in MoveList)
            {
                sb.AppendLine($"{++index}. {move.Name}");
            }

            return sb.ToString();
        }

        public void TakeDamage(int dmg)
        {
            if (dmg > 0)
                CurrentHP -= dmg;
            if (CurrentHP <= 0)
            {
                HP = 0;
                //Fainted
                System.Diagnostics.Debug.WriteLine($"{Name} fainted");
            }

        }

        public void ApplyStatus(StatusEffect status)
        {
            if (status == StatusEffect.None)
            {
                System.Diagnostics.Debug.Write($"No status effect to apply.");
                return;
            }

            if (ActiveStatus != StatusEffect.None)
            {
                System.Diagnostics.Debug.Write($"{Name} already has an active status: {ActiveStatus}. Cannot apply new status {status}");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"{Name} has been {status} status now.");
            ActiveStatus = status;
        }
    }
}
