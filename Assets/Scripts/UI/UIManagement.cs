using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;
    [Header("Win")]
    [SerializeField] private GameObject winScreen;
    [Header("GameOver")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [Header("Point")]
    [SerializeField] private Text pointValue;
    [SerializeField] private Health playerPoint;

    private bool isGameOver;
    private void Awake()
    {
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
        isGameOver = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            //If pause screen already active unpause and viceversa
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
        pointValue.text = playerPoint.currentPoint.ToString();
        if (Input.GetKeyDown(KeyCode.G))
            Win();
    }
    public void PauseGame(bool status)
    {
        //if status == true pause || false not pause
        pauseScreen.SetActive(status);

        //When pause status is true change timescale to 0 (time stops)||change it back to 1(times goes by normally)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    public void Quit()
    {
        Application.Quit();//Quits the game (only works on build)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Exists play mode (will only be excuted in the editor)
#endif
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
        isGameOver = true;
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
        isGameOver = false;
    }
    public void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
        isGameOver = true;
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
