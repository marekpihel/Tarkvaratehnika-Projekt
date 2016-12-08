using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static int playerHealth = 9;
    public static int playerDMG = 1;
    private int playerDMGBasedOnLevel = 1;
    public static int currentScore = 0;
    public static int experiancePoints = 0;
    private readonly int EXP_NEEDED_FOR_LEVEL_UP = 2;
    private readonly int LOKEN_MAX_HEALTH = 9;
    private readonly int ATK_POWERUP_DURATION = 30;
    public float powerUpTime;
    private InGameUI ingameUi;

    // Use this for initialization
    void Start()
    {
        ingameUi = GameObject.Find("Map").GetComponent<InGameUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ingameUi.getIsPaused())
        {
            if (isDead())
            {
                levelEnd();
                Destroy(this.gameObject);
            }
            if (experiancePoints >= EXP_NEEDED_FOR_LEVEL_UP)
            {
                playerDMG += 1;
                playerDMGBasedOnLevel += 1;
                experiancePoints -= EXP_NEEDED_FOR_LEVEL_UP;
                Debug.Log("Leveled up");
            }
            powerUpTime += Time.deltaTime;
            if (powerUpTime >= ATK_POWERUP_DURATION)
            {
                lowerLokenAtk();
            }
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
        else if (collisionObject.name == "HealthPowerUp")
        {
            if (playerHealth < LOKEN_MAX_HEALTH)
            {
                playerHealth += 1;
                Destroy(collisionObject.gameObject);
            }
        }
        else if (collisionObject.name == "AttackPowerUp")
        {
            playerDMG += 1;
            Destroy(collisionObject.gameObject);
            powerUpTime = 0;
        }
    }

    private void lowerLokenAtk()
    {   
        if (playerDMG > playerDMGBasedOnLevel)
        {
            playerDMG -= 1;
        }
    }
    private void loadLevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
    }
    public void levelEnd()
    {
        Scoreboard.writeToScoreboard(SetName.getCharacterName(), Player.currentScore);
        Invoke("loadHighScoreScene", 0.25f);
    }

    public void loadHighScoreScene()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("Highscore");
    }

    public static void addKillCredit(int points, int experiance)
    {
        currentScore += points;
        experiancePoints += experiance;
    }
}
