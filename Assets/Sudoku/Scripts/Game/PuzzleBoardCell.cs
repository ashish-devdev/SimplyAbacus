using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames.Sudoku
{
	public class PuzzleBoardCell : ClickableListItem
	{
		#region Inspector Variables

		[SerializeField] public Text				numberText		= null;
		[SerializeField] public Image				cellBackground	= null;
		[SerializeField] public LeanGraphicColor  bGColorRedToWhiteFade= null;

		[SerializeField] private GridLayoutGroup	notesContainer	= null;

		[Header("Borders")]
		[SerializeField] private Image	leftBorder		= null;
		[SerializeField] private Image	rightBorder		= null;
		[SerializeField] private Image	topBorder		= null;
		[SerializeField] private Image	bottomBorder	= null;

		[Header("Grid Lines")]
		[SerializeField] private Image	leftGridLine	= null;
		[SerializeField] private Image	topGridLine		= null;

		[Header("Number Text Colors")]
		[SerializeField] private string clueTextColor		= "";
		[SerializeField] private string placedTextColor		= "";
		[SerializeField] private string hintGivenTextColor	= "";
		[SerializeField] private string incorrectTextColor	= "";

		[Header("Background Colors")]
		[SerializeField] private string defaultColor			= "";
		[SerializeField] private string selectedPrimaryColor	= "";
		[SerializeField] private string selectedSecondaryColor	= "";
		[SerializeField] private string highlightedColor		= "";

		#endregion

		#region Member Variables

		private List<GameObject> noteTextObjs;

		#endregion

		#region Properties

		public int Number { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Sets the cell blank (No number)
		/// </summary>
		public void SetBlank()
		{
			Number			= -1;
			numberText.text	= "";
		}

		/// <summary>
		/// Sets the number that should be displayed in this cell
		/// </summary>
		public void SetNumber(int number)
		{
			Number = number;

			// Set the number text
			numberText.text = number.ToString();
		}

		/// <summary>
		/// Sets the border lines for this cell visible
		/// </summary>
		public void SetBorders(bool left, bool right, bool top, bool bottom)
		{
			leftBorder.enabled		= left;
			rightBorder.enabled		= right;
			topBorder.enabled		= top;
			bottomBorder.enabled	= bottom;
		}

		/// <summary>
		/// Sets the grid lines for this cell visible
		/// </summary>
		public void SetGridLines(bool left, bool top)
		{
			leftGridLine.enabled	= left;
			topGridLine.enabled		= top;
		}

		/// <summary>
		/// Sets the text color to the clue color
		/// </summary>
		public void SetTextClue()
		{
			numberText.color = GetThemeColor(clueTextColor);
		}

		/// <summary>
		/// Sets the text color to the placed text color
		/// </summary>
		public void SetTextPlaced()
		{
			numberText.color = GetThemeColor(placedTextColor);
		}

		/// <summary>
		/// Sets the text color to the highlighted text color
		/// </summary>
		public void SetTextHint()
		{
			numberText.color = GetThemeColor(hintGivenTextColor);
		}

		/// <summary>
		/// Sets the text color to the incorrect text color
		/// </summary>
		public void SetTextIncorrect()
		{
			numberText.color = GetThemeColor(incorrectTextColor);
		}

		/// <summary>
		/// Sets the background color to the default unselected color
		/// </summary>
		public void SetUnselected()
		{
			cellBackground.color = GetThemeColor(defaultColor);
		}

		/// <summary>
		/// Sets the background color to the selected primary color
		/// </summary>
		public void SetSelectedPrimary()
		{
			cellBackground.color = GetThemeColor(selectedPrimaryColor);
		}

		/// <summary>
		/// Sets the background color to the selected secondary color
		/// </summary>
		public void SetSelectedSecondary()
		{
			cellBackground.color = GetThemeColor(selectedSecondaryColor);
		}

		/// <summary>
		/// Sets the background color to the selected highlighted color
		/// </summary>
		public void SetHighlighted()
		{
			cellBackground.color = GetThemeColor(highlightedColor);
		}

		/// <summary>
		/// Sets up the notesContainer to contain the required number of text object containers
		/// </summary>
		public void SetupNotesContainer(int boxRows, int boxCols)
		{
			// Set the cell size of the notes container
			Vector2	containerSize	= (notesContainer.transform as RectTransform).sizeDelta;
			float	cellWidth		= (containerSize.x - notesContainer.padding.left - notesContainer.padding.right) / boxCols;
			float	cellHeight		= (containerSize.y - notesContainer.padding.top - notesContainer.padding.bottom) / boxRows;

			notesContainer.cellSize = new Vector2(cellWidth, cellHeight);

			// If this is the first time this cell is setting up then we need to create the noteTextObjs list
			if (noteTextObjs == null)
			{
				noteTextObjs = new List<GameObject>();
			}

			// Make sure there is an active GameObject for each of the cells
			int num = boxRows * boxCols;

			for (int i = 0; i < num || i < noteTextObjs.Count; i++)
			{
				if (i < num)
				{
					if (i >= noteTextObjs.Count)
					{
						GameObject textObj = new GameObject("text_obj", typeof(RectTransform));

						textObj.transform.SetParent(notesContainer.transform, false);

						noteTextObjs.Add(textObj);
					}

					noteTextObjs[i].SetActive(true);
				}
				else
				{
					noteTextObjs[i].SetActive(false);
				}
			}
		}

		/// <summary>
		/// Adds a note for the given number
		/// </summary>
		public void AddNote(int number, ObjectPool notesPool)
		{
			if (number - 1 < noteTextObjs.Count)
			{
				GameObject	textObj		= noteTextObjs[number - 1];
				Text		noteText	= notesPool.GetObject<Text>(textObj.transform);

				noteText.text = number.ToString();
			}
		}

		/// <summary>
		/// Removes the note for the given number if it is visible
		/// </summary>
		public void RemoveNote(int number)
		{
			if (number - 1 < noteTextObjs.Count)
			{
				GameObject textObj = noteTextObjs[number - 1];

				// Check if a not has been added for this number
				if (textObj.transform.childCount == 1)
				{
					// Return the note object ot its pool
					ObjectPool.ReturnObjectToPool(textObj.transform.GetChild(0).gameObject);
				}
			}
		}

		/// <summary>
		/// Removes all notes for the cell
		/// </summary>
		public void RemoveAllNotes()
		{
			for (int i = 1; i <= noteTextObjs.Count; i++)
			{
				RemoveNote(i);
			}
		}

		#endregion

		#region Private Methods

		private Color GetThemeColor(string itemId)
		{
			Color color = Color.white;

			ThemeManager.Instance.GetItemColor(itemId, out color);

			return color;
		}

		#endregion
	}
}
