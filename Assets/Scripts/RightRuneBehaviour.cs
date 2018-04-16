﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRuneBehaviour : MonoBehaviour {

    private Vector3 direction;

    private void Start() {
        direction = new Vector3(CaveGeneration.instance.spacing, 0, CaveGeneration.instance.spacing);
    }
    private void OnTriggerEnter(Collider other) {
        PlayerControlBehaviour.instance.direction = direction;
        PlayerControlBehaviour.instance.dashing = true;
        gameObject.SetActive(false);
    }
}
