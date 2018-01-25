using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shop : MonoBehaviour, IStoreListener {

	public GameObject shopCanvas, typeEL, typeCS, typeSS, speed, size, extraLife, buyButton, home, playButton;
	string type;
	bool buttonClicked, selectedCountx1, selectedCountx2, selectedCountx3;
	Sprite typeEL0, typeEL1, typeCS0, typeCS1, typeSS0, typeSS1, buyButton0, buyButton1;
	private static IStoreController m_StoreController;          
	private static IExtensionProvider m_StoreExtensionProvider;
	public static string feature0_extralife = "feature0_extralife";   
	public static string feature1_slowspeed = "feature1_slowspeed";   
	public static string feature2_changesizepoints = "feature2_changesizepoints";   

	void Start () {
		PlayGamesScript.Instance.LoadData ();

		if (m_StoreController == null)
		{
			InitializePurchasing();
		}

		typeEL0 = Resources.Load <Sprite>("shopExtraLifeBuyNormal");
		typeEL1 = Resources.Load <Sprite>("shopExtraLifeBuySelected");
		typeCS0 = Resources.Load <Sprite>("shopSlowSizeBuyNormal");
		typeCS1 = Resources.Load <Sprite>("shopSlowSizeBuySelected");
		typeSS0 = Resources.Load <Sprite>("shopSlowSpeedBuyNormal");
		typeSS1 = Resources.Load <Sprite>("shopSlowSpeedBuySelected");
		buyButton0 = Resources.Load <Sprite>("shopBuyButtonNormal");
		buyButton1 = Resources.Load <Sprite>("shopBuyButtonLocked");

		typeEL.GetComponent<Image> ().sprite = typeEL0;
		typeCS.GetComponent<Image> ().sprite = typeCS0;
		typeSS.GetComponent<Image> ().sprite = typeSS0;
	}

	public void typeEL_Click(){
		type = "typeEL";
		typeEL.GetComponent<Image> ().sprite = typeEL1;
		typeCS.GetComponent<Image> ().sprite = typeCS0;
		typeSS.GetComponent<Image> ().sprite = typeSS0;
	}

	public void typeCS_Click(){
		type = "typeCS";
		typeEL.GetComponent<Image> ().sprite = typeEL0;
		typeCS.GetComponent<Image> ().sprite = typeCS1;
		typeSS.GetComponent<Image> ().sprite = typeSS0;
	}

	public void typeSS_Click(){
		type = "typeSS";
		typeEL.GetComponent<Image> ().sprite = typeEL0;
		typeCS.GetComponent<Image> ().sprite = typeCS0;
		typeSS.GetComponent<Image> ().sprite = typeSS1;
	}

	public void buy_Click(){
		if(type == "typeEL" && CloudVariables.ImportantValues[5] != 3)
			BuyProductID(feature0_extralife);
		if(type == "typeSS" && CloudVariables.ImportantValues[4] != 3)
			BuyProductID(feature1_slowspeed);
		if(type == "typeCS" && CloudVariables.ImportantValues[6] != 3)
			BuyProductID(feature2_changesizepoints);
	}	

	public void home_Click(){
		shopCanvas.GetComponent<Animator> ().Play ("shopOff", -1, 0f);
		buttonClicked = true;
	}	

	public void play_Click(){
		shopCanvas.GetComponent<Animator> ().Play ("shopOff", -1, 0f);
		buttonClicked = false;
	}

	void Update () {
		if (shopCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("shopOff") &&
			shopCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length <
			shopCanvas.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime) {
			if (!shopCanvas.GetComponent<AudioSource> ().isPlaying) {
				if (buttonClicked) {
					SceneManager.LoadScene ("menu");
				} else if (!buttonClicked){
					if (CloudVariables.ImportantValues [3] == 0)
						SceneManager.LoadScene ("extras");
					else if (CloudVariables.ImportantValues [3] == 1)
						SceneManager.LoadScene ("gameIn");
				}
			}
		}

		Text speed1 = speed.GetComponent<Text>();
		speed1.text = "x" + CloudVariables.ImportantValues[4].ToString();
		Text size1 = size.GetComponent<Text>();
		size1.text = "x" + CloudVariables.ImportantValues[6].ToString();
		Text extraLife1 = extraLife.GetComponent<Text>();
		extraLife1.text = "x" + CloudVariables.ImportantValues[5].ToString();

		if (type == null || (type == "typeEL" && CloudVariables.ImportantValues [5] == 3) || (type == "typeSS" && CloudVariables.ImportantValues [4] == 3) || (type == "typeCS" && CloudVariables.ImportantValues [6] == 3)) {
			buyButton.GetComponent<Image> ().sprite = buyButton1;
			buyButton.GetComponent<Button> ().interactable = false;
		} else if (type != null || (type == "typeEL" && CloudVariables.ImportantValues [5] != 3) || (type == "typeSS" && CloudVariables.ImportantValues [4] != 3) || (type == "typeCS" && CloudVariables.ImportantValues [6] != 3)) {
			buyButton.GetComponent<Image> ().sprite = buyButton0;
			buyButton.GetComponent<Button> ().interactable = true;
		}
	}

	public void InitializePurchasing() 
	{
		if (IsInitialized())
		{
			return;
		}

		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		builder.AddProduct(feature0_extralife, ProductType.Consumable);
		builder.AddProduct(feature1_slowspeed, ProductType.Consumable);
		builder.AddProduct(feature2_changesizepoints, ProductType.Consumable);

		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	void BuyProductID(string productId)
	{
		if (IsInitialized())
		{
			Product product = m_StoreController.products.WithID(productId);

			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}


	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Debug.Log("OnInitialized: PASS");

		m_StoreController = controller;
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		if (String.Equals(args.purchasedProduct.definition.id, feature0_extralife, StringComparison.Ordinal))
		{
			CloudVariables.ImportantValues[5] += 1;
			PlayGamesScript.Instance.SaveData ();
			PlayGamesScript.Instance.LoadData ();
		} else if (String.Equals(args.purchasedProduct.definition.id, feature1_slowspeed, StringComparison.Ordinal))
		{
			CloudVariables.ImportantValues[4] += 1;
			PlayGamesScript.Instance.SaveData ();
			PlayGamesScript.Instance.LoadData ();
		} else if (String.Equals(args.purchasedProduct.definition.id, feature2_changesizepoints, StringComparison.Ordinal))
		{
			CloudVariables.ImportantValues[6] += 1;
			PlayGamesScript.Instance.SaveData ();
			PlayGamesScript.Instance.LoadData ();
		} else {
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

}