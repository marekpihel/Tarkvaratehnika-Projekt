using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scoreboard : MonoBehaviour {
 
    public static List<KeyValuePair<string, int>> writeToScoreboard(String playerName, int playerScore) {
        var scoreList = readFromScoreboard();
        var currentPair = new KeyValuePair<string, int>(playerName, playerScore);

        scoreList.Add(currentPair);
        scoreList = orderByValue(scoreList);

        File.WriteAllLines("scoreboard.txt", scoreList.Select(value => value.ToString()).ToArray());
        return scoreList;
    }

    public static List<KeyValuePair<string, int>> readFromScoreboard() {
        var scoreList = new List<KeyValuePair<string, int>>();
        if (File.Exists("scoreboard.txt")) {
            scoreList = parseFileToList();
            scoreList = orderByValue(scoreList);
        }
        return scoreList;
    }


    private static List<KeyValuePair<string, int>> parseFileToList() {
        var temporaryList = new List<KeyValuePair<string, int>>();
        string[] lines = File.ReadAllLines("scoreboard.txt");
        for (int i = 0; i < lines.Length; i++) {
            string cleanString = lines[i].Replace("[", "").Replace("]", "");
            string[] line = cleanString.Split(',');
            string playerName = line[0];
            int playerScore = Int32.Parse(line[1]);
            temporaryList.Add(new KeyValuePair<string, int>(playerName, playerScore));
        }
        return temporaryList;
    }

    public static List<KeyValuePair<string, int>> orderByValue(List<KeyValuePair<string, int>> scoreList) {
        for (int i = 0; i < scoreList.Count; i++) {
            scoreList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        }
        scoreList.Reverse();
        return scoreList;
    }
}
