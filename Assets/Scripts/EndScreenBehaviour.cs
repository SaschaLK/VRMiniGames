using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenBehaviour : MonoBehaviour {

    public static EndScreenBehaviour instance;

    public TextMeshPro scoreText;

    private void Awake() {
        instance = this;
        scoreText.text = "";
    }

    private void Start() {
        scoreText.text = "Score: " + GameStageBehaviour.instance.score;

        Instantiate(scoreText);
    }

    private void Update() {
        if(Time.timeSinceLevelLoad > 15) {
            scoreText.text = "";
            GameStageBehaviour.instance.SetPickaxeStage();
        }
    }

}
