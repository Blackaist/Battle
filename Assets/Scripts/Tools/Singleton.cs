using UnityEngine;

namespace Project
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance { get; private set; }

		protected void Awake()
		{
			if (Instance == null)
			{
				Instance = GetComponent<T>();
			}
			else
			{
				Destroy(gameObject);
			}
		}

		protected void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
	}

	public class SingletonScene<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance { get; private set; }

		protected void Awake()
		{
			if (Instance == null)
			{
				Instance = GetComponent<T>();
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		protected void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
	}

}
