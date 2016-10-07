using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scoreboard : MonoBehaviour {


    // Use this for initialization
    void Start() {
        WriteScoreboard();
        ReadScoreboard();
    }

    // Update is called once per frame
    void Update() {

    }

    private static void WriteScoreboard() {

        var listForWrite = new List<KeyValuePair<string, int>>();

        KeyValuePair<string, int> testpair = new KeyValuePair<string, int>("Dachshund", 3);

        listForWrite.Add(new KeyValuePair<string, int>("Cat", 5));
        listForWrite.Add(new KeyValuePair<string, int>("Dog", 2));
        listForWrite.Add(new KeyValuePair<string, int>("Rabbit", 4));

        for (int i = 0; i < listForWrite.Count; i++) {
            if (testpair.Value > listForWrite[i].Value) {
                listForWrite[0] = testpair; //only add to list if bigger than top 10
                listForWrite.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                break;
            }
        }

        foreach (var element in listForWrite) {
            print(element);
        }

        string path = Directory.GetCurrentDirectory();
        print("Your scoreboard is saved in: " + path);


        listForWrite.Reverse();
        File.WriteAllLines(@".\scoreboard.txt", listForWrite.Select(value => value.ToString()).ToArray());
    }

    private static void ReadScoreboard() {
        if (File.Exists("scoreboard.txt")) {
            var listForRead = new List<KeyValuePair<string, int>>();
            string[] lines = File.ReadAllLines(@".\scoreboard.txt");

            for (int i = 0; i < lines.Length; i++) {
                string cleanValue = lines[i].Replace("[", "").Replace("]", "");
                string[] line = cleanValue.Split(',');
                string valString = line[0];
                int valInt = Int32.Parse(line[1]);
                listForRead.Add(new KeyValuePair<string, int>(valString, valInt));
            }
            //listForRead.Reverse();

            for (int i = 0; i < lines.Length; i++) {
                print("Line from textfile: " + listForRead[i]);
            }
        }
    }
}
