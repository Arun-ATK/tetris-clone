using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public float startTimer = 1f;
    public float fallTimer = 1f;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Time.time >= startTimer) {
    //        Fall();

    //        startTimer = Time.time + fallTimer;
    //    }
    //}
    public bool IsSafe()
    {
        // TODO: Ensure that floating values don't occur
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        //print(!gm.playField[x, y - 1] && y >= 0);
        return (y > 0 && !gm.playField[x, y - 1]);
    }

    public void markField()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        gm.playField[x, y] = true;
    }

    //void Fall()
    //{
    //    int x = (int) transform.position.x;
    //    int y = (int) transform.position.y;

    //    if (y <= 0 || gm.playField[x, y - 1]) {

    //        print("caller: " + transform.position);
    //        GetComponentInParent<TetHolder>().StopFalling();

    //        return;
    //    }

    //    if (!gm.playField[x,y - 1]) {
    //        transform.position = new Vector2(x, y - 1);
    //    }
    //}

    //void StopFall(int x, int y)
    //{
    //    gm.playField[x, y] = true;

    //    print(x + "," + y + " : " + gm.playField[x, y]);


    //    GetComponent<Cell>().enabled = false;
    //}
}
