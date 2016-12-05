using UnityEngine;
using System.Collections.Generic;
using System;

public class RngMapGenerator : MonoBehaviour {
    /*
     * 0 - Empty space / floor
     * 1 - Wall top
     * 2 - Wall topside
     * 3 - Wall bottomside
     * 
     */
    // Integers for for loops to generate the map
    public int maxWidth;
    public int maxHeight;
    public int maxRooms;

    // Gameobjects that we will instantiate on the map
    public GameObject wallTop;
    public GameObject wallUpper;
    public GameObject wallLower;
    public GameObject floor;
    public GameObject trapdoor;

    // Path generation pointer
    Vector2 pathGenerationPointer;
    int pointerDirection;

    // Lists for path and map
    public List<List<int>> path;
    public List<List<int>> map;

	// Use this for initialization
	void Start () {
        map = new List<List<int>>();
        initalizeEmptyMap();
        initalizeOuterWall();
        initializePlayerSpawnAndEnd();
        drawMap();

        //Show map for development purpoose
        String mapString = "";
        foreach (List<int> row in map)
        {
            String rowString = "";
            foreach (int spot in row) {
                rowString += spot;
            }
            mapString += rowString + "\n";
        }
        print(mapString);
        
	}

    

    // Update is called once per frame
    void Update () {

    }

    void initalizeEmptyMap() {
        for (int row = 0; row < maxHeight; row++) {
            List<int> rowTiles = new List<int>();
            for (int spot = 0; spot < maxWidth; spot++) {
                rowTiles.Add(0);
            }
            map.Add(rowTiles);
        }
    }

    private void initalizeOuterWall()
        
    {
        for (int row = 0; row < maxHeight; row++) {
            for (int column = 0; column < maxWidth; column++) {
                if (column == 0 || column == maxWidth - 1) {
                    map[row][column] = 1;
                } else if (row == 0 || row == maxHeight - 3)
                {
                    map[row][column] = 1;
                } else if (row == 1 || row == maxHeight - 2)
                {
                    map[row][column] = 2;
                } else if (row == 2 || row == maxHeight - 1) {
                    map[row][column] = 3;
                }
            }
        }
        map[maxHeight - 2][0] = 2;
        map[maxHeight - 2][maxWidth - 1] = 2;
        map[maxHeight - 1][0] = 3;
        map[maxHeight - 1][maxWidth - 1] = 3;

    }

    private void initializePlayerSpawnAndEnd()
    {
        map[maxHeight - 5][1] = 8;
        map[3][maxWidth - 3] = 9;
    }

    void initalizePathGenerator() {
        
    }

    void generatePath() {

    }

    void generateMap() {

    }

    void drawMap() {
        for (int row = 0; row < maxHeight; row++)
        {
            for (int column = 0; column < maxWidth; column++)
            {
                print(map[row][column]);
                if (map[row][column] == 0 || map[row][column] == 8)
                {
                    floor.transform.position = new Vector3(64 * column, -64 * row, 0);
                    Instantiate<GameObject>(floor);
                }
                else if (map[row][column] == 1)
                {
                    wallTop.transform.position = new Vector3(64 * column, -64 * row, 0);
                    Instantiate<GameObject>(wallTop);
                }
                else if (map[row][column] == 2)
                {
                    wallUpper.transform.position = new Vector3(64 * column, -64 * row, 0);
                    Instantiate<GameObject>(wallUpper);
                }
                else if (map[row][column] == 3)
                {
                    wallLower.transform.position = new Vector3(64 * column, -64 * row, 0);
                    Instantiate<GameObject>(wallLower);
                }
                else if (map[row][column] == 9) {
                    trapdoor.transform.position = new Vector3(64 * column + 32, -64 * row - 32, 0);
                    Instantiate<GameObject>(trapdoor);
                }
            }
        }
    }
}
