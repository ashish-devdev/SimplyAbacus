using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	[System.Serializable]
	public class PuzzleGroupData
	{
		#region Inspector Variables

		public string			groupId;
		public string			displayName;
		public List<TextAsset>	puzzleFiles;

		#endregion

		#region Member Variables

		private HashSet<int>	playedPuzzles;	// Contains all puzzleFile indices which have been played/started by the user
		private int				shiftAmount;	// The amount we will "shift" the numbers on the puzzle so it appears to be a differnt puzzle

		#endregion

		#region Properties

		public int		PuzzlesCompleted	{ get; set; }
		public float	MinTime 			{ get; set; }
		public float	TotalTime			{ get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Returns an object that represents this groups save data
		/// </summary>
		/// <returns>The save.</returns>
		public Dictionary<string, object> Save()
		{
			Dictionary<string, object> groupSaveData = new Dictionary<string, object>();

			groupSaveData["played"] 			= new List<int>(playedPuzzles);
			groupSaveData["shift_amount"]		= shiftAmount;
			groupSaveData["puzzles_completed"]	= PuzzlesCompleted;
			groupSaveData["min_time"]			= MinTime;
			groupSaveData["total_time"]			= TotalTime;

			return groupSaveData;
		}

		/// <summary>
		/// Loads the PuzzleGroupData and sets the save values
		/// </summary>
		public void Load(JSONNode groupSaveData = null)
		{
			playedPuzzles = new HashSet<int>();

			if (groupSaveData != null)
			{
				// Load the played puzzles
				JSONArray playedPuzzlesJson = groupSaveData["played"].AsArray;

				for (int i = 0; i < playedPuzzlesJson.Count; i++)
				{
					playedPuzzles.Add(playedPuzzlesJson[i].AsInt);
				}

				shiftAmount			= groupSaveData["shift_amount"].AsInt;
				PuzzlesCompleted	= groupSaveData["puzzles_completed"].AsInt;
				MinTime				= groupSaveData["min_time"].AsFloat;
				TotalTime			= groupSaveData["total_time"].AsFloat;
			}
		}

		/// <summary>
		/// Returns a puzzle data for a random puzzle that has not yet been played
		/// </summary>
		public PuzzleData GetPuzzle()
		{
			// Check if the list is empty, if we don't it could lead to infinite recursion
			if (puzzleFiles.Count == 0)
			{
				return null;
			}

			for (int i = 0; i < puzzleFiles.Count; i++)
			{
				// Get a random puzzle index to play next
				int puzzleIndex = Random.Range(i, puzzleFiles.Count);

				// Check that it has not already been played
				if (!playedPuzzles.Contains(puzzleIndex))
				{
					// Add the puzzles index to the set of played puzzles
					playedPuzzles.Add(puzzleIndex);

					return new PuzzleData(puzzleFiles[puzzleIndex], shiftAmount, groupId);
				}
			}

			/* If we get here all puzzles have been played (or atleast started) by the user */

			// Increase the shift amount so when a puzzle is replayed by the user they are unlikely to know it is the same puzzle
			shiftAmount++;

			// Clear the played puzzles so we can use them again
			playedPuzzles.Clear();

			// Call the method again, this time it will return a puzzle for sure
			return GetPuzzle();
		}

		#endregion
	}
}
