using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    private float coolDownTimer = Mathf.Infinity;
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    private Health enemyHealth;
    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;
    private Animator anim;
    private PlayerMovement playerMovement;
    public bool isBlocked { get; private set; }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && coolDownTimer > attackCooldown && playerMovement.CanAttack())
            Attack();
        coolDownTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && playerMovement.CanAttack())
            Block(true);
        if (Input.GetMouseButtonUp(1) && playerMovement.CanAttack())
            Block(false);
    }
    private void Attack()
    {
        SoundManager.instance.PlaySound(attackSound);
        anim.SetTrigger("attack");
        DamageEnemy();
        coolDownTimer = 0;
    }
    private void Block(bool status)
    {
        SoundManager.instance.PlaySound(attackSound);
        anim.SetBool("block", status);
        isBlocked = status;
        playerMovement.canRun = !status;
    }
    private bool EnemyInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null)
            enemyHealth = hit.collider.GetComponent<Health>();

        return hit.collider != null;
    }
    private void DamageEnemy()
    {
        //If enemy still in range damage him
        if (EnemyInSight())
            enemyHealth.TakeDamage(damage);
    }
}
