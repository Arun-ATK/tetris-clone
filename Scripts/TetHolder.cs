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
        //Rotate();
        LeftRightMovement();

        if (Time.time >= fallTimer) {
            Rotate();
            Fall();
            fallTimer = Time.time + fallIncrement;
        }
    }

    private void Rotate()
    {
        bool isBlocked = false;

        Vector2[] newLocalChildPos = new Vector2[children.Length];

        for(int i = 0; i < children.Length; ++i) {
            newLocalChildPos[i].x = -children[i].transform.localPosition.y;
            newLocalChildPos[i].y =  children[i].transform.localPosition.x;

            float globalX = transform.position.x + newLocalChildPos[i].x;
            float globalY = transform.position.y + newLocalChildPos[i].y;

            if (globalX < 0 || globalX > 9 || globalY < 0) {
                isBlocked = true;
            }
            else {
                isBlocked |= gm.playField[(int)globalX, (int)globalY];
            }
        }

        if(!isBlocked) {
            for(int i = 0; i < children.Length; ++i) {
                children[i].transform.localPosition = new Vector2(newLocalChildPos[i].x, newLocalChildPos[i].y);
            }
        }
    }

    private void LeftRightMovement()
    {
        bool moveLeft = false;
        bool moveRight = false;

        float holderX = transform.position.x;
        float holderY = transform.position.y;

        if (Time.time > moveTimer) {
            moveLeft = Input.GetKey(KeyCode.A);
            moveRight = Input.GetKey(KeyCode.D);

            moveTimer = Time.time + moveVal;
        }

        foreach (Cell child in children) {
            float childX = child.transform.position.x;
            float childY = child.transform.position.y;

            if (childX <= 0 || gm.playField[(int)childX - 1, (int)childY]) {
                moveLeft = false;
            }
            else if (childX >= 9 || gm.playField[(int)childX + 1, (int)childY]) {
                moveRight = false;
            }
        }

        if (moveLeft) {
            transform.position = new Vector2(holderX - 1, holderY);
        }
        else if (moveRight) {
            transform.position = new Vector2(holderX + 1, holderY);
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
        transform.DetachChildren();
        Destroy(gameObject);

        gm.SpawnNext();

    }
}
