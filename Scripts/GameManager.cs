using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[,] playField = new bool[10, 22];
    public Transform[] spawnerPoints;

    public GameObject[] tetrominos;

    //public GameObject tetrominos;s
    //public GameObject cell;
    //public Transform[] spawnPoints;
    //[Header("Tetrominos")]
    //int[] tetOSpawnPoints = { 0, 1, 3, 4 };

    private void Start()
    {
        //GameObject tetO = tetrominos.transform.GetChild(3).gameObject;
        //foreach (var spPoint in tetO) {
        //    Instantiate(cell, spawnPoints[spPoint].position, Quaternion.identity);
        //}

        GameObject tetO = tetrominos[3].transform.gameObject;
        Instantiate(tetO, spawnerPoints[1].position, Quaternion.identity);

    }
}