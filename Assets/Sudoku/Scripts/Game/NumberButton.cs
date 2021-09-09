using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames.Sudoku
{
	public class NumberButton : ClickableListItem
	{
		#region Inspector Variables

		[SerializeField] private Text numberText = null;

		#endregion

		#region Member Variables

		private CanvasGroup cg;

		#endregion

		#region Public Methods

		public void Setup(int number)
		{
			numberText.text = number.ToString();

			SetVisible(true);
		}

		public void SetVisible(bool isVisible)
		{
			if (cg == null)
			{
				cg = gameObject.GetComponent<CanvasGroup>();

				if (cg == null)
				{
					cg = gameObject.AddComponent<CanvasGroup>();
				}
			}

			cg.alpha			= isVisible ? 1 : 0;
			cg.interactable	= isVisible;
			cg.blocksRaycasts	= isVisible;
		}

		#endregion
	}
}
