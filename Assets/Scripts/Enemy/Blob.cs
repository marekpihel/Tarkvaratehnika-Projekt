using UnityEngine;
using System.Collections;

public class Blob : MonoBehaviour {
    public int blobHealth = 3;
    public int blobAttackDMG = 1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead())
        {
            Player.addKillCredit(100, 1);
            Destroy(this.gameObject);
        }
    }

    private bool isDead()
    {
        if (blobHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
