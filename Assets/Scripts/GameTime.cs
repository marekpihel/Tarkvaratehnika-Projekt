using UnityEngine;
using System.Collections;
using System;

public class GameTime : MonoBehaviour {
    double playedTime;
	// Use this for initialization
	void Start () {
        playedTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        playedTime += Time.deltaTime;
        print(Math.Round((playedTime), 2));
    }
}
