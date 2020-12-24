using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
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
        if (Time.time >= fallTimer) {
            Fall();

            fallTimer = Time.time + 1;
        }
    }

    void Fall()
    {
        int x = (int) transform.position.x;
        int y = (int) transform.position.y;

        if (y <= 0) {
            return;
        }

        if (!gm.playField[x,y - 1]) {
            transform.position = new Vector2(x, y - 1);
        }
    }
}
