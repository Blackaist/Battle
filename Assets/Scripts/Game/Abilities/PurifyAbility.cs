using Project.Effect;
using Project.Player;

namespace Project.Ability
{
	public class PurifyAbility : BaseAbility
	{
		public override void Init(IPlayer ownerPlayer, IPlayer enemyPlayer)
		{
			_affectedPlayer = ownerPlayer;
		}

		public override void Execute()
		{
			if (_currentCooldown == 0)
			{
				_affectedPlayer.GetEffects().RemoveAll(x =>
				{
					if (x is NegativeEffect)
					{
						x.DestroyEffect();
						return true;
					}
					return false;
				});
			}
		}
	}
}