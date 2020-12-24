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

    public void StopFalling(int x, int y)
    {
        foreach (Cell child in children) {
            //print(child.transform.position);
            child.StopFall(x, y);
        }

        gm.SpawnNext();

        transform.DetachChildren();
        Destroy(gameObject);
    }
}
