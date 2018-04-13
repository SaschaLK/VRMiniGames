using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeBehaviour : MonoBehaviour {

    public static PickaxeBehaviour instance;

    public AudioClip pickAxeStoneSoundClip;
    public AudioClip pickaxeBell;
    public AudioClip pickaxeAura;
    public GameObject particles;
	public float floatStrength;
	private Vector3 floatVector;

    private AudioSource aura;

	public enum GrabbedState {
		neverGrabbed, isGrabbedFirstTime, wasGrabbed,
	}
	public GrabbedState pickState;

	private void Awake () {
        instance = this;
    }

	private void Start() {
		floatVector = this.transform.position;
		pickState = GrabbedState.neverGrabbed;
        aura = GetComponent<AudioSource>();
        aura.Play();
	}

	private void FixedUpdate() {

        //Start Stone initialisation because Pick has just been grabbed
		if(gameObject.GetComponent<OVRGrabbable>() != null) {
			if (pickState.Equals(GrabbedState.neverGrabbed) && gameObject.GetComponent<OVRGrabbable>().isGrabbed) {
				pickState = GrabbedState.isGrabbedFirstTime;

                GameStageBehaviour.instance.SetStartGameStoneStage();
			}
		}

		//Setup Stage for first time and Game Initialisation
		//The Pick hasn't been grabbed yet and summons the "start game" stone as soon as it is grabbed
		if (pickState.Equals(GrabbedState.neverGrabbed)) {
			transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time) * floatStrength), transform.position.z);
		}
		else if (pickState.Equals(GrabbedState.isGrabbedFirstTime)) {
			pickState = GrabbedState.wasGrabbed;
			GameStageBehaviour.instance.SetStartGameStoneStage();
			gameObject.GetComponent<Rigidbody>().useGravity = true;
            AudioSource.PlayClipAtPoint(pickaxeBell, transform.position);
            aura.Stop();

            particles.SetActive(false);
            //TO Do Slow disappear of particles
            //foreach(particles in )
		}
	}

	public void PlayPickSound (Transform stonePosition) {
        AudioSource.PlayClipAtPoint (pickAxeStoneSoundClip, stonePosition.position);
		PlayerControlBehaviour.instance.StoneHitVibrate(pickAxeStoneSoundClip);
    }
}
