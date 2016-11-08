using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class SetName : MonoBehaviour {

	public Text alertText;
	public Text placeholderText;
	string input;
	static string characterName;

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
		} else if (!Regex.IsMatch(input, "^[a-zA-Z0-9]*$", RegexOptions.IgnoreCase)) {
			alertText.text = "Only alphanumeric characters allowed!";
		} else if (input.Length > 20) {
			alertText.text = "Name cannot be longer than 20 characters!";
		} else {
			characterName = input;
			loadFirstLevel();
		}
	}

	public void backToMainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}

	public void loadFirstLevel(){
		SceneManager.LoadScene("LevelOne");
	}

    public static string getCharacterName() {
        return characterName;
    }
}