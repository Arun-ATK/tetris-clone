using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[,] playField = new bool[10, 24];
    public Transform[] spawnerPoints;

    public GameObject[] tetrominos;

    public void SpawnNext()
    {
        bool canPlay = true;
        for(int i = 0; i < playField.GetLength(0); ++i) {
            for(int j = 20; j < playField.GetLength(1); ++j) {
                canPlay &= !playField[i, j];
            }
        }
        if (canPlay) {
            Start(); 
        }
        else {
            print("Game Over");
        }
        //print(playField[5, 1]);
    }

    private void Start()
    {
        //Wall();

        int tetToSpawn = Random.Range(0, tetrominos.Length);

        //tetToSpawn = 1;

        GameObject tetromino = tetrominos[tetToSpawn].transform.gameObject;

        int spawnPoint = 0;
        if(tetromino.CompareTag("TetO")) {
            spawnPoint = 1;
        }

        Instantiate(tetromino, spawnerPoints[spawnPoint].position, Quaternion.identity);

    }

    private void Wall()
    {
        //for(int i = 0; i < 10; ++i) {
        //    playField[i, 5] = true;
        //}
        playField[5, 5] = true;
    }
}