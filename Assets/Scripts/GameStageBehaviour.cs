using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageBehaviour : MonoBehaviour {

	public static GameStageBehaviour instance;

	public GameObject map;
    public GameObject player;
    public GameObject musicManager;

	//private enum GameStates {
	//	PickaxeStage, StartGameStoneStage, MiningStage, EndGameStage,
	//}
 //   private GameStates currentState;

	private void Awake() {
		instance = this;
        player.GetComponentInChildren<PlayerControlBehaviour>().enabled = true;
	}

	private void Start() {
        SetPickaxeStage();
	}

    //If Game hasn't begun, only Pickaxe is visible
    public void SetPickaxeStage() {
        map.GetComponent<CaveGeneration>().SpawnEmpty();
        //musicManager.
    }

    //If Pickaxe is picked up, spawn StartStone
    public void SetStartGameStoneStage() {
        map.GetComponent<CaveGeneration>().PlaceStartStone();
	}

    //If StartStone is smashed, Spawn Map
    public void SetMiningStage() {
        map.GetComponent<CaveGeneration>().BuildCave();
    }

    //If Time is over, set Endscreen
    public void SetEndGameStage() {
    }
}
