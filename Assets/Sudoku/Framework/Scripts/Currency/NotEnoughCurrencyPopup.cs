using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BizzyBeeGames
{
	public class NotEnoughCurrencyPopup : Popup
	{
		#region Inspector Variables

		[Space]

		[SerializeField] private Text		titleText			= null;
		[SerializeField] private Text		messageText			= null;
		[SerializeField] private Text		rewardAdButtonText	= null;
		[SerializeField] private GameObject rewardAdButton		= null;
		[SerializeField] private GameObject storeButton			= null;
		[SerializeField] private GameObject buttonsContainer	= null;

		#endregion

		#region Member Variables

		private string	currencyId;
		private int		rewardAmount;

		#endregion

		#region Public Methods

		public override void OnShowing(object[] inData)
		{
			base.OnShowing(inData);

			currencyId = inData[0] as string;

			titleText.text		= inData[1] as string;
			messageText.text	= inData[2] as string;

			bool showStoreButton	= (bool)inData[3];
			bool showRewardAdButton	= (bool)inData[4] && MobileAdsManager.Instance.AreRewardAdsEnabled;

			rewardAdButtonText.text = inData[5] as string;

			rewardAmount = (int)inData[6];

			storeButton.SetActive(showStoreButton);
			buttonsContainer.SetActive(showRewardAdButton || showStoreButton);

			if (showRewardAdButton)
			{
				rewardAdButton.SetActive(MobileAdsManager.Instance.RewardAdState == AdNetworkHandler.AdState.Loaded);

				MobileAdsManager.Instance.OnRewardAdLoaded	+= OnRewardAdLoaded;
				MobileAdsManager.Instance.OnAdsRemoved		+= OnAdsRemoved;
			}
			else
			{
				rewardAdButton.SetActive(false);

				MobileAdsManager.Instance.OnRewardAdLoaded	-= OnRewardAdLoaded;
				MobileAdsManager.Instance.OnAdsRemoved		-= OnAdsRemoved;
			}
		}

		public void OnRewardAdButtonClick()
		{
			if (MobileAdsManager.Instance.RewardAdState != AdNetworkHandler.AdState.Loaded)
			{
				rewardAdButton.SetActive(false);

				Debug.LogError("[NotEnoughCurrencyPopup] The reward button was clicked but there is no ad loaded to show.");

				return;
			}

			MobileAdsManager.Instance.ShowRewardAd(OnRewardAdClosed, OnRewardAdGranted);

			Hide(false);
		}

		#endregion

		#region Private Methods

		private void OnRewardAdLoaded()
		{
			rewardAdButton.SetActive(true);
		}

		private void OnRewardAdClosed()
		{
			rewardAdButton.SetActive(false);
		}

		private void OnRewardAdGranted(string rewardId, double amount)
		{
			// Give the currency to the player
			CurrencyManager.Instance.Give(currencyId, rewardAmount);
		}

		private void OnAdsRemoved()
		{
			MobileAdsManager.Instance.OnRewardAdLoaded -= OnRewardAdLoaded;
			rewardAdButton.SetActive(false);
		}

		#endregion
	}
}
