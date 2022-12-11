using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TimeManager tm;

    public bool canListenInput = true;
    private bool gameOver = false;

    private void Start()
    {
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        gameOver = false;
    }

    private void Update()
    {
        canListenInput = !PauseMenu.activeSelf;
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            if (PauseMenu.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if(Health.gameOver && !gameOver)
        {   
            gameOver = true;
            Invoke(nameof(GameOver), 1f);
        }
    }
    private void PauseGame()
    {
        tm.PauseGame();
    }

    public void ResumeGame()
    {   
        PauseMenu.SetActive(false);
        tm.StopSlowMotion();
    }

    public void GameOver()
    {
        scoreText.text = "Score: " + ScoreManager.currentScore.ToString("00");
        tm.PauseGame();
        GameOverMenu.SetActive(true);
    }

     public void Retry()
    {
        Health.gameOver = false;
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
