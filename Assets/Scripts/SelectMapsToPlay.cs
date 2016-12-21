using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMapsToPlay : MonoBehaviour {

	public Text alertText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Cancel")) {
			backToSetName ();
		}
	}

	public void backToSetName() {
		SceneManager.LoadScene ("SetName");
	}

	public void playNormalMaps() {
		SceneManager.LoadScene ("LevelOne");
	}

	public void playRngMap() {
		alertText.text = "Loading RNG map...";
		SceneManager.LoadScene ("RngMap");
	}
}
