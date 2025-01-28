using Project.Player;

namespace Project.Ability
{
	public class RegenerationAbility : BaseAbility
	{
		private int _duration = 0;
		private int _healPower = 0;

		public override void Init(IPlayer ownerPlayer, IPlayer enemyPlayer)
		{
			_affectedPlayer = ownerPlayer;
		}

		public override void Execute()
		{
			if (_currentCooldown == 0)
			{
				_healPower = _abilityData.BasicPower + _affectedPlayer.GetStats.Magic;
				_affectedPlayer.UpdateHP(_healPower);

				_currentCooldown = _abilityData.Cooldown;
				_duration = _abilityData.Durability;
			}
		}

		public override void DecreaseCooldown()
		{
			if (_duration == 0)
			{
				base.DecreaseCooldown();
			}
			else
			{
				_affectedPlayer.UpdateHP(_healPower);
				_duration--;
			}
		}

		public override void Reset()
		{
			_duration = 0;
			_healPower = 0;

			base.Reset();
		}
	}
}