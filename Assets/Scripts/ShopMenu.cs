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

	int firecost = 20;
	int watercost = 20;
	int lightningcost = 20;
	int earthcost = 20;
	int healthcost = 50;
	int efficiencycost = 50;

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
		waterUpButton.onClick.AddListener (decreaseWater);
		lightningUpButton.onClick.AddListener (increaseLightning);
		lightningUpButton.onClick.AddListener (decreaseLightning);
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

	}

	public void decreaseWater(){

	}

	public void increaseLightning(){

	}

	public void decreaseLightning(){

	}

	public void increaseEarth(){

	}

	public void decreaseEarth(){

	}

	public void increaseHealth(){

	}

	public void decreaseHealth(){

	}

	public void increaseEfficiency(){

	}

	public void decreaseEfficiency(){

	}
}
