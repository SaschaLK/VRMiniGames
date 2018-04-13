using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRuneBehaviour : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        PlayerControlBehaviour.instance.SetDirectionLeft();
        PlayerControlBehaviour.instance.dashing = true;
        gameObject.SetActive(false);
    }
}
