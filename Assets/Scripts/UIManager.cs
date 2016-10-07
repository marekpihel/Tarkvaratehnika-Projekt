using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {

	public Button playButton;
	public Button highscoreButton;
	public Button settingsButton;
	public Button exitButton;


	// Use this for initialization
	void Start () {
		playButton = playButton.GetComponent<Button> ();
		highscoreButton = highscoreButton.GetComponent<Button> ();
		settingsButton = settingsButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
		// SceneManager.LoadScene ("MainMenu");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitPress () {
		SceneManager.LoadScene ("QuitMenu");
	}
		
	public void PlayPress() {
		SceneManager.LoadScene ("SetName");
	}
}