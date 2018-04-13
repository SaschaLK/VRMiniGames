using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStoneBehaviour : MonoBehaviour {

    public int hitpoints;

    private Transform stonePosition;

    private void Awake() {
        stonePosition = gameObject.transform;
    }

    public void OnTriggerEnter(Collider other) {
        if(hitpoints > 0) {
            hitpoints--;
        }
        else if(hitpoints == 0) {
            RuneBehaviour.instance.SpawnCenterRune(gameObject.transform.position);

            Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
        PickaxeBehaviour.instance.PlayPickSound(stonePosition);
    }

	private void OnDestroy() {
		GameStageBehaviour.instance.SetMiningStage();
	}

}
