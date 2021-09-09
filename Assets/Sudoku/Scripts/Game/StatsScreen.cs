using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BizzyBeeGames.Sudoku
{
	public class StatsScreen : Screen
	{
		#region Inspector Variables

		[Space]

		[SerializeField] private StatsListItem	statsListItemPrefab	= null;
		[SerializeField] private Transform		statsListContainer	= null;

		#endregion

		#region Member Variables

		private ObjectPool statsListItemPool;

		#endregion

		#region Public Methods

		public override void Initialize()
		{
			base.Initialize();

			statsListItemPool = new ObjectPool(statsListItemPrefab.gameObject, 4, statsListContainer);
		}

		public override void Show(bool back, bool immediate)
		{
			base.Show(back, immediate);

			statsListItemPool.ReturnAllObjectsToPool();

			for (int i = 0; i < GameManager.Instance.PuzzleGroupDatas.Count; i++)
			{
				statsListItemPool.GetObject<StatsListItem>().Setup(GameManager.Instance.PuzzleGroupDatas[i]);
			}
		}

		#endregion
	}
}
