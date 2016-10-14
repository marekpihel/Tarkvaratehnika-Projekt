﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scoreboard : MonoBehaviour {
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
  
    }

    public static List<KeyValuePair<string, int>> WriteScoreboard(String name, int score) {
        var listForWrite = ReadScoreboard();

        KeyValuePair<string, int> currentPair = new KeyValuePair<string, int>(name, score);
        listForWrite.Add(currentPair);
        // For testing
        //listForWrite.Add(new KeyValuePair<string, int>("rabbit", 1));
        //listForWrite.Add(new KeyValuePair<string, int>("dog", 11));
        //listForWrite.Add(new KeyValuePair<string, int>("x", 12));
        //listForWrite.Add(new KeyValuePair<string, int>("marek", 100));


        listForWrite = OrderByValue(listForWrite);

        //string path = Directory.GetCurrentDirectory();


        File.WriteAllLines(@".\scoreboard.txt", listForWrite.Select(value => value.ToString()).ToArray());
        return listForWrite;
    }
    public static List<KeyValuePair<string, int>> OrderByValue(List<KeyValuePair<string, int>> listToOrder) {
        for (int i = 0; i < listToOrder.Count; i++) {
            listToOrder.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        }
        listToOrder.Reverse();
        return listToOrder;
    }


    public static List<KeyValuePair<string, int>> ReadScoreboard() {
        var listForRead = new List<KeyValuePair<string, int>>();
        if (File.Exists("scoreboard.txt")) {
            string[] lines = File.ReadAllLines(@".\scoreboard.txt");

            for (int i = 0; i < lines.Length; i++) {
                string cleanValue = lines[i].Replace("[", "").Replace("]", "");
                string[] line = cleanValue.Split(',');
                string valString = line[0];
                int valInt = Int32.Parse(line[1]);
                listForRead.Add(new KeyValuePair<string, int>(valString, valInt));
            }
            listForRead = OrderByValue(listForRead);
        }
        return listForRead;
    }
}
