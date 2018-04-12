using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	// obtain user preferences
	// should initialize here? or inside the Awake() method
	// hmmm should it be static??? maybe not...
	public static int fireCap;
	public static int waterCap;
	public static int lightningCap;
	public static int earthCap;
	public static int maxHealth;
	public static float efficiency;
	public static int gold;


	// Use this for initialization
	void Awake () {
        if (gm == null)
        {
            gm = this;
			fireCap = getFireCap();
			waterCap = getWaterCap();
			lightningCap = getLightningCap();
			earthCap = getEarthCap();
			maxHealth = getMaxHealth();
			efficiency = getEfficiency();
			gold = getGold ();
        }
        else if (gm != this)
        {
            Destroy(gameObject);
            Debug.LogError("Multiple GameManagers");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public static int getFireCap(){
		if (!PlayerPrefs.HasKey ("Firecap")) {
			// initialize the firecap at 1
			PlayerPrefs.SetInt("Firecap",1);
		}
		return PlayerPrefs.GetInt ("Firecap");
	}

	public static void setFireCap(int newcap){
		PlayerPrefs.SetInt("Firecap",newcap);
	}

	public static int getWaterCap(){
		if (!PlayerPrefs.HasKey ("Watercap")) {
			// initialize the firecap at 1
			PlayerPrefs.SetInt("Watercap",1);
		}
		return PlayerPrefs.GetInt ("Watercap");
	}

	public static void setWaterCap(int newcap){
		PlayerPrefs.SetInt("Watercap",newcap);
	}

	public static int getLightningCap(){
		if (!PlayerPrefs.HasKey ("Lightningcap")) {
			// initialize the firecap at 1
			PlayerPrefs.SetInt("Lightningcap",1);
		}
		return PlayerPrefs.GetInt ("Lightningcap");
	}

	public static void setLightningCap(int newcap){
		PlayerPrefs.SetInt("Lightningcap",newcap);
	}

	public static int getEarthCap(){
		if (!PlayerPrefs.HasKey ("Earthcap")) {
			// initialize the firecap at 1
			PlayerPrefs.SetInt("Earthcap",1);
		}
		return PlayerPrefs.GetInt ("Earthcap");
	}

	public static void setEarthCap(int newcap){
		PlayerPrefs.SetInt("Earthcap",newcap);
	}

	// max health
	public static int getMaxHealth(){
		if (!PlayerPrefs.HasKey ("Maxhealth")) {
			// initialize the firecap at 1
			PlayerPrefs.SetInt("Maxhealth",100);
		}
		return PlayerPrefs.GetInt ("Maxhealth");
	}

	public static void setMaxHealth(int newcap){
		PlayerPrefs.SetInt("Maxhealth",newcap);
	}

	// energy up efficiency..
	public static float getEfficiency(){
		if (!PlayerPrefs.HasKey ("Efficiency")) {
			// initialize the firecap at 1
			PlayerPrefs.SetFloat("Efficiency",1.0f);
		}
		return PlayerPrefs.GetFloat ("Efficiency");
	}

	public static void setEfficiency(float newcap){
		PlayerPrefs.SetFloat("Efficiency",newcap);
	}

	public static int getGold(){
		if (!PlayerPrefs.HasKey ("Gold")) {
			// initialize the firecap at 1
			PlayerPrefs.SetInt("Gold",0);
		}
		return PlayerPrefs.GetInt ("Gold");
	}

	public static void setGold(int newgold){
		PlayerPrefs.SetInt("Gold",newgold);
		Debug.Log ("current Gold set to " + newgold);
	}
}
