using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneBehaviour : MonoBehaviour {

    public static RuneBehaviour instance;

    public GameObject leftRune;
    public GameObject centerRune;
    public GameObject rightRune;

    private GameObject tempLeftRune;
    private GameObject tempCenterRune;
    private GameObject tempRightRune;

    private Vector3 leftRuneStandardPosition;
    private Vector3 centerRuneStandardPosition;
    private Vector3 rightRuneStandardPosition;

    private void Awake() {
        instance = this;

        leftRuneStandardPosition = leftRune.transform.localPosition;
        centerRuneStandardPosition = centerRune.transform.localPosition;
        rightRuneStandardPosition = rightRune.transform.localPosition;

        tempLeftRune = Instantiate(leftRune, transform);
        tempCenterRune = Instantiate(centerRune, transform);
        tempRightRune = Instantiate(rightRune, transform);
    }

    private void Start() {
        tempLeftRune.SetActive(false);
        tempCenterRune.SetActive(false);
        tempRightRune.SetActive(false);
    }

    public void SpawnLeftRune(Vector3 stonePosition) {
        tempLeftRune.transform.localPosition = stonePosition + leftRuneStandardPosition;
        tempLeftRune.SetActive(true);
    }

    public void SpawnCenterRune(Vector3 stonePosition) {
        tempCenterRune.transform.localPosition = stonePosition + centerRuneStandardPosition;
        tempCenterRune.SetActive(true);
    }

    public void SpawnRightRune(Vector3 stonePosition) {
        tempRightRune.transform.localPosition = stonePosition + rightRuneStandardPosition;
        tempRightRune.SetActive(true);
    }
}
