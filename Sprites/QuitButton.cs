using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    public int mainMenuIndex = 0;
    public int gameIndex     = 1;

    // Start is called before the first frame update
    public void QuitGame()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }
}
