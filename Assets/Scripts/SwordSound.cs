using UnityEngine;
using System.Collections;

public class SwordSound : MonoBehaviour {

	public AudioSource swordSound;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			swordSound.Play ();
		}
	}
}
