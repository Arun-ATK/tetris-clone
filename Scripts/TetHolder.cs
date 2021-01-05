using UnityEngine;

public class TetHolder : MonoBehaviour
{
    public GameManager gm;

    private Cell[] children;

    private float fallTimer;
    private float fallIncrement;

    private float moveIncrement;
    private float moveTimer;

    private float rotateIncrement;
    private float rotateTimer;

    private void Start()
    {
        gm       = FindObjectOfType<GameManager>();
        children = GetComponentsInChildren<Cell>();

        fallTimer = fallIncrement = gm.fallVal;
        moveTimer = moveIncrement = gm.moveVal;
        rotateTimer = rotateIncrement = gm.rotateVal;
    }

    private void Update()
    {
        if(Time.time >= rotateTimer) {
            Rotate(Input.GetKey(KeyCode.Slash), Input.GetKey(KeyCode.Period));
        }

        if (Time.time >= moveTimer) {
            LeftRightMovement(Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D));
        }

        if (Time.time >= fallTimer) {
            Fall();
        }
    }

    private void Rotate(bool clockwise = false, bool antiClockwise = false)
    {
        /// NOTE: Could be done better
        if (!clockwise && !antiClockwise) {
            return;    /// Dont execute if no input given
        }

        Vector2[] newLocalChildPos = new Vector2[children.Length];

        float parentX = transform.position.x;
        float parentY = transform.position.y;

        bool isBlocked = false;

        /// Rotation is done by transposing the cell matrix (wrt TetHolder), then
        /// rows are reversed for anticlockwise rotation
        /// columns are reversed for clockwise rotation
        /// (Opp to usual maths, since matrix 0,0 taken at centre
        /// and not at corner
        for(int i = 0; i < children.Length; ++i) {
            /// Transposing a matrix by interchanging is x,y cords
            newLocalChildPos[i].x = children[i].transform.localPosition.y;
            newLocalChildPos[i].y = children[i].transform.localPosition.x;

            /// Reversing column or row based on input for rotation direction
            if (antiClockwise) {
                newLocalChildPos[i].x = -newLocalChildPos[i].x;
            }
            else if (clockwise) { 
                newLocalChildPos[i].y = -newLocalChildPos[i].y;
            }

            float globalCellX = parentX + newLocalChildPos[i].x;
            float globalCellY = parentY + newLocalChildPos[i].y;

            /// Checking if any cell crosses over the boundary
            /// or if they are being blocked by another present cell
            if (globalCellX < 0 || globalCellX > 9 || globalCellY < 0) {
                isBlocked = true;
            }
            else {
                isBlocked |= gm.playField[(int)globalCellX, (int)globalCellY];
            }
        }

        /// Old position being replaced by new one
        /// since no problems encountered in above checks
        if(!isBlocked) {
            for(int i = 0; i < children.Length; ++i) {
                children[i].transform.localPosition = new Vector2(newLocalChildPos[i].x, newLocalChildPos[i].y);
            }
        }

        rotateTimer = Time.time + rotateIncrement;
    }

    private void LeftRightMovement(bool moveLeft = false, bool moveRight = false)
    {
        float parentX = transform.position.x;
        float parentY = transform.position.y;

        foreach (Cell child in children) {
            float childX = child.transform.position.x;
            float childY = child.transform.position.y;

            if (childX <= 0 || gm.playField[(int)childX - 1, (int)childY]) {
                moveLeft = false;
                break;
            }
            else if (childX >= 9 || gm.playField[(int)childX + 1, (int)childY]) {
                moveRight = false;
                break;
            }
        }

        if (moveLeft) {
            transform.position = new Vector2(parentX - 1, parentY);
        }
        else if (moveRight) {
            transform.position = new Vector2(parentX + 1, parentY);
        }

        moveTimer = Time.time + moveIncrement;
    }

    private void Fall()
    {
        /// Checks if the tetrimino is unhindered
        /// i.e., it can fall safely without anything blocking it
        bool canFall = true;
        foreach (Cell cell in children) {
            canFall &= cell.IsSafe();
        }

        if(canFall) {
            float x = transform.position.x;
            float y = transform.position.y;
            transform.position = new Vector2(x, y - 1);

            gm.score += 1;
        }
        else {
            StopFalling();
            gm.SpawnNext();
        }

        fallTimer = Time.time + fallIncrement;
    }

    private void StopFalling()
    {
        foreach (Cell child in children) {
            child.MarkField();
        }
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
