using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    public GameObject[] stones;
    public int quantityX;
    public int quantityZ;

    private float stoneRangeX;
    private float stoneRangeZ;

    private void Awake () {
        GameObject stonesEmpty = new GameObject("stonesEmpty");

        for (int i = 0; i < quantityX; i++) {
            for(int j = 0; j < quantityZ; j++) {
                stoneRangeX = Random.Range (1f, 5f);
                stoneRangeZ = Random.Range (1f, 5f);
                Instantiate (stones [0], new Vector3(i + stoneRangeX, 0, j + stoneRangeZ),Quaternion.identity, stonesEmpty.transform);
            }
        }
    }

}
