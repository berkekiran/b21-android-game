using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;
using TappxSDK;
using UnityEngine.SceneManagement;

public class TappxManagerUnity : MonoBehaviour
{
    public enum Position
    {
        TOP = 0,
        BOTTOM = 1
    }

	private static TappxManagerUnity mInstance = null;


#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void trackInstallIOS_(string tappxID, bool testMode);
    //Banner
    [DllImport("__Internal")]
	private static extern void createBannerIOS_(Position positionBanner, bool mrec);
	[DllImport("__Internal")]
    private static extern void hideAdIOS_();
    [DllImport("__Internal")]
	private static extern void showAdIOS_(Position positionBanner);
	[DllImport("__Internal")]
	private static extern void releaseTappxIOS_();

	//Interstitial
    [DllImport("__Internal")]
	private static extern void loadInterstitialIOS_(bool autoShow );
	[DllImport("__Internal")]
	private static extern void showInterstitialIOS_();
	[DllImport("__Internal")]
	private static extern void releaseInterstitialTappxIOS_();
    [DllImport("__Internal")]
    private static extern bool isInterstitialReady_ ();
    
#elif UNITY_ANDROID
    private AndroidJavaObject bannerControl = null;
	private AndroidJavaObject interstitialControl = null;
#endif


	public static TappxManagerUnity instance
    {
        get
        {
            return mInstance;
        }
    }

    private int sceneIndexLoaded = -1;

    public void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this;
            DontDestroyOnLoad(gameObject);
#if !UNITY_5_4_OR_NEWER
	        OnLevelWasLoaded(0);
#endif
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        if (Application.isEditor) return;

#if UNITY_IPHONE
        if (mInstance == this)
        {
            bool Banner = TappxSettings.Instance.bannerSceneIndex[sceneIndexLoaded];
            bool Interstitial = TappxSettings.Instance.interstitialSceneIndex[sceneIndexLoaded];

			if(Banner ^ Interstitial){
				if(Banner){
					releaseTappxIOS_();
				}else{
					releaseInterstitialTappxIOS_();
				}
			}
        }
#endif
    }




    public void OnApplicationPause(bool pause)
    {
        if (Application.isEditor) return;
        if (pause)
        {

            if (TappxSettings.Instance.sceneIndex[sceneIndexLoaded] == true && TappxSettings.Instance.sceneListEnabled)
            {
                bool Banner = TappxSettings.Instance.bannerSceneIndex[sceneIndexLoaded];
                bool Interstitial = TappxSettings.Instance.interstitialSceneIndex[sceneIndexLoaded];

                if (Banner)
                {	                    
                    show();
                }
                if ( Interstitial )
                {
                    interstitialShow();
                }
                
            }
        }
    }


#if UNITY_5_4_OR_NEWER

	private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

#endif




#if UNITY_5_4_OR_NEWER

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
#else
	void OnLevelWasLoaded( int level )

