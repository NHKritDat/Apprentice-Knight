using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;//first enemy position
    private bool movingLeft;//Check moving left or right
    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Animator anim;
    private void Awake()
    {
        initScale = enemy.localScale;
        movingLeft = true;
    }
    private void OnDisable()
    {
        anim.SetBool("run",false); //Disable when killed
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("run", true);
        //Make enemy face direction
        enemy.localScale = new Vector3(-initScale.x * _direction, initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
    private void DirectionChange()
    {
        anim.SetBool("run", false);
        idleTimer += Time.deltaTime;//wait to change direction
        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }
    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }
}
