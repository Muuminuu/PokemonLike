using System;

namespace PokemonLike.Models.Moves
{
    public class BattleMove : IIdentifiable, INamed
    {
        public int Id { get; set; }
        public MonsterType Type { get; set; }
        public string Name { get; set; }
        public int MaxPP { get; set; }
        public int CurrentPP { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public int Accuracy { get; set; }
        public Func<StatusEffect>? StatusAction { get; private set; }

        public BattleMove(int id, string name, MonsterType type, int maxPP, string description, int power, int accuracy, Func<StatusEffect>? statusAction = null)
        {
            Id = id;
            Type = type;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MaxPP = maxPP;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Power = power;
            Accuracy = accuracy;
            statusAction = statusAction;
        }

        public void Perform(Monsters.BattleMonster user, Monsters.BattleMonster target)
        {
            System.Diagnostics.Debug.WriteLine("Performing " + Name + " on " + target.Name);
            var isCrit = Utilities.Calculator.RngIsWithinRange(6.25);
            var dmg = Utilities.Calculator.Round(Utilities.Calculator.CalculateDamage(
                aLevel: user.Level,
                aPower: Power,
                aAttack: user.Attack,
                dDefense: target.Defense,
                numberOfTargets: 1,
                isCritical: isCrit,
                isSTAB: user.Type == Type,
                // when we have non physical attacks, this needs to handle that. burn modifier only applies to physical attacks ?????
                applyBurnModifier: user.ActiveStatus == StatusEffect.Burn

                ));

            var status = StatusAction?.Invoke() ?? StatusEffect.None;
            target.ApplyStatus(status);

            System.Diagnostics.Debug.WriteLine($"{user.Name} inflicted {dmg} damage on {target.Name} using {Name}!");
            target.TakeDamage(dmg);
        }
    }
}
