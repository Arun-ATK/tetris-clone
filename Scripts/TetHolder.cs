using UnityEngine;

public class TetHolder : MonoBehaviour
{
    public GameManager gm;

    public float moveVal = 0.1f;

    private Cell[] children;
    private float fallTimer;
    private float fallIncrement;

    private float moveIncrement;
    private float moveTimer;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        children = GetComponentsInChildren<Cell>();

        fallTimer = fallIncrement = gm.fallVal;
        moveTimer = moveIncrement = moveVal;
    }

    private void Update()
    {
        bool moveLeft  = false;
        bool moveRight = false;


        if (Time.time > moveTimer) {
            moveLeft  = Input.GetKey(KeyCode.A);
            moveRight = Input.GetKey(KeyCode.D);

            moveTimer = Time.time + moveVal;
        }

        float x = transform.position.x;
        float y = transform.position.y;

        foreach (Cell child in children) {
            if(child.transform.position.x <= 0 || gm.playField[(int)child.transform.position.x - 1, (int)child.transform.position.y]) {
                moveLeft = false;
            }
            else if(child.transform.position.x >= 9 || gm.playField[(int)child.transform.position.x + 1, (int)child.transform.position.y]) {
                moveRight = false;
            }
        }
        if(moveLeft) {
            transform.position = new Vector2(x - 1, y);
        }
        else if(moveRight) {
            transform.position = new Vector2(x + 1, y);
        }

        if(Time.time >= fallTimer) {
            Fall();
            fallTimer = Time.time + fallIncrement;
        }
    }

    private void Fall()
    {
        bool canFall = true;
        foreach (Cell cell in children) {
            canFall &= cell.IsSafe();
        }

        if(!canFall) {
            StopFalling();
        }
        else {
            float x = transform.position.x;
            float y = transform.position.y;
            transform.position = new Vector2(x, y - 1);
        }
    }

    private void StopFalling()
    {
        foreach (Cell child in children) {
            child.markField();
        }

        gm.SpawnNext();

        transform.DetachChildren();
        Destroy(gameObject);
    }
}
