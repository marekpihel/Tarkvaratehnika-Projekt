using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class HighScore : MonoBehaviour {
    Text name1;
    Text score1;
    Text scoreBoardNumbering1;
    int counter = 0;

    // Use this for initialization
    void Start () {
        name1 = GameObject.Find("textName").GetComponent<Text>();
        score1 = GameObject.Find("textScore").GetComponent<Text>();
        scoreBoardNumbering1 = GameObject.Find("scoreboardNumbering").GetComponent<Text>();

        foreach (KeyValuePair<string, int> entry in Scoreboard.ReadScoreboard()) {
            if (counter < 10) {
                scoreBoardNumbering1.text += (counter +1) + ". \n";
                name1.text += entry.Key + " \n";
                score1.text += Convert.ToString(entry.Value) + " \n";
                counter++;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Cancel")) {
			backToMainMenu();
		}
	}

	public void backToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
