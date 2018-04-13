using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour {

    public static GameManagerBehaviour instance;

    public Light directionLight;

    public OVRGrabbable pickaxe;

    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    [Range(1, 25)]
    public int vibrationIntensity;

	private void Awake() {
        instance = this;
        directionLight.enabled = false;
    }

    public void StoneHitVibrate (AudioClip pickaxeClip) {
        OVRHapticsClip hapticClip = new OVRHapticsClip (pickaxeClip);
        if(pickaxe.grabbedBy.m_controller == OVRInput.Controller.LTouch) {
            for (int i = 0; i < vibrationIntensity; i++) {
                OVRHaptics.LeftChannel.Mix (hapticClip);
            }
        }
        else if(pickaxe.grabbedBy.m_controller == OVRInput.Controller.RTouch) {
            for (int i = 0; i < vibrationIntensity; i++) {
                OVRHaptics.RightChannel.Mix (hapticClip);
            }
        }
    }
}
