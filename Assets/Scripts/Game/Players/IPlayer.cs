using Project.Ability;
using Project.Effect;
using System;
using System.Collections.Generic;

namespace Project.Player
{
	public interface IPlayer
	{
		EntityStats GetStats { get; }
		public int CurrentHP { get; }
		public int CurrentShield { get; }

		Action<IAbility> OnAbilityExecuted { get; set; }

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
