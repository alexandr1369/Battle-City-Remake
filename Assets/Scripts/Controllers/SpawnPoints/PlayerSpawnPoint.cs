using System.Collections;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField] private Animator spawnEffectAnimator;

    private GameObject playerPrefab;
    private Player currentPlayer;
    private bool isPlayerAboutToBeSpawned;

    private void Start()
    {
        playerPrefab = PlayerInventory.Instance.GetCurrentSkinPlayer();
        isPlayerAboutToBeSpawned = false;
    }
    private void Update()
    {
        if (!currentPlayer && !isPlayerAboutToBeSpawned)
        {
            StartCoroutine(SpawnPlayer());
        }
    }

    private IEnumerator SpawnPlayer()
    {
        isPlayerAboutToBeSpawned = true;
        yield return new WaitForSeconds(.5f);

        spawnEffectAnimator.SetTrigger("ActivateEffect");
        yield return new WaitForSeconds(1.5f);

        currentPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity).GetComponent<Player>();
        isPlayerAboutToBeSpawned = false;
    }
}
