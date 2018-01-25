using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using TappxSDK;

public class mobileAds : MonoBehaviour {

	public static mobileAds Instance{ set; get; }

	private BannerView bannerView;
	private RewardBasedVideoAd rewardBasedVideo;

	public void Start()
	{
		Instance = this;
		DontDestroyOnLoad (gameObject);

		#if UNITY_ANDROID
			string appId = "ca-app-pub-2631128054673594~7386716496";
		#else
			string appId = "unexpected_platform";
		#endif

		MobileAds.Initialize(appId);
	
		mobileAds.Instance.rewardBasedVideo = RewardBasedVideoAd.Instance;

		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;

		mobileAds.Instance.RequestRewardedVideo();

		mobileAds.Instance.RequestBanner ();

	}

	private void RequestBanner()
	{
		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-2631128054673594/9787317425";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		BannerView bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

		AdRequest request = new AdRequest.Builder().Build();

		bannerView.LoadAd(request);

	}

	private void RequestRewardedVideo()
	{
		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-2631128054673594/2979927964";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		AdRequest request = new AdRequest.Builder ().Build ();

		mobileAds.Instance.rewardBasedVideo.LoadAd (request, adUnitId);
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		CloudVariables.SetImportantValues (0, 3);
		PlayGamesScript.Instance.SaveData ();
		SceneManager.LoadScene ("gameIn");
	}

	public void GameOver()
	{
		if (mobileAds.Instance.rewardBasedVideo.IsLoaded()) {
			mobileAds.Instance.rewardBasedVideo.Show();
		}
	}
}
