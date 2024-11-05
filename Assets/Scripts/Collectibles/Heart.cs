using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private int pointValue;
    [SerializeField] private AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().AddHealth(healthValue,pointValue);
            gameObject.SetActive(false);
        }
    }
}
