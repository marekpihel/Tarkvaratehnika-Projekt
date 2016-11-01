using UnityEngine;
using System.Collections;

public class GameSound : MonoBehaviour {
	public static GameSound instance = null;
	private AudioSource backgroundMusic;

	void Awake() {
		if (instance == null) {
			instance = this; // set instance to this
		} else if (instance != this) { // if instance already exists and it's not this:
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
		backgroundMusic = GetComponent<AudioSource> ();
		backgroundMusic.Play ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
	}
}
