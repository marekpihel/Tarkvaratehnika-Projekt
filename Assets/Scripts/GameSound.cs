using UnityEngine;
using System.Collections;

public class GameSound : MonoBehaviour {
	public static GameSound instance = null;
	private AudioSource backgroundMusic;

	void Awake() {
		if (instance == null) {
			instance = this; // set instance to this
            DontDestroyOnLoad(gameObject);
            backgroundMusic = GetComponent<AudioSource>();
            backgroundMusic.Play();
        } else if (instance != this) { // if instance already exists and it's not this:
			Destroy (gameObject);
		}
		
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
	}
}
