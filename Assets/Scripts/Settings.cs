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

	// TO-DO VOLUME MANIMUPLATION

	// TO-DO RESOLUTION MANIPULATION

	public void backToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
