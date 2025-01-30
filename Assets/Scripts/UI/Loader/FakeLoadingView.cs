using System;
using UnityEngine;

public class FakeLoadingView : MonoBehaviour
{
    public Action OnCompleted = null;
	public void OnAnimatorCompleted()
    {
		OnCompleted?.Invoke();
    }
}
