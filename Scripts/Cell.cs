using UnityEngine;

public class Cell : MonoBehaviour
{
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
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
