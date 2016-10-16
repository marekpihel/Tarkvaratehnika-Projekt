using UnityEngine;
using System.Collections;
using System;

public class GameTime : MonoBehaviour {
    static double playedTime;
	// Use this for initialization
	void Start () {
        playedTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        playedTime += Time.deltaTime;
    }

    public static double getPlayedTime(){
        return playedTime;
    }
}
