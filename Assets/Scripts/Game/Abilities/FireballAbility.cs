using Project.Effect;
using Project.Player;

namespace Project.Ability
{
	public class FireballAbility : BaseAbility
	{
		private BurningEffect _burningEffect = new();

		public override void Init(IPlayer ownerPlayer, IPlayer enemyPlayer)
		{
			_affectedPlayer = enemyPlayer;
		}

		public override void Execute()
		{
			if (_currentCooldown == 0)
			{
				_affectedPlayer.UpdateHP(-_abilityData.BasicPower);

				if (_abilityData is AbilityEffectData data)
				{
					_burningEffect.Bind(data.Effect);
					_burningEffect.Init(_otherPlayer, _affectedPlayer);
					_burningEffect.AddEffect();
				}

				_currentCooldown = _abilityData.Cooldown;
			}
		}

		public override void DecreaseCooldown()
		{
			if (_burningEffect.Durability == 0)
			{
				base.DecreaseCooldown();
			}
		}
	}
}