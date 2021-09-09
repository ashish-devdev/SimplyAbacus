using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	public class GameSettingsPopup : Popup
	{
		#region Inspector Variables

		[Space]

		[SerializeField] private ToggleSlider mistakesToggle	= null;
		[SerializeField] private ToggleSlider notesToggle		= null;
		[SerializeField] private ToggleSlider numbersToggle		= null;

		#endregion

		#region Unity Methods

		private void Start()
		{
			mistakesToggle.SetToggle(GameManager.Instance.ShowIncorrectNumbers, false);
			notesToggle.SetToggle(GameManager.Instance.RemoveNumbersFromNotes, false);
			numbersToggle.SetToggle(GameManager.Instance.HideAllPlacedNumbers, false);

			mistakesToggle.OnValueChanged	+= OnMistakesToggleChanged;
			notesToggle.OnValueChanged		+= OnNotesToggleChanged;
			numbersToggle.OnValueChanged	+= OnNumbersToggleChanged;
		}

		#endregion

		#region Private Methods

		private void OnMistakesToggleChanged(bool isOn)
		{
			GameManager.Instance.SetGameSetting("mistakes", isOn);
		}

		private void OnNotesToggleChanged(bool isOn)
		{
			GameManager.Instance.SetGameSetting("notes", isOn);
		}

		private void OnNumbersToggleChanged(bool isOn)
		{
			GameManager.Instance.SetGameSetting("numbers", isOn);
		}

		#endregion
	}
}
