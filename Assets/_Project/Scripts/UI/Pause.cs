using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI
{
	public class Pause : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		public void MenuPanel()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("EnvPanel"));
			upgradePanel.transform.SetParent (transform, false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
		}
		public void Restart()
		{
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}

		public void Resume()
		{
			Time.timeScale = 1;
			Invoke (nameof(ForDestroy), 0.1f);
		}
		public void ForDestroy()
		{
			Destroy (gameObject);
		}

	}
}
