using Project.Player;

namespace Project.Effect
{
	public interface IEffect
	{
		int Durability { get; }

		EffectData EffectData { get; }

		void Bind(EffectData effectData);
		void Init(IPlayer ownerPlayer, IPlayer enemyPlayer);
		void OnStartTurn();
		void OnEndTurn();

		void RestartDurability();

		void AddEffect();
		void DestroyEffect();
	}
}
