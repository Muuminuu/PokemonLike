using System;
using PokemonLike.Models.Monsters;
using PokemonLike.Models.Moves;


namespace PokemonLike.Repositories
{
    // https://pokemondb.net/pokedex/{pokemon}
    public class BattleMonsterRepository : BaseRepository<BattleMonster>
    {
        protected override IList<BattleMonster> Items { get; } = new List<BattleMonster>
        { 
            new BattleMonster(1, "Bulbasaur", Models.MonsterType.Grass, 45, 49, 49, 45),
            new BattleMonster(4, "Charmander", Models.MonsterType.Fire, 39, 52, 43, 65),
            new BattleMonster(7, "Squirtle", Models.MonsterType.Water, 44, 48, 65, 43)
        };
    }
}
