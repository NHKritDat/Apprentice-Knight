using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private float damage;
    private float lifeTime;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool hit;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void Activate()
    {
        //Activate fireball
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        //Fireball move
        float movementSpeed = Time.deltaTime * speed;
        transform.Translate(movementSpeed, 0, 0);
        //Fireball get kill
        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime)
            gameObject.SetActive(false);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        if (collision.tag == "Player")
            collision.GetComponent<Health>().PlayerTakeDamage(damage);
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
}
