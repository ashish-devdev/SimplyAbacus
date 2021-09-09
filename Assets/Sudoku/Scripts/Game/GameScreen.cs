using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames.Sudoku
{
	public class GameScreen : Screen
	{
		#region Inspector Variables

		[Space]

		[SerializeField] private Text difficultyText	= null;
		[SerializeField] private Text timeText			= null;

		#endregion

		#region Unity Methods

		private void Update()
		{
			timeText.text = GameManager.Instance.ActivePuzzleTimeStr;
		}

		#endregion

		#region Public Methods

		public override void Show(bool back, bool immediate)
		{
			base.Show(back, immediate);

			if (GameManager.Instance.ActivePuzzleData != null)
			{
				difficultyText.text = GameManager.Instance.ActivePuzzleDifficultyStr;
			}
		}

		#endregion
	}
}
