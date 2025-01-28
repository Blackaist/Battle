using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
	public class EffectViewItem : MonoBehaviour
	{
		[SerializeField] private Image _image = null;
		[SerializeField] private Text _durability = null;

		public string Name { get; private set; }

		public EffectViewItem SetSprite(Sprite sprite)
		{
			_image.sprite = sprite;
			return this;
		}

		public EffectViewItem SetColor(Color color)
		{
			_image.color = color;
			return this;
		}

		public EffectViewItem SetDurability(int durability)
		{
			_durability.text = durability.ToString();
			return this;
		}

		public EffectViewItem SetName(string name)
		{
			Name = name;
			return this;
		}
	}
}
