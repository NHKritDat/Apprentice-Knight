using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManagement uiManagement;
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManagement = FindObjectOfType<UIManagement>();
    }
    public void CheckRespawn()
    {
        //Check if check point available
        if (playerHealth.currentLife == 0)
        {
            //Show game over screen
            uiManagement.GameOver();
            return;
        }
        playerHealth.Respawn();//Restore player health and reset animation
        transform.position = currentCheckpoint.position;//Move player to checkpoint position
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Activate checkpoints
        if (collision.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;//Store the checkpoint that we activated as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            playerHealth.AddLife();//Restore number of life
            collision.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger checkpoint animation
        }
    }
}
