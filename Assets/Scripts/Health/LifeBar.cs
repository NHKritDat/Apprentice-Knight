using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Health playerLife;
    [SerializeField] private Image totalLifeBar;
    [SerializeField] private Image currentLifeBar;
    private void Start()
    {
        totalLifeBar.fillAmount = playerLife.currentLife / 10;
    }
    private void Update()
    {
        currentLifeBar.fillAmount = playerLife.currentLife / 10;
    }
}
