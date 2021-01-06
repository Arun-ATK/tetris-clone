using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    private int m_score;
    

    // Start is called before the first frame update
    void Start()
    {
        m_score = PlayerPrefs.GetInt("Score", 5);

        scoreText.text = m_score.ToString();
    }

}
