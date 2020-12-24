using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[,] playField = new bool[10, 22];
    public Transform[] spawnerPoints;

    public GameObject[] tetrominos;

    public void SpawnNext()
    {
        Start();
        //print(playField[5, 1]);
    }

    private void Start()
    {
        Wall();

        int tetToSpawn = Random.Range(0, tetrominos.Length);

        tetToSpawn = 1;

        GameObject tetromino = tetrominos[tetToSpawn].transform.gameObject;

        int spawnPoint = 0;
        if(tetromino.tag == "tetO") {
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