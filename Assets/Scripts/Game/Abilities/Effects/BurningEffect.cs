using Project.Player;

namespace Project.Effect
{
	public class BurningEffect : NegativeEffect
	{
		public override void Init(IPlayer ownerPlayer, IPlayer enemyPlayer)
		{
			_affectedPlayer = enemyPlayer;
		}

		public override void OnStartTurn()
		{
			if (_currentDurability > 0)
			{
				_affectedPlayer.UpdateHP(-_effectData.BasicPower);
				_currentDurability--;
			}

			if (_currentDurability == 0)
			{
				DestroyEffect();
			}
		}

		public override void OnEndTurn()
		{

		}

		public override void AddEffect()
		{
			_affectedPlayer.AddEffect(this);
		}

		public override void DestroyEffect()
		{
			_currentDurability = 0;
		}
	}

}
