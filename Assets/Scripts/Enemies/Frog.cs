using UnityEngine;

public class Frog : EnemyDamage
{
    private Animator anim;
    private MoveEnemy moveEnemy;
    private Health playerHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        moveEnemy = GetComponentInParent<MoveEnemy>();
    }
    private void Update()
    {
        moveEnemy.enabled = true;
    }
}
