using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField]private UIManagement uIManagement;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            uIManagement.Win();
    }
}
