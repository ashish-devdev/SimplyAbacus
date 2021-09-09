using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames.Sudoku
{
	public class ThemeGraphicBehaviour : ThemeBehaviour
	{
		#region Member Variables

		private Graphic graphic;

		#endregion

		#region Unity Methods

		private void Awake()
		{
			graphic = gameObject.GetComponent<Graphic>();

			if (graphic == null)
			{
				Debug.LogError("[ThemeColorBehaviour] There is no Graphic component attached to this GameObject, gameObject.name: " + gameObject.name);
			}
		}

		#endregion

		#region Protected Methods

		protected override void SetColor(Color color)
		{
			if (graphic != null)
			{
				graphic.color = color;
			}
		}

		#endregion
	}
}
