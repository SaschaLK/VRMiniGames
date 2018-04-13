using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRuneBehaviour : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        PlayerControlBehaviour.instance.SetDirectionCenter();
        PlayerControlBehaviour.instance.dashing = true;
        gameObject.SetActive(false);
    }
}
