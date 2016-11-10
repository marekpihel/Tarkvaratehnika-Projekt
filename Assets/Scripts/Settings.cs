using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Cancel")) {
            backToMainMenu();
        }
	}
		
	public void changeVolume() {
		GameSound gameSound = GameSound.GetInstance();
		float updatedVolume = GUILayout.HorizontalSlider (gameSound.masterVolume, 0.0f, 0.1f);
		if (updatedVolume != gameSound.masterVolume) { // the volume have changed
			gameSound.masterVolume = updatedVolume;
			gameSound.updateBackgroundVolume ();
		}
	}

	// TO-DO RESOLUTION MANIPULATION

	public void backToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
