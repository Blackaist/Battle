using Project.Player;
using UnityEngine;

namespace Project.Ability
{
	public class AttackAbility : BaseAbility
	{
		public override void Init(IPlayer ownerPlayer, IPlayer enemyPlayer)
		{
			_affectedPlayer = enemyPlayer;
			_otherPlayer = ownerPlayer;
		}

		public override void Execute()
		{
			if (_currentCooldown == 0)
			{
				var damage = Mathf.Max(0, _abilityData.BasicPower + _otherPlayer.GetStats.Attack - _affectedPlayer.GetStats.Def);
				_affectedPlayer.UpdateHP(-damage);

				_currentCooldown = _abilityData.Cooldown;
			}
		}
	}
}
