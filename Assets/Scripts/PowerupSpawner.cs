using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour {
    public GameObject attackBoost;
    public GameObject healthBoost;

	// Use this for initialization
	void Start () {
        int powerup = (int)UnityEngine.Random.Range(1f, 2.99f);
        if (powerup == 1) {
            attackBoost.gameObject.transform.position = this.gameObject.transform.position;
            Instantiate<GameObject>(attackBoost);
        } else if (powerup == 2)
        {
            healthBoost.gameObject.transform.position = this.gameObject.transform.position;
            Instantiate<GameObject>(healthBoost);
        }
    }
}
