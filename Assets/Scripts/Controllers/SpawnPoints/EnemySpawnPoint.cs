using System.Collections;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Animator spawnEffectAnimator;

    private Enemy currentEnemy;
    private bool isEnemyAboutToBeSpawned;

    private void Start()
    {
        isEnemyAboutToBeSpawned = false;
    }
    private void Update()
    {
        if (!currentEnemy && !isEnemyAboutToBeSpawned && GameManager.Instance.CanSpawnOneMoreEnemy())
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        GameManager.Instance.SpawnEnemy();
        isEnemyAboutToBeSpawned = true;
        yield return new WaitForSeconds(.5f);

        spawnEffectAnimator.SetTrigger("ActivateEffect");
        yield return new WaitForSeconds(1.5f);

        currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        isEnemyAboutToBeSpawned = false;
    }
}