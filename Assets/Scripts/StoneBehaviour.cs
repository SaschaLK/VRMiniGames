using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehaviour : MonoBehaviour {

    public int hitpoints;
    public enum StoneType { lcrStone, lStone, lcStone, rStone, rcStone }
    private StoneType localStoneType;

    private Transform stonePosition;

    private void Awake () {
        stonePosition = gameObject.transform;
        localStoneType = StoneType.lcrStone;
    }

    public void OnTriggerEnter (Collider other) {

        if(hitpoints > 0) {
            hitpoints--;
        }
        else if(hitpoints == 0) {
            if (localStoneType.Equals(StoneType.lcrStone)) {
                RuneBehaviour.instance.SpawnCenterRune(gameObject.transform.position);
                RuneBehaviour.instance.SpawnLeftRune(gameObject.transform.position);
                RuneBehaviour.instance.SpawnRightRune(gameObject.transform.position);
            }
            else if (localStoneType.Equals(StoneType.lStone)) {
                RuneBehaviour.instance.SpawnLeftRune(gameObject.transform.position);
            }
            else if (localStoneType.Equals(StoneType.lcStone)) {
                RuneBehaviour.instance.SpawnLeftRune(gameObject.transform.position);
                RuneBehaviour.instance.SpawnCenterRune(gameObject.transform.position);
            }
            else if (localStoneType.Equals(StoneType.rStone)) {
                RuneBehaviour.instance.SpawnRightRune(gameObject.transform.position);
            }
            else if(localStoneType.Equals(StoneType.rcStone)) {
                RuneBehaviour.instance.SpawnRightRune(gameObject.transform.position);
                RuneBehaviour.instance.SpawnCenterRune(gameObject.transform.position);
            }
            else {
                Debug.Log("You fucked up");
            }
			gameObject.transform.parent.gameObject.SetActive(false);
            //Destroy (gameObject.transform.parent.gameObject);
            //Destroy (gameObject);
        }
        PickaxeBehaviour.instance.PlayPickSound (stonePosition);
    }

	private void OnDisable() {
		GameStageBehaviour.instance.score++;
	}

	public void SetStoneType(StoneType stoneType) {
        if(stoneType.Equals(StoneType.lcrStone)) {
            SetLCRStone();
        }
        else if (stoneType.Equals(StoneType.lStone)) {
            SetLStone();
        }
        else if (stoneType.Equals(StoneType.lcStone)) {
            SetLCStone();
        }
        else if (stoneType.Equals(StoneType.rStone)) {
            SetRStone();
        }
        else if (stoneType.Equals(StoneType.rcStone)) {
            SetRCStone();
        }
    }

    private void SetLCRStone() {
        localStoneType = StoneType.lcrStone;
    }

    private void SetLStone() {
        localStoneType = StoneType.lStone;
    }

    private void SetLCStone() {
        localStoneType = StoneType.lcStone;
    }

    private void SetRStone() {
        localStoneType = StoneType.rStone;
    }

    private void SetRCStone() {
        localStoneType = StoneType.rcStone;
    }

}
