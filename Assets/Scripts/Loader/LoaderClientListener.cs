using Project;
using UnityEngine;

public class LoaderClientListener : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader = null;

	public void Start()
	{
		_sceneLoader.AddTask();

		ClientManager.Instance.OnLoaded += OnAssetsLoaded;
	}

	private void OnAssetsLoaded()
	{
		ClientManager.Instance.OnLoaded -= OnAssetsLoaded;

		_sceneLoader.RemoveTask();
	}
}
