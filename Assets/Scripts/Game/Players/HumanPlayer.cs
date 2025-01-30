using System;
using System.Linq;
using UnityEngine;

namespace Project.Player
{
	public class HumanPlayer : BasePlayer
	{
		public override void ExecuteAbility(string abilityName)
		{
			var ability = _abilities.FirstOrDefault(x => x.AbilityData.Name == abilityName) ?? null;
			if (ability != null)
			{
				ability.Execute();
				OnAbilityExecuted(ability);
			}
		}
	}
}
