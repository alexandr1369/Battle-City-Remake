using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    private float movementSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isAboutToBeDestroyed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        movementSpeed = 5f;

        isAboutToBeDestroyed = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAboutToBeDestroyed)
        {
            switch (collision.tag)
            {
                case "Enemy":
                {
                    if(tag != "EnemyBullet")
                    {
                        sr.enabled = false;
                        rb.velocity = Vector2.zero;
                        isAboutToBeDestroyed = true;
                        StartCoroutine(BigBoomEffect());
                        GameManager.Instance.CountEnemyDeath();
                        collision.GetComponent<Enemy>().Die();
                    }
                } break;
                case "Player":
                {
                    if(tag != "PlayerBullet")
                    {
                        sr.enabled = false;
                        rb.velocity = Vector2.zero;
                        isAboutToBeDestroyed = true;
                        StartCoroutine(BigBoomEffect());
                        collision.GetComponent<Player>().Die();
                    }
                } break;
                case "Tile":
                {
                    isAboutToBeDestroyed = true;
                    BoomEffect();
                    Destroy(gameObject);
                } break;
                case "Brick":
                case "PlayerBullet":
                case "EnemyBullet":
                {
                    isAboutToBeDestroyed = true;
                    BoomEffect();
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                } break;
            }
        }
    }

    public void SetTransformAndMovementVelocity(Vector3 position, float rotationValue)
    {
        rb.position = position;
        rb.rotation = rotationValue;
        if(rotationValue == 0)
        {
            rb.velocity = new Vector2(0, movementSpeed);
        }
        else if (rotationValue == 180f)
        {
            rb.velocity = new Vector2(0, -movementSpeed);
        }
        else if(rotationValue == -90f)
        {
            rb.velocity = new Vector2(movementSpeed, 0);
        }
        else if(rotationValue == 90f)
        {
            rb.velocity = new Vector2(-movementSpeed, 0);
        }
    }

    private void BoomEffect()
    {
        BlockFabrick.Instance.GetBlock(BlockType.Boom, transform.position);
    }
    private IEnumerator BigBoomEffect()
    {
        float spawnPositionDelta = .2f;
        int randomAmountOfBooms = Random.Range(3, 6);
        for(int i = 0; i < randomAmountOfBooms; i++)
        {
            yield return new WaitForSeconds(.1f);
            Vector2 deltaRange = new Vector2(Random.Range(-spawnPositionDelta, spawnPositionDelta), Random.Range(-spawnPositionDelta, spawnPositionDelta));
            BlockFabrick.Instance.GetBlock(BlockType.Boom, transform.position + (Vector3)deltaRange);
        }

        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}