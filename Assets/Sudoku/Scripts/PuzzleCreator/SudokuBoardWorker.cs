using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	public class SudokuCreatorWorker : Worker
	{
		#region Enums

		private enum SolveBoardResult
		{
			Success,
			Fail,
			FailAll
		}

		#endregion

		#region Inspector Variables

		#endregion

		#region Member Variables

		private System.Random	rand;
		private Stopwatch 		stopwatch;

		// Set to true to print verbose logs
		private bool printLogs			= false;
		private bool printVerboseLogs	= false;

		#endregion

		#region Properties

		public int			BoxRows				{ get; set; }
		public int			BoxCols				{ get; set; }
		public int			DesiredNumClues		{ get; set; }
		public bool			ForceNumClues		{ get; set; }
		public List<bool>	RejectedStrategies	{ get; set; }
		public List<bool>	RequiredStrategies	{ get; set; }
		public int			Seed				{ get; set; }
		public float		RestartTimeout		{ get; set; }

		public string		Error				{ get; set; }
		public WorkingBoard	WorkingBoard		{ get; set; }

		#endregion

		#region Public Methods

		protected override void Begin()
		{
			rand = (Seed > 0) ? new System.Random(Seed) : new System.Random();

			stopwatch = new System.Diagnostics.Stopwatch();
		}

		protected override void DoWork()
		{
			while (true)
			{
				try
				{
					// Initialize the WorkingBoard object
					Init();

					// Place a few random numbers on the board (The number of random numbers that
					// are placed is equal to the number of boxes, one random number per box)
					PlaceInitialNumbers();

					// Start a stopwatch that will keep track of the amount of time that the algo is taking to complete
					stopwatch.Reset();
					stopwatch.Start();

					// Solve the sudoku puzzle using brute force, trying all possible combinations
					// and backtracking when we make a mistake that creates an un-solvable board.
					if (SolveBoard(true, false) != SolveBoardResult.Success)
					{
						if (Stopping) break;

						// If we did not get a solved board then restart from the beginning
						continue;
					}

					// Stop the timer, it's no longer needed
					stopwatch.Stop();
					WorkingBoard.ClearUndo();

					// Set the solvedState list, this will hold all the numbers in the rows/cols for the solved board. This is so we can do more
					// work on the board by removing/adding numbers while still having a way of checking what number goes where
					WorkingBoard.SetSolvedState();

					// Remove numbers from the board until we cannot remove any more numbers or we have reached the desired number of numbers on the board
					if (!RemoveNumbers(DesiredNumClues))
					{
						if (Stopping) break;

						// Force desired num clues is true and we could not create a valid puzzle
						continue;
					}

					// Set the unSolvedState list show we know what the board should look like to the player when playing it
					WorkingBoard.SetUnSolvedState();

					if (!Grade())
					{
						if (Stopping) break;

						// We could not solve the board with the selected strategies
						continue;
					}
				}
				catch (System.Exception ex)
				{
					Error = ex.Message + "\n" + ex.StackTrace;
				}

				break;
			}
				
			// Tell the worker it is done
			Stop();
		}

		#endregion

		#region Sudoku Creation Methods

		private void Init()
		{
			WorkingBoard = new WorkingBoard();

			WorkingBoard.Init(BoxRows, BoxCols);
		}

		/// <summary>
		/// Places the first numbers randomly on the board
		/// </summary>
		private void PlaceInitialNumbers()
		{
			List<int> initialNumbers = new List<int>();

			for (int i = 0; i < WorkingBoard.size; i++)
			{
				initialNumbers.Add(i);
			}

			for (int r = 0; r < WorkingBoard.size; r++)
			{
				int randIndex	= rand.Next(0, initialNumbers.Count);
				int number		= initialNumbers[randIndex];

				initialNumbers.RemoveAt(randIndex);

				int c = (r % WorkingBoard.boxRows) * WorkingBoard.boxCols + Mathf.FloorToInt(r / WorkingBoard.boxRows);

				WorkingBoard.SetNumber(r, c, number);
			}
		}

		/// <summary>
		/// Returns true if the time has elapsed passed the restart timeout
		/// </summary>
		private bool CheckTimeout()
		{
			return RestartTimeout > 0 && stopwatch.Elapsed.TotalSeconds >= RestartTimeout;
		}

		/// <summary>
		/// Solves the board using brute force
		/// </summary>
		private SolveBoardResult SolveBoard(bool canTimeout, bool onlyOneSolution)
		{
			if (Stopping || (canTimeout && CheckTimeout())) return SolveBoardResult.FailAll;

			List<WorkingBoard.Cell>	nextCells				= new List<WorkingBoard.Cell>();
			int						possibleNumbersCount	= int.MaxValue;

			// Get the cell with the least amount of possible numbers
			for (int r = 0; r < WorkingBoard.size; r++)
			{
				for (int c = 0; c < WorkingBoard.size; c++)
				{
					WorkingBoard.Cell cell = WorkingBoard.cells[r][c];

					// If the cell has a number of in then skip it
					if (cell.HasNumber)
					{
						continue;
					}

					// If the cell has no possible numbers then we have an un-solvable board so return false
					if (cell.possibleNumbersCount == 0)
					{
						return SolveBoardResult.Fail;
					}

					// If the cell has less possible numbers then the current nextCell set it as the next cell
					if (possibleNumbersCount > cell.possibleNumbersCount)
					{
						nextCells.Clear();
						nextCells.Add(cell);
						possibleNumbersCount = cell.possibleNumbersCount;
					}
					else if (possibleNumbersCount == cell.possibleNumbersCount)
					{
						nextCells.Add(cell);
					}
				}
			}

			// If nextCell is null at this point then all cells have a number and the board has been solved
			if (nextCells.Count == 0)
			{
				return SolveBoardResult.Success;
			}

			// Get the list of possible numbers that can be placed on this cell
			WorkingBoard.Cell	nextCell		= nextCells[rand.Next(0, nextCells.Count - 1)];
			List<int>			possibleNumbers	= nextCell.PossibleNumbers;

			bool foundSolution = false;

			// Try and place each of the numbers and cell if it leads to a solved board
			for (int i = 0; i < possibleNumbers.Count; i++)
			{
				int number = possibleNumbers[i];

				// Start a new undo stack
				WorkingBoard.BeginUndo();

				// Set the cell number
				WorkingBoard.SetNumber(nextCell.row, nextCell.col, number);

				// Find any cells with only one possible number and set that as the cells number
				FindNakedSingles();

				// Check for an un-solvable board then try and solve the rest of the puzzle
				SolveBoardResult result = SolveBoard(canTimeout, onlyOneSolution);

				if (result == SolveBoardResult.Success)
				{
					if (!onlyOneSolution)
					{
						// The board has been solved
						return SolveBoardResult.Success;
					}

					if (foundSolution)
					{
						// Remove any changes that were made
						WorkingBoard.EndUndo();

						// There is already a solution using another number
						return SolveBoardResult.FailAll;
					}

					// Set found solution to true and try again with the other possible numbers
					foundSolution = true;
				}
				else if (result == SolveBoardResult.FailAll)
				{
					// Remove any changes that were made
					WorkingBoard.EndUndo();

					// If the result was fail all then return the result so exit the recursive algorithm
					return SolveBoardResult.FailAll;
				}

				// Remove any changes that were made and try the next number
				WorkingBoard.EndUndo();
			}

			return (foundSolution ? SolveBoardResult.Success : SolveBoardResult.Fail);
		}

		/// <summary>
		/// Removes numbers from the board so that the board has one unique solution
		/// </summary>
		private bool RemoveNumbers(int desiredNumberOfClues)
		{
			List<WorkingBoard.Cell> allCells = new List<WorkingBoard.Cell>();

			for (int r = 0; r < WorkingBoard.size; r++)
			{
				for (int c = 0; c < WorkingBoard.size; c++)
				{
					allCells.Add(WorkingBoard.cells[r][c]);
				}
			}

			int numCluesRemaining = WorkingBoard.size * WorkingBoard.size;

			for (int i = 0; i < allCells.Count && numCluesRemaining > desiredNumberOfClues; i++)
			{
				if (Stopping) return false;

				// Get a random cell to remove next
				int					randIndex	= rand.Next(i, allCells.Count);
				WorkingBoard.Cell	cell		= allCells[randIndex];

				// Swap them so cell cannot be choosen again on the next iteration of the for loop
				allCells[randIndex]	= allCells[i];
				allCells[i]			= cell;

				// Get the cells number in-case we need to put it back
				int cellNumber = cell.number;

				// Remove the number
				WorkingBoard.RemoveNumber(cell.row, cell.col);

				// Try and solve the puzzle every possible way to see if there is only one unique solution
				SolveBoardResult result = SolveBoard(false, true);

				if (result == SolveBoardResult.Success)
				{
					// There is only one solution after removing the number so it will stay removed
					numCluesRemaining--;
				}
				else
				{
					// There are more than one solutions after removing the number so put it back and try another
					WorkingBoard.SetNumber(cell.row, cell.col, cellNumber);
				}
			}

			if (ForceNumClues && numCluesRemaining != desiredNumberOfClues)
			{
				return false;
			}

			return true;
		}

		#endregion

		#region Sudoku Grading Methods

		private bool Grade()
		{
			if (printVerboseLogs)
			{
				UnityEngine.Debug.Log("Grading puzzle:\n" + WorkingBoard);
			}

			int totalSteps				= 0;
			int totalNakedSingles		= 0;
			int totalNakedPairs			= 0;
			int totalNakedTriples		= 0;
			int totalNakedQuads			= 0;
			int totalHiddenSingles		= 0;
			int totalHiddenPairs		= 0;
			int totalHiddenTriples		= 0;
			int totalHiddenQuads		= 0;
			int totalPointingPairs		= 0;
			int totalBoxLineReductions	= 0;
			int totalRandomPlacements	= 0;

			while (!CheckSolved())
			{
				if (Stopping) return false;

				totalSteps++;

				bool useRandom = true;

				// Look for naked singles
				int nakedSingles = FindNakedSingles(printVerboseLogs);

				if (nakedSingles > 0)
				{
					totalNakedSingles += nakedSingles;

					continue;
				}

				if (!RejectedStrategies[0])
				{
					// Look for hidden singles
					int hiddenSingles = FindHiddenSingles();

					if (hiddenSingles > 0)
					{
						totalHiddenSingles += hiddenSingles;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (!RejectedStrategies[1])
				{
					// Look for naked pairs
					int nakedPairs = FindNakedGroups(2);

					if (nakedPairs > 0)
					{
						totalNakedPairs += nakedPairs;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (!RejectedStrategies[2])
				{
					// Look for naked triples
					int nakedTriples = FindNakedGroups(3);

					if (nakedTriples > 0)
					{
						totalNakedTriples += nakedTriples;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (!RejectedStrategies[3])
				{
					// Look for hidden pairs
					int hiddenPairs = FindHiddenGroups(2);

					if (hiddenPairs > 0)
					{
						totalHiddenPairs += hiddenPairs;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (!RejectedStrategies[4])
				{
					// Look for hidden pairs
					int hiddenTriples = FindHiddenGroups(3);

					if (hiddenTriples > 0)
					{
						totalHiddenTriples += hiddenTriples;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (!RejectedStrategies[5])
				{
					// Look for naked triples
					int nakedQuads = FindNakedGroups(4);

					if (nakedQuads > 0)
					{
						totalNakedQuads += nakedQuads;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (!RejectedStrategies[6])
				{
					// Look for hidden pairs
					int hiddenQuads = FindHiddenGroups(4);

					if (hiddenQuads > 0)
					{
						totalHiddenQuads += hiddenQuads;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (!RejectedStrategies[7])
				{
					// Look for pointing pairs/triples
					int pointingPairs = FindPointingPairs();

					if (pointingPairs > 0)
					{
						totalPointingPairs += pointingPairs;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (!RejectedStrategies[8])
				{
					// Look for box line reductions
					int boxLineReductions = FindBoxLineReductions();

					if (boxLineReductions > 0)
					{
						totalBoxLineReductions += boxLineReductions;

						continue;
					}
				}
				else
				{
					useRandom = false;
				}

				if (useRandom && !RejectedStrategies[9])
				{
					// If we get here we have run out of basic human strategies to use and one of the more advanced sudoku solving strategies must be used
					// So lets just cheat and place a random number on the board
					PlaceRandomNumber();

					totalRandomPlacements += 1;

					continue;
				}

				if (printVerboseLogs)
				{
					UnityEngine.Debug.Log("Rejecting because a rejected strategy was needed to solve the puzzle");
				}

				// If we get here we have run out of stategies to try so return false so a new puzzle is generated
				return false;
			}

			List<int> totals = new List<int>()
			{
				totalHiddenSingles,
				totalNakedPairs,
				totalNakedTriples,
				totalHiddenPairs,
				totalHiddenTriples,
				totalNakedQuads,
				totalHiddenQuads,
				totalPointingPairs,
				totalBoxLineReductions,
				totalRandomPlacements
			};

			bool anyRequied = false;
			bool anyPass	= false;

			for (int i = 0; i < totals.Count; i++)
			{
				if (RejectedStrategies[i] || !RequiredStrategies[i])
				{
					continue;
				}

				anyRequied = true;

				if (totals[i] > 0)
				{
					anyPass = true;

					break;
				}
			}

			if (anyRequied && !anyPass)
			{
				if (printVerboseLogs)
				{
					UnityEngine.Debug.Log("Rejecting because a required strategy was not used");
				}

				return false;
			}

			if (printLogs)
			{
				string log = "";

				log += "totalSteps: " + totalSteps + "\n"; 
				log += "totalNakedSingles: " + totalNakedSingles + "\n"; 
				log += "totalHiddenSingles: " + totalHiddenSingles + "\n"; 
				log += "totalNakedPairs: " + totalNakedPairs + "\n"; 
				log += "totalNakedTriples: " + totalNakedTriples + "\n"; 
				log += "totalHiddenPairs: " + totalHiddenPairs + "\n"; 
				log += "totalHiddenTriples: " + totalHiddenTriples + "\n"; 
				log += "totalNakedQuads: " + totalNakedQuads + "\n"; 
				log += "totalHiddenQuads: " + totalHiddenQuads + "\n"; 
				log += "totalPointingPairs: " + totalPointingPairs + "\n"; 
				log += "totalBoxLineReductions: " + totalBoxLineReductions + "\n"; 
				log += "totalRandomPlacements: " + totalRandomPlacements + "\n"; 

				UnityEngine.Debug.Log(log);
			}

			return true;
		}

		/// <summary>
		/// Checks if the WorkingBoard has a number set on all cells
		/// </summary>
		private bool CheckSolved()
		{
			for (int r = 0; r < WorkingBoard.size; r++)
			{
				for (int c = 0; c < WorkingBoard.size; c++)
				{
					WorkingBoard.Cell cell = WorkingBoard.cells[r][c];

					if (!cell.HasNumber)
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Finds all cells where there is only one possible number and sets that as the cells number
		/// </summary>
		private int FindNakedSingles(bool printLogs = false)
		{
			int numNakedSingles = 0;

			for (int r = 0; r < WorkingBoard.size; r++)
			{
				for (int c = 0; c < WorkingBoard.size; c++)
				{
					WorkingBoard.Cell cell = WorkingBoard.cells[r][c];

					if (cell.HasNumber)
					{
						continue;
					}

					// Check if there is only one possible number, if so set it as the number now
					if (!cell.HasNumber && cell.possibleNumbersCount == 1)
					{
						WorkingBoard.SetNumber(r, c, cell.PossibleNumbers[0]);

						if (printLogs)
						{
							UnityEngine.Debug.Log("NAKED SINGLE at " + cell + "\n" + WorkingBoard);
						}

						numNakedSingles++;
					}
				}
			}

			return numNakedSingles;
		}

		/// <summary>
		/// Finds all cells where there is only one possible number and sets that as the cells number
		/// </summary>
		private int FindHiddenSingles()
		{
			int numHiddenSingles = 0;

			for (int r = 0; r < WorkingBoard.size; r++)
			{
				for (int c = 0; c < WorkingBoard.size; c++)
				{
					WorkingBoard.Cell cell = WorkingBoard.cells[r][c];

					if (cell.HasNumber)
					{
						continue;
					}

					if (FindHiddenSinglesHelper(cell, cell.rowSeenCells) ||
					    FindHiddenSinglesHelper(cell, cell.colSeenCells) ||
					    FindHiddenSinglesHelper(cell, cell.boxSeenCells))
					{
						if (printVerboseLogs)
						{
							UnityEngine.Debug.Log("HIDDEN SINGLE at " + cell + "\n" + WorkingBoard);
						}

						numHiddenSingles++;
					}
				}
			}

			return numHiddenSingles;
		}

		/// <summary>
		/// Checks if the given cells possible numbers contains exactly one number that doesn't appear in any of the seenCells possible numbers
		/// </summary>
		private bool FindHiddenSinglesHelper(WorkingBoard.Cell cell, List<WorkingBoard.Cell> seenCells)
		{
			// Get the list of possible numbers for the current cell
			List<int> possibleNumbers = cell.PossibleNumbers;

			// Check all the current cells seen cells for those possible numbers
			for (int i = 0; i < seenCells.Count; i++)
			{
				if (possibleNumbers.Count == 0)
				{
					// If there are no more possible numbers then we can break now
					break;
				}

				WorkingBoard.Cell seenCell = seenCells[i];

				if (seenCell.HasNumber)
				{
					continue;
				}

				for (int j = 0; j < possibleNumbers.Count; j++)
				{
					int possibleNumber = possibleNumbers[j];

					// Check if the seen cell also has this number as a possible number, if so remove it from the list
					if (seenCell.possibleNumbers[possibleNumber])
					{
						possibleNumbers.RemoveAt(j);
						j--;
					}
				}
			}

			if (possibleNumbers.Count == 1)
			{
				WorkingBoard.SetNumber(cell.row, cell.col, possibleNumbers[0]);

				return true;
			}

			return false;
		}

		/// <summary>
		/// Finds any naked group (groupSize == 2 is pair, groupSize == 3 is triple, groupSize == 4 is quad)
		/// </summary>
		private int FindNakedGroups(int groupSize)
		{
			int nakedGroups = 0;

			for (int i = 0; i < WorkingBoard.size; i++)
			{
				List<WorkingBoard.Cell> rowCells	= WorkingBoard.cells[i];
				List<WorkingBoard.Cell> colCells	= WorkingBoard.colCells[i];
				List<WorkingBoard.Cell> boxCells	= WorkingBoard.boxCells[i];

				HashSet<int>			groupNumbers	= new HashSet<int>();
				List<WorkingBoard.Cell>	groupCells		= new List<WorkingBoard.Cell>();

				if (FindNakedGroupCells("row", groupSize, rowCells, groupSize, 0, groupNumbers, groupCells))
				{
					bool anyRemoved = RemovePossibleNumbers(new List<int>(groupNumbers), rowCells, groupCells);

					if (anyRemoved)
					{
						nakedGroups++;

						if (printVerboseLogs)
						{
							UnityEngine.Debug.LogFormat("NAKED GROUP row at of size {0} at cells {1}\n{2}", groupSize, ListToStr(groupCells), WorkingBoard);
						}
					}
				}

				groupNumbers.Clear();
				groupCells.Clear();

				if (FindNakedGroupCells("col", groupSize, colCells, groupSize, 0, groupNumbers, groupCells))
				{
					bool anyRemoved = RemovePossibleNumbers(new List<int>(groupNumbers), colCells, groupCells);

					if (anyRemoved)
					{
						nakedGroups++;

						if (printVerboseLogs)
						{
							UnityEngine.Debug.LogFormat("NAKED GROUP col at of size {0} at cells {1}\n{2}", groupSize, ListToStr(groupCells), WorkingBoard);
						}
					}
				}

				groupNumbers.Clear();
				groupCells.Clear();

				if (FindNakedGroupCells("box", groupSize, boxCells, groupSize, 0, groupNumbers, groupCells))
				{
					bool anyRemoved = RemovePossibleNumbers(new List<int>(groupNumbers), boxCells, groupCells);

					if (anyRemoved)
					{
						nakedGroups++;

						if (printVerboseLogs)
						{
							UnityEngine.Debug.LogFormat("NAKED GROUP box at of size {0} at cells {1}\n{2}", groupSize, ListToStr(groupCells), WorkingBoard);
						}
					}
				}
			}

			return nakedGroups;
		}

		/// <summary>
		/// Tries to find a naked group in the given list of cells
		/// </summary>
		private bool FindNakedGroupCells(string id, int groupSize, List<WorkingBoard.Cell> cells, int numLeft, int startIndex, HashSet<int> groupNumbers, List<WorkingBoard.Cell> groupCells)
		{
			if (cells.Count - startIndex < numLeft)
			{
				// There aren't enough cells left
				return false;
			}

			for (int i = startIndex; i < cells.Count; i++)
			{
				WorkingBoard.Cell cell = cells[i];

				// Check if the amount of possible numbers in for this cell is greater than the size of the group, if so it cannot be part of the naked group
				if (cell.HasNumber || cell.possibleNumbersCount > groupSize)
				{
					continue;
				}

				string groupId = string.Format("{0}_{1}_{2}", id, groupSize, cell);

				// Check if we have already used this cell in a naked group of the given id/size
				if (cell.nakedGroups.Contains(groupId))
				{
					continue;
				}

				List<int> addedNumbers = new List<int>();

				// Add all the possible numbers to the numbers hash set, keeping track of the numbers that where added
				for (int number = 0; number < cell.possibleNumbers.Count; number++)
				{
					if (cell.possibleNumbers[number] && !groupNumbers.Contains(number))
					{
						groupNumbers.Add(number);
						addedNumbers.Add(number);
					}
				}

				// Check if we added the last cell and we have seen the correct amount of numbers
				if (numLeft == 1 && groupNumbers.Count == groupSize)
				{
					cell.nakedGroups.Add(groupId);

					groupCells.Add(cell);

					return true;
				}
				// Check if we have cells left to add and we haven't seen more numbers than required
				else if (numLeft > 1 && groupNumbers.Count <= groupSize)
				{
					if (FindNakedGroupCells(id, groupSize, cells, numLeft - 1, i + 1, groupNumbers, groupCells))
					{
						cell.nakedGroups.Add(groupId);

						groupCells.Add(cell);

						return true;
					}
				}

				// This cell doesn't contribute to any naked group, remove all the numbers that where added from the hashset
				for (int j = 0; j < addedNumbers.Count; j++)
				{
					groupNumbers.Remove(addedNumbers[j]);
				}
			}

			return false;
		}

		/// <summary>
		/// Finds any hidden group (groupSize == 2 is pair, groupSize == 3 is triple, groupSize == 4 is quad)
		/// </summary>
		private int FindHiddenGroups(int groupSize)
		{
			int hiddenGroups = 0;

			for (int i = 0; i < WorkingBoard.size; i++)
			{
				List<WorkingBoard.Cell> rowCells	= WorkingBoard.cells[i];
				List<WorkingBoard.Cell> colCells	= WorkingBoard.colCells[i];
				List<WorkingBoard.Cell> boxCells	= WorkingBoard.boxCells[i];

				List<WorkingBoard.Cell>	groupCells		= new List<WorkingBoard.Cell>();
				List<int>				groupNumbers	= new List<int>();

				if (FindHiddenGroupCells("row", groupSize, GetCellsGroupedByNumbers(rowCells), groupSize, 0, groupCells, groupNumbers))
				{
					bool anyRemoved = RemovePossibleNumbersForHiddenGroups(groupNumbers, groupCells);

					if (anyRemoved)
					{
						hiddenGroups++;

						if (printVerboseLogs)
						{
							UnityEngine.Debug.LogFormat("HIDDEN GROUP in row of size {0} at cells {1}\n{2}", groupSize, ListToStr(groupCells), WorkingBoard);
						}
					}
				}

				groupNumbers.Clear();
				groupCells.Clear();

				if (FindHiddenGroupCells("col", groupSize, GetCellsGroupedByNumbers(colCells), groupSize, 0, groupCells, groupNumbers))
				{
					bool anyRemoved = RemovePossibleNumbersForHiddenGroups(groupNumbers, groupCells);

					if (anyRemoved)
					{
						hiddenGroups++;

						if (printVerboseLogs)
						{
							UnityEngine.Debug.LogFormat("HIDDEN GROUP in col of size {0} at cells {1}\n{2}", groupSize, ListToStr(groupCells), WorkingBoard);
						}
					}
				}

				groupNumbers.Clear();
				groupCells.Clear();

				if (FindHiddenGroupCells("box", groupSize, GetCellsGroupedByNumbers(boxCells), groupSize, 0, groupCells, groupNumbers))
				{
					bool anyRemoved = RemovePossibleNumbersForHiddenGroups(groupNumbers, groupCells);

					if (anyRemoved)
					{
						hiddenGroups++;

						if (printVerboseLogs)
						{
							UnityEngine.Debug.LogFormat("HIDDEN GROUP in box of size {0} at cells {1}\n{2}", groupSize, ListToStr(groupCells), WorkingBoard);
						}
					}
				}
			}

			return hiddenGroups;
		}

		/// <summary>
		/// Finds any cells in the given cellsGroupedByNumber that are part of a hidden group (pair, triple, quad)
		/// </summary>
		private bool FindHiddenGroupCells(string id, int groupSize, List<List<WorkingBoard.Cell>> cellsGroupedByNumber, int numLeft, int startIndex, List<WorkingBoard.Cell> groupCells, List<int> groupNumbers)
		{
			if (cellsGroupedByNumber.Count - startIndex < numLeft)
			{
				return false;
			}

			for (int number = startIndex; number < cellsGroupedByNumber.Count; number++)
			{
				List<WorkingBoard.Cell> cells = cellsGroupedByNumber[number];

				if (cells.Count == 0 || cells.Count > groupSize)
				{
					continue;
				}

				string groupId = string.Format("{0}_{1}_{2}", id, groupSize, number);

				if (WorkingBoard.marked.Contains(groupId))
				{
					continue;
				}

				List<WorkingBoard.Cell> addedCells = new List<WorkingBoard.Cell>();

				for (int i = 0; i < cells.Count; i++)
				{
					WorkingBoard.Cell cell = cells[i];

					if (!groupCells.Contains(cell))
					{
						groupCells.Add(cell);
						addedCells.Add(cell);
					}
				}

				if (numLeft == 1 && groupCells.Count == groupSize)
				{
					groupNumbers.Add(number);

					WorkingBoard.marked.Add(groupId);

					return true;
				}
				else if (numLeft > 1 && groupCells.Count <= groupSize)
				{
					if (FindHiddenGroupCells(id, groupSize, cellsGroupedByNumber, numLeft - 1, number + 1, groupCells, groupNumbers))
					{
						groupNumbers.Add(number);

						WorkingBoard.marked.Add(groupId);

						return true;
					}
				}

				for (int j = 0; j < addedCells.Count; j++)
				{
					groupCells.Remove(addedCells[j]);
				}
			}

			return false;
		}

		/// <summary>
		/// Finds any pointing pairs/triples
		/// </summary>
		private int FindPointingPairs()
		{
			int numPointingPairs = 0;

			for (int b = 0; b < WorkingBoard.boxCells.Count; b++)
			{
				List<WorkingBoard.Cell>			boxCells				= WorkingBoard.boxCells[b];
				List<List<WorkingBoard.Cell>>	cellsGroupedByNumber	= GetCellsGroupedByNumbers(boxCells);

				// Look for numbers that belong in less than 4 cells
				for (int number = 0; number < cellsGroupedByNumber.Count; number++)
				{
					string markId = string.Format("pp_{0}_{1}", b, number);

					// Check if we already handled the pointing pair/triple for this number in this box
					if (WorkingBoard.marked.Contains(markId))
					{
						continue;
					}

					List<WorkingBoard.Cell> cells = cellsGroupedByNumber[number];

					// Check if the number belongs to 3 or less cells (It wont be 1 because it was then it would have been detected by the FindNakedSingles)
					if (cells.Count > 0 && cells.Count <= 3)
					{
						// Check if all the cells are in the same row, if so it is a pointing pair/triple
						if (SameRow(cells))
						{
							// Mark the pointing pair so it isn't checked again next time the loop runs
							WorkingBoard.marked.Add(markId);

							// Remove any possible numbers from other cells in the row
							if (RemovePossibleNumbers(new List<int>() { number }, cells[0].rowSeenCells, cells))
							{
								if (printVerboseLogs)
								{
									UnityEngine.Debug.Log("POINTING PAIR/TRIPLE in box " + b + " in row " + cells[0].row + " for number " + number + ", cells are: " + ListToStr(cells) + "\n" + WorkingBoard);
								}

								// Some possibilities were removed from other cells
								numPointingPairs++;
							}
						}
						// Check if all the cells are in the same column, if so it is a pointing pair/triple
						else if (SameCol(cells))
						{
							// Mark the pointing pair so it isn't checked again next time the loop runs
							WorkingBoard.marked.Add(markId);

							// Remove any possible numbers from other cells in the column
							if (RemovePossibleNumbers(new List<int>() { number }, cells[0].colSeenCells, cells))
							{
								if (printVerboseLogs)
								{
									UnityEngine.Debug.Log("POINTING PAIR/TRIPLE in box " + b + " in column " + cells[0].col + " for number " + number + ", cells are: " + ListToStr(cells) + "\n" + WorkingBoard);
								}

								// Some possibilities were removed from other cells
								numPointingPairs++;
							}
						}
					}
				}
			}

			return numPointingPairs;
		}

		/// <summary>
		/// Finds any box line reductions
		/// </summary>
		private int FindBoxLineReductions()
		{
			int numBoxLineReductions = 0;

			for (int i = 0; i < WorkingBoard.size; i++)
			{
				numBoxLineReductions += BoxLineReductionHelper("row_" + i, WorkingBoard.cells[i]);
				numBoxLineReductions += BoxLineReductionHelper("col_" + i, WorkingBoard.colCells[i]);
			}

			return numBoxLineReductions;
		}

		private int BoxLineReductionHelper(string id, List<WorkingBoard.Cell> cells)
		{
			int numBoxLineReductions = 0;

			List<List<WorkingBoard.Cell>> cellsGroupedByNumber = GetCellsGroupedByNumbers(cells);

			for (int number = 0; number < cellsGroupedByNumber.Count; number++)
			{
				string markId = string.Format("blr_{0}_{1}", id, number);

				// Check if we already handled the box line reductions for this number in this box
				if (WorkingBoard.marked.Contains(markId))
				{
					continue;
				}

				List<WorkingBoard.Cell> groupedCells = cellsGroupedByNumber[number];

				if (groupedCells.Count > 0 && groupedCells.Count <= 3 && SameBox(groupedCells))
				{
					// Mark the pointing pair so it isn't checked again next time the loop runs
					WorkingBoard.marked.Add(markId);

					// Remove any possible numbers from other cells in the box
					if (RemovePossibleNumbers(new List<int>() { number }, groupedCells[0].boxSeenCells, groupedCells))
					{
						if (printVerboseLogs)
						{
							UnityEngine.Debug.Log("BOX LINE REDUCTION in box " + groupedCells[0].boxIndex + " / " + id + " for number " + number + ", cells are: " + ListToStr(groupedCells) + "\n" + WorkingBoard);
						}

						// Some possibilities were removed from other cells
						numBoxLineReductions++;
					}
				}
			}

			return numBoxLineReductions;
		}

		/// <summary>
		/// Checks if all cells are on the same row
		/// </summary>
		private bool SameRow(List<WorkingBoard.Cell> cells)
		{
			for (int i = 0; i < cells.Count - 1; i++)
			{
				if (cells[i].row != cells[i + 1].row)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Checks if all cells are on the same column
		/// </summary>
		private bool SameCol(List<WorkingBoard.Cell> cells)
		{
			for (int i = 0; i < cells.Count - 1; i++)
			{
				if (cells[i].col != cells[i + 1].col)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Checks if all cells are in the same box
		/// </summary>
		private bool SameBox(List<WorkingBoard.Cell> cells)
		{
			for (int i = 0; i < cells.Count - 1; i++)
			{
				if (cells[i].boxIndex != cells[i + 1].boxIndex)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Returns all cells grouped by number
		/// </summary>
		private List<List<WorkingBoard.Cell>> GetCellsGroupedByNumbers(List<WorkingBoard.Cell> cells)
		{
			List<List<WorkingBoard.Cell>> cellsGroupedByNumber = new List<List<WorkingBoard.Cell>>();

			// Add a new list for each number
			for (int i = 0; i < WorkingBoard.size; i++)
			{
				cellsGroupedByNumber.Add(new List<WorkingBoard.Cell>());
			}

			for (int i = 0; i < cells.Count; i++)
			{
				WorkingBoard.Cell cell = cells[i];

				if (cell.HasNumber)
				{
					continue;
				}

				for (int j = 0; j < cell.possibleNumbers.Count; j++)
				{
					if (cell.possibleNumbers[j])
					{
						cellsGroupedByNumber[j].Add(cell);
					}
				}
			}

			return cellsGroupedByNumber;
		}

		/// <summary>
		/// Removes all possible numbers for the given cells that appear in the possibleNumbers list
		/// </summary>
		private bool RemovePossibleNumbers(List<int> numbers, List<WorkingBoard.Cell> cells, List<WorkingBoard.Cell> excludeCells)
		{
			bool anyRemoved = false;

			for (int i = 0; i < cells.Count; i++)
			{
				WorkingBoard.Cell cell = cells[i];

				if (cell.HasNumber || excludeCells.Contains(cell))
				{
					continue;
				}

				for (int j = 0; j < numbers.Count; j++)
				{
					int number = numbers[j];

					if (cell.possibleNumbers[number])
					{
						cell.possibleNumbers[number] = false;
						cell.possibleNumbersCount--;

						anyRemoved = true;
					}
				}
			}

			return anyRemoved;
		}

		/// <summary>
		/// Removes all possible numbers in the given cells take are not in the given numbers list
		/// </summary>
		private bool RemovePossibleNumbersForHiddenGroups(List<int> numbers, List<WorkingBoard.Cell> cells)
		{
			bool anyRemoved = false;

			for (int i = 0; i < cells.Count; i++)
			{
				WorkingBoard.Cell cell = cells[i];

				for (int j = 0; j < cell.possibleNumbers.Count; j++)
				{
					if (cell.possibleNumbers[j] && !numbers.Contains(j))
					{
						cell.possibleNumbers[j] = false;
						cell.possibleNumbersCount--;

						anyRemoved = true;
					}
				}
			}

			return anyRemoved;
		}

		/// <summary>
		/// Lists to string.
		/// </summary>
		private string ListToStr(List<WorkingBoard.Cell> cells)
		{
			string str = "";

			for (int i = 0; i < cells.Count; i++)
			{
				if (i != 0) str += " ";
				str += string.Format("[{0},{1}]", cells[i].row, cells[i].col);
			}

			return str;
		}

		/// <summary>
		/// Places a random number on the board
		/// </summary>
		private void PlaceRandomNumber()
		{
			List<WorkingBoard.Cell> blankCells = new List<WorkingBoard.Cell>();

			// Gather all blank cells with the least amount of possible numbers
			for (int r = 0; r < WorkingBoard.size; r++)
			{
				for (int c = 0; c < WorkingBoard.size; c++)
				{
					WorkingBoard.Cell cell = WorkingBoard.cells[r][c];

					if (!cell.HasNumber)
					{
						if (blankCells.Count == 0 || cell.possibleNumbersCount < blankCells[0].possibleNumbersCount)
						{
							blankCells.Clear();
							blankCells.Add(cell);
						}
						else if (cell.possibleNumbersCount == blankCells[0].possibleNumbersCount)
						{
							blankCells.Add(cell);
						}
					}
				}
			}

			// Pick a random cell to show the number on
			WorkingBoard.Cell cellToShow = blankCells[rand.Next(0, blankCells.Count)];

			// Get the number that is suppose to go on this cell
			int number = WorkingBoard.solvedState[cellToShow.row][cellToShow.col];

			if (printVerboseLogs)
			{
				UnityEngine.Debug.LogFormat("RANDOM NUMBER {0} on cell {1}\n{2}", number, cellToShow, WorkingBoard);
			}

			// Set the number on the board
			WorkingBoard.SetNumber(cellToShow.row, cellToShow.col, number);
		}

		#endregion
	}
}
