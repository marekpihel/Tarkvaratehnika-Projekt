using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class HighScore : MonoBehaviour {
    Text playerName;
    Text playerScore;
    Text scoreBoardNumbering;
    int counter = 0;

    // Use this for initialization
    void Start () {
        playerName = GameObject.Find("textName").GetComponent<Text>();
        playerScore = GameObject.Find("textScore").GetComponent<Text>();
        scoreBoardNumbering = GameObject.Find("scoreboardNumbering").GetComponent<Text>();

        foreach (KeyValuePair<string, int> entry in Scoreboard.readFromScoreboard()) {
            if (counter < 10) {
                scoreBoardNumbering.text += (counter + 1) + ". \n";
                playerName.text += entry.Key + " \n";
                playerScore.text += Convert.ToString(entry.Value) + " \n";
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
