using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelCleared : MonoBehaviour {
	public TextMeshProUGUI timeTakenDisplay;
	public GameObject starImage1;
	public GameObject starImage2;
	public GameObject starImage3;

	private int starsNo;
	private float timeTaken;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		setTimeTaken ();
	}

	public void setStar(int starsNo){
		if (starsNo == 0) {     starImage1.SetActive (false);starImage2.SetActive (false);starImage3.SetActive (false);		} 
		else if (starsNo == 1) {starImage1.SetActive (true);starImage2.SetActive (false);starImage3.SetActive (false);} 
		else if (starsNo == 2) {starImage1.SetActive (true);starImage2.SetActive (true);starImage3.SetActive (false);}
		else if (starsNo == 3) {starImage1.SetActive (true);starImage2.SetActive (true);starImage3.SetActive (true);} 
	}
	public void setTimeTaken(){
		timeTaken=PlayerPrefs.GetFloat ("Timetaken");
		timeTakenDisplay.text = timeTaken.ToString();
	}
	public void nextLevel(){
		NetworkManager_Custom nw = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager_Custom>();
		nw.ServerChangeScene ("Level" + (PlayerPrefs.GetInt ("LevelReached")));
		Time.timeScale = 1f;
	}
	public void MainMenu(){
		GameObject networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
		GameObject.Destroy(networkManager);
		NetworkManager_Custom.Shutdown();
		SceneManager.LoadScene (0);
	}
}
