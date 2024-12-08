using UnityEngine;
using UnityEngine.SceneManagement;

namespace SalvationOfSouls.UI.MainMenu
{
	public sealed class MainMenuController : MonoBehaviour
	{
		[SerializeField] private MainMenuView mainMenuView;
		private int _testTotalTime;

		private void Awake()
		{
			mainMenuView.Init(_testTotalTime);
			mainMenuView.StartGameButton.onClick.AddListener(OnStartButton);
			mainMenuView.SettingsButton.onClick.AddListener(OnSettingsButton);
			mainMenuView.ExitButton.onClick.AddListener(OnExitButton);
		}

		private void OnStartButton()
		{
			SceneManager.LoadScene(sceneBuildIndex: 1);
		}

		private void OnSettingsButton()
		{
			Debug.Log($"{nameof(OnSettingsButton)}");
		}

		private void OnExitButton()
		{
			Debug.Log($"{nameof(OnExitButton)}");
			Application.Quit();
		}
	}
}
