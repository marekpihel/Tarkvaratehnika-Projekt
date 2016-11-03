using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
    private int playerHealth;
    private Text healthText;
    private string playerName;

    // Use this for initialization
    void Start () {
        playerHealth = PlayerController.playerHealth;
        healthText = GameObject.Find("healthText").GetComponent<UnityEngine.UI.Text>();
        playerName = SetName.getCharacterName();
    }
	
	// Update is called once per frame
	void Update () {
        healthText.text = playerHealth.ToString();
        GameObject.Find("nameText").GetComponent<Text>().text = playerName + " : " + PlayerController.currentScore;
    }
}
