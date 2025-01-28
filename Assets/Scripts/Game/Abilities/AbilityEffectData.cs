using System;
using UnityEngine;

namespace Project
{
    [Serializable]
    [CreateAssetMenu(fileName = "Assets/Scriptable Objects/Ability/Default name", menuName = "Project/Create Ability with Effect")]
    public class AbilityEffectData : AbilityData
	{
		[SerializeField] private EffectData _effect = null;

		public EffectData Effect => _effect;
	}
}