using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject controls;
    [SerializeField] GameObject story;

    public int currentControlsPage = 0;

    private void Start()
    {
        CloseControls();
        story.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ViewControls()
    {
        CloseControls();
        currentControlsPage = 0;
        controls.transform.GetChild(currentControlsPage).gameObject.SetActive(true);
    }

     public void NextPage()
    {
        CloseControls();
        currentControlsPage++;
        controls.transform.GetChild(currentControlsPage).gameObject.SetActive(true);
    }

    public void PrevPage()
    {
        CloseControls();
        currentControlsPage--;
        controls.transform.GetChild(currentControlsPage).gameObject.SetActive(true);
    }

    public void CloseControls()
    {
        foreach (Transform control in controls.transform)
        {
            control.gameObject.SetActive(false);
        }
    }

    public void ViewStory()
    {
        story.SetActive(true);
    }

    public void CloseStory()
    {
        story.SetActive(false);
    }
}
