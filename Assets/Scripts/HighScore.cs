using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class HighScore : MonoBehaviour {
    Text name1;
    Text score1;

    // Use this for initialization
    void Start () {
       name1 = GameObject.Find("textName").GetComponent<Text>();
        score1 = GameObject.Find("textScore").GetComponent<Text>();



        foreach (KeyValuePair<string, int> entry in Scoreboard.ReadScoreboard()) {
            name1.text += entry.Key + " \n";
            score1.text += Convert.ToString(entry.Value) + " \n";

        }

        
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void backToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
