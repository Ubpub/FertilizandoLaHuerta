using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Image pauseButtonImage;

    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;

    private bool isPaused;

    public void TogglePause()
    {
        isPaused = !isPaused;

        pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        if (pauseButtonImage != null)
        {
            pauseButtonImage.sprite =
                isPaused ? playSprite : pauseSprite;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;

        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        if (pauseButtonImage != null)
            pauseButtonImage.sprite = pauseSprite;
    }
}