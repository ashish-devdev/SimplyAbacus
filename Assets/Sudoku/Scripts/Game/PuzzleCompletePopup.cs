using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames.Sudoku
{
	public class PuzzleCompletePopup : Popup
	{
		#region Inspector Variables

		[Space]

		[SerializeField] private Text		difficultyNameText	= null;
		[SerializeField] private Text		puzzleTimeText		= null;
		[SerializeField] private Text		bestTimeText		= null;
		[SerializeField] private GameObject	newBestIndicator	= null;
		[SerializeField] private Text		awardedHintsText	= null;

		#endregion

		#region Public Methods

		public override void OnShowing(object[] inData)
		{
			base.OnShowing(inData);

			string	groupName	= (string)inData[0];
			float	puzzleTime	= (float)inData[1];
			float	bestTime	= (float)inData[2];
			bool	newBest		= (bool)inData[3];

			difficultyNameText.text	= groupName;
			puzzleTimeText.text		= Utilities.FormatTimer(puzzleTime);
			bestTimeText.text		= Utilities.FormatTimer(bestTime);

			newBestIndicator.SetActive(newBest);

			awardedHintsText.text = "+" + GameManager.Instance.HintsPerCompletedPuzzle;
		}

		#endregion
	}
}
