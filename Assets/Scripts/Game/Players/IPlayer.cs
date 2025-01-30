using Project.Ability;
using Project.Effect;
using System;
using System.Collections.Generic;

namespace Project.Player
{
	public interface IPlayer
	{
		event Action<IAbility> AbilityExecuted;
		event Action<IEffect> EffectAdded;
		event Action HPUpdated;
		event Action ShieldUpdated;

		EntityStats GetStats { get; }
		public int CurrentHP { get; }
		public int CurrentShield { get; }

		void Init(List<IAbility> abilities, EntityObject entity);

		void ExecuteAbility(string abilityName);

		void UpdateHP(int count, bool ignoreShield = false);
		void UpdateShield(int count);

		List<IEffect> GetEffects();
		void AddEffect(IEffect effect);
		void RemoveEffect(IEffect effect);

		void StartTurn();
		void EndTurn();
	}
}
