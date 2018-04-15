using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStageBehaviour : MonoBehaviour {

	public static GameStageBehaviour instance;

	public GameObject map;
	//public Scene endGameScene;
    public GameObject player;
    public GameObject musicManager;
	[HideInInspector]
	public int score;

	private bool temp;

	private void Awake() {
		instance = this;
		DontDestroyOnLoad(this.gameObject);

        player.GetComponentInChildren<PlayerControlBehaviour>().enabled = true;

		musicManager.GetComponent<AudioSource>().enabled = false;
		musicManager.SetActive(false);

		score = 0;
	}

	private void Start() {
        SetPickaxeStage();
	}

    //If Game hasn't begun, only Pickaxe is visible
    public void SetPickaxeStage() {
        map.GetComponent<CaveGeneration>().SpawnEmpty();
	}

    //If Pickaxe is picked up, spawn StartStone
    public void SetStartGameStoneStage() {
        map.GetComponent<CaveGeneration>().PlaceStartStone();
	}

    //If StartStone is smashed, Spawn Map
    public void SetMiningStage() {
        map.GetComponent<CaveGeneration>().BuildCave();
		StartCoroutine(BuildingCave());
	}

    //If Time is over, set Endscreen
    public void SetEndGameStage() {
		SceneManager.LoadScene("EndGame");
	}

	IEnumerator BuildingCave() {
		//Before Build

		yield return new WaitForSecondsRealtime(5);

		//After Build
		musicManager.SetActive(true);
		musicManager.GetComponent<AudioSource>().enabled = true;
	}

	//no oculus test
	private void Update() {
		if(!temp && Time.time > 5) {
			temp = true;
			//SetMiningStage();
			//SetStartGameStoneStage();
			SetEndGameStage();
		}
		Debug.Log(score);
	}
}
