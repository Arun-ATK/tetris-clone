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
    void Update()
    {
        if (Time.time >= startTimer) {
            Fall();

            startTimer = Time.time + fallTimer;
        }
    }

    void Fall()
    {
        int x = (int) transform.position.x;
        int y = (int) transform.position.y;

        if (y <= 0 || gm.playField[x, y - 1]) {
            //TetHolder parent = GetComponentInParent<TetHolder>();
            //parent.StopFalling(x, y);

            Debug.Log("(" + x + ", " + y + "): " + gm.playField[x, y]);
            GetComponentInParent<TetHolder>().StopFalling(x, y);

            return;
        }

        if (!gm.playField[x,y - 1]) {
            transform.position = new Vector2(x, y - 1);
        }

        //print("Falling");
    }

    public void StopFall(int x, int y)
    {
        gm.playField[x, y] = true;

        print(transform.position + " : " + gm.playField[x, y]);


        GetComponent<Cell>().enabled = false;
    }
}
