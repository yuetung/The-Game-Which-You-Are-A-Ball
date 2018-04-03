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

	int firecost = 50;
	int watercost = 50;
	int lightningcost = 50;
	int earthcost = 50;
	int healthcost = 70;
	int efficiencycost = 70;

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
	}

	void Update(){
		gold = GameManager.getGold ();
		goldDisplay.text = gold.ToString();
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
			gold = gold + firecost;
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
			gold = gold + watercost;
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
			gold = gold + lightningcost;
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
			gold = gold + earthcost;
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
		if (gold >= healthcost && !(maxHealth+20>200)) {
			// deduct the money and increase firecap
			gold = gold - healthcost;
			maxHealth = maxHealth + 20;
			// update the user pref for gold and firecap
			GameManager.setGold(gold);
			GameManager.setMaxHealth (maxHealth);
			// update the UI for gold and firecap
			maxhealthDisplay.text = maxHealth.ToString();
			goldDisplay.text = gold.ToString();
		}
	}

	public void decreaseHealth(){
		if (!(maxHealth - 20 < 100)) {
			// increase the money and deduct the firecap
			gold = gold + healthcost;
			maxHealth = maxHealth - 20;
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
