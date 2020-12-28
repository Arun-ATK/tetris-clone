using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[,] playField = new bool[10, 24];

    public Transform[] spawnerPoints;
    public GameObject[] tetrominos;

    public float fallVal = 0.3f;

    public void SpawnNext()
    {
        bool canPlay = true;
        for(int i = 0; i < playField.GetLength(0); ++i) {
            for(int j = 20; j < playField.GetLength(1); ++j) {
                canPlay &= !playField[i, j];
            }
        }
        if (canPlay) {
            checkLines();
            Start(); 
        }
        else {
            print("Game Over");
        }
    }

    private void Start()
    {

        int tetToSpawn = Random.Range(0, tetrominos.Length);

        // Debug commands
        //Wall();
        //tetToSpawn = 3;

        GameObject tetromino = tetrominos[tetToSpawn].transform.gameObject;

        int spawnPoint = 0;
        if(tetromino.CompareTag("TetO")) {
            spawnPoint = 1;
        }

        Instantiate(tetromino, spawnerPoints[spawnPoint].position, Quaternion.identity);
    }

    private void checkLines()
    {

        bool lineFilled = true;
        Debug.Log("Checking lines " + lineFilled);
        for(int row = 0; row < playField.GetLength(1); ++row) {
            for(int col = 0; col < playField.GetLength(0); ++col) {
                lineFilled &= playField[col, row];
            }

            if(lineFilled) {
                clearLine(row);
                --row;
            }
            else {
                lineFilled = true;
            }
        }
    }

    private void clearLine(int row)
    {
        Cell[] cells = FindObjectsOfType<Cell>();

        List<Cell> cellsAbove = new List<Cell>();
        List<Cell> cellsToDelete = new List<Cell>();

        foreach (Cell cell in cells) {
            if(cell.transform.position.y > row) {
                cellsAbove.Add(cell);
            }
            else if(cell.transform.position.y == row) {
                cellsToDelete.Add(cell);
            }
        }

        foreach (Cell cell in cellsToDelete) {
            Destroy(cell.gameObject);
            playField[(int)cell.transform.position.x, (int)cell.transform.position.y] = false;
        }

        foreach (Cell cell in cellsAbove) {
            cell.transform.position = new Vector2(cell.transform.position.x, cell.transform.position.y - 1);
        }

        for(int j = row; j < playField.GetLength(1) - 1; ++j) {
            for (int i = 0; i < playField.GetLength(0); ++i) {
                playField[i, j] = playField[i, j + 1];
            }
        }
    }


    //private void Wall()
    //{
    //    for (int i = 0; i < 10; ++i) {
    //        playField[i, 5] = true;
    //    }
    //    playField[5, 5] = true;
    //}
}