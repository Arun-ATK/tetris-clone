using UnityEngine;

public class TetHolder : MonoBehaviour
{
    public GameManager gm;

    private Cell[] children;
    private float fallTimer;
    private float fallIncrement;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        children = GetComponentsInChildren<Cell>();

        fallTimer = fallIncrement = gm.fallVal;
    }

    private void Update()
    {
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
