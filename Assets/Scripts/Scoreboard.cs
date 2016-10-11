using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scoreboard : MonoBehaviour {
    // Use this for initialization
    void Start() {
        WriteScoreboard("TESTVAL", 60);
    }

    // Update is called once per frame
    void Update() {

    }

    public static void WriteScoreboard(String name, int score) {

        var listForWrite = ReadScoreboard();

        KeyValuePair<string, int> currentPair = new KeyValuePair<string, int>(name, score);

        // For testing
        //listForWrite.Add(new KeyValuePair<string, int>("rabbit", 1));
        //listForWrite.Add(new KeyValuePair<string, int>("dog", 11));
        //listForWrite.Add(new KeyValuePair<string, int>("x", 12));
        //listForWrite.Add(new KeyValuePair<string, int>("marek", 100));
        listForWrite.Add(currentPair);

        for (int i = 0; i < listForWrite.Count; i++) {
             //only add to list if bigger than top 10
            listForWrite.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        }

        foreach (var element in listForWrite) {
            print(element);
        }

        string path = Directory.GetCurrentDirectory();
        print("Your scoreboard is saved in: " + path);


        listForWrite.Reverse();
        File.WriteAllLines(@".\scoreboard.txt", listForWrite.Select(value => value.ToString()).ToArray());
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
            listForRead.Sort((x, y) => x.Value.CompareTo(y.Value));
            listForRead.Reverse();

            for (int i = 0; i < lines.Length; i++) {
                print("Line from textfile: " + listForRead[i]);
            }
        }
        return listForRead;
    }
}
