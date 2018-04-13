using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRuneBehaviour : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        PlayerControlBehaviour.instance.dashing = true;
        gameObject.SetActive(false);
    }
}
