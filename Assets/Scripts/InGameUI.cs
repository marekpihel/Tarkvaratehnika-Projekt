using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class InGameUI : MonoBehaviour {
    private Text healthText;
    private string playerName;
    private string playerTime;
	public GameObject ingameQuitMenu;
    public static bool isPaused;

    // Use this for initialization
    void Start () {
		ingameQuitMenu.SetActive (false);
        healthText = GameObject.Find("healthText").GetComponent<UnityEngine.UI.Text>();
        playerName = SetName.getCharacterName();
        isPaused = false;
    }
	
	// Update is called once per frame
	void Update () {
        healthText.text = PlayerAttacking.playerHealth.ToString() ;
        GameObject.Find("nameText").GetComponent<Text>().text = playerName + " : " + PlayerAttacking.currentScore;
        GameObject.Find("timeText").GetComponent<Text>().text = Mathf.Round((float)GameTime.getPlayedTime())   + " s" ;

		if (Input.GetButtonDown("Cancel")) {
            if (!isPaused)
            {
                pauseGame();
            }
            else {
                resumeGame();
            }
            isPaused = !isPaused;
		}
    }

    private void pauseGame()
    {
        ingameQuitMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeGame() { 
		ingameQuitMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void backToMainMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}
}
