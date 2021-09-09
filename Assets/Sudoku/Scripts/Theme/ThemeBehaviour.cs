using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	public abstract class ThemeBehaviour : MonoBehaviour, IThemeBehaviour
	{
		#region Inspector Variables

		[SerializeField] private string id = "";

		#endregion

		#region Abstract Methods

		protected abstract void	SetColor(Color color);

		#endregion

		#region Unity Methods

		private void Start()
		{
			if (ThemeManager.Exists() && ThemeManager.Instance.Enabled)
			{
				ThemeManager.Instance.Register(this);

				SetColor();
			}
		}

		#endregion

		#region Public Methods

		public void NotifyThemeChanged()
		{
			SetColor();
		}

		#endregion

		#region Private Methods

		private void SetColor()
		{
			Color color;

			if (ThemeManager.Instance.GetItemColor(id, out color))
			{
				SetColor(color);
			}
			else
			{
				Debug.LogErrorFormat("[ThemeBehaviour] Could not find theme id \"{0}\", gameObject: {1}", id, gameObject.name);
			}
		}

		#endregion
	}
}
