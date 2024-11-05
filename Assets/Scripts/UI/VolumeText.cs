using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string txtIntro;//Sound: or Music:
    private Text txt;
    private void Awake()
    {
        txt = GetComponent<Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        float volumnValue = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = txtIntro + volumnValue.ToString();
    }
}
