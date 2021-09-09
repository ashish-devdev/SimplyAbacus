using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using BizzyBeeGames.Sudoku;

namespace BizzyBeeGames
{
	[RequireComponent(typeof(Button))]
	public class ClickableListItem : UIMonoBehaviour,IPointerUpHandler,IDropHandler,IPointerDownHandler,IDragHandler
	{
		#region Member Variables

		private Button uiButton;

		#endregion

		#region Properties

		public int							Index				{ get; set; }
		public object						Data				{ get; set; }
		public System.Action<int, object>	OnListItemClicked	{ get; set; }

		/// <summary>
		/// Gets the Button component attached to this GameObject
		/// </summary>
		private Button UIButton
		{
			get
			{
				if (uiButton == null)
				{
					uiButton = gameObject.GetComponent<Button>();
				}

				return uiButton;
			}
		}

		#endregion

		#region Unity Methods

		private void Start()
		{
			if (UIButton != null)
			{
				UIButton.onClick.AddListener(OnButtonClicked);
				
			}
			else
			{
				Debug.LogError("[ClickableListItem] There is no Button component on this GameObject.");
			}
		}

		#endregion

		#region Private Methods

		private void OnButtonClicked()
		{
			if (OnListItemClicked != null)
			{
				OnListItemClicked(Index, Data);
			}
			else
			{
				Debug.LogWarning("[ClickableListItem] OnListItemClicked has not been set on object " + gameObject.name);
			}
		}

        public void OnPointerUp(PointerEventData eventData)
        {
			//  throw new System.NotImplementedException();
			//print(gameObject.name);
			//OnButtonClicked();
			Invoke(nameof(MakeClickedOnNumbersFalse), 0.1f);
			//PuzzleBoard.clickedOnNumber = false;

		}

		public void OnPointerDown(PointerEventData eventData)
        {
			
		}

        public void OnDrop(PointerEventData eventData)
        {
			print(gameObject.name);
			OnButtonClicked();
			PuzzleBoard.clickedOnNumber = false;
		}

        public void OnDrag(PointerEventData eventData)
        {
			print(gameObject.name);
			OnButtonClicked();
		}

		public void MakeClickedOnNumbersFalse()
		{
			PuzzleBoard.clickedOnNumber = false;
		}




        #endregion
    }
}
