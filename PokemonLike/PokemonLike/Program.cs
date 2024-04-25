using PokemonLike.Models;
using PokemonLike.Repositories;
using PokemonLike.Models.Monsters;
using PokemonLike.Models.Players;
using PokemonLike.BattleSystem.Exceptions;
using PokemonLike.Utilities;

namespace PokemonLike.Console;

public class Program
{
    public static void Main(string[] args)
    {
        var inputManager = new UserInputManager();
        var displayManager = new DisplayManager();
        var battleManager = new BattleSystem.BattleManager();

        // may have to change input and display in the right order, but error ??
        var playerOne = new HumanPlayer("Muuminuu", inputManager, displayManager);
        var playerTwo = new HumanPlayer("AI Player 2", inputManager, displayManager);

        var moveRepository = new BattleMoveRepository();
        var monsterRepository = new BattleMonsterRepository();

        var tackle = moveRepository.Get(2);
        var growl = moveRepository.Get(3);
        var scratch = moveRepository.Get(4);

        var bulbasaur = monsterRepository.Get(1);
        bulbasaur?.MoveList.Add(tackle);
        bulbasaur?.MoveList.Add(growl);
        bulbasaur?.MoveList.Add(moveRepository.Get(6));

        var charmander = monsterRepository.Get(4);
        charmander?.MoveList.Add(scratch);
        charmander?.MoveList.Add(growl);
        charmander?.MoveList.Add(moveRepository.Get(5));

        playerOne.Party = new List<BattleMonster>
        {
            bulbasaur,
            charmander,

        };

        playerTwo.Party = new List<BattleMonster>
        {
            charmander.Clone(),
            bulbasaur.Clone(),
        };

        try
        {
            displayManager.DisplayTitle("PokemonLike");
            displayManager.DisplayMessage($"Starting Battle between {playerOne.Name} and {playerTwo.Name}!");
            battleManager.StartBattle(playerOne, playerTwo);

            var loser = playerOne.CurrentPokemon == null ? playerOne : playerTwo;
            var winner = playerOne.CurrentPokemon == null ? playerTwo : playerOne;
            displayManager.DisplayMessage($"{loser.Name} is out of Pokemon. {winner.Name} has won the battle!");
        } 
        catch (BattleStateException e)
        {
            displayManager.DisplayMessage("Battle State Exception caught: " + e.Message);
        }

        System.Console.ReadLine();
    }
}