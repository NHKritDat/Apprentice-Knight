using UnityEngine;

public class Dragon : EnemyDamage
{
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Fireball Sound")]
    [SerializeField] private AudioClip fireballSound;
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    private float cooldownTimer = Mathf.Infinity;
    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private MoveEnemy moveEnemy;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        moveEnemy=GetComponentInParent<MoveEnemy>();
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;//cooldown
        //Attack only when player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }
        if(moveEnemy != null) 
            moveEnemy.enabled = !PlayerInSight();//In sight do not move
    }
    private int FindFireballl()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        cooldownTimer = 0;
        fireballs[FindFireballl()].transform.position = firepoint.position;
        fireballs[FindFireballl()].GetComponent<Fireball>().Activate();
    }
}
