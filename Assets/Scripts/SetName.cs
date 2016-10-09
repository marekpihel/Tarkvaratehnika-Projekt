using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetName : MonoBehaviour {

	public Text alertText;
	public Text placeholderText;
	string input;
	string characterName;

	// Use this for initialization
	void Start () {
		alertText = GameObject.Find ("AlertText").GetComponent<Text> ();
		placeholderText = GameObject.Find ("Placeholder").GetComponent<Text> ();
	}
	
	public void setCharacterName(string inputFieldString) {
		input = inputFieldString;
	}

	public void confirmCharacterName() {
		if (placeholderText.enabled == true) {
			alertText.text = "Please enter your character name!";
		} else {
			characterName = input;
			Debug.Log (characterName);
			loadFirstLevel();
		}
	}

	public void backToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}

	public void loadFirstLevel(){
		SceneManager.LoadScene("LevelOne");
	}
}