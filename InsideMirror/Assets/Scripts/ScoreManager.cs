using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public static int currentScore = 0;

    private void Start()
    {
        currentScore = 0;
        text.text = currentScore.ToString("00");
    }

    public void IncrementScore()
    {
        currentScore++;
        text.text = currentScore.ToString("00");
    }
}
