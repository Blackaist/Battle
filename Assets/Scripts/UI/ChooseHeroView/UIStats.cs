using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
	public class UIStats : MonoBehaviour
	{
		[SerializeField] private Text _hpCount = null;
		[SerializeField] private Text _attackCount = null;
		[SerializeField] private Text _magicCount = null;
		[SerializeField] private Text _defCount = null;

		public void Setup(EntityStats stats)
		{
			_hpCount.text = stats.HP.ToString();
			_attackCount.text = stats.Attack.ToString();
			_magicCount.text = stats.Magic.ToString();
			_defCount.text = stats.Def.ToString();
		}
	}
}
