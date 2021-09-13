using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	public class GameManager : SaveableManager<GameManager>
	{
		#region Inspector Variables

		[Header("Data")]
		[SerializeField] public List<PuzzleGroupData>	puzzleGroups			= null;

		[Header("Values")]
		[SerializeField] private int					hintsPerCompletedPuzzle	= 1;
		[SerializeField] private int					numLevelsBetweenAds		= 3;

		[Header("Game Settings")]
		[SerializeField] private bool					showIncorrectNumbers	= true;
		[SerializeField] private bool					removeNumbersFromNotes	= true;
		[SerializeField] private bool					hideAllPlacedNumbers	= true;

		[Header("Components")]
		[SerializeField] private PuzzleBoard			puzzleBoard				= null;

		#endregion

		#region Properties

		public override string			SaveId					{ get { return "game_manager"; } }
		public bool						IsPaused				{ get; private set; }
		public PuzzleData				ActivePuzzleData		{ get; private set; }
		public int						NumLevelsTillAdd		{ get; private set; }

		public List<PuzzleGroupData>	PuzzleGroupDatas		{ get { return puzzleGroups; } }
		public bool						ShowIncorrectNumbers	{ get { return showIncorrectNumbers; } }
		public bool						RemoveNumbersFromNotes	{ get { return removeNumbersFromNotes; } }
		public bool						HideAllPlacedNumbers	{ get { return hideAllPlacedNumbers; } }
		public int						HintsPerCompletedPuzzle	{ get { return hintsPerCompletedPuzzle; } }

		public System.Action<string> OnGameSettingChanged { get; set; }

		public string ActivePuzzleDifficultyStr
		{
			get
			{
				if (ActivePuzzleData == null) return "";

				PuzzleGroupData puzzleGroupData = GetPuzzleGroup(ActivePuzzleData.groupId);

				if (puzzleGroupData == null) return "";

				return puzzleGroupData.displayName;
			}
		}

		public string ActivePuzzleTimeStr
		{
			get
			{
				if (ActivePuzzleData == null) return "00:00";

				return Utilities.FormatTimer(ActivePuzzleData.elapsedTime);
			}
		}

		#endregion

		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();

			puzzleBoard.Initialize();

			for (int i = 0; i < puzzleGroups.Count; i++)
			{
				puzzleGroups[i].Load();
			}

			InitSave();
		}
		public void LoadInitSave()
		{
			InitSave();
		}
		private void Update()
		{
			if (!IsPaused && ActivePuzzleData != null && ScreenManager.Instance.CurrentScreenId == "game")
			{
				ActivePuzzleData.elapsedTime += Time.deltaTime;
			}
		}

		#endregion

		#region Public Methods

		public void PlayNewGame(int groupIndex)
		{
			// Make sure the groupIndex is within the bounds of puzzleGroups
			if (groupIndex >= 0 && groupIndex < puzzleGroups.Count)
			{
				PlayNewGame(puzzleGroups[groupIndex]);

				return;
			}

			Debug.LogErrorFormat("[GameManager] PlayNewGame(int groupIndex) : The given groupIndex ({0}) is out of bounds for the puzzleGroups of size {1} \"{0}\"", groupIndex, puzzleGroups.Count);
		}

		public void PlayNewGame(string groupId)
		{
			// Get the PuzzleGroupData for the given groupId
			for (int i = 0; i < puzzleGroups.Count; i++)
			{
				PuzzleGroupData puzzleGroupData = puzzleGroups[i];

				if (groupId == puzzleGroupData.groupId)
				{
					PlayNewGame(puzzleGroupData);

					return;
				}
			}

			Debug.LogErrorFormat("[GameManager] PlayNewGame(string groupId) : Could not find a PuzzleGroupData with the given id \"{0}\"", groupId);
		}

		public void ContinueActiveGame()
		{
			PlayGame(ActivePuzzleData);
		}

		public void SetGameSetting(string setting, bool value)
		{
			switch (setting)
			{
				case "mistakes":
					showIncorrectNumbers = value;
					break;
				case "notes":
					removeNumbersFromNotes = value;
					break;
				case "numbers":
					hideAllPlacedNumbers = value;
					break;
			}

			if (OnGameSettingChanged != null)
			{
				OnGameSettingChanged(setting);
			}
		}

		public void ActivePuzzleCompleted()
		{
			// Get the PuzzleGroupData for the puzzle
			PuzzleGroupData	puzzleGroup	= GetPuzzleGroup(ActivePuzzleData.groupId);
			float			elapsedTime	= ActivePuzzleData.elapsedTime;

			// Set the puzzle data to null now so the game can't be continued
			ActivePuzzleData = null;

			puzzleGroup.PuzzlesCompleted	+= 1;
			puzzleGroup.TotalTime			+= elapsedTime;

			bool newBest = false;

			if (puzzleGroup.MinTime == 0 || elapsedTime < puzzleGroup.MinTime)
			{
				newBest				= true;
				puzzleGroup.MinTime	= elapsedTime;
			}

			// Award the player their hint
			CurrencyManager.Instance.Give("hints", hintsPerCompletedPuzzle);

			object[] popupData = 
			{
				puzzleGroup.displayName,
				elapsedTime,
				puzzleGroup.MinTime,
				newBest
			};

			// Show the puzzle complete popup
		/*	PopupManager.Instance.Show("puzzle_complete", popupData, (bool cancelled, object[] outData) => 
			{
				// If the popup was closed without the cancelled flag being set then the player selected New Game
				if (!cancelled && puzzleGroup != null)
				{
					PlayNewGame(puzzleGroup);
				}
				// Else go back to the main screen
				else
				{
					ScreenManager.Instance.Back();
				}
			});*/
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets a new PuzzleData from the given PuzzleGroupData and sets up the game to play it
		/// </summary>
		private void PlayNewGame(PuzzleGroupData puzzleGroupData)
		{
			// Get a puzzle that has not yet been played by the user
			PuzzleData puzzleData = puzzleGroupData.GetPuzzle();

			// Play the game using the new puzzle data
			PlayGame(puzzleData);
		}

		/// <summary>
		/// Starts the game using the given PuzzleData
		/// </summary>
		private void PlayGame(PuzzleData puzzleData)
		{
			// Set the active puzzle dat
			ActivePuzzleData = puzzleData;

			// Setup the puzzle board to display the numbers
			puzzleBoard.Setup(puzzleData);

			// Show the game screen
			ScreenManager.Instance.Show("game");

			NumLevelsTillAdd++;

			if (NumLevelsTillAdd > numLevelsBetweenAds)
			{
				if (MobileAdsManager.Instance.ShowInterstitialAd(null))
				{
					NumLevelsTillAdd = 0;
				}
			}
		}

		/// <summary>
		/// Gets the puzzle group with the given id
		/// </summary>
		private PuzzleGroupData GetPuzzleGroup(string id)
		{
			for (int i = 0; i < puzzleGroups.Count; i++)
			{
				PuzzleGroupData puzzleGroup = puzzleGroups[i];

				if (id == puzzleGroup.groupId)
				{
					return puzzleGroup;
				}
			}

			return null;
		}

		#endregion

		#region Save Methods

		public override Dictionary<string, object> Save()
		{
			Dictionary<string, object> saveData = new Dictionary<string, object>();

			// Save the active puzzle if there is one
			if (ActivePuzzleData != null)
			{
				saveData["activePuzzle"] = ActivePuzzleData.Save();
			}

			// Save all the puzzle groups data
			List<object> savedPuzzleGroups = new List<object>();

			for (int i = 0; i < puzzleGroups.Count; i++)
			{
				PuzzleGroupData				puzzleGroup			= puzzleGroups[i];
				Dictionary<string, object>	savedPuzzleGroup	= new Dictionary<string, object>();

				savedPuzzleGroup["id"]		= puzzleGroup.groupId;
				savedPuzzleGroup["data"]	= puzzleGroup.Save();

				savedPuzzleGroups.Add(savedPuzzleGroup);
			}

			saveData["savedPuzzleGroups"] = savedPuzzleGroups;

			// Save the game settings
			saveData["showIncorrectNumbers"]	= showIncorrectNumbers;
			saveData["removeNumbersFromNotes"]	= removeNumbersFromNotes;
			saveData["hideAllPlacedNumbers"]	= hideAllPlacedNumbers;
			saveData["NumLevelsTillAdd"]		= NumLevelsTillAdd;

			return saveData;
		}

		protected override void LoadSaveData(bool exists, JSONNode saveData)
		{
			if (!exists)
			{
				return;
			}

			// If there is a saved active puzzle load it
			if (saveData.AsObject.HasKey("activePuzzle"))
			{
				ActivePuzzleData = new PuzzleData(saveData["activePuzzle"]);
			}

			// Load the saved group data
			JSONArray savedPuzzleGroups = saveData["savedPuzzleGroups"].AsArray;

			for (int i = 0; i < savedPuzzleGroups.Count; i++)
			{
				JSONNode		savedPuzzleGroup	= savedPuzzleGroups[i];
				PuzzleGroupData	puzzleGroup			= GetPuzzleGroup(savedPuzzleGroup["id"].Value);

				if (puzzleGroup != null)
				{
					puzzleGroup.Load(savedPuzzleGroup["data"]);
				}
			}

			// Load the game settings
			showIncorrectNumbers	= saveData["showIncorrectNumbers"].AsBool;
			removeNumbersFromNotes	= saveData["removeNumbersFromNotes"].AsBool;
			hideAllPlacedNumbers	= saveData["hideAllPlacedNumbers"].AsBool;
			NumLevelsTillAdd		= saveData["NumLevelsTillAdd"].AsInt;
		}

		#endregion
	}
}
