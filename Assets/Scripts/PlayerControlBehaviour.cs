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
    public bool dashing;
    public Vector3 leftDash;
    public Vector3 centerDash;
    public Vector3 rightDash;

    public float dashTime;
    public float dashSpeed;
    public float dashStoppingSpeed;

    private Vector3 direction;
    private float currentDashTime;
    private float spacing;

	private void Awake() {
		instance = this;
        currentDashTime = dashTime;
        direction = Vector3.zero;
    }

    private void Start() {
        spacing = CaveGeneration.instance.spacing;
        //leftDash = new Vector3(-spacing, 0, spacing);
        //centerDash = new Vector3(0, 0, spacing);
        //rightDash = new Vector3(spacing, 0, spacing);
    }

    private void FixedUpdate() {
        if (dashing) {
            currentDashTime = 0f;
            dashing = false;
        }
        if (currentDashTime < dashTime) {
            gameObject.transform.localPosition = gameObject.transform.localPosition + direction;
            currentDashTime += dashStoppingSpeed;
        }
    }

    public void StoneHitVibrate(AudioClip pickaxeClip) {
		OVRHapticsClip hapticClip = new OVRHapticsClip(pickaxeClip);
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
	}

    public void SetDirectionLeft() {
        direction = leftDash;
    }
    
    public void SetDirectionCenter() {
        direction = centerDash;
    }

    public void SetDirectionRight() {
        direction = rightDash;
    }
}
