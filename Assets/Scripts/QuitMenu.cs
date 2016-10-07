using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour {

	public Text yes;
	public Text no;

	// Use this for initialization
	void Start () {
		yes = GameObject.Find ("Yes").GetComponent<Text> ();
		no = GameObject.Find ("No").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// exitMenu -> No
	public void ExitNoPress() {
		SceneManager.LoadScene ("MainMenu");
	}

	// exitMenu -> Yes
	public void QuitGame() {
		Application.Quit ();
	}
}
