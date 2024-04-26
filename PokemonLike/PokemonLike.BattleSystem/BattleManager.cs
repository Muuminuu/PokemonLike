using System;
using PokemonLike.BattleSystem.Exceptions;
using PokemonLike.Models;
using PokemonLike.Models.Players;

namespace PokemonLike.BattleSystem
{
    public class BattleManager
    {        
        public void StartBattle(BasePlayer playerOne, BasePlayer playerTwo, Action<ITurnResult?>? onTurnResult = null)
        {
            var playerOneMonster = playerOne.CurrentPokemon;
            var playerTwoMonster = playerTwo.CurrentPokemon;

            if (playerOneMonster == null || playerTwoMonster == null)
            {
                // we can't battle*
                throw new BattleStateException("A player's party does not have a valid Battle Monster");
            }

            // Check speed of each BattleMonster
            var startingPlayer = playerTwo;
            var otherPlayer = playerOne;

            if (playerOneMonster.Speed >= playerTwoMonster.Speed) 
            {
                startingPlayer = playerOne;
                otherPlayer = playerTwo;
            }
            do
            {

                var result = startingPlayer.StartTurn(otherPlayer);

                result?.Apply();
                onTurnResult?.Invoke(result);

                var tmp = startingPlayer;
                startingPlayer = otherPlayer;
                otherPlayer = tmp;
            }
            while (startingPlayer.CurrentPokemon != null && otherPlayer.CurrentPokemon != null);
            

        }
    }
}
