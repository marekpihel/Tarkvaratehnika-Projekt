using UnityEngine;
using System.Collections;

public class BlobStats : MonoBehaviour {
    public int blobHealth = 3;
    public int blobAttackDMG = 1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead())
        {
            PlayerAttacking.addPointsToCurrentScore(100);
            Destroy(this.gameObject);
        }
    }

    private bool isDead()
    {
        if (blobHealth <= 0)
        {
            return true;
        }
        return false;
    }
}
