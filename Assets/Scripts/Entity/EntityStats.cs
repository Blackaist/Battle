using System;
using UnityEngine;

[Serializable]
public struct EntityStats
{
	[SerializeField] private int _hp;
	[SerializeField] private int _attack;
	[SerializeField] private int _magic;
	[SerializeField] private int _def;

	public readonly int HP => _hp;
	public readonly int Attack => _attack;
	public readonly int Magic => _magic;
	public readonly int Def => _def;
}
