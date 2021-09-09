using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	public class PuzzleData
	{
		#region Classes

		private class CellData
		{
			public int			placedNumber	= -1;
			public bool			isHint			= false;
			public List<int>	notes			= new List<int>();
		}

		private class UndoItem
		{
			public enum Action
			{
				NumberChanged,
				NoteToggled
			}

			public Action	action;
			public int		row;
			public int		col;
			public int		number;
		}

		#endregion

		#region Enums

		public enum CellType
		{
			Clue,
			Blank,
			Placed,
			Hint,
			Incorrect
		}

		#endregion

		#region Member Variables

		public int		seed;
		public int		boxRows;
		public int		boxCols;
		public int		boardSize;
		public int		shiftAmount;
		public float	elapsedTime;
		public string	groupId;

		private List<List<int>>			defaultState;
		public List<List<int>>			solvedState;
		private List<List<CellData>>	cellDatas;
		private List<List<UndoItem>>	undoStack;
		private bool					logUndos;

		#endregion

		#region Public Methods

		public PuzzleData(JSONNode saveData)
		{
			LoadSave(saveData);
		}

		public PuzzleData(TextAsset puzzleFile, int shiftAmount, string groupId)
		{
			this.shiftAmount	= shiftAmount;
			this.groupId		= groupId;
			this.undoStack		= new List<List<UndoItem>>();

			Init(puzzleFile.text);
		}



		/// <summary>
		/// Gets the current cell type for the given row/col
		/// </summary>
		public CellType GetCellType(int row, int col)
		{
			if (defaultState[row][col] != -1)
			{
				return CellType.Clue;
			}

			CellData cellData = cellDatas[row][col];

			if (cellData.placedNumber == -1)
			{
				return CellType.Blank;
			}

			if (cellData.isHint)
			{
				return CellType.Hint;
			}

			if (cellData.placedNumber != GetBoardNumber(solvedState[row][col]))
			{
				return CellType.Incorrect;
			}

			return CellType.Placed;
		}

		/// <summary>
		/// Gets the number that is placed by the user on the give row/col, returns -1 if there is no placed number
		/// </summary>
		public int GetNumber(int row, int col)
		{
			switch (GetCellType(row, col))
			{
				case CellType.Clue:
					return GetBoardNumber(defaultState[row][col]);
				case CellType.Placed:
				case CellType.Hint:
				case CellType.Incorrect:
					return cellDatas[row][col].placedNumber;
			}

			return -1;
		}

		/// <summary>
		/// Sets the placed number
		/// </summary>
		public bool SetPlacedNumber(int row, int col, int number)
		{
			if (defaultState[row][col] != -1)
			{
				// Cant place numbers on clue cells
				return false;
			}

			CellData cellData = cellDatas[row][col];

			if (number == cellData.placedNumber)
			{
				return false;
			}

			// Make sure all notes have been removed
			RemoveAllNotes(row, col);

			AddUndoItem(UndoItem.Action.NumberChanged, row, col, cellData.placedNumber);

			cellData.placedNumber = number;

			return true;
		}

		/// <summary>
		/// Removes the placed number and any notes
		/// </summary>
		public void RemoveNumbers(int row, int col)
		{
			// Remove any notes
			RemoveAllNotes(row, col);

			switch (GetCellType(row, col))
			{
				case CellType.Blank:
				case CellType.Clue:
				case CellType.Hint:
					return;
			}

			CellData cellData = cellDatas[row][col];

			AddUndoItem(UndoItem.Action.NumberChanged, row, col, cellData.placedNumber);

			// Set to -1 to indicate it's blank
			cellData.placedNumber = -1;
		}

		/// <summary>
		/// Shows the number at the given row/col and sets it as a hint
		/// </summary>
		public void ShowHint(int row, int col)
		{
			switch (GetCellType(row, col))
			{
				case CellType.Clue:
				case CellType.Hint:
					return;
			}

			// Make sure all notes have been removed
			RemoveAllNotes(row, col);

			CellData cellData = cellDatas[row][col];

			// Set the number as the solved state number
			cellData.placedNumber = GetBoardNumber(solvedState[row][col]);

			// Set the cell data as a hint
			cellData.isHint = true;

			// Remove all UndoItems for the row/col since now it is a hint and should not change
			for (int i = undoStack.Count - 1; i >= 0; i--)
			{
				List<UndoItem> undoItems = undoStack[i];

				for (int j = undoItems.Count - 1; j >= 0; j--)
				{
					UndoItem undoItem = undoItems[j];

					if (row == undoItem.row && col == undoItem.col)
					{
						undoItems.RemoveAt(j);
					}
				}

				if (undoItems.Count == 0)
				{
					undoStack.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// Returns the list of notes on the cell
		/// </summary>
		public List<int> GetNotes(int row, int col)
		{
			// Return a new list so the notes can't be altered
			return new List<int>(cellDatas[row][col].notes);
		}

		/// <summary>
		/// Adds or removes the given number as a note in the given row/col. Returns true if the number was added and false if the number was remove
		/// </summary>
		public bool ToggleNote(int row, int col, int number)
		{
			CellType cellType = GetCellType(row, col);

			switch (cellType)
			{
				case CellType.Clue:
				case CellType.Hint:
					// Cant place notes on clue or hint cells
					return false;
			}

			CellData cellData = cellDatas[row][col];

			AddUndoItem(UndoItem.Action.NoteToggled, row, col, number);

			// Try and remove the note
			if (!cellData.notes.Remove(number))
			{
				// If the cell is not blank
				if (cellType != CellType.Blank)
				{
					AddUndoItem(UndoItem.Action.NumberChanged, row, col, cellData.placedNumber);

					cellData.placedNumber = -1;
				}

				// Note was not in the list, add it
				cellData.notes.Add(number);

				return true;
			}

			return false;
		}

		/// <summary>
		/// Removes the note on the given row/col if it exists. Returns true if the note existed and was removed.
		/// </summary>
		public bool RemoveNote(int row, int col, int number)
		{
			CellType cellType = GetCellType(row, col);

			if (cellType != CellType.Blank)
			{
				// Only blank cells can have notes on them
				return false;
			}

			CellData cellData = cellDatas[row][col];

			// Try and remove the note
			if (cellData.notes.Remove(number))
			{
				// If the note was removed log it as a note toggled undo item
				AddUndoItem(UndoItem.Action.NoteToggled, row, col, number);

				return true;
			}

			return false;
		}

		/// <summary>
		/// Begins a new list of UndoItems
		/// </summary>
		public void BeginUndo()
		{
			logUndos = true;
			undoStack.Add(new List<UndoItem>());
		}

		/// <summary>
		/// Ends logging moves into the undo stack
		/// </summary>
		public void EndUndo()
		{
			logUndos = false;

			// Check if anything was actually added to the list
			if (undoStack[undoStack.Count - 1].Count == 0)
			{
				undoStack.RemoveAt(undoStack.Count - 1);
			}
		}

		/// <summary>
		/// Undoes all moves in the last list of UndoItems in the undoStack
		/// </summary>
		public bool UndoLastMove(out List<int[]> changedCells)
		{
			if (undoStack.Count == 0)
			{
				changedCells = null;

				return false;
			}

			changedCells = new List<int[]>();

			HashSet<string> addedCells = new HashSet<string>();

			List<UndoItem> undoItems = undoStack[undoStack.Count - 1];

			undoStack.RemoveAt(undoStack.Count - 1);

			for (int i = 0; i < undoItems.Count; i++)
			{
				UndoItem undoItem = undoItems[i];

				switch (undoItem.action)
				{
					case UndoItem.Action.NumberChanged:
						SetPlacedNumber(undoItem.row, undoItem.col, undoItem.number);
						break;
					case UndoItem.Action.NoteToggled:
						ToggleNote(undoItem.row, undoItem.col, undoItem.number);
						break;
				}

				string cellId = string.Format("{0}_{1}", undoItem.row, undoItem.col);

				if (!addedCells.Contains(cellId))
				{
					changedCells.Add(new int[] { undoItem.row, undoItem.col });
					addedCells.Add(cellId);
				}
			}

			return true;
		}

		/// <summary>
		/// Checks if the board is solved or not
		/// </summary>
		public bool CheckBoard(out List<int> numberCounts)
		{
			bool isSolved = true;

			// Get a count of how many times each number appears on the board
			numberCounts = new List<int>();

			for (int i = 0; i < boardSize; i++)
			{
				numberCounts.Add(0);
			}

			// Check each cell for the number that is placed
			for (int row = 0; row < boardSize; row++)
			{
				for (int col = 0; col < boardSize; col++)
				{
					CellData cellData = cellDatas[row][col];
					CellType cellType = GetCellType(row, col);

					if (cellType == CellType.Blank)
					{
						isSolved = false;

						continue;
					}

					if (cellType == CellType.Incorrect)
					{
						isSolved = false;
					}

					int placedNumber = cellData.placedNumber;

					if (cellType == CellType.Clue)
					{
						placedNumber = GetBoardNumber(solvedState[row][col]);
					}

					numberCounts[placedNumber - 1]++;
				}
			}

			return isSolved;
		}

		/// <summary>
		/// Returns the correct number that should go in the given row/col
		/// </summary>
		public int GetCorrectNumber(int row, int col)
		{
			return GetBoardNumber(solvedState[row][col]);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Parses the level text file
		/// </summary>
		private void Init(string puzzleFileContents)
		{
			string[] items = puzzleFileContents.Split(',');

			int index = 0;

			seed	= System.Convert.ToInt32(items[index++]);
			boxRows	= System.Convert.ToInt32(items[index++]);
			boxCols	= System.Convert.ToInt32(items[index++]);

			boardSize = boxRows * boxCols;

			defaultState	= new List<List<int>>();
			solvedState		= new List<List<int>>();
			cellDatas		= new List<List<CellData>>();

			for (int r = 0; r < boardSize; r++)
			{
				defaultState.Add(new List<int>());
				solvedState.Add(new List<int>());
				cellDatas.Add(new List<CellData>());

				for (int c = 0; c < boardSize; c++)
				{
					string	item	= items[index++];
					bool	isClue	= false;

					if (item[0] == 't')
					{
						isClue	= true;
						item	= item.Remove(0, 1);
					}

					int number = System.Convert.ToInt32(item);

					solvedState[r].Add(number);
					defaultState[r].Add(isClue ? number : -1);
					cellDatas[r].Add(new CellData());
				}
			}
		}

		public void ResetSudoku()
		{
            for (int i = 0; i < cellDatas.Count; i++)
            {
                for (int j = 0; j < cellDatas[i].Count; j++)
                {
					if (defaultState[i][j] == -1)
					{
						cellDatas[i][j].placedNumber = -1;
					}
                }
            }
		
		}
		/// <summary>
		/// Gets the number as it show be displayed on the board
		/// </summary>
		private int GetBoardNumber(int number)
		{
			return 1 + ((number + shiftAmount) % boardSize);
		}

		/// <summary>
		/// Clears the notes list so that no notes are on the cell
		/// </summary>
		private void RemoveAllNotes(int row, int col)
		{
			CellData cellData = cellDatas[row][col];

			// Call ToggleNote on all notes, this will remove them and also add them to the undo stack
			for (int i = cellData.notes.Count - 1; i >= 0; i--)
			{
				ToggleNote(row, col, cellData.notes[i]);
			}
		}

		/// <summary>
		/// Adds a new UndoItem to the current stack
		/// </summary>
		private void AddUndoItem(UndoItem.Action action, int row, int col, int number)
		{
			if (!logUndos)
			{
				return;
			}

			UndoItem undoItem = new UndoItem();

			undoItem.action = action;
			undoItem.row	= row;
			undoItem.col	= col;
			undoItem.number	= number;

			undoStack[undoStack.Count - 1].Add(undoItem);
		}

		#endregion

		#region Save Methods

		public Dictionary<string, object> Save()
		{
			Dictionary<string, object> saveData = new Dictionary<string, object>();

			saveData["seed"]			= seed;
			saveData["boxRows"]			= boxRows;
			saveData["boxCols"]			= boxCols;
			saveData["shiftAmount"]		= shiftAmount;
			saveData["elapsedTime"]		= elapsedTime;
			saveData["defaultState"]	= defaultState;
			saveData["solvedState"]		= solvedState;
			saveData["groupId"]		= groupId;

			List<object> savedCells = new List<object>();

			for (int row = 0; row < boardSize; row++)
			{
				for (int col = 0; col < boardSize; col++)
				{
					Dictionary<string, object> savedCell = new Dictionary<string, object>();

					savedCell["defaultState"]	= defaultState[row][col];
					savedCell["solvedState"]	= solvedState[row][col];
					savedCell["placedNumber"]	= cellDatas[row][col].placedNumber;
					savedCell["isHint"]			= cellDatas[row][col].isHint;
					savedCell["notes"]			= cellDatas[row][col].notes;

					savedCells.Add(savedCell);
				}
			}

			List<object> savedUndoStack = new List<object>();

			for (int i = 0; i < undoStack.Count; i++)
			{
				List<UndoItem>	undoItems		= undoStack[i];
				List<object>	savedUndoItems	= new List<object>();

				for (int j = 0; j < undoItems.Count; j++)
				{
					UndoItem					undoItem		= undoItems[j];
					Dictionary<string, object>	savedUndoItem	= new Dictionary<string, object>();

					savedUndoItem["action"]	= (int)undoItem.action;
					savedUndoItem["row"]	= undoItem.row;
					savedUndoItem["col"]	= undoItem.col;
					savedUndoItem["number"]	= undoItem.number;

					savedUndoItems.Add(savedUndoItem);
				}

				savedUndoStack.Add(savedUndoItems);
			}

			saveData["savedCells"]		= savedCells;
			saveData["savedUndoStack"]	= savedUndoStack;

			return saveData;
		}

		private void LoadSave(JSONNode saveData)
		{
			seed		= saveData["seed"].AsInt;
			boxRows		= saveData["boxRows"].AsInt;
			boxCols		= saveData["boxCols"].AsInt;
			boardSize	= boxRows * boxCols;
			shiftAmount	= saveData["shiftAmount"].AsInt;
			elapsedTime	= saveData["elapsedTime"].AsFloat;
			groupId		= saveData["groupId"].Value;

			defaultState	= new List<List<int>>();
			solvedState		= new List<List<int>>();
			cellDatas		= new List<List<CellData>>();
			undoStack		= new List<List<UndoItem>>();

			JSONArray savedCells		= saveData["savedCells"].AsArray;
			JSONArray savedUndoStack	= saveData["savedUndoStack"].AsArray;

			int index = 0;

			// Load the cell values
			for (int r = 0; r < boardSize; r++)
			{
				defaultState.Add(new List<int>());
				solvedState.Add(new List<int>());
				cellDatas.Add(new List<CellData>());

				for (int c = 0; c < boardSize; c++)
				{
					JSONNode savedCell	= savedCells[index++];
					CellData cellData	= new CellData();

					cellData.placedNumber	= savedCell["placedNumber"].AsInt;
					cellData.isHint			= savedCell["isHint"].AsBool;
					cellData.notes			= new List<int>();

					for (int i = 0; i < savedCell["notes"].AsArray.Count; i++)
					{
						cellData.notes.Add(savedCell["notes"][i].AsInt);
					}

					defaultState[r].Add(savedCell["defaultState"].AsInt);
					solvedState[r].Add(savedCell["solvedState"].AsInt);
					cellDatas[r].Add(cellData);
				}
			}

			// Load the undo stack
			for (int i = 0; i < savedUndoStack.Count; i++)
			{
				JSONArray savedUndoItems = savedUndoStack[i].AsArray;

				undoStack.Add(new List<UndoItem>());

				for (int j = 0; j < savedUndoItems.Count; j++)
				{
					JSONNode savedUndoItem	= savedUndoItems[j];
					UndoItem undoItem		= new UndoItem();

					undoItem.action	= (UndoItem.Action)savedUndoItem["action"].AsInt;
					undoItem.row	= savedUndoItem["row"].AsInt;
					undoItem.col	= savedUndoItem["col"].AsInt;
					undoItem.number	= savedUndoItem["number"].AsInt;

					undoStack[i].Add(undoItem);
				}
			}
		}

		#endregion
	}
}
