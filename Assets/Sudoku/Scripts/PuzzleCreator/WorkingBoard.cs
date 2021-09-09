using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	public class WorkingBoard
	{
		#region Classes

		public class Cell
		{
			public int			row;
			public int			col;
			public int			boxIndex;
			public int			number;
			public List<bool>	possibleNumbers;
			public int			possibleNumbersCount;
			public List<Cell>	allSeenCells;
			public List<Cell>	rowSeenCells;
			public List<Cell>	colSeenCells;
			public List<Cell>	boxSeenCells;

			public HashSet<string> nakedGroups = new HashSet<string>();

			public Cell(int row, int col)
			{
				this.row = row;
				this.col = col;
			}

			/// <summary>
			/// Returns true if the cell has a number placed on it already
			/// </summary>
			public bool HasNumber { get { return number != -1; } }

			/// <summary>
			/// Returns the list of integer numbers that can be placed on this cell
			/// </summary>
			public List<int> PossibleNumbers
			{
				get
				{
					List<int> numbers = new List<int>();

					for (int i = 0; i < possibleNumbers.Count; i++)
					{
						if (possibleNumbers[i]) numbers.Add(i);
					}

					return numbers;
				}
			}

			/// <summary>
			/// Returns a string where all possible numbers are seperated by an underscore
			/// </summary>
			public string PossibleNumbersStr
			{
				get
				{
					string str = "";

					for (int i = 0; i < possibleNumbers.Count; i++)
					{
						if (possibleNumbers[i])
						{
							if (!string.IsNullOrEmpty(str))
							{
								str += "_";
							}

							str += i;
						}
					}

					return str;
				}
			}

			/// <summary>
			/// Returns a string that represents the current cell
			/// </summary>
			public override string ToString()
			{
				return string.Format("{0},{1}", row, col);
			}
		}

		private class Undo
		{
			public enum Type
			{
				CellNumber,
				RemovedPossibleNumber,
				AddPossibleNumber
			}

			public Type	type;
			public int	row;
			public int	col;
			public int	number;
		}

		#endregion

		#region Member Variables

		private bool UseLettersInsteadOfNumbers = false;

		public int size;
		public int boxRows;
		public int boxCols;
		public int rBoxes;
		public int cBoxes;

		public List<List<Cell>>		cells;
		public List<List<Cell>>		colCells;
		public List<List<Cell>>		boxCells;
		public List<HashSet<int>>	boxNumbers;
		public List<HashSet<int>>	rowNumbers;
		public List<HashSet<int>>	colNumbers;
		public HashSet<string>		marked;
		public List<List<int>>		solvedState;
		public List<List<int>>		unSolvedState;

		private List<List<Undo>> undoStack;

		#endregion

		#region Public Methods

		public void Init(int boxRows, int boxCols)
		{
			this.boxRows = boxRows;
			this.boxCols = boxCols;

			size	= boxRows * boxCols;
			cBoxes	= boxRows;
			rBoxes	= boxCols;

			cells		= new List<List<Cell>>();
			colCells	= new List<List<Cell>>();
			boxCells	= new List<List<Cell>>();
			boxNumbers	= new List<HashSet<int>>();
			rowNumbers	= new List<HashSet<int>>();
			colNumbers	= new List<HashSet<int>>();
			marked		= new HashSet<string>();

			undoStack = new List<List<Undo>>();

			for (int i = 0; i < size; i++)
			{
				boxNumbers.Add(new HashSet<int>());
				rowNumbers.Add(new HashSet<int>());
				colNumbers.Add(new HashSet<int>());
			}

			List<bool> possibleNumbersTemplate = new List<bool>();

			for (int i = 0; i < size; i++)
			{
				possibleNumbersTemplate.Add(true);
			}

			// Create all the Cells
			for (int r = 0; r < size; r++)
			{
				cells.Add(new List<Cell>());
				          
				for (int c = 0; c < size; c++)
				{
					Cell cell = new Cell(r, c);

					cell.boxIndex				= GetBoxIndex(r, c);
					cell.number					= -1;
					cell.possibleNumbers		= new List<bool>(possibleNumbersTemplate);
					cell.possibleNumbersCount	= possibleNumbersTemplate.Count;

					cells[r].Add(cell);

					if (r == 0)
					{
						colCells.Add(new List<Cell>());
					}

					colCells[c].Add(cell);
				}
			}

			// Create the boxCells list
			for (int b = 0; b < size; b++)
			{
				int boxRow, boxCol;

				GetBoxRowCol(b, out boxRow, out boxCol);

				boxCells.Add(new List<Cell>());

				for (int r = 0; r < boxRows; r++)
				{
					for (int c = 0; c < boxCols; c++)
					{
						boxCells[b].Add(cells[boxRow + r][boxCol + c]);
					}
				}
			}

			// Set all cells "seen" cells
			for (int cellRow = 0; cellRow < size; cellRow++)
			{
				for (int cellCol = 0; cellCol < size; cellCol++)
				{
					Cell cell = cells[cellRow][cellCol];

					cell.allSeenCells = new List<Cell>();
					cell.rowSeenCells = new List<Cell>();
					cell.colSeenCells = new List<Cell>();
					cell.boxSeenCells = new List<Cell>();

					for (int r = 0; r < size; r++)
					{
						Cell seenCell = cells[r][cellCol];

						if (seenCell != cell)
						{
							cell.allSeenCells.Add(seenCell);
							cell.colSeenCells.Add(seenCell);
						}
					}

					for (int c = 0; c < size; c++)
					{
						Cell seenCell = cells[cellRow][c];

						if (seenCell != cell)
						{
							cell.allSeenCells.Add(seenCell);
							cell.rowSeenCells.Add(seenCell);
						}
					}

					int boxRow = Mathf.FloorToInt(cellRow / boxRows) * boxRows;
					int boxCol = Mathf.FloorToInt(cellCol / boxCols) * boxCols;

					for (int r = 0; r < boxRows; r++)
					{
						for (int c = 0; c < boxCols; c++)
						{
							Cell seenCell = cells[boxRow + r][boxCol + c];

							if (seenCell != cell)
							{
								cell.boxSeenCells.Add(seenCell);

								if (!cell.allSeenCells.Contains(seenCell))
								{
									cell.allSeenCells.Add(seenCell);
								}
							}
						}
					}
				}
			}
		}

		public void BeginUndo()
		{
			undoStack.Add(new List<Undo>());
		}

		public void EndUndo()
		{
			List<Undo> undos = undoStack[undoStack.Count - 1];

			undoStack.RemoveAt(undoStack.Count - 1);

			for (int i = 0; i < undos.Count; i++)
			{
				Undo undo = undos[i];

				switch (undo.type)
				{
					case Undo.Type.CellNumber:
						if (undo.number == -1)
						{
							int curNumber = cells[undo.row][undo.col].number;

							boxNumbers[GetBoxIndex(undo.row, undo.col)].Remove(curNumber);
							rowNumbers[undo.row].Remove(curNumber);
							colNumbers[undo.col].Remove(curNumber);
						}
						else
						{
							boxNumbers[GetBoxIndex(undo.row, undo.col)].Add(undo.number);
							rowNumbers[undo.row].Add(undo.number);
							colNumbers[undo.col].Add(undo.number);
						}

						cells[undo.row][undo.col].number = undo.number;
						break;
					case Undo.Type.RemovedPossibleNumber:
						cells[undo.row][undo.col].possibleNumbers[undo.number] = true;
						cells[undo.row][undo.col].possibleNumbersCount++;
						break;
					case Undo.Type.AddPossibleNumber:
						cells[undo.row][undo.col].possibleNumbers[undo.number] = false;
						cells[undo.row][undo.col].possibleNumbersCount--;
						break;
				}
			}
		}

		public void ClearUndo()
		{
			undoStack.Clear();
		}

		public void SetNumber(int row, int col, int number)
		{
			int curNumber = cells[row][col].number;

			if (number == curNumber)
			{
				return;
			}

			Cell cell = cells[row][col];

			SetCellNumber(row, col, number);

			if (number == -1)
			{
				// If we are removing the number then set the current number back to all the hash sets
				boxNumbers[cell.boxIndex].Remove(curNumber);
				rowNumbers[row].Remove(curNumber);
				colNumbers[col].Remove(curNumber);
			}
			else
			{
				// Add the number to the HashSets so we know what rows/cols/boxes contain what numbers
				boxNumbers[cell.boxIndex].Add(number);
				rowNumbers[row].Add(number);
				colNumbers[col].Add(number);
			}

			for (int i = 0; i < cell.allSeenCells.Count; i++)
			{
				Cell seenCell = cell.allSeenCells[i];

				if (number == -1)
				{
					AddPossibleNumber(seenCell.row, seenCell.col, curNumber);
				}
				else
				{
					RemovePossibleNumber(seenCell.row, seenCell.col, number);
				}
			}
		}

		public void RemoveNumber(int row, int col)
		{
			SetNumber(row, col, -1);
		}

		public int GetBoxIndex(int row, int col)
		{
			return Mathf.FloorToInt(row / boxRows) * cBoxes + Mathf.FloorToInt(col / boxCols);
		}

		public void GetBoxRowCol(int boxIndex, out int row, out int col)
		{
			row = Mathf.FloorToInt(boxIndex / boxRows) * boxRows;
			col = (boxIndex % boxRows) * boxCols;
		}

		/// <summary>
		/// Sets the solvedState list using the current numbers in all the cells
		/// </summary>
		public void SetSolvedState()
		{
			solvedState = new List<List<int>>();

			for (int r = 0; r < size; r++)
			{
				solvedState.Add(new List<int>());

				for (int c = 0; c < size; c++)
				{
					solvedState[r].Add(cells[r][c].number);
				}
			}
		}

		/// <summary>
		/// Sets the unSolvedState list using the current numbers in all the cells
		/// </summary>
		public void SetUnSolvedState()
		{
			unSolvedState = new List<List<int>>();

			for (int r = 0; r < size; r++)
			{
				unSolvedState.Add(new List<int>());

				for (int c = 0; c < size; c++)
				{
					unSolvedState[r].Add(cells[r][c].number);
				}
			}
		}

		public override string ToString()
		{
			string str = "";

			for (int r = 0; r < size; r++)
			{
				if (r != 0) str += "\n";
				if (r != 0 && r % boxRows == 0) str += "\n";

				for (int c = 0; c < size; c++)
				{
					if (c != 0 && c % boxCols == 0) str += " ";

					int number = cells[r][c].number;

					if (number == -1)
					{
						str += "_";
					}
					else
					{
						str += GetNumberPrintStr(number);
					}
				}
			}

			str += "\n\n";

			for (int r = 0; r < size; r++)
			{
				if (r != 0) str += "\n";

				for (int c = 0; c < size; c++)
				{
					Cell cell = cells[r][c];

					if (!cell.HasNumber)
					{
						str += string.Format("{0},{1} | ", r, c);

						List<int> possibleNumbers = cell.PossibleNumbers;

						for (int i = 0; i < possibleNumbers.Count; i++)
						{
							if (i != 0) str += "_";

							str += (possibleNumbers[i] + 1).ToString();
						}

						str += "\n";
					}
				}
			}

			return str;
		}

		public void PrintPossibleNumbers(int row = -1, int col = -1)
		{
			int rowStart	= (row == -1) ? 0 : row;
			int rowEnd		= (row == -1) ? size - 1 : row;
			int colStart	= (col == -1) ? 0 : col;
			int colEnd		= (col == -1) ? size - 1 : col;

			for (int r = rowStart; r <= rowEnd; r++)
			{
				for (int c = colStart; c <= colEnd; c++)
				{
					string numbers = "";

					Cell cell = cells[r][c];

					for (int i = 0; i < cell.possibleNumbers.Count; i++)
					{
						if (cell.possibleNumbers[i])
						{
							if (!string.IsNullOrEmpty(numbers)) numbers += ",";

							numbers += GetNumberPrintStr(i);
						}
					}

					Debug.LogFormat("r:{0},c:{1} {2}", r, c, numbers);
				}
			}
		}

		#endregion

		#region Protected Methods

		#endregion

		#region Private Methods

		private void SetCellNumber(int row, int col, int number)
		{
			Cell cell = cells[row][col];

			if (cell.number != number)
			{
				CreateUndo(cell, Undo.Type.CellNumber, cell.number);

				cell.number = number;
			}
		}

		private void RemovePossibleNumber(int row, int col, int number)
		{
			Cell cell = cells[row][col];

			if (cell.possibleNumbers[number])
			{
				CreateUndo(cell, Undo.Type.RemovedPossibleNumber, number);

				cell.possibleNumbers[number] = false;
				cell.possibleNumbersCount--;
			}
		}

		private void AddPossibleNumber(int row, int col, int number)
		{
			Cell cell = cells[row][col];

			if (!cell.possibleNumbers[number] &&
			    !rowNumbers[row].Contains(number) &&
			    !colNumbers[col].Contains(number) &&
			    !boxNumbers[cell.boxIndex].Contains(number))
			{
				CreateUndo(cell, Undo.Type.AddPossibleNumber, number);

				cell.possibleNumbers[number] = true;
				cell.possibleNumbersCount++;
			}
		}

		private void CreateUndo(Cell cell, Undo.Type type, int number)
		{
			if (undoStack.Count <= 0) return;

			Undo undo	= new Undo();
			undo.type	= type;
			undo.row	= cell.row;
			undo.col	= cell.col;
			undo.number	= number;

			undoStack[undoStack.Count - 1].Add(undo);
		}

		/// <summary>
		/// Returns the string to use for printing numbers
		/// </summary>
		private string GetNumberPrintStr(int number)
		{
			if (UseLettersInsteadOfNumbers)
			{
				return ((char)('A' + number)).ToString();
			}

			return (number + 1).ToString();
		}

		#endregion
	}
}