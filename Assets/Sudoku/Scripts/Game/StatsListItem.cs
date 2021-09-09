using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames.Sudoku
{
	public class StatsListItem : MonoBehaviour
	{
		#region Inspector Variables

		[SerializeField] private Text groupNameText			= null;
		[SerializeField] private Text puzzleCompletedText	= null;
		[SerializeField] private Text bestTimeText			= null;
		[SerializeField] private Text averageTimeText		= null;

		#endregion

		#region Public Methods

		public void Setup(PuzzleGroupData puzzleGroup)
		{
			groupNameText.text			= puzzleGroup.displayName;
			puzzleCompletedText.text	= puzzleGroup.PuzzlesCompleted.ToString();
			bestTimeText.text			= Utilities.FormatTimer(puzzleGroup.MinTime);

			if (puzzleGroup.PuzzlesCompleted == 0)
			{
				averageTimeText.text = "00:00";
			}
			else
			{
				averageTimeText.text = Utilities.FormatTimer(puzzleGroup.TotalTime / puzzleGroup.PuzzlesCompleted);
			}
		}

		#endregion
	}
}
