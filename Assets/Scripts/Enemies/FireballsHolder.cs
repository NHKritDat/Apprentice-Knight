using UnityEngine;

public class FireballsHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    private void Update()
    {
        transform.localScale = enemy.localScale;
    }
}
