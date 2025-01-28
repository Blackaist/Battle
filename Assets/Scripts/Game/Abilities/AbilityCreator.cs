using System.Collections.Generic;

namespace Project.Ability
{
	public class AbilityCreator
	{
		public static List<IAbility> Create(IReadOnlyCollection<AbilityData> abilityDatas)
		{
			var abilityList = new List<IAbility>();

			foreach(var abilityData in abilityDatas)
			{
				switch(abilityData.Name)
				{
					case "Attack": abilityList.Add(Bind(new AttackAbility(), abilityData)); break;
					case "Barrier": abilityList.Add(Bind(new BarrierAbility(), abilityData)); break;
					case "Fireball": abilityList.Add(Bind(new FireballAbility(), abilityData)); break;
					case "Purify": abilityList.Add(Bind(new PurifyAbility(), abilityData)); break;
					case "Regeneration": abilityList.Add(Bind(new RegenerationAbility(), abilityData)); break;
				}
			}

			return abilityList;
		}

		private static IAbility Bind(IAbility ability, AbilityData data)
		{
			ability.Bind(data);
			return ability;
		}
	}
}
