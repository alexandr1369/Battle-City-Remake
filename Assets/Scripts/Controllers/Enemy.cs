using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float movementSpeed;

    private float[] allAvailableSides;
    private bool canTryToMove;

    private float shootCooldown;
    private float currentShootCooldown;

    private Coroutine turningCoroutine;
    private Coroutine shootingCoroutine;

    #region Stuck Check System

    private float stuckTime;
    private float currentStuckTime;
    private Vector3 stuckPosition;
    private Vector3 deltaPosition;

    #endregion

    private void Start()
    {
        #region Stuck Check System

        stuckTime = .25f;
        currentStuckTime = stuckTime;
        stuckPosition = transform.position;
        deltaPosition = new Vector2(.25f, .25f);

        #endregion

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movementSpeed = 2f;
        shootCooldown = .6f;
        currentShootCooldown = 0;
        allAvailableSides = new float[] { -90f, 0, 90f, 180f };
        turningCoroutine = StartCoroutine(TurnToRandomSide(.5f));
    }
    private void Update()
    {
        #region Stuck Check System

        if (stuckPosition.x - deltaPosition.x <= transform.position.x && transform.position.x <= stuckPosition.x + deltaPosition.x
            && stuckPosition.y - deltaPosition.y <= transform.position.y && transform.position.y <= stuckPosition.y + deltaPosition.y
            && currentStuckTime > 0)
        {
            //print("Is stucked");
            currentStuckTime -= Time.deltaTime;
        }
        else
        {
            stuckPosition = transform.position;
            currentStuckTime = stuckTime;
        }

        if (currentStuckTime <= 0)
        {
            if (rb.velocity.x != 0)
            {
                rb.velocity = new Vector2(0, new float[2] { -movementSpeed, movementSpeed }[Random.Range(0, 2)]);
            }
            else
            {
                rb.velocity = new Vector2(new float[2] { -movementSpeed, movementSpeed }[Random.Range(0, 2)], 0);
            }
        }

        #endregion

        #region Shooting System

        if (currentShootCooldown <= 0)
        {
            if (CanSeePlayer())
            {
                Shoot();
            }
        }
        else
        {
            currentShootCooldown -= Time.deltaTime;
        }

        if(shootingCoroutine == null)
        {
            float randomDelay = Random.Range(.25f, 2f);
            shootingCoroutine = StartCoroutine(Shoot(randomDelay));
        }

        #endregion
    }
    private void FixedUpdate()
    {
        if (canTryToMove)
        {
            Vector3 enemyVelocity = Vector3.zero;
            int sideIndex = Random.Range(0, allAvailableSides.Length - 1);
            float randomSideAngle = allAvailableSides[sideIndex];
            if (sideIndex == 0)
            {
                enemyVelocity.x += movementSpeed;
            }
            else if (sideIndex == 1)
            {
                enemyVelocity.y += movementSpeed;
            }
            else if (sideIndex == 2)
            {
                enemyVelocity.x -= movementSpeed;
            }
            else if (sideIndex == 3)
            {
                enemyVelocity.y -= movementSpeed;
            }
            rb.velocity = enemyVelocity;

            animator.SetBool("Moving", false);
            if (enemyVelocity != Vector3.zero)
            {
                animator.SetBool("Moving", true);
            }
        }

        if (rb.velocity.x < 0)
        {
            rb.rotation = allAvailableSides[2];
        }
        else if (rb.velocity.y > 0)
        {
            rb.rotation = allAvailableSides[1];
        }
        else if (rb.velocity.x > 0)
        {
            rb.rotation = allAvailableSides[0];
        }
        else if (rb.velocity.y < 0)
        {
            rb.rotation = allAvailableSides[3];
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (turningCoroutine == null)
        {
            canTryToMove = false;
            turningCoroutine = StartCoroutine(TurnToRandomSide(3f));
        }
    }

    private IEnumerator Shoot(float delay)
    {
        yield return new WaitForSeconds(delay);

        Bullet bullet = BlockFabrick.Instance.GetBlock(BlockType.Bullet, transform.position).GetComponent<Bullet>();
        bullet.tag = "EnemyBullet";
        bullet.SetTransformAndMovementVelocity(rb.position, rb.rotation);
        currentShootCooldown = shootCooldown;
        shootingCoroutine = null;
    }
    private void Shoot()
    {
        Bullet bullet = BlockFabrick.Instance.GetBlock(BlockType.Bullet, transform.position).GetComponent<Bullet>();
        bullet.tag = "EnemyBullet";
        bullet.SetTransformAndMovementVelocity(rb.position, rb.rotation);
        currentShootCooldown = shootCooldown;
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    private bool CanSeePlayer()
    {
        float distance = 100f;
        int layerMask = ~LayerMask.GetMask("Enemy") & ~LayerMask.GetMask("Bullet");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, layerMask);
        if (hit.collider && hit.collider.tag == "Player")
        {
            return true;
        }

        return false;
    }
    private IEnumerator TurnToRandomSide(float delay)
    {
        yield return new WaitForSeconds(delay);
        canTryToMove = true;
        turningCoroutine = null;
    }
}