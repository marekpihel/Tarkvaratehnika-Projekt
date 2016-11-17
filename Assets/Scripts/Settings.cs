using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class Settings : MonoBehaviour {

	public Dropdown resolutionDropdown;
	List<string> resolutions = new List<string> () {"16:9 2560x1440", "16:9 1920x1080",
		"16:9 1600x900", "16:9 1366x768", "16:9 1280x720", "16:9 1024x576", "16:9 854x480",
		"16:10 2560x1600", "16:10 1920x1200", "16:10 1680x1050", "16:10 1440x900",
		"16:10  1280x800", "4:3  2048x1536", "4:3  1600x1200", "4:3  1440x1080", "4:3  1400x1050",
		"4:3 1280x960", "4:3 1152x864", "4:3 1024x768", "4:3 800x600", "4:3 640x480"};

	// Use this for initialization
	void Start () {
		addResolutionsToDropdown ();
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

	void addResolutionsToDropdown() {
		resolutionDropdown.AddOptions (resolutions);
	}

	public void changeResolution(int index) {
		string[] selected = resolutions [index].Split (' ');
		string[] resolution = selected[1].Split('x');
		if (Screen.fullScreen) {
			Screen.SetResolution (Int32.Parse (resolution [0]), Int32.Parse (resolution [1]), true);
		} else {
			Screen.SetResolution (Int32.Parse(resolution[0]), Int32.Parse(resolution[1]), false);
		}
	}

	public void backToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
