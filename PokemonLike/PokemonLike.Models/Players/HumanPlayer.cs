using PokemonLike.Models.Items;
using PokemonLike.Models.Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonLike.Models.Players
{
    public class HumanPlayer : BasePlayer
    {
        private IUserInputManager _inputManager;

        public HumanPlayer(string name, IUserInputManager inputManager, IDisplayManager displayManager) : base(name, displayManager)
        {
            _inputManager = inputManager;
        }

        public override void StartTurn(BasePlayer targetPlayer)
        {
            base.StartTurn(targetPlayer);
            if (CurrentPokemon == null)
            {
                DisplayManager.DisplayMessage($"{Name} is out of pokemon. {Name} blacked out");
                return;
            }

            var userChoice = _inputManager.GetUserSelection($"It is {Name}'s turn. What would you like to do?\n" +
                    $"\n\t1. Fight" +
                    $"\n\t2. Pokemon" +
                    $"\n\t3. Bag" +
                    $"\n\t4. Run", 1, 4);

            IList<INamed> emptyItemList = new List<INamed>();

            switch (userChoice)
            {
                case 1: // fight
                    {
                        var choice = MakeSelection(CurrentPokemon.MoveList, $"What do you want {CurrentPokemon.Name} to do?");
                        if (choice <= 0 || choice >= CurrentPokemon.MoveList.Count + 1)
                        {
                            // Cancel
                            StartTurn(targetPlayer);
                        }
                        else
                        {
                            CurrentPokemon.MoveList[choice - 1].Perform(CurrentPokemon, targetPlayer.CurrentPokemon);
                        }
                        break;
                    }
                case 2: // pokemon
                    {
                        var choice = MakeSelection(Party, $"Which pokemon would you swap in?");
                        if (choice <= 0 || choice >= Party?.Count + 1)
                        {
                            StartTurn(targetPlayer);
                        }
                        else
                        {
                            var swap = Party[choice - 1];
                            if (swap == CurrentPokemon)
                            {
                                DisplayManager.DisplayMessage($"{CurrentPokemon.Name} is already out!");
                                StartTurn(targetPlayer);
                            }
                            else
                            {
                                DisplayManager.DisplayMessage($"{CurrentPokemon.Name}, come back! Go {swap.Name}!");
                                Party.Remove(swap);
                                Party.Insert(0, swap);
                            }
                        }
                        break;
                    }
                case 3: // bag
                    {

                    }
                    MakeSelection(emptyItemList, "Bag is empty");
                    // only option sshould be Cancel

                    break;
                case 4: // run
                    //if in a wild encounter, try to escape

                    // if ina trainer battle, this is'nt allowed
                    // for now force the 2nd ^path
                    MakeSelection(emptyItemList, "Cannot run from a Trainer Battle.");
                    break;
                default: // should b unreachable
                    break;
            }
        }

        private int MakeSelection<T>(IList<T>? collection, string prompt) where T : INamed
        {
            if (collection == null)
            {
                DisplayManager.DisplayMessage("Collection is null");
                return -1;
            }
            var sb = new StringBuilder();
            var index = 0;
            foreach (var item in collection)
            {
                sb.AppendLine($"{++index}. {item.Name}");
            }
            sb.AppendLine($"{++index}. Cancel");
            return _inputManager.GetUserSelection($"{prompt} \n{sb}", 1, index);
        }
    } 
}
