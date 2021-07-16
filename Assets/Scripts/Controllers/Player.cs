using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    private Rigidbody2D rb;
    private Animator animator;
    private float movementSpeed;

    private float shootCooldown;
    private float currentShootCooldown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movementSpeed = 3f;
        shootCooldown = .4f;
        currentShootCooldown = 0;
        StartCoroutine(DisableEffect(2f));
    }
    private void Update()
    { 
        if(currentShootCooldown <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }
        }
        else
        {
            currentShootCooldown -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Vector3 playerVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.rotation = 0f;
            playerVelocity.y += movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.rotation = 90f;
            playerVelocity.x -= movementSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.rotation = 180f;
            playerVelocity.y -= movementSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.rotation = -90;
            playerVelocity.x += movementSpeed;
        }

        animator.SetBool("Moving", false);
        if(playerVelocity != Vector3.zero)
        {
            animator.SetBool("Moving", true);
        }
        rb.velocity = playerVelocity;
    }

    private void Shoot()
    {
        Bullet bullet = BlockFabrick.Instance.GetBlock(BlockType.Bullet, transform.position).GetComponent<Bullet>();
        bullet.tag = "PlayerBullet";
        bullet.SetTransformAndMovementVelocity(rb.position, rb.rotation);
        currentShootCooldown = shootCooldown;
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator DisableEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.SetActive(false);
    }
}
