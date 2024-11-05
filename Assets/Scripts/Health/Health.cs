using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] private float startingLife;
    private Animator anim;
    private bool dead; //Check player dead or not
    public float currentHealth { get; private set; }//store current player's health
    public float currentLife { get; private set; }//store current player's life
    public int currentPoint { get; private set; }//store current player's point
    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;
    private bool invulnerable;
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GameObject player;
    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [Header("Hurt Sound")]
    [SerializeField] private AudioClip hurtSound;
    private void Awake()
    {
        currentHealth = startingHealth;
        currentLife = startingLife;
        currentPoint = 0;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void PlayerTakeDamage(float damage)
    {
        if (!playerAttack.isBlocked)//Check player is blocking or not
            TakeDamage(damage);
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); //minuse current health
        if (currentHealth > 0) //hurt when currentHealth >0, otherwise, die
        {
            currentPoint -= 1;
            anim.SetTrigger("hurt");
            StartCoroutine(Invunnerability());//Play invunnerable
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)//Make sure play die one time
            {
                currentPoint -= 5;
                SoundManager.instance.PlaySound(deathSound);
                foreach (Behaviour component in components)//disable all component
                    component.enabled = false;
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");
                dead = true;//Make sure play die one time
            }
        }
    }
    private IEnumerator Invunnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)//Play Iframe flashes
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }
    public void AddHealth(float _value,int _point)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        currentPoint += _point;
    }
    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth,0);
        if (currentLife > 0)
            currentLife -= 1;
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunnerability());
        foreach (Behaviour component in components)
            component.enabled = true;
    }
    public void AddLife()
    {
        currentLife = startingLife;
    }
    public void Deactive() //Disable enemy
    {
        player.GetComponent<Health>().AddHealth(0, 5);
        gameObject.SetActive(false);
    }
}
