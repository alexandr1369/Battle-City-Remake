using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    #endregion

    [Header("Enemies Counter")]
    [Range(1, 20)]
    [SerializeField] private int neededEnemiesAmountToDestroy;
    [SerializeField] private Transform enemiesHeadsParent;
    [SerializeField] private GameObject enemyHeadPrefab;

    [Header("Score")]
    [SerializeField] private Text scoreText;

    private List<GameObject> enemiesHeads;
    private int currentScore;

    private void Start()
    {
        currentScore = 0;
        AddScore(0);
        enemiesHeads = new List<GameObject>();
        for(int i = 0; i < neededEnemiesAmountToDestroy; i++)
        {
            GameObject enemyHead = Instantiate(enemyHeadPrefab, enemiesHeadsParent);
            enemiesHeads.Add(enemyHead);
        }
    }

    public void ReloadCurrentScene()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }
    public void CountEnemyDeath()
    {
        List<GameObject> host = enemiesHeads.FindAll(t => t.activeSelf == true);
        if (host.Count > 0)
        {
            host[0].SetActive(false);
            AddScore(100);
            if(host.Count == 1)
            {
                ReloadCurrentScene();
            }
        }
    }

    public bool CanSpawnOneMoreEnemy()
    {
        return neededEnemiesAmountToDestroy > 0;
    }
    public void SpawnEnemy()
    {
        --neededEnemiesAmountToDestroy;
    }

    private void AddScore(int scoreAmount)
    {
        currentScore += scoreAmount;
        scoreText.text = "Score: \n" + currentScore.ToString();
    }
}
