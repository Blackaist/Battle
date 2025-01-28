using System;
using UnityEngine;

namespace Project
{
	[CreateAssetMenu(fileName = "Assets/Scriptable Objects/Entity/Default name", menuName = "Project/Create Entity")]
	public class EntityObject : ScriptableObject, IEquatable<EntityObject>
	{
		[SerializeField] private string _name = "";
		[SerializeField] private Color _entityColor = Color.white;
		[SerializeField] private Sprite _entityModel = null;
		[SerializeField] private EntityStats _stats;

		public string Name => _name;
		public Color Color => _entityColor;
		public EntityStats Stats => _stats;
		public Sprite Model => _entityModel;

		public bool Equals(EntityObject other)
		{
			return Equals(_name, other._name) && Equals(_stats, other._stats);
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			_name = name;
		}
#endif
	}
}
