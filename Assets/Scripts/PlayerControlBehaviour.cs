using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlBehaviour : MonoBehaviour {

	public static PlayerControlBehaviour instance;

	public OVRInput.Controller leftController;
	public OVRInput.Controller rightController;
	[Range(1, 30)]
	public int vibrationIntensity;

    //Dash
	[HideInInspector]
    public bool dashing;
    public float dashTime;
    public float dashSpeed;
    public float dashStoppingSpeed;
    private float currentDashTime;
    [HideInInspector]
    public Vector3 direction;
    private float dashDistance;

	private void Awake() {
		instance = this;
		DontDestroyOnLoad(this.gameObject);
        currentDashTime = dashTime;
        direction = Vector3.zero;
    }

    private void Start() {
        dashDistance = CaveGeneration.instance.spacing;
    }

    private void FixedUpdate() {
        if (dashing) {
            currentDashTime = 0f;
            dashing = false;
        }
        if (currentDashTime < dashTime) {
            gameObject.transform.position = gameObject.transform.position + direction;
            currentDashTime += dashStoppingSpeed;
        }
    }

    public void StoneHitVibrate(AudioClip pickaxeClip) {
		OVRHapticsClip hapticClip = new OVRHapticsClip(pickaxeClip);
        /*
		if (PickaxeBehaviour.instance.gameObject.GetComponent<OVRGrabbable>().grabbedBy.m_controller == leftController) {
			for (int i = 0; i < vibrationIntensity; i++) {
				OVRHaptics.LeftChannel.Mix(hapticClip);
			}
		}
		else if (PickaxeBehaviour.instance.gameObject.GetComponent<OVRGrabbable>().grabbedBy.m_controller == rightController) {
			for (int i = 0; i < vibrationIntensity; i++) {
				OVRHaptics.RightChannel.Mix(hapticClip);
			}
		}
        */
	}
}
