using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLike.Utilities
{
    public class Calculator
    {
        public Calculator()
        {
        }

        /// <summary>
        /// Calculate if next random double is within the percentage threshold
        /// </summary>
        /// <param name="chancePercentage">Percentage to check. Example, 6.25% will be 6.25, not 0.0625</param>
        /// <returns>true if within range, false otherwise</returns>
        public static bool RngIsWithinRange(double chancePercentage)
        {
            if (chancePercentage <= 0)
                return false;
            if (chancePercentage >= 100d)
                return true;

            return new Random(DateTime.UtcNow.Millisecond).Next() < (chancePercentage / 100d);
        }

        public static int Round(double value)
        {
            return (int)(value + 0.5);
        }

        /// <summary>
        /// Calculates the number of Hit Points that the target will lose after all caculations are made
        /// </summary>
        /// <param name="aLevel">level numer of the attacking pokemon</param>
        /// <param name="aPower">base power of the move</param>
        /// <param name="aAttack">attack or special attack stat of the attacking pokemon</param>
        /// <param name="dDefense">defense or special defense of the defending pokemon</param>
        /// <param name="numberOfTargets">The number of targets that this move will be applied to</param>
        /// <param name="isCritical">Did this move crit? 1.5 modifier if true, 1 if false</param>
        /// <param name="isSTAB">Is move going to get the same Type Attack bonus aplied</param>
        /// <param name="applyBurnModifier">If attacking pokemon is burned, physical attacks get a 0.5 modifier</param>
        /// <param name="typeEffectiveness">Optional type effectiveness condition used to add conditon modifiers to the calculation</param>
        /// <param name="weatherCondition">Optional weather condition used to add conditon modifiers to the calculation</param>
        /// <param name="other"></param>
        /// <returns>the number of Hit Points that the target will lose after all caculations are made</returns>
        public static double CalculateDamage(
            int aLevel,
            double aPower,
            double aAttack,
            double dDefense,
            int numberOfTargets,
            bool isCritical,
            bool isSTAB,
            bool applyBurnModifier = false,
            TypeEffectiveness typeEffectiveness = TypeEffectiveness.Neutral,
            Weather weatherCondition = Weather.Normal,
            double other = 1d)
        {

            var typeEffectivenessModifier = 1d;
            switch (typeEffectiveness)
            {
                case TypeEffectiveness.Immune:
                    return 0;
                case TypeEffectiveness.DoubleResistant:
                    typeEffectivenessModifier = 0.25;
                    break;
                case TypeEffectiveness.Resistant:
                    typeEffectivenessModifier = 0.5;
                    break;
                case TypeEffectiveness.Weak:
                    typeEffectivenessModifier = 2d;
                    break;
                case TypeEffectiveness.DoubleWeak:
                    typeEffectivenessModifier = 4d;
                    break;
                default: // neutral
                    break;
            }


            var levelModifier = ((2d * aLevel) / 5d) + 2;
            var moveDamage = ((levelModifier * aPower * (aAttack / dDefense)) / 50d) + 2d;
            // targets effectiveness is 1 if single target, 0.75 if 
            var effectiveness = numberOfTargets > 1 ? 0.75 : 1d;
            var critModifier = isCritical ? 1.5 : 1d;
            var stabModifier = isSTAB ? 1.5 : 1d;
            var burnModifier = applyBurnModifier ? 0.5 : 1d;

            var rng = new Random().Next(85, 100) / 100d;

            return moveDamage * effectiveness * critModifier * stabModifier * rng * burnModifier * other * typeEffectivenessModifier;
        }
    }


    public enum Weather
    {
        Normal,
        Rain,
        HarshSunLight,
        Hail
    }

    /// <summary>
    /// Damage modifier for Resistances and Weaknesses
    /// </summary>
    public enum TypeEffectiveness
    {
        Immune,
        DoubleResistant,
        Resistant,
        Neutral,
        Weak,
        DoubleWeak
    }
}
