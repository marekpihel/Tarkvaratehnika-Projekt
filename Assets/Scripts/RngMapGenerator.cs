using UnityEngine;
using System.Collections.Generic;
using System;

public class RngMapGenerator : MonoBehaviour {
    /*
     * 0 - Empty space / floor
     * 1 - Wall top
     * 2 - Wall topside
     * 3 - Wall bottomside
     * 4 - path
     * 8 - Player spawn
     * 9 - trapdoor
     * 
     */
    // Integers for for loops to generate the map
    public int maxWidth;
    public int maxHeight;

    public int maxRooms;

    public int maxRoomWidth;
    public int maxRoomHeight;

    // Gameobjects that we will instantiate on the map
    public GameObject wallTop;
    public GameObject wallUpper;
    public GameObject wallLower;
    public GameObject floor;
    public GameObject trapdoor;
    public GameObject playerSpawner;

    // Path generation pointer
    List<int> pathGenerationPointer;
    int pointerDirection;

    // Lists for path and map
    public List<List<int>> path;
    public List<List<int>> map;
    


    // Use this for initialization
    void Start () {
        map = new List<List<int>>();
        path = new List<List<int>>();
        pathGenerationPointer = new List<int>();
        initalizeEmptyMap();
        initalizeOuterWall();
        initalizePathGenerator();
        generatePath();
        initializePlayerSpawnAndEnd();
        
        
        instantiateMap();
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
        pathGenerationPointer.Add(1);
        pathGenerationPointer.Add(maxHeight - 5);
        path.Add(pathGenerationPointer);
    }

    void generatePath() {
        while (pathGenerationPointer[0] != maxWidth - 3 || pathGenerationPointer[1] != 3)
        {

            /**
             * Directions:
             * 1 - up 
             * 2 - right 
             * */

            if (pathGenerationPointer[0] != maxWidth - 3)
            {
                pointerDirection = (int)UnityEngine.Random.Range(1f, 2.99f);
                if(pathGenerationPointer[1] != 3) {
                    if (pointerDirection == 1)
                    {
                        pathGenerationPointer[1] -= 1;
                    }
                }
                if (pathGenerationPointer[0] != maxWidth - 3)
                {
                    if (pointerDirection == 2)
                    {
                        pathGenerationPointer[0] += 1;
                    }
                }
            }
            else
            {
                pathGenerationPointer[1] -= 1;
            }
            path.Add(pathGenerationPointer);
            foreach (List<int> spot in path)
            {
                int x = spot[0];
                int y = spot[1];
                print("X: " + x + " Y: " + y);
                map[y][x] = 4;
                map[y][x + 1] = 4;
                map[y + 1][x] = 4;
                map[y + 1][x + 1] = 4;
            }
        }
        map[maxHeight - 5][1] = 4;
        map[maxHeight - 5][2] = 4;
        map[maxHeight- 4][1] = 4;
        map[maxHeight - 4][2] = 4;



        foreach (List<int> spot in path)
        {
            
        }
    }

    void generateMap() {

    }

    void instantiateMap() {
        string mapstring = "";
        foreach (List<int> row in map) {
            foreach(int spot in row)
            {
                mapstring += spot.ToString();
            }
            mapstring += "\n";
        }
        print(mapstring);
        for (int row = 0; row < maxHeight; row++)
        {
            for (int column = 0; column < maxWidth; column++)
            {
                if (map[row][column] == 0)
                {
                    floor.transform.position = new Vector3(64 * column, -64 * row, 5);
                    Instantiate<GameObject>(floor);
                }
                else if (map[row][column] == 1)
                {
                    wallTop.transform.position = new Vector3(64 * column, -64 * row, 5);
                    Instantiate<GameObject>(wallTop);
                }
                else if (map[row][column] == 2)
                {
                    wallUpper.transform.position = new Vector3(64 * column, -64 * row, 5);
                    Instantiate<GameObject>(wallUpper);
                }
                else if (map[row][column] == 3)
                {
                    wallLower.transform.position = new Vector3(64 * column, -64 * row, 5);
                    Instantiate<GameObject>(wallLower);
                }
                else if (map[row][column] == 9)
                {
                    trapdoor.transform.position = new Vector3(64 * column + 32, -64 * row - 32, -3);
                    Instantiate<GameObject>(trapdoor);
                }
                else if (map[row][column] == 8) {
                    playerSpawner.transform.position = new Vector3(64 * column + 32, -64 * row - 32, -5);
                    Instantiate<GameObject>(playerSpawner);
                }
            }
        }
    }
}
