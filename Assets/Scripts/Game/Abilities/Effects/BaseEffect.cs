using Project.Player;

namespace Project.Effect
{
	public abstract class BaseEffect : IEffect
	{
		protected EffectData _effectData = null;

		protected int _currentDurability = 0;

		protected IPlayer _affectedPlayer, _otherPlayer = null;

		public int Durability => _currentDurability;

		public EffectData EffectData => _effectData;

		public abstract void OnEndTurn();

		public abstract void OnStartTurn();

		public abstract void Init(IPlayer ownerPlayer, IPlayer enemyPlayer);

		public void Bind(EffectData effectData)
		{
			_effectData = effectData;
			_currentDurability = effectData.Durability;
		}

		public void RestartDurability()
		{
			_currentDurability = _effectData.Durability;
		}

		public abstract void AddEffect();
		public abstract void DestroyEffect();
	}

}
