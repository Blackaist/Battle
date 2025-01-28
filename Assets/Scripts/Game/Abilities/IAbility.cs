using Project.Player;

namespace Project.Ability
{
	public interface IAbility
	{
		int Durability { get; }
		int Cooldown { get; }

		bool IsReady { get; }

		AbilityData AbilityData { get; }

		void Bind(AbilityData data);
		void Init(IPlayer ownerPlayer, IPlayer enemyPlayer);
		void Execute();
		void DecreaseCooldown();
		void Reset();
	}

}
