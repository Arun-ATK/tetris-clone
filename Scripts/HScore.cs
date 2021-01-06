using UnityEngine;
using UnityEngine.UI;

public class HScore : MonoBehaviour
{
    public Text highScoreText;

    private int m_highScore;
    // Start is called before the first frame update
    void Start()
    {
        m_highScore = PlayerPrefs.GetInt("High Score", 0);
        highScoreText.text = m_highScore.ToString();
    }
}
