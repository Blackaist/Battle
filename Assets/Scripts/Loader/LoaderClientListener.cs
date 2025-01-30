using Project;
using UnityEngine;

public class LoaderClientListener : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader = null;

	public void Start()
	{
		_sceneLoader.AddTask();

		ClientManager.Instance.Loaded += OnAssetsLoaded;
	}

	private void OnAssetsLoaded()
	{
		ClientManager.Instance.Loaded -= OnAssetsLoaded;

		_sceneLoader.RemoveTask();
	}
}
