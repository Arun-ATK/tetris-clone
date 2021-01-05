using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    public GameManager gm;

    void Awake()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Checks if the position below a cell is empty
    public bool IsSafe()
    {
        // TODO: Ensure that floating values don't occur
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        // Error Conditions
        if(x < 0 || x > 19) {
            x = 5;
            Debug.LogError("<b>ERROR</b>: Position of cell out of bounds!");
        }

        if(!gm) {
            Debug.LogError("GM NOT SET!");
            Debug.Break();
        }

        return (y > 0 && !gm.playField[x, y - 1]);
    }

    // Marks the current position as occupied
    public void MarkField()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        gm.playField[x, y] = true;
    }
}