using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SalvationOfSouls.DeadMoroze.Runtime.UI.MainMenu
{
	[Serializable]
	public sealed class MainMenuView
	{
		[Header("Buttons")]
		[SerializeField] private Button startGameButton;
		[SerializeField] private Button settingsButton;
		[SerializeField] private Button exitButton;

		[SerializeField] private TextMeshProUGUI bestTime;

		public Button StartGameButton => startGameButton;
		public Button SettingsButton => settingsButton;
		public Button ExitButton => exitButton;

		public void Init(int time)
		{
			bestTime.text = $"Best Time: <color=red>{time}";
		}
	}
}