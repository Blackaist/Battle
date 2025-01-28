using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int _sceneIndex = 1;
    [SerializeField] private FakeLoadingView _loadingView = null;

	private AsyncOperation _asyncOperation = null;

	private int _tasksCount = 0;

    IEnumerator Start()
    {
		AddTask();

		_loadingView.OnCompleted += OnFakeViewCompleted;

		_asyncOperation = SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Single);

		_asyncOperation.allowSceneActivation = false;

        while (!_asyncOperation.isDone)
        {
			yield return _asyncOperation;
		}
	}

	private void OnFakeViewCompleted()
	{
		_loadingView.OnCompleted -= OnCompleted;
		RemoveTask();
		OnCompleted();
	}

	public void AddTask()
	{
		_tasksCount++;
	}

	public void RemoveTask()
	{
		_tasksCount--;
	}

	private void OnCompleted()
	{
		if (_tasksCount == 0)
		{
			_asyncOperation.allowSceneActivation = true;
		}
	}
}
