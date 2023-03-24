using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static Enemy enemy;
    public static Enemy EnemyScript
    {
        get
        {
            if (enemy == null)
            {
                enemy = GameObject.FindObjectOfType<Enemy>();
            }
            return enemy;
        }
    }

    private Animator EnemyAnimator;

    [SerializeField]
    private float EnemyHeal, EnemySpeed, Distance, AttackDistance, maxCooldown, cooldown;

    [SerializeField]
    private Vector3 EnemyDirection;

    [SerializeField]
    private bool inRange, cooling;

    public bool damaged, die, run, attack;

    [SerializeField]
    private Transform leftLimit, rightLimit, TargetEnemy;

    [SerializeField]
    private Transform raycastTransform;
    [SerializeField]
    private float raycastLenght;
    [SerializeField]
    private LayerMask raycastMask;
    [SerializeField]
    private RaycastHit2D Hit;

    [SerializeField]
    private GameObject FireBall;
    [SerializeField]
    private Transform FireTransform;


    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();

        EnemyHeal = 100;
        EnemySpeed = 5;

        maxCooldown = Random.Range(1, 5);

        die = false;
        damaged = false;

        SelectTarget();
    }

    private void Update()
    {
        RaycastHit ();

        EnemyLogic ();

        RaycastDebugger();
    }

    private void FixedUpdate()
    {
        if (!attack && !die && !damaged)
        {
            EnemyAnimator.SetBool("Run", true);

            Move();
        }
    }

    private void Move()
    {
        Vector2 targetPosition = new Vector2(TargetEnemy.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, EnemySpeed * Time.fixedDeltaTime);
    }

    private void RaycastHit()
    {
        Hit = Physics2D.Raycast(raycastTransform.position, transform.right, raycastLenght, raycastMask);

        if (Hit.collider != null)
        {
            inRange = true;

            EnemySpeed = 10;

            TargetEnemy = Hit.collider.transform;

            Flip();
        }
        else
        {
            inRange = false;

            EnemySpeed = 5;
        }
    }

    private void EnemyLogic()
    {
        Distance = Vector2.Distance(transform.position, TargetEnemy.position);

        if (!InsideOfLimits())
        {
            if (!inRange)
            {
                SelectTarget();
            }
        }

        Cooldown();

        if (inRange)
        {
            Attack();
        }
        else
        {
            StopAttack();
        }
    }

    private void RaycastDebugger()
    {
        if (!inRange)
        {
            Debug.DrawRay(raycastTransform.position, transform.right * raycastLenght, Color.green);
        }
        else
        {
            Debug.DrawRay(raycastTransform.position, transform.right * raycastLenght, Color.red);
        }
    }

    private void Attack()
    {
        if (Distance < AttackDistance)
        {
            attack = true;

            EnemyAnimator.SetBool("Attack", true);
        }
    }

    private void StopAttack()
    {
        attack = false;

        EnemyAnimator.SetBool("Attack", false);
    }

    private void Cooldown()
    {
        if (cooldown < maxCooldown)
        {
            cooldown += Time.deltaTime;
        }
        else
        {
            cooling = true;

            cooldown = 0;
        }
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            TargetEnemy = leftLimit;
        }
        else
        {
            TargetEnemy = rightLimit;
        }

        Flip ();
    }

    private void Flip()
    {
        EnemyDirection = transform.eulerAngles;

        if (transform.position.x > TargetEnemy.position.x)
        {
            EnemyDirection.y = 180;
        }
        else
        {
            EnemyDirection.y = 0;
        }
        transform.eulerAngles = EnemyDirection;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mermi"))
        {
            EnemyHeal -= Random.Range(10, 50);

            if (EnemyHeal <= 0)
            {
                die = true;

                EnemyAnimator.SetTrigger("Death");
            }
            else
            {
                EnemyAnimator.SetTrigger("Damaged");
            }
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    // Ateþ Etmesi Saðlanacak
    public IEnumerator FireBallThrow()
    {
        if (cooling)
        {
            //yield return new WaitForSeconds(Random.Range(1, 3));

            yield return null;

            Instantiate(FireBall, FireTransform.position, Quaternion.Euler(new Vector3(0, 0, -90)));

            cooling = false;

            StartCoroutine(FireBallThrow());
        }
    }
}
