using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {
    private int playerHealth;
    private Text healthText;
    private string playerName;
    private string playerTime;
	public GameObject ingameQuitMenu;

    // Use this for initialization
    void Start () {
		ingameQuitMenu.SetActive (false);
        playerHealth = PlayerController.playerHealth;
        healthText = GameObject.Find("healthText").GetComponent<UnityEngine.UI.Text>();
        playerName = SetName.getCharacterName();
    }
	
	// Update is called once per frame
	void Update () {
        healthText.text = playerHealth.ToString();
        GameObject.Find("nameText").GetComponent<Text>().text = playerName + " : " + PlayerController.currentScore;

        GameObject.Find("timeText").GetComponent<Text>().text = Mathf.Round((float)GameTime.getPlayedTime())   + " s" ;

		if (Input.GetButton("Cancel")) {
			ingameQuitMenu.SetActive (true);
			Time.timeScale = 0;
		}
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
