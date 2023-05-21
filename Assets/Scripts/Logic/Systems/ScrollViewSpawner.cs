using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewSpawner : MonoBehaviour
{
  
    public ObjectPool multiTypePool;
    public PlayerStats playerStats;
    public LevelManager levelManager;

    public Transform spawnPoint;
    public int maxComplexity = 1;

    private int objectsSpawnedSoFar = 0;
    public float timeBetweenSpawns = 3;
    private bool stopSpawning = false;

    private CodeLineDataCollection codeLineData;
    public List<GameObject> currentPool;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        levelManager = FindObjectOfType<LevelManager>();

        currentPool = new List<GameObject>();

        codeLineData = new CodeLineDataCollection();
        codeLineData.LoadDataFromJson("DataLines");
    }

    public void StartSpawning()
    {
        stopSpawning = false;
        StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
        stopSpawning = true;
    }

    private IEnumerator SpawnRoutine()
    {
        while (!stopSpawning && !playerStats.IsGameOver())
        {
            yield return new WaitForSeconds(timeBetweenSpawns -= (levelManager.CurrentLevel/10));
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        CodeLineData data = GetRandomDataWithinMaxComplexity();
        SoundManager.Instance.PlaySpawnLineSound();

        if (data != null)
        {
            GameObject item = multiTypePool.GetItem(data.type);
            currentPool.Add(item);

            Vector3 offset = new Vector3(0, objectsSpawnedSoFar * -40, 0);
            item.transform.SetParent(spawnPoint, false);
            item.transform.localPosition = Vector3.zero + offset;
            item.transform.localScale = Vector3.one;
            item.SetActive(true);

            CodeLine codeLine = item.GetComponent<CodeLine>();
            if (codeLine != null)
            {
                codeLine.SetData(data);
                StartCoroutine(codeLine.Move());
            }

            objectsSpawnedSoFar++;
        }
    }

    private CodeLineData GetRandomDataWithinMaxComplexity()
    {
        List<CodeLineData> validDataItems = new List<CodeLineData>();
        foreach (var data in codeLineData.items)
        {
            if (data.complexity <= maxComplexity)
            {
                validDataItems.Add(data);
            }
        }

        if (validDataItems.Count > 0)
        {
            int randomIndex = Random.Range(0, validDataItems.Count);
            return validDataItems[randomIndex];
        }

        return null;
    }

    public void ClearCodeLines()
    {
        objectsSpawnedSoFar = 0;

        foreach (var item in currentPool)
        {
            var codeLine = item.GetComponent<CodeLine>();
            codeLine.Die();
        }
        currentPool.Clear();
    }

    private void Update()
    {
        for (int i = currentPool.Count - 1; i >= 0; i--)
        {
            var item = currentPool[i];
            if (item.activeInHierarchy && item.transform.localPosition.y >= 560)
            {
                var codeLine = item.GetComponent<CodeLine>();

                playerStats.DecreaseMoney((int)codeLine.Complexity);
                playerStats.DecreaseHealth((int)codeLine.Complexity);

                codeLine.Die();
                currentPool.RemoveAt(i);
            }
        }
    }

}
