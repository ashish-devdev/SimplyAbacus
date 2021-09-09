using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames.Sudoku
{
	public class MainScreen : Screen
	{
		#region Inspector Variables

		[Space]

		[SerializeField] private GameObject	continueButton			= null;
		[SerializeField] private Text		continueDifficultyText	= null;
		[SerializeField] private Text		continueTimeText		= null;

		#endregion

		#region Public Methods

		public override void Show(bool back, bool immediate)
		{
			base.Show(back, immediate);

			continueButton.SetActive(GameManager.Instance.ActivePuzzleData != null);

			if (GameManager.Instance.ActivePuzzleData != null)
			{
				continueDifficultyText.text	= "Difficulty: " + GameManager.Instance.ActivePuzzleDifficultyStr;
				continueTimeText.text		= "Time: " +GameManager.Instance.ActivePuzzleTimeStr;
			}
		}

		#endregion
	}
}
