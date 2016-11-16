using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public static int playerDMG = 1;
    public static int currentScore = 0;
    public static bool aliveState = true;
    private float waitOnLevelSwitch = 0.5f;
    public static int playerHealth = 9;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead())
        {
            levelEnd();
        }
    }

    private bool isDead()
    {
        if (playerHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.name == "trapdoorLevel2")
        {
            levelEnd();
        }
        else if (collisionObject.name == "trapdoor")
        {
            loadLevelTwo();
        }
    }

    private void loadLevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void levelEnd()
    {
        Scoreboard.writeToScoreboard(SetName.getCharacterName(), Player.currentScore);
        Invoke("loadHighScoreScene", waitOnLevelSwitch);
    }

    public void loadHighScoreScene()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("Highscore");
    }

    public static void addPointsToCurrentScore(int points)
    {
        currentScore += points;
    }
}
