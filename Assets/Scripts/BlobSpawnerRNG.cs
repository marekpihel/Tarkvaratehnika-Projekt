using UnityEngine;
using System.Collections;
using System;

public class BlobSpawnerRNG : MonoBehaviour {
    public GameObject blobPrefab;

	// Use this for initialization
	void Start () {
        spawnBlob();
	}
	 
    private void spawnBlob()
    {
        blobPrefab.transform.position = this.gameObject.transform.position;
        Instantiate<GameObject>(blobPrefab);
    }
}
