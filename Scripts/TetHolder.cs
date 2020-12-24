using UnityEngine;

public class TetHolder : MonoBehaviour
{
    public GameManager gm;
    private Cell[] children;

    public void Start()
    {
        gm = FindObjectOfType<GameManager>();

        children = GetComponentsInChildren<Cell>();
        //print(children.Length);
    }

    public void StopFalling()
    {
        foreach (Cell child in children) {
            print(child.transform.position + " is called");
            child.StopFall((int)child.transform.position.x, (int)child.transform.position.y);
        }

        gm.SpawnNext();

        transform.DetachChildren();
        Destroy(gameObject);
    }
}
