using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStageBehaviour : MonoBehaviour {

	public static GameStageBehaviour instance;

	public GameObject map;
	//public Scene endGameScene;
    public GameObject player;
    private bool playerSpawned;
    private Vector3 playerStartPosition;
    public GameObject musicManager;
	public GameObject stoneDust;
	[HideInInspector]
    public int score;
    [HideInInspector]
    public int gameTime;
	private bool temp;

	private void Awake() {
		instance = this;
		DontDestroyOnLoad(this.gameObject);

        player.GetComponentInChildren<PlayerControlBehaviour>().enabled = true;

		musicManager.GetComponent<AudioSource>().enabled = false;
		musicManager.SetActive(false);

        gameTime = 0;
        score = 0;
        playerStartPosition = player.transform.position;
	}

	private void Start() {
        if (!playerSpawned) {
            playerSpawned = true;
            Instantiate(player);
        }
        map.GetComponent<CaveGeneration>().SpawnEmpty();
	}

    //If Game hasn't begun, only Pickaxe is visible
    public void SetPickaxeStage() {
        SceneManager.LoadScene("MiningGameV2");
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
        StartCoroutine(GameTime());
	}

    //If Time is over, set Endscreen
    public void SetEndGameStage() {
        Debug.Log(score);
		SceneManager.LoadScene("EndGame");
	}

	IEnumerator BuildingCave() {
		//Before Build
		yield return new WaitForSecondsRealtime(5);
		//After Build
		musicManager.SetActive(true);
		musicManager.GetComponent<AudioSource>().enabled = true;
	}

    IEnumerator GameTime() {
        //62 Seconds is game time
        //57 Seconds
        yield return new WaitForSecondsRealtime(62);
        player.transform.position = playerStartPosition;
        SetEndGameStage();
    }

	public IEnumerator PlayDust(Vector3 position) {
		stoneDust.transform.position = position;
		stoneDust.SetActive(true);
		yield return new WaitForSecondsRealtime(2);
		stoneDust.SetActive(false);
	}

	//no oculus test
	private void Update() {
		if(!temp && Time.timeSinceLevelLoad > 5) {
			temp = true;
			//StartCoroutine(PlayDust());
			//SetMiningStage();
			//SetStartGameStoneStage();
			//SetEndGameStage();

		}
	}
}
