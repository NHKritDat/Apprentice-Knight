using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject menuScreen;
    private void Awake()
    {
        menuScreen.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
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
}
