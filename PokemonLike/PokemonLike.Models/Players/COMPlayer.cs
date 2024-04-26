using System;

namespace PokemonLike.Models.Players
{
    public class COMPlayer : BasePlayer
    {
        public COMPlayer(string name, IDisplayManager displayManager) : base(name, displayManager)
        {

        }

        public override ITurnResult? StartTurn(BasePlayer targetPlayer)
        {
            base.StartTurn(targetPlayer);
            throw new NotImplementedException();
        }
    }
}
