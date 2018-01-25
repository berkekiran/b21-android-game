using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace TappxSDK {
	[CustomEditor(typeof(TappxSettings))]
	public class TappxSettingEditor : Editor {
		
		GUIContent iOSAppIdLabel = new GUIContent("Tappx Key [?]:", "Tappx App Keys can be found at http://www.tappx.com/en/admin/apps/");
		GUIContent androidAppIdLabel = new GUIContent("Tappx Key [?]:", "Tappx App Keys can be found at http://www.tappx.com/en/admin/apps/");
		GUIContent iOSLabel = new GUIContent("iOS");
		GUIContent androidLabel = new GUIContent("Android");

		// minimum version of the Google Play Services library project
		private long MinGmsCoreVersionCode = 4030530;
		private string googlePlayServicesVersion = "10.0.1";

		private string sError = "Error";
	    private string sOk = "OK";
	    private string sCancel = "Cancel";
	    private string sSuccess = "Success";
	    private string sWarning = "Warning";

        private string sSdkNotFound = "Android SDK Not found";
        private string sSdkNotFoundBlurb = "The Android SDK path was not found. " +
                "Please configure it in the Unity preferences window (under External Tools).";

        private string sLibProjNotFound = "Google Play Services Library Project Not Found";
        private string sLibProjNotFoundBlurb = "Google Play Services library project " +
                "could not be found your SDK installation. Make sure it is installed (open " +
                "the SDK manager and go to Extras, and select Google Play Services).";
                
        private string sLibProjVerNotFound = "The version of your copy of the Google Play " +
                "Services Library Project could not be determined. Please make sure it is " +
                "at least version {0}. Continue?";
                
        private string sLibProjVerTooOld = "Your copy of the Google Play " +
            "Services Library Project is out of date. Please launch the Android SDK manager " +
            "and upgrade your Google Play Services bundle to the latest version (your version: " +
            "{0}; required version: {1}). Proceeding may cause problems. Proceed anyway?";
        
        private string sSetupComplete = "Tappx configured successfully.";



	    private Vector2 scrollPos;
	    bool showPosition = true;
		private TappxSettings instance;


		public override void OnInspectorGUI() {
			instance = (TappxSettings)target;

			SetupUI();
		}



	    private void SetupUI() {
//			EditorGUILayout.HelpBox("Add the Chartboost App Id and App Secret associated with this game", MessageType.None);


	        if (instance && instance.sceneIndex.Length <= 0)
	        {
	            int numScenes = EditorSceneManager.sceneCountInBuildSettings;
		        instance.bannerSceneIndex = new bool[numScenes];
	            instance.interstitialSceneIndex = new bool[numScenes];
	            instance.sceneIndex = new bool[numScenes];
	            instance.interstitialAutoShow = new bool[numScenes];
		        instance.mrecSceneIndex = new bool[numScenes];

	            instance.positionSceneIndex = new TappxSettings.POSITION_BANNER[numScenes];
	            for (int i = 0; i < instance.positionSceneIndex.Length; i++)
	            {
	                instance.positionSceneIndex[i] = TappxSettings.POSITION_BANNER.BOTTOM;
	            }
	        }


	        // iOS
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(iOSLabel);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
	        EditorGUILayout.LabelField(iOSAppIdLabel);
	        EditorGUILayout.EndHorizontal();

	        EditorGUILayout.BeginHorizontal();
            instance.SetIOSAppId(EditorGUILayout.TextField(instance.iOSTappxID));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
			EditorGUILayout.Space();

			// Android
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(androidLabel);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(androidAppIdLabel);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			instance.SetAndroidAppId(EditorGUILayout.TextField(instance.androidTappxID));
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

		    EditorGUILayout.BeginHorizontal();
		    if (GUILayout.Button("Add Google Play Services (Android)"))
		    {
			    AddGooglePlayServices();
		    }
		    EditorGUILayout.EndHorizontal();
		    
		    EditorGUILayout.Space();
		    EditorGUILayout.Space();
		    EditorGUILayout.Space();
		    EditorGUILayout.Space();
		    EditorGUILayout.Space();
		    EditorGUILayout.Space();
		    
			instance.testEnabled = EditorGUILayout.Toggle( "Test Mode", instance.testEnabled );	    
		    instance.sceneListEnabled = EditorGUILayout.Toggle( "Select Scenes", instance.sceneListEnabled );

		    if (instance.sceneListEnabled)
		    {

			    
		        EditorGUILayout.HelpBox("Scene Manager", MessageType.Info);

		        int numberScenes = EditorSceneManager.sceneCountInBuildSettings;
		        for (int i = 0; i < numberScenes; i++)
		        {

		            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

			        if (instance.sceneIndex.Length != scenes.Length)
			        {
						instance.sceneIndex = new bool[scenes.Length];
				        instance.bannerSceneIndex = new bool[scenes.Length];
				        instance.interstitialSceneIndex = new bool[scenes.Length];
				        instance.positionSceneIndex = new TappxSettings.POSITION_BANNER[scenes.Length];
				        instance.interstitialAutoShow = new bool[scenes.Length];
				        instance.mrecSceneIndex = new bool[scenes.Length];
				        				        
				        for (int j = 0; j < instance.positionSceneIndex.Length; j++)
                        {
                            instance.positionSceneIndex[j] = TappxSettings.POSITION_BANNER.BOTTOM;
                        }
			        }
			        
			        instance.sceneIndex[i] = EditorGUILayout.Foldout(instance.sceneIndex[i], scenes[i].path, EditorStyles.foldoutPreDrop );

		            if (instance.sceneIndex[i])
		            {
		                instance.bannerSceneIndex[i] = EditorGUILayout.Toggle("Banner", instance.bannerSceneIndex[i]);

		                if (instance.bannerSceneIndex[i])
		                {
		                    GUILayout.BeginHorizontal();
		                    GUILayout.BeginVertical(GUILayout.ExpandWidth(true));

		                    instance.positionSceneIndex[i] = (TappxSettings.POSITION_BANNER) EditorGUILayout.EnumPopup("Position ", instance.positionSceneIndex[i]);

		                    GUILayout.EndVertical();
		                    GUILayout.EndHorizontal();
			                
			                instance.mrecSceneIndex[i] = EditorGUILayout.Toggle("---> Mrec Size", instance.mrecSceneIndex[i]);

		                }

		                instance.interstitialSceneIndex[i] = EditorGUILayout.Toggle("Interstitial", instance.interstitialSceneIndex[i]);
		                if (instance.interstitialSceneIndex[i])
		                {
		                    instance.interstitialAutoShow[i] = EditorGUILayout.Toggle("---> Auto Show", instance.interstitialAutoShow[i]);
		                }

			            
			            			            
			      
		            }

		        }

		    }


		}
		
		private void AddGooglePlayServices() {
			string sdkPath = GetAndroidSdkPath();
			string libProjPath = sdkPath +
			                     FixSlashes("/extras/google/google_play_services/libproject/google-play-services_lib");
			string libProjPathv30 = sdkPath + 
			                        FixSlashes("/extras/google/m2repository/com/google/android/gms/play-services-ads/"
			                                   + googlePlayServicesVersion
			                                   + "/play-services-ads-" + googlePlayServicesVersion + ".aar");
			string libProjPathv30_basement = sdkPath + 
			                        FixSlashes("/extras/google/m2repository/com/google/android/gms/play-services-basement/"
			                                   + googlePlayServicesVersion
			                                   + "/play-services-basement-" + googlePlayServicesVersion + ".aar");

			string libProjPathv30_base = sdkPath + 
			                                 FixSlashes("/extras/google/m2repository/com/google/android/gms/play-services-base/"
			                                            + googlePlayServicesVersion
			                                            + "/play-services-base-" + googlePlayServicesVersion + ".aar");

			string libProjPathv30_ads_lite = sdkPath + 
			                             FixSlashes("/extras/google/m2repository/com/google/android/gms/play-services-ads-lite/"
			                                        + googlePlayServicesVersion
			                                        + "/play-services-ads-lite-" + googlePlayServicesVersion + ".aar");

			
			string libProjAM = libProjPath + FixSlashes("/AndroidManifest.xml");
			string libProjDestDir = FixSlashes("Assets/Plugins/Android/google-play-services_lib");
			string libProjDestDirv30 = FixSlashes ("Assets/Plugins/Android" 
			                                       + "/play-services-ads-" + googlePlayServicesVersion + ".aar");;

			string libProjDestDirv30_basement = FixSlashes ("Assets/Plugins/Android" 
			                                       + "/play-services-basement-" + googlePlayServicesVersion + ".aar");;

			string libProjDestDirv30_base = FixSlashes ("Assets/Plugins/Android" 
			                                                + "/play-services-base-" + googlePlayServicesVersion + ".aar");;

			
			string libProjDestDirv30_ads_lite = FixSlashes ("Assets/Plugins/Android" 
			                                            + "/play-services-ads-lite-" + googlePlayServicesVersion + ".aar");;
			
			
			
			// check that Android SDK is there
			if (!HasAndroidSdk()) {
				Debug.LogError("Android SDK not found.");
				EditorUtility.DisplayDialog(sSdkNotFound,
					sSdkNotFoundBlurb, sOk);
				return;
			}

			// create needed directories
			EnsureDirExists("Assets/Plugins");
			EnsureDirExists("Assets/Plugins/Android");

			// Delete old libraries, to avoid duplicate symbols
			DeleteDirIfExists(libProjDestDir);
			DeleteFileIfExists(libProjDestDirv30);
			DeleteFileIfExists(libProjDestDirv30_basement);
			DeleteFileIfExists(libProjDestDirv30_base);
			DeleteFileIfExists(libProjDestDirv30_ads_lite);

			// check that the Google Play Services lib project is there
			if (System.IO.Directory.Exists (libProjPath) || System.IO.File.Exists (libProjAM)) {
				// Old Google Play Services directory structure
				// Copy the full jar into the project 

				// Check lib project version
				if (!CheckAndWarnAboutGmsCoreVersion(libProjAM)) {
					return;
				}

				// Clear out the destination library project
				DeleteDirIfExists(libProjDestDir);

				// Copy Google Play Services library
				FileUtil.CopyFileOrDirectory(libProjPath, libProjDestDir);
			} else {
				if (System.IO.File.Exists (libProjPathv30)) {
					// New Google Play Services directory structure
					// Copy the .aar file that contains the needed classes
					FileInfo libProjFile = new FileInfo(libProjPathv30);
					libProjFile.CopyTo(libProjDestDirv30,true);
					
					libProjFile = new FileInfo(libProjPathv30_basement);
					libProjFile.CopyTo(libProjDestDirv30_basement,true);

					libProjFile = new FileInfo(libProjPathv30_base);
					libProjFile.CopyTo(libProjDestDirv30_base,true);
					
					libProjFile = new FileInfo(libProjPathv30_ads_lite);
					libProjFile.CopyTo(libProjDestDirv30_ads_lite,true);

					
				} else {
					Debug.LogError ("Google Play Services lib project not found at: " + libProjPath);
					EditorUtility.DisplayDialog (sLibProjNotFound,
						sLibProjNotFoundBlurb, sOk);
					return;
				}
			}
	
			// refresh assets, and we're done
			AssetDatabase.Refresh();
			EditorUtility.DisplayDialog(sSuccess,
				sSetupComplete, sOk);
		}
	    
		private bool CheckAndWarnAboutGmsCoreVersion(string libProjAMFile) {
			string manifestContents = ReadFile(libProjAMFile);
			string[] fields = manifestContents.Split('\"');
			int i;
			long vercode = 0;
			for (i = 0; i < fields.Length; i++) {
				if (fields[i].Contains("android:versionCode") && i + 1 < fields.Length) {
					vercode = System.Convert.ToInt64(fields[i + 1]);
				}
			}
			if (vercode == 0) {
				return EditorUtility.DisplayDialog(sWarning, string.Format(
						sLibProjVerNotFound,
						MinGmsCoreVersionCode),
					sOk, sCancel);
			} else if (vercode < MinGmsCoreVersionCode) {
				return EditorUtility.DisplayDialog(sWarning, string.Format(
						sLibProjVerTooOld, vercode,
						MinGmsCoreVersionCode),
					sOk, sCancel);
			}
			return true;
		}

	
	    private void EnsureDirExists(string dir) {
	        dir = dir.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString());
	        if (!System.IO.Directory.Exists(dir)) {
	            System.IO.Directory.CreateDirectory(dir);
	        }
	    }
	
	    private void DeleteDirIfExists(string dir) {
	        if (System.IO.Directory.Exists(dir)) {
	            System.IO.Directory.Delete(dir, true);
	        }
	    }

		private void DeleteFileIfExists(string file) {
			if (System.IO.File.Exists (file)) {
				System.IO.File.Delete (file);
			}
		}
		private string FixSlashes(string path) {
	        return path.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString());
	    }
		
	    private string ReadFile(string filePath) {
	        filePath = FixSlashes(filePath);
	        if (!File.Exists(filePath)) {
	            EditorUtility.DisplayDialog(sError, "Plugin error: file not found: " + filePath, sOk);
	            return null;
	        }
	        StreamReader sr = new StreamReader(filePath);
	        string body = sr.ReadToEnd();
	        sr.Close();
	        return body;
	    }
		
	    private string GetAndroidSdkPath() {
	        string sdkPath = EditorPrefs.GetString("AndroidSdkRoot");
	        if (sdkPath != null && (sdkPath.EndsWith("/") || sdkPath.EndsWith("\\"))) {
	            sdkPath = sdkPath.Substring(0, sdkPath.Length - 1);
	        }
	        return sdkPath;
	    }
	
	    private bool HasAndroidSdk() {
	        string sdkPath = GetAndroidSdkPath();
	        return sdkPath != null && sdkPath.Trim() != "" && System.IO.Directory.Exists(sdkPath);
	    }
	}
}
