using Project.Player;
using UnityEngine;

namespace Project.Ability
{
	public class BarrierAbility : BaseAbility
	{
		private int _duration = 0;
		protected int _currentDefense = 0;

		public override void Init(IPlayer ownerPlayer, IPlayer enemyPlayer)
		{
			_affectedPlayer = ownerPlayer;
		}

		public override void Execute()
		{
			if (_currentCooldown == 0)
			{
				_currentDefense = _abilityData.BasicPower + _affectedPlayer.GetStats.Def;

				_affectedPlayer.UpdateShield(_currentDefense);

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
				if (_affectedPlayer.CurrentShield == 0)
				{
					_duration = 0;
				}
				else
				{
					_duration--;
				}

				if (_duration == 0)
				{
					var damageToShield = Mathf.Min(_affectedPlayer.CurrentShield, _currentDefense);
					_affectedPlayer.UpdateShield(-damageToShield);
					_currentDefense = 0;
				}
			}
		}

		public override void Reset()
		{
			_duration = 0;
			_currentDefense = 0;

			base.Reset();
		}
	}
}