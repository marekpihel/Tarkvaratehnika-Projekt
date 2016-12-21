using UnityEngine;
using System.Collections;

public class GameSound : MonoBehaviour {

	public float masterVolume;
	public static GameSound instance = null;
	public static AudioSource backgroundMusic;
	public AudioSource swordSound;

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

	public static GameSound GetInstance() {
		return instance;
	}

	public void updateBackgroundVolume() {
		backgroundMusic.volume = masterVolume;
	}

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
