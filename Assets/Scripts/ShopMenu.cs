using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenu : MonoBehaviour {

	private static int fireCap;
	private static int waterCap;
	private static int lightningCap;
	private static int earthCap;
	private static int maxHealth;
	private static float efficiency;
	private static int gold;

	public TextMeshProUGUI firecapDisplay;
	public TextMeshProUGUI watercapDisplay;
	public TextMeshProUGUI lightningcapDisplay;
	public TextMeshProUGUI earthcapDisplay;
	public TextMeshProUGUI maxhealthDisplay;
	public TextMeshProUGUI efficiencyDisplay;
	public TextMeshProUGUI goldDisplay;

	public Button fireUpButton;
	public Button fireDownButton;
	public Button waterUpButton;
	public Button waterDownButton;
	public Button lightningUpButton;
	public Button lightningDownButton;
	public Button earthUpButton;
	public Button earthDownButton;
	public Button healthUpButton;
	public Button healthDownButton;
	public Button efficiencyUpButton;
	public Button efficiencyDownButton;

	public TextMeshProUGUI informationDisplay;
	public TextMeshProUGUI costDisplay;
	public GameObject backgroundInformationDisplay;
	public int displayNum=0;

	public Button firePowerText;
	public Button waterPowerText;
	public Button lightningPowerText;
	public Button earthPowerText;
	public Button maxHealthText;
	public Button efficiencyText;

	public GameObject mePRECIOUS;
	public Vector3 whereMePRECIOUS;

	public GameObject meFire;
	public Vector3 whereMeFire;
	public GameObject meWater;
	public Vector3 whereMeWater;
	public GameObject meLightning;
	public Vector3 whereMeLightning;
	public GameObject meEarth;
	public Vector3 whereMeEarth;
	public GameObject meHealth;
	public Vector3 whereMeHealth;

	int firecost;
	int watercost;
	int lightningcost;
	int earthcost;
	int healthcost;
	int efficiencycost;

	// Use this for initialization
	void Start () {
		fireCap = GameManager.getFireCap();
		waterCap = GameManager.getWaterCap();
		lightningCap = GameManager.getLightningCap();
		earthCap = GameManager.getEarthCap();
		maxHealth = GameManager.getMaxHealth();
		efficiency = GameManager.getEfficiency();
		gold = GameManager.getGold ();

		firecapDisplay.text = fireCap.ToString();
		watercapDisplay.text = waterCap.ToString();
		lightningcapDisplay.text = lightningCap.ToString();
		earthcapDisplay.text = earthCap.ToString();
		maxhealthDisplay.text = maxHealth.ToString();
		efficiencyDisplay.text = efficiency.ToString();
		goldDisplay.text = gold.ToString();

		fireUpButton.onClick.AddListener (increaseFire);
		fireDownButton.onClick.AddListener (decreaseFire);
		waterUpButton.onClick.AddListener (increaseWater);
		waterDownButton.onClick.AddListener (decreaseWater);
		lightningUpButton.onClick.AddListener (increaseLightning);
		lightningDownButton.onClick.AddListener (decreaseLightning);
		earthUpButton.onClick.AddListener (increaseEarth);
		earthDownButton.onClick.AddListener (decreaseEarth);
		healthUpButton.onClick.AddListener (increaseHealth);
		healthDownButton.onClick.AddListener (decreaseHealth);
		efficiencyUpButton.onClick.AddListener (increaseEfficiency);
		efficiencyDownButton.onClick.AddListener (decreaseEfficiency);

		firePowerText.onClick.AddListener (infoFire);
		waterPowerText.onClick.AddListener (infoWater);
		lightningPowerText.onClick.AddListener (infoLightning);
		earthPowerText.onClick.AddListener (infoEarth);
		maxHealthText.onClick.AddListener (infoMaxHealth);
		efficiencyText.onClick.AddListener (infoEfficiency);

		
		whereMePRECIOUS = mePRECIOUS.transform.position;
		whereMeFire = meFire.transform.position;
		whereMeWater = meWater.transform.position;
		whereMeLightning = meLightning.transform.position;
		whereMeEarth = meEarth.transform.position;
		whereMeHealth = meHealth.transform.position;

		firecost = getCostPower (GameManager.getFireCap());
		watercost = getCostPower (GameManager.getWaterCap());
		lightningcost = getCostPower (GameManager.getLightningCap());
		earthcost = getCostPower (GameManager.getEarthCap());
		healthcost = getCostHP (GameManager.getMaxHealth());
	}

	void Update(){
		gold = GameManager.getGold ();
		goldDisplay.text = gold.ToString();

		firecost = getCostPower (GameManager.getFireCap());
		watercost = getCostPower (GameManager.getWaterCap());
		lightningcost = getCostPower (GameManager.getLightningCap());
		earthcost = getCostPower (GameManager.getEarthCap());
		healthcost = getCostHP (GameManager.getMaxHealth());
	}

	public int getCostHP(int currentLevel){
		return Mathf.RoundToInt(Mathf.Pow (currentLevel / 10.0f, 3));
	}

	public int getCostPower(int currentLevel){
		return Mathf.RoundToInt (50*Mathf.Pow (currentLevel, 3));
	}

	public void infoFire(){
		if (displayNum != 1) {
			mePRECIOUS.SetActive(true);
			costDisplay.transform.position = new Vector3 (whereMePRECIOUS.x + 80, whereMePRECIOUS.y, whereMePRECIOUS.z);
			costDisplay.text = "-"+firecost;

			meFire.SetActive (true);
			meWater.SetActive (false);
			meLightning.SetActive (false);
			meEarth.SetActive (false);
			meHealth.SetActive (false);
			informationDisplay.transform.position = new Vector3(whereMeFire.x + 80, whereMeFire.y, whereMeFire.z);
			informationDisplay.text = "+1 Max Level";

			displayNum = 1;
		} else {
			mePRECIOUS.SetActive (false);
			costDisplay.text = "";
			meFire.SetActive (false);
			informationDisplay.text = "";
			displayNum = 0;
		}
	}
	public void infoWater(){
		if (displayNum != 2) {
			mePRECIOUS.SetActive(true);
			costDisplay.transform.position = new Vector3 (whereMePRECIOUS.x + 80, whereMePRECIOUS.y, whereMePRECIOUS.z);
			costDisplay.text = "-"+watercost;

			meWater.SetActive (true);
			meFire.SetActive (false);
			meLightning.SetActive (false);
			meEarth.SetActive (false);
			meHealth.SetActive (false);
			informationDisplay.transform.position = new Vector3(whereMeWater.x + 80, whereMeWater.y, whereMeWater.z);
			informationDisplay.text = "+1 Max Level";

			displayNum = 2;
		} else {
			mePRECIOUS.SetActive (false);
			costDisplay.text = "";
			meWater.SetActive (false);
			informationDisplay.text = "";
			displayNum = 0;
		}
	}
	public void infoLightning(){
		if (displayNum != 3) {
			mePRECIOUS.SetActive(true);
			costDisplay.transform.position = new Vector3 (whereMePRECIOUS.x + 80, whereMePRECIOUS.y, whereMePRECIOUS.z);
			costDisplay.text = "-"+lightningcost;

			meLightning.SetActive (true);
			meWater.SetActive (false);
			meFire.SetActive (false);
			meEarth.SetActive (false);
			meHealth.SetActive (false);
			informationDisplay.transform.position = new Vector3(whereMeLightning.x + 80, whereMeLightning.y, whereMeLightning.z);
			informationDisplay.text = "+1 Max Level";

			displayNum = 3;
		} else {
			mePRECIOUS.SetActive (false);
			costDisplay.text = "";
			meLightning.SetActive (false);
			informationDisplay.text = "";
			displayNum = 0;
		}
	}
	public void infoEarth(){
		if (displayNum != 4) {
			mePRECIOUS.SetActive(true);
			costDisplay.transform.position = new Vector3 (whereMePRECIOUS.x + 80, whereMePRECIOUS.y, whereMePRECIOUS.z);
			costDisplay.text = "-"+earthcost;

			meEarth.SetActive (true);
			meWater.SetActive (false);
			meLightning.SetActive (false);
			meFire.SetActive (false);
			meHealth.SetActive (false);
			informationDisplay.transform.position = new Vector3(whereMeEarth.x + 80, whereMeEarth.y, whereMeEarth.z);
			informationDisplay.text = "+1 Max Level";

			displayNum = 4;
		} else {
			mePRECIOUS.SetActive (false);
			costDisplay.text = "";
			meEarth.SetActive (false);
			informationDisplay.text = "";
			displayNum = 0;
		}
	}
	public void infoMaxHealth(){
		if (displayNum != 5) {
			mePRECIOUS.SetActive(true);
			costDisplay.transform.position = new Vector3 (whereMePRECIOUS.x + 80, whereMePRECIOUS.y, whereMePRECIOUS.z);
			costDisplay.text = "-"+healthcost;

			meHealth.SetActive (true);
			meWater.SetActive (false);
			meLightning.SetActive (false);
			meEarth.SetActive (false);
			meFire.SetActive (false);
			informationDisplay.transform.position = new Vector3(whereMeHealth.x + 80, whereMeHealth.y, whereMeHealth.z);
			informationDisplay.text = "+10 Max Health";

			displayNum = 5;
		} else {
			mePRECIOUS.SetActive (false);
			costDisplay.text = "";
			meHealth.SetActive (false);
			informationDisplay.text = "";
			displayNum = 0;
		}
	}
	public void infoEfficiency(){
		if (displayNum != 6) {
			backgroundInformationDisplay.SetActive(true);
			informationDisplay.text = "Level up to increase your energy gain efficiency by 10%. Each level up cost 70 crystals."; 
			displayNum = 6;
		} else {
			backgroundInformationDisplay.SetActive(false);
			informationDisplay.text = "";
			displayNum = 0;
		}
	}

	public void increaseFire(){
		// check if enough money and maxcap is 4
		if (gold >= firecost && !(fireCap+1>=4)) {
			// deduct the money and increase firecap
			gold = gold - firecost;
			fireCap = fireCap + 1;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setFireCap (fireCap);
			// update the UI for gold and firecap
			firecapDisplay.text = fireCap.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void decreaseFire(){
		if (!(fireCap - 1 <= 0)) {
			// increase the money and deduct the firecap
			gold = gold + getCostPower(GameManager.getFireCap()-1);
			fireCap = fireCap - 1;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setFireCap (fireCap);
			// update the UI for gold and firecap
			firecapDisplay.text = fireCap.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void increaseWater(){
		
		// check if enough money and maxcap is 4
		if (gold >= watercost && !(waterCap+1>=4)) {
			// deduct the money and increase firecap
			gold = gold - watercost;
			waterCap = waterCap + 1;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setWaterCap (waterCap);
			// update the UI for gold and firecap
			watercapDisplay.text = waterCap.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void decreaseWater(){
		if (!(waterCap - 1 <= 0)) {
			// increase the money and deduct the firecap
			gold = gold + getCostPower(GameManager.getWaterCap()-1);
			waterCap = waterCap - 1;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setWaterCap (waterCap);
			// update the UI for gold and firecap
			watercapDisplay.text = waterCap.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void increaseLightning(){
		// check if enough money and maxcap is 4
		if (gold >= lightningcost && !(lightningCap+1>=4)) {
			// deduct the money and increase firecap
			gold = gold - lightningcost;
			lightningCap = lightningCap + 1;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setLightningCap (lightningCap);
			// update the UI for gold and firecap
			lightningcapDisplay.text = lightningCap.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void decreaseLightning(){
		if (!(lightningCap - 1 <= 0)) {
			// increase the money and deduct the firecap
			gold = gold + getCostPower(GameManager.getLightningCap()-1);
			lightningCap = lightningCap - 1;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setLightningCap (lightningCap);
			// update the UI for gold and firecap
			lightningcapDisplay.text = lightningCap.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void increaseEarth(){
		// check if enough money and maxcap is 4
		if (gold >= earthcost && !(earthCap+1>=4)) {
			// deduct the money and increase firecap
			gold = gold - earthcost;
			earthCap = earthCap + 1;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setEarthCap (earthCap);
			// update the UI for gold and firecap
			earthcapDisplay.text = earthCap.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void decreaseEarth(){
		if (!(earthCap - 1 <= 0)) {
			// increase the money and deduct the firecap
			gold = gold + getCostPower(GameManager.getEarthCap()-1);
			earthCap = earthCap - 1;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setEarthCap (earthCap);
			// update the UI for gold and firecap
			earthcapDisplay.text = earthCap.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void increaseHealth(){
		// check if enough money and maxcap is 4
		if (gold >= healthcost && !(maxHealth+10>200)) {
			// deduct the money and increase firecap
			gold = gold - healthcost;
			maxHealth = maxHealth + 10;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setMaxHealth (maxHealth);
			// update the UI for gold and firecap
			maxhealthDisplay.text = maxHealth.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void decreaseHealth(){
		if (!(maxHealth - 10 < 100)) {
			// increase the money and deduct the firecap
			gold = gold + getCostHP(GameManager.getMaxHealth()-10);
			maxHealth = maxHealth - 10;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setMaxHealth (maxHealth);
			// update the UI for gold and firecap
			maxhealthDisplay.text = maxHealth.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void increaseEfficiency(){
		// check if enough money and maxcap is 4
		if (gold >= efficiencycost && !(efficiency+0.1>2)) {
			// deduct the money and increase firecap
			gold = gold - efficiencycost;
			efficiency = efficiency + 0.1f;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setEfficiency (efficiency);
			// update the UI for gold and firecap
			efficiencyDisplay.text = efficiency.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void decreaseEfficiency(){
		if (!(efficiency - 0.1 < 1)) {
			// increase the money and deduct the firecap
			gold = gold + efficiencycost;
			efficiency = efficiency - 0.1f;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setEfficiency (efficiency);
			// update the UI for gold and firecap
			efficiencyDisplay.text = efficiency.ToString();
			goldDisplay.text = gold.ToString();
		}
	}
}
