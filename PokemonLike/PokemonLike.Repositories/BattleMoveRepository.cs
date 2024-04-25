using System;
using PokemonLike.Models.Moves;

namespace PokemonLike.Repositories
{
    public class BattleMoveRepository : BaseRepository<BattleMove>
    {
        
        // https://pokemondb.net/move/{move}
        protected override IList<BattleMove> Items { get; } = new List<BattleMove>
        {
            new Struggle(),
            new BattleMove(2, "Tackle", Models.MonsterType.Normal, 45, "A physical attack in which the user charges, full body, into the foe.", 40, 100),
            new BattleMove(3, "Growl", Models.MonsterType.Normal, 40, "The user growls in an endearing way, making the foe less wary. The target's Attack is lowered.", 0, 100),
            new BattleMove(4, "Scratch", Models.MonsterType.Normal, 35, "Hard, pointed, sharp claws rake the target to inflict damage.", 40, 100),
            new BattleMove(5, "Ember", Models.MonsterType.Fire, 25, "The target is attacked with small flames. this may also leave the target with a burn.", 40, 100, () =>
            {
                return Utilities.Calculator.RngIsWithinRange(10) ? StatusEffect.Burn : StatusEffect.None;
            }),
            new BattleMove(6, "Vine Whip", Models.MonsterType.Grass, 25, "The target is struck with slender, whiplike vines to inflict damage", 45, 100)
        };
    }
}
