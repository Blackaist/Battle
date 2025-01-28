using System;
using UnityEngine;

namespace Project
{
	[Serializable]
	[CreateAssetMenu(fileName = "Assets/Scriptable Objects/Ability/Default name", menuName = "Project/Create Effect")]
	public class EffectData : ScriptableObject
	{
		[SerializeField] private string _name = "";
		[SerializeField] private int _basicPower = 1;
		[SerializeField] private int _durability = 1;
		[SerializeField] private Color _color = Color.white;

		public string Name => _name;
		public int BasicPower => _basicPower;
		public int Durability => _durability;
		public Color Color => _color;

#if UNITY_EDITOR
		private void OnValidate()
		{
			_name = name;
		}
#endif
	}

}
