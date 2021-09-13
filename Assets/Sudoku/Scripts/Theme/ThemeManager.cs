using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	public class ThemeManager : SaveableManager<ThemeManager>
	{
		#region Classes

		[System.Serializable]
		public class Theme
		{
			public string		name			= "";
			public bool			defaultTheme	= false;
			public List<Color>	themeColors		= null;
		}

		#endregion

		#region Inspector Variables

		[SerializeField] private bool			themesEnabled	= false;
		[SerializeField] private List<string>	itemIds			= null;
		[SerializeField] private List<Theme>	themes			= null;

		#endregion

		#region Member Variables

		private List<IThemeBehaviour> themeBehaviours;

		#endregion

		#region Properties

		public override string SaveId { get { return "theme_manager"; } }

		public bool			Enabled				{ get { return themesEnabled; } }
		public List<Theme>	Themes				{ get { return themes; } }
		public int			ActiveThemeIndex	{ get; private set; }
		public Theme		ActiveTheme			{ get { return themes[ActiveThemeIndex]; } }

		public System.Action OnThemeChanged { get; set; }

		#endregion

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();

			themeBehaviours = new List<IThemeBehaviour>();

			InitSave();
		}
		public void LoadInitSave()
		{
			InitSave();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Registers a theme behaviour to recieve notifications when the theme changes
		/// </summary>
		public void Register(IThemeBehaviour themeBehaviour)
		{
			themeBehaviours.Add(themeBehaviour);
		}

		/// <summary>
		/// Sets the given theme as the active theme
		/// </summary>
		public void SetActiveTheme(Theme theme)
		{
			SetActiveTheme(themes.IndexOf(theme));
		}

		/// <summary>
		/// Sets the active theme to the given index
		/// </summary>
		public void SetActiveTheme(int themeIndex)
		{
			if (ActiveThemeIndex == themeIndex)
			{
				return;
			}

			ActiveThemeIndex = themeIndex;

			for (int i = 0; i < themeBehaviours.Count; i++)
			{
				themeBehaviours[i].NotifyThemeChanged();
			}

			if (OnThemeChanged != null)
			{
				OnThemeChanged();
			}
		}

		/// <summary>
		/// Gets the theme item with the given id in the active theme
		/// </summary>
		public bool GetItemColor(string itemId, out Color color)
		{
			for (int i = 0; i < itemIds.Count; i++)
			{
				if (itemId == itemIds[i])
				{
					color = ActiveTheme.themeColors[i];

					return true;
				}
			}

			color = Color.white;

			return false;
		}

		#endregion

		#region Save Methods

		public override Dictionary<string, object> Save()
		{
			Dictionary<string, object> json = new Dictionary<string, object>();

			json["active_theme_index"] = ActiveThemeIndex;

			return json;
		}

		protected override void LoadSaveData(bool exists, JSONNode saveData)
		{
			if (exists)
			{
				ActiveThemeIndex = saveData["active_theme_index"].AsInt;
			}
			else
			{
				// Find the default theme and set it as the active theme
				for (int i = 0; i < themes.Count; i++)
				{
					if (themes[i].defaultTheme)
					{
						ActiveThemeIndex = i;
						break;
					}
				}
			}
		}

		#endregion
	}
}