#endif

    {

#if UNITY_ANDROID
        if (bannerControl != null)
        {
            bannerControl.Call("hideBannerGONE");
            bannerControl = null;
        }
#endif

#if UNITY_5_4_OR_NEWER
        sceneIndexLoaded = scene.buildIndex;
#else
		sceneIndexLoaded = level;
#endif

	    if (Application.isEditor) return;

#if UNITY_IPHONE

		hide();

		trackInstallIOS_( TappxSettings.getIOSAppId(),  TappxSettings.Instance.testEnabled  );

        if (TappxSettings.Instance.sceneIndex[sceneIndexLoaded] == true && TappxSettings.Instance.sceneListEnabled )
        {
            bool Banner = TappxSettings.Instance.bannerSceneIndex[sceneIndexLoaded];
	        bool mrec = TappxSettings.Instance.mrecSceneIndex[sceneIndexLoaded];
            bool Interstitial = TappxSettings.Instance.interstitialSceneIndex[sceneIndexLoaded];
            bool AutoShowInterstitial = TappxSettings.Instance.interstitialAutoShow[sceneIndexLoaded];

            if(Banner ^ Interstitial){

                if(Banner){
                    TappxSettings.POSITION_BANNER posBanner = TappxSettings.Instance.positionSceneIndex[sceneIndexLoaded];
					createBannerIOS_( (posBanner == TappxSettings.POSITION_BANNER.TOP ) ? Position.TOP : Position.BOTTOM, mrec );

                }else{
		    		loadInterstitialIOS_(AutoShowInterstitial);
                }
            }
        }



#elif UNITY_ANDROID


        if (TappxSettings.Instance.sceneIndex[sceneIndexLoaded] == true && TappxSettings.Instance.sceneListEnabled )
        {
            bool Banner = TappxSettings.Instance.bannerSceneIndex[sceneIndexLoaded];
	        bool mrec = TappxSettings.Instance.mrecSceneIndex[sceneIndexLoaded];
            bool Interstitial = TappxSettings.Instance.interstitialSceneIndex[sceneIndexLoaded];
            bool AutoShowInterstitial = TappxSettings.Instance.interstitialAutoShow[sceneIndexLoaded];
            TappxSettings.POSITION_BANNER positionBanner = TappxSettings.Instance.positionSceneIndex[sceneIndexLoaded];


                if(Banner){
                    bool posit = true;
                    if(positionBanner == TappxSettings.POSITION_BANNER.BOTTOM)
                        posit = false;
                    bannerControl = new AndroidJavaObject ("com.tappx.unity.bannerTappx", TappxSettings.getAndroidAppId(), mrec, posit, TappxSettings.Instance.testEnabled, "TappxManagerUnity");

                }
	        	
	        	if ( Interstitial ){
                    interstitialControl = new AndroidJavaObject ("com.tappx.unity.interstitialTappx",TappxSettings.getAndroidAppId(),AutoShowInterstitial, TappxSettings.Instance.testEnabled ,"TappxManagerUnity");
                }
        }

#endif



    }


    public void hide()
    {
		#if UNITY_IPHONE
			hideAdIOS_();
		#elif UNITY_ANDROID
			bannerControl.Call("hideBannerGONE");
			bannerControl = null;
		#endif
    }


	public void show( TappxSettings.POSITION_BANNER pos, bool mrec )
	{
		
#if UNITY_IPHONE
			TappxSettings.POSITION_BANNER positionBanner = TappxSettings.Instance.positionSceneIndex[sceneIndexLoaded];
			showAdIOS_( positionBanner == pos ? Position.TOP : Position.BOTTOM);
#elif UNITY_ANDROID
		
		if (bannerControl != null)
		{
			bannerControl.Call("hideBannerGONE");
			bannerControl = null;
		}
		
		bool posit = true;
		if (pos == TappxSettings.POSITION_BANNER.BOTTOM)
			posit = false;
		bannerControl = new AndroidJavaObject("com.tappx.unity.bannerTappx", TappxSettings.getAndroidAppId(), mrec, posit, TappxSettings.Instance.testEnabled,
			"TappxManagerUnity");
#endif	
		
		
	}
	
    public void show()
    {

	    if (TappxSettings.Instance.sceneListEnabled)
	    {
		   	TappxSettings.POSITION_BANNER positionBanner = TappxSettings.Instance.positionSceneIndex[sceneIndexLoaded];
		    bool mrec = TappxSettings.Instance.mrecSceneIndex[sceneIndexLoaded];
			show( positionBanner, mrec );
	    }
	    else
	    {
		    Debug.LogWarning("Tappx: You should use show( TappxSettings.POSITION_BANNER pos, bool mrec ) instead if you are not using Scene Manager");
		    
	    }
    }

    public bool isBannerVisible()
    {
		#if UNITY_ANDROID
		if(bannerControl!=null){
        	return bannerControl.Call<bool>("isBannerVisible");
		}
		#endif
        return false;
    }

	public bool isInterstitialReady()
	{
		
#if UNITY_IPHONE

	return isInterstitialReady_(); 

#endif

#if UNITY_ANDROID
	if (  interstitialControl != null )
		return interstitialControl.Call<bool>("isReady");
	else
		return false;
#endif
				
	}

	public void loadInterstitial(bool _autoShow)
	{
		#if UNITY_IPHONE
			loadInterstitialIOS_( _autoShow );
		#elif UNITY_ANDROID
		if(interstitialControl!=null){
			interstitialControl = null;
		}
		interstitialControl = new AndroidJavaObject ("com.tappx.unity.interstitialTappx",TappxSettings.getAndroidAppId(), _autoShow, TappxSettings.Instance.testEnabled ,"TappxManagerUnity");

		#endif

	}
	
	public void loadInterstitial(){

	    bool AutoShowInterstitial = TappxSettings.Instance.interstitialAutoShow[sceneIndexLoaded];
		loadInterstitial( AutoShowInterstitial );

	}

	public void interstitialShow(){
		#if UNITY_IPHONE
			showInterstitialIOS_();
		#elif UNITY_ANDROID
		if(interstitialControl!=null){
		    interstitialControl.Call("show");
		}
		#endif
	}

	#if UNITY_IPHONE
	public void tappxBannerDidReceiveAd(){
		UnityEngine.Debug.Log("Banner Received");
	}
	
	public void tappxBannerFailedToLoad(string error){
		UnityEngine.Debug.Log("Banner Error " + error);
	}
	
	public void tappxInterstitialDidReceiveAd(){
		UnityEngine.Debug.Log("Interstitial Load");
	}
	
	public void tappxInterstitialFailedToLoad(string error){
		UnityEngine.Debug.Log("Interstitial Error " + error);
	}

	public void tappxViewWillLeaveApplication() {
		UnityEngine.Debug.Log("Banner did click ");	
	}

	public void interstitialWillLeaveApplication() {
		UnityEngine.Debug.Log("Interstitial did click ");	
	}


	#elif UNITY_ANDROID
	public void OnAdLoaded(){
		UnityEngine.Debug.Log("Banner Received");
	}

	public void OnAdFailedToLoad(string error){
		UnityEngine.Debug.Log("Banner Error " + error);
	}

	public void InterstitialLoaded(){
		UnityEngine.Debug.Log("Interstitial Load");
	}
	
	public void InterstitialFailedToLoad(string error){
		UnityEngine.Debug.Log("Interstitial Error " + error);
	}
	
	public void InterstitialLeftApplication(){
		UnityEngine.Debug.Log("Interstitial Cliked");
	}
	
	public void OnAdLeftApplication()
	{
		UnityEngine.Debug.Log("Banner did click" );
	}


	#endif
}
