using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    public bool TestAds = false;        
    public bool unityAds = true;        

    private static BannerView bannerView;
    private InterstitialAd interstitialView;
    RewardBasedVideoAd rewardBasedVideoAd;


    public static bool firstTime = true;

    public static AdManager admanagerInstance = null;

   
    [SerializeField] private string appID = "";
    [SerializeField] private string bannerID = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] private string interstitialID = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] private string rewardVideoID = "ca-app-pub-3940256099942544/5224354917";

    void Awake()
    {
        if (admanagerInstance == null)
        {
            admanagerInstance = this;
        }
        else if (admanagerInstance != this)
        {
            Destroy(gameObject);
        }

        if (firstTime)
        {
            firstTime = false;
            DontDestroyOnLoad(gameObject);

            MobileAds.Initialize(appID);
            RequestInterstitial();
            admanagerInstance.RequestBanner();
        }
    }

    void Start()
    {
        
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        
        bannerView.OnAdOpening += HandleOnAdOpened;
        
        bannerView.OnAdClosed += HandleOnAdClosed;
        
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        
        admanagerInstance.rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        
        rewardBasedVideoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
        
        rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        
        rewardBasedVideoAd.OnAdOpening += HandleRewardBasedVideoOpened;
        
        rewardBasedVideoAd.OnAdStarted += HandleRewardBasedVideoStarted;
        
        rewardBasedVideoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
        
        rewardBasedVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;
        
        rewardBasedVideoAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        
        admanagerInstance.RequestRewardBasedVideo();
    }

    #region AdmobBannerCallBackEvents
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //ShowAdmobBanner();
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //		RequestBanner ();
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //		bannerView.Destroy ();
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
    #endregion

        public void ShowAdmobBanner()
    {
        bannerView.Show();
    }

        public void HideAdmobBanner()
    {
        bannerView.Hide();
    }

    
    public void ShowAdmobInterstitial()
    {
        if (admanagerInstance.interstitialView.IsLoaded())
            admanagerInstance.interstitialView.Show();

        RequestInterstitial();
    }

    public void ShowAdmobRewardVideo()
    {
        if (rewardBasedVideoAd.IsLoaded())
        {
            rewardBasedVideoAd.Show();
        }
    }


    #region AdmobRequests
    private void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = null;
        if (!TestAds)
            request = new AdRequest.Builder().Build();

        if (TestAds)
        {
            request = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
            .AddTestDevice(SystemInfo.deviceUniqueIdentifier)  // My test device.
            .Build();
        }
        bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        if (admanagerInstance.interstitialView != null)
        {
            admanagerInstance.interstitialView.Destroy();
        }

        admanagerInstance.interstitialView = new InterstitialAd(interstitialID);        //orignal
                                                                                        // Create an empty ad request.

        AdRequest request = null;

        if (!TestAds)
            request = new AdRequest.Builder().Build();

        if (TestAds)
        {
            request = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
            .AddTestDevice(SystemInfo.deviceUniqueIdentifier)  // My test device.
            .Build();
        }
        admanagerInstance.interstitialView.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded video ad with the request.
        admanagerInstance.rewardBasedVideoAd.LoadAd(request, rewardVideoID);
    }
    #endregion


    #region AdmobRewardCallBackEvents
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
            + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        admanagerInstance.RequestRewardBasedVideo();

        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    //This is called when user completes Admob reward video
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Debug.Log("The ad was shown successfully");
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }
    #endregion     //Reward Events Ends




    
    /*
public void ShowUnityVideoAd()
{
    Debug.Log("ShowUnityVideoAd");

    if (Advertisement.IsReady("video"))
        Advertisement.Show("video");
}

//Call this to show reward video ad
public void ShowUnityRewardVideoAd()
{
    Debug.Log("ShowUnityRewardVideoAd");
    if (Advertisement.IsReady("rewardedVideo"))
    {
        Debug.Log("Showing Advertisement");
        var options = new ShowOptions { resultCallback = HandleSHowResult };
        Advertisement.Show("rewardedVideo", options);
    }
}

private void HandleSHowResult(ShowResult result)
{
    switch (result)
    {
        case ShowResult.Finished:
            Debug.Log("The Unity Reward ad was shown successfully");
            break;
        case ShowResult.Skipped:
            Debug.Log("Ad was skipped");
            break;
        case ShowResult.Failed:
            Debug.LogError("The ad fialed to be shown");
            break;
    }
}
*/
}
