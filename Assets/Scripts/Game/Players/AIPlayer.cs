using System.Collections;
using System.Linq;
using UnityEngine;

namespace Project.Player
{
	public class AIPlayer : BasePlayer
	{
		public override void StartTurn()
		{
			base.StartTurn();

			StartCoroutine(OnProccess());
		}

		private IEnumerator OnProccess()
		{
			yield return new WaitForSeconds(1.0f);
			ExecuteAbility();
		}

		public override void ExecuteAbility(string abilityName = "")
		{
			var random = new System.Random();

			var abilities = _abilities.Where(a => a.IsReady).ToList();

			var abilityIndex = random.Next(0, abilities.Count);

			abilities[abilityIndex].Execute();
			OnAbilityExecuted?.Invoke(abilities[abilityIndex]);
		}
	}
}
