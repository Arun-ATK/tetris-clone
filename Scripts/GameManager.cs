using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[,] playField = new bool[10, 24];

    public Transform[] spawnPoints;
    public GameObject[] tetriminos;

    public float fallVal = 0.3f;

    private void Start()
    {
        CreateNewTetrimino();
    }

    public void SpawnNext()
    {
        bool canPlay = true;

        // Iterate through every cell point above 20.
        // If any of the point is occupied, then the player has
        // reached game over conditions.

        // TODO: Implement a function in Cell.cs that checks for endgame,
        //       or calls the check script based on it's location.

        for(int i = 0; i < playField.GetLength(0); ++i) {
            for(int j = 20; j < playField.GetLength(1); ++j) {
                canPlay &= !playField[i, j];
            }
        }

        if (canPlay) {
            CheckLines();
            CreateNewTetrimino();
        }
        else {
            print("Game Over");
        }
    }

    private void CreateNewTetrimino()
    {
        int tetToSpawn = Random.Range(0, tetriminos.Length);
        GameObject tetrimino = tetriminos[tetToSpawn].gameObject;

        int spawnPoint = tetrimino.CompareTag("TetO") ? 1 : 0;

        Instantiate(tetrimino, spawnPoints[spawnPoint].position, Quaternion.identity);
    }

    private void CheckLines()
    {
        /// boolean playfield matrix will have an entire row as true
        /// when it is completely filled. AND-ing all the values will
        /// show if the row is filled or not (false indicates a gap in
        /// the row)
        bool lineFilled = true;
        for(int row = 0; row < playField.GetLength(1); ++row) {
            for(int col = 0; col < playField.GetLength(0); ++col) {
                lineFilled &= playField[col, row];
            }

            if(lineFilled) {
                ClearLine(row);
                --row;
            }
            else {
                /// An empty spot was present in the row (value = false)
                /// This sets lineFilled to false and it must be reset
                /// for checking the next line.
                lineFilled = true;
            }
        }
    }

    private void ClearLine(int row)
    {
        Cell[] cells = FindObjectsOfType<Cell>();

        List<Cell> cellsAbove    = new List<Cell>();
        List<Cell> cellsToDelete = new List<Cell>();

        /// Goes thru every cell in the playfield
        /// and adds cells in the filled row and cells above that
        /// in the respective arrays
        foreach (Cell cell in cells) {
            if(cell.transform.position.y > row) {
                cellsAbove.Add(cell);
            }
            else if(cell.transform.position.y == row) {
                cellsToDelete.Add(cell);
            }
        }

        /// All cells in the filled row are deleted
        foreach (Cell cell in cellsToDelete) {
            Destroy(cell.gameObject);
            playField[(int)cell.transform.position.x, (int)cell.transform.position.y] = false;
        }

        /// All cells in the rows above the filled row
        /// are pushed downwards by 1 step
        foreach (Cell cell in cellsAbove) {
            cell.transform.position = new Vector2(cell.transform.position.x, cell.transform.position.y - 1);
        }

        /// Values of the filled row is replaced by values from 
        /// the above row(since cells are pushed down). This repeats
        /// for every row upto the top
        for(int j = row; j < playField.GetLength(1) - 1; ++j) {
            for (int i = 0; i < playField.GetLength(0); ++i) {
                playField[i, j] = playField[i, j + 1];
            }
        }
    }
}