using UnityEngine;
using System.Collections;

public class PlayerLoading : MonoBehaviour {
    public GameObject player;
    public GameObject spawnPoint;

    // Use this for initialization
    void Start() {
        player.transform.transform.position = spawnPoint.transform.position;
        Instantiate<GameObject>(player);
    }
}
