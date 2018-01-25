using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TappxSDK {

#if UNITY_EDITOR

    [InitializeOnLoad]
#endif

	public class TappxSettings : ScriptableObject
	{
		const string tappxSettingsAssetName = "TappxSettings";
	    const string tappxSettingsPath = "Tappx/Resources";
	    const string tappxSettingsAssetExtension = ".asset";


	    static TappxSettings instance;

	    public enum POSITION_BANNER
	    {
	        TOP,
	        BOTTOM
	    }

		[SerializeField]
		public bool testEnabled;
	    [SerializeField]
	    public bool sceneListEnabled;
	    [SerializeField]
	    public bool[] bannerSceneIndex;
	    [SerializeField]
	    public bool[] interstitialSceneIndex;
		[SerializeField]
		public bool[] mrecSceneIndex;
	    [SerializeField] 
	    public bool[] sceneIndex = new bool[0];
	    [SerializeField]
	    public bool[] interstitialAutoShow;
	    [SerializeField]
	    public POSITION_BANNER[] positionSceneIndex;



	    public static TappxSettings Instance
	    {
	        get
	        {
	            if (instance == null)
	            {
	                instance = Resources.Load(tappxSettingsAssetName) as TappxSettings;
	                if (instance == null)
	                {
	                    // If not found, autocreate the asset object.
	                    instance = CreateInstance<TappxSettings>();
		                instance.sceneListEnabled = true;
	#if UNITY_EDITOR
	                    string properPath = Path.Combine(Application.dataPath, tappxSettingsPath);
	                    if (!Directory.Exists(properPath))
	                    {
	                        AssetDatabase.CreateFolder("Assets/Tappx", "Resources");
	                    }

	                    string fullPath = Path.Combine(Path.Combine("Assets", tappxSettingsPath),
	                                                   tappxSettingsAssetName + tappxSettingsAssetExtension
	                                                  );
	                    AssetDatabase.CreateAsset(instance, fullPath);
	#endif
	                }
	            }
	            return instance;
	        }
	    }

	#if UNITY_EDITOR
	    [MenuItem("Tappx/Edit Settings")]
	    public static void Edit()
	    {
	        Selection.activeObject = Instance;
	    }

	    [MenuItem("Tappx/SDK Documentation")]
	    public static void OpenDocumentation()
	    {
	        string url = "http://www.tappx.com/es/manual/?os=uni#0_gettingStarted";
	        Application.OpenURL(url);
	    }
	#endif

	    #region App Settings
		[SerializeField]
		public string iOSTappxID = "";
		[SerializeField]
		public string androidTappxID = "";

		// allow mediation partners to set the appId and appSignature from code
		// if set, overrides the values set in the editor
		public static void setAppId(string appId, string appSignature)
		{
#if UNITY_IOS
			Debug.Log("Overriding IOS AppId: " + appId);
			Instance.SetIOSAppId(appId);
#elif UNITY_ANDROID
			// Google
		    Debug.Log("Overriding Android AppId: " + appId);
		    Instance.SetAndroidAppId(appId);
#endif
		}

		// iOS
	    public void SetIOSAppId(string id)
	    {
	        if (!Instance.iOSTappxID.Equals(id))
	        {
	            Instance.iOSTappxID = id;
	            DirtyEditor();
	        }
	    }

		public static string getIOSAppId()
		{
			return Instance.iOSTappxID;
		}


		// Android
		public void SetAndroidAppId(string id)
		{
			if (!Instance.androidTappxID.Equals(id))
			{
				Instance.androidTappxID = id;
				DirtyEditor();
			}
		}
		
		public static string getAndroidAppId()
		{
			return Instance.androidTappxID;
		}

 
	    public static void DirtyEditor()
	    {
	#if UNITY_EDITOR
	        EditorUtility.SetDirty(Instance);
	#endif
	    }

	    #endregion
	}
}
