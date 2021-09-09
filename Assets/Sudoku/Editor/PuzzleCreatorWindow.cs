using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

namespace BizzyBeeGames.Sudoku
{
	public class PuzzleCreatorWindow : EditorWindow
	{
		#region Member Variables

		private int					boxRows;
		private int					boxColumns;
		private int					desiredNumClues;
		private bool				forceDesiredNumClues;
		private int					numPuzzlesToCreate;
		private int					seed;
		private float				restartTimeout;
		private string				filenamePrefix;
		private Object				outputFolder;

		private bool				rejectedStrategiesFoldout;
		private bool				requiredStrategiesFoldout;
		private List<bool>			rejectedStrategies;
		private List<bool>			requiredStrategies;
		private List<string>		strategyLabels;

		private SudokuCreatorWorker	worker;
		private int					numPuzzlesCreated;

		private Vector2 windowScrollPosition;

		#endregion

		#region Properties

		private string OutputFolderAssetPath
		{
			get { return EditorPrefs.GetString("OutputFolderAssetPath", ""); }
			set { EditorPrefs.SetString("OutputFolderAssetPath", value); }
		}

		#endregion

		[MenuItem("Tools/Bizzy Bee Games/Sudoku Puzzle Creator", priority = 100)]
		public static void Init()
		{
			EditorWindow.GetWindow<PuzzleCreatorWindow>("Puzzle Creator");
		}

		#region Unity Methods

		private void OnEnable()
		{
			rejectedStrategiesFoldout = EditorPrefs.GetBool("rejectedStrategiesFoldout", false);
			requiredStrategiesFoldout = EditorPrefs.GetBool("requiredStrategiesFoldout", false);

			strategyLabels = new List<string>()
			{
				"Hidden singles",
				"Naked Pairs",
				"Naked Triples",
				"Hidden Pairs",
				"Hidden Triples",
				"Naked Quads",
				"Hidden Quads",
				"Pointing Pairs/Triples",
				"Box Line Reductions",
				"Random"
			};

			rejectedStrategies	= new List<bool>();
			requiredStrategies	= new List<bool>();

			for (int i = 0; i < strategyLabels.Count; i++)
			{
				rejectedStrategies.Add(EditorPrefs.GetBool("rejectedStrategies_" + i));
				requiredStrategies.Add(EditorPrefs.GetBool("requiredStrategies_" + i));
			}
		}

		private void OnDisable()
		{
			EditorPrefs.SetBool("rejectedStrategiesFoldout", rejectedStrategiesFoldout);
			EditorPrefs.SetBool("requiredStrategiesFoldout", requiredStrategiesFoldout); 

			for (int i = 0; i < strategyLabels.Count; i++)
			{
				EditorPrefs.SetBool("rejectedStrategies_" + i, rejectedStrategies[i]);
				EditorPrefs.SetBool("requiredStrategies_" + i, requiredStrategies[i]);
			}
		}

		private void Update()
		{
			if (worker != null)
			{
				if (worker.Stopped)
				{
					WorkerFinishedGeneratingPuzzle();
				}
				else
				{
					bool cancelled = EditorUtility.DisplayCancelableProgressBar("Generating Sudoku Puzzles", string.Format("Creating puzzle {0} of {1}", numPuzzlesCreated + 1, numPuzzlesToCreate), (float)numPuzzlesCreated / (float)numPuzzlesToCreate);

					if (cancelled)
					{
						worker.Stop();

						StoppedGeneratingPuzzles();

						Debug.Log("User cancelled puzzle generation");
					}
				}
			}
		}

		private void OnGUI()
		{
			windowScrollPosition = EditorGUILayout.BeginScrollView(windowScrollPosition);

			EditorGUILayout.Space();

			boxRows					= Mathf.Max(1, EditorGUILayout.IntField("Box Rows", boxRows));
			boxColumns				= Mathf.Max(1, EditorGUILayout.IntField("Box Columns", boxColumns));
			EditorGUILayout.HelpBox(string.Format("This will create a board of size {0}x{0}", boxRows * boxColumns), MessageType.None);

			EditorGUILayout.Space();

			desiredNumClues			= Mathf.Max(1, EditorGUILayout.IntField("Desired Number Of Clues", desiredNumClues));
			forceDesiredNumClues	= EditorGUILayout.Toggle("Force Number Of Clues", forceDesiredNumClues);

			DrawRejectedStrategies();
			DrawRequiredStrategies();

			EditorGUILayout.Space();

			numPuzzlesToCreate	= Mathf.Max(0, EditorGUILayout.IntField("Num Puzzles To Create", numPuzzlesToCreate));
			restartTimeout		= Mathf.Max(0, EditorGUILayout.FloatField("Restart Timeout", restartTimeout));

			if (numPuzzlesToCreate == 1)
			{
				seed = Mathf.Max(0, EditorGUILayout.IntField("Seed", seed));
			}
			else
			{
				seed = 0;
			}

			EditorGUILayout.Space();

			filenamePrefix	= EditorGUILayout.TextField("Filename Prefix", filenamePrefix);
			outputFolder	= EditorGUILayout.ObjectField("Output Folder", outputFolder, typeof(Object), false);

			OutputFolderAssetPath = (outputFolder != null) ? AssetDatabase.GetAssetPath(outputFolder) : null;

			if (outputFolder != null)
			{
				if (!CheckOutputFolder())
				{
					EditorGUILayout.HelpBox("Output Folder must be a folder from the Project window.", MessageType.Error);
				}
			}
			else
			{
				EditorGUILayout.HelpBox("Level files will be placed in the Assets/Resources folder", MessageType.None);
			}

			if (worker != null)
			{
				GUI.enabled = false;
			}

			EditorGUILayout.Space();

			if (GUILayout.Button("Generate levels"))
			{
				StartGeneratingPuzzles();
			}

			GUI.enabled = true;
			
			EditorGUILayout.Space();

			EditorGUILayout.EndScrollView();
		}

