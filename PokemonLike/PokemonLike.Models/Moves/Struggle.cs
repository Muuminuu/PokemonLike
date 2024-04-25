using System;

namespace PokemonLike.Models.Moves
{
    public  class Struggle : BattleMove
    {
        public Struggle() : base(
            id: 1,
            name: "Struggle", 
            type: MonsterType.Normal, 
            maxPP: -1,
            description: "Default move when a battle Monster is out of moves. Inflicts damage to the target as well as the user.",
            power: 10, 
            accuracy: 100) 
        {
        }
    }
}
