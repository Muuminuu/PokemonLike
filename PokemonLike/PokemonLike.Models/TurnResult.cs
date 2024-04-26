using PokemonLike.Models.Monsters;
using PokemonLike.Models.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLike.Models
{
    public abstract class TurnResult : ITurnResult
    {
        //todo : Enforce property assignement through constructors.
        public TurnResult() 
        { 
        }

        public abstract void Apply();
        public virtual bool Validate()
        {
            return true;
        }

        public static TurnResult? Default { get; } = default;
        public BattleMonster? Pokemon { get; set; }
        public TurnAction Action { get; set; }
    }

    public class AttackTurnResult : TurnResult
    {
        public BattleMove? Value { get; set; }
        public BattleMonster? Target { get; set; }
        public StatusEffect AppliedStatusCondition { get; set; }
        public int Damage { get; private set; }

        public override void Apply()
        {
            if (Target == null)
                throw new Exception($"Attempted to attack null pokemon {nameof(Target)}.");
            Damage = Convert.ToInt32(CalculateDamage());
            Target.TakeDamage(Damage);
            Target.ApplyStatus(AppliedStatusCondition);
        }

        
        private double CalculateDamage()
        {
            if (Pokemon == null)
                throw new Exception(nameof(Pokemon) + " is null, can't attack");
            if (Target == null)
                throw new Exception($"Attempted to attack null pokemon {nameof(Target)}.");
            if (Value == null)
                throw new Exception($"Attempted to operate on a null {nameof(Value)}.");

            var isCrit = Utilities.Calculator.RngIsWithinRange(6.25);
            return Utilities.Calculator.Round(Utilities.Calculator.CalculateDamage(
            aLevel: Pokemon.Level,
            aPower: Value.Power,
            aAttack: Pokemon.Attack,
            dDefense: Target.Defense,
            numberOfTargets: 1,
            isCritical: isCrit,
            isSTAB: Pokemon.Type == Value.Type,
            // when we have non physical attacks, this needs to handle that. burn modifier only applies to physical attacks ?????
            applyBurnModifier: Pokemon?.ActiveStatus == StatusEffect.Burn
            ));
        }
        
    }

    public class SwapTurnResult : TurnResult
    {
        public IList<Monsters.BattleMonster>? Party { get; set; }
        public override void Apply()
        {
            if (Pokemon == null)
                throw new Exception("Attempted to swap with a null target Pokemon.");
            if (Party == null)
                throw new Exception("Attempted to swap from a null Party");
            if (!Party.Contains(Pokemon))
                throw new InvalidOperationException("Attempting to swap Pokemon that doesn't exist in Party");

            Party.Remove(Pokemon);
            Party.Insert(0, Pokemon);
        }
    }

    public interface ITurnResult
    {
        void Apply();
    }

    public enum TurnAction
    {
        ApplyDamage,
        SwapPokemon
    }
}
