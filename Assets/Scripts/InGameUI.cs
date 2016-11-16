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
    private bool isPaused;

    // Use this for initialization
    void Start () {
		ingameQuitMenu.SetActive (false);
        healthText = GameObject.Find("healthText").GetComponent<UnityEngine.UI.Text>();
        playerName = SetName.getCharacterName();
        isPaused = false;
    }
	
	// Update is called once per frame
	void Update () {
        healthText.text = Player.playerHealth.ToString() ;
        GameObject.Find("nameText").GetComponent<Text>().text = playerName + " : " + Player.currentScore;
        GameObject.Find("timeText").GetComponent<Text>().text = Mathf.Round((float)GameTime.getPlayedTime())   + " s" ;

		if (Input.GetButtonDown("Cancel")) {
            if (!isPaused)
            {
                pauseGame();
                isPaused = true;
            }
            else {
                resumeGame();
            }
        }
    }

    private void pauseGame()
    {
        ingameQuitMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public Boolean getIsPaused() {
        return isPaused;
    }

    public void resumeGame()
    {
        isPaused = false;
        ingameQuitMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void backToMainMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}
}