		private void DrawRejectedStrategies()
		{
			rejectedStrategiesFoldout	= EditorGUILayout.Foldout(rejectedStrategiesFoldout, "Rejected Strategies");

			if (rejectedStrategiesFoldout)
			{
				EditorGUI.indentLevel++;

				EditorGUILayout.HelpBox("If a strategy is selected and that strategy must be used in order to solve a puzzle then the puzzle will be rejected and it will try again with another randomly generated puzzle", MessageType.None);

				bool anyChecked = false;

				for (int i = 0; i < rejectedStrategies.Count; i++)
				{
					if (i == rejectedStrategies.Count - 1)
					{
						if (anyChecked)
						{
							GUI.enabled = false;

							// If any strategy is rejected then we also have to reject randomly placing numbers because randomly placing will always
							// lead to a board being solved
							EditorGUILayout.Toggle(strategyLabels[i], true);

							GUI.enabled = true;

							continue;
						}
					}

					rejectedStrategies[i] = EditorGUILayout.Toggle(strategyLabels[i], rejectedStrategies[i]);

					if (rejectedStrategies[i])
					{
						anyChecked = true;
					}
				}

				EditorGUI.indentLevel--;
			}
		}

		private void DrawRequiredStrategies()
		{
			requiredStrategiesFoldout = EditorGUILayout.Foldout(requiredStrategiesFoldout, "Required Strategies");

			if (requiredStrategiesFoldout)
			{
				EditorGUI.indentLevel++;

				EditorGUILayout.HelpBox("If a selected strategy is not used when solving the puzzle then the puzzle is rejected and it will try again with another randomly generated puzzle.", MessageType.None);

				for (int i = 0; i < requiredStrategies.Count; i++)
				{
					if (rejectedStrategies[i])
					{
						GUI.enabled = false;

						// You can't require a rejected strategy
						EditorGUILayout.Toggle(strategyLabels[i], false);

						GUI.enabled = true;

						continue;
					}

					requiredStrategies[i] = EditorGUILayout.Toggle(strategyLabels[i], requiredStrategies[i]);
				}

				EditorGUI.indentLevel--;
			}
		}

		#endregion

		#region Private Methods

		private bool CheckOutputFolder()
		{
			string assetPath	= AssetDatabase.GetAssetPath(outputFolder);
			string fullPath		= Application.dataPath + assetPath.Substring("Assets".Length);

			return System.IO.Directory.Exists(fullPath);
		}

		private void StartGeneratingPuzzles()
		{
			numPuzzlesCreated = 0;

			GenerateNextPuzzle();
		}

		private void StoppedGeneratingPuzzles()
		{
			worker.Stop();
			worker = null;
			EditorUtility.ClearProgressBar();
		}

		private void GenerateNextPuzzle()
		{
			// Create a new worker object and set the starting values
			worker = new SudokuCreatorWorker();

			worker.BoxRows				= boxRows;
			worker.BoxCols				= boxColumns;
			worker.DesiredNumClues		= desiredNumClues;
			worker.ForceNumClues		= forceDesiredNumClues;
			worker.RejectedStrategies	= rejectedStrategies;
			worker.RequiredStrategies	= requiredStrategies;
			worker.Seed					= (seed == 0) ? Random.Range(0, int.MaxValue) : seed;
			worker.RestartTimeout		= restartTimeout;

			// Starts the worker on a new thread
			worker.StartWorker();
		}

		private void WorkerFinishedGeneratingPuzzle()
		{
			if (!string.IsNullOrEmpty(worker.Error))
			{
				Debug.LogError(worker.Error);

				StoppedGeneratingPuzzles();

				return;
			}

			numPuzzlesCreated++;

			WriteToFile(worker.WorkingBoard, worker.Seed);

			if (numPuzzlesCreated < numPuzzlesToCreate)
			{
				GenerateNextPuzzle();
			}
			else
			{
				StoppedGeneratingPuzzles();
			}
		}

		private void WriteToFile(WorkingBoard workingBoard, int seedUsed)
		{
			List<string> data = new List<string>();

			data.Add(seedUsed.ToString());
			data.Add(workingBoard.boxRows.ToString());
			data.Add(workingBoard.boxCols.ToString());

			for (int r = 0; r < workingBoard.size; r++)
			{
				for (int c = 0; c < workingBoard.size; c++)
				{
					int		number = workingBoard.solvedState[r][c];
					bool	isClue = workingBoard.unSolvedState[r][c] != -1;

					data.Add(string.Format("{0}{1}", (isClue ? "t" : ""), number));
				}
			}

			string fileContents = "";

			for (int i = 0; i < data.Count; i++)
			{
				if (i != 0)
				{
					fileContents += ",";
				}

				fileContents += data[i];
			}

			string folderPath	= Application.dataPath + (string.IsNullOrEmpty(OutputFolderAssetPath) ? "/Resources" : OutputFolderAssetPath.Substring("Assets".Length));
			string filename		= (!string.IsNullOrEmpty(filenamePrefix) ? filenamePrefix + "_" : "") + numPuzzlesCreated + ".txt";
			string filePath		= folderPath + "/" + filename;

			if (!System.IO.Directory.Exists(folderPath))
			{
				System.IO.Directory.CreateDirectory(folderPath);
			}

			System.IO.File.WriteAllText(filePath, fileContents);

			AssetDatabase.Refresh();
		}

		#endregion
	}
}
