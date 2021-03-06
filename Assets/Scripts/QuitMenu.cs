﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour {

	// public Canvas canvas;
	public Button yes;
	public Button no;

	// Use this for initialization
	void Start () {
		//canvas = GameObject.GetComponent<Canvas> ();
		yes = GameObject.Find ("Yes").GetComponent<Button> ();
		no = GameObject.Find ("No").GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Cancel")) {
			ExitNoPress();
		}
	}

	// exitMenu -> No
	public void ExitNoPress() {
		SceneManager.LoadScene ("MainMenu");
	}

	// exitMenu -> Yes
	public void QuitGame() {
		Application.Quit ();
	}
}
