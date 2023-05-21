using System.Collections;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel { get; private set; } = 1;

    public float levelIncreaseTime = 50f;
    public float timerBetweenLevels = 10f;
    public TMP_Text versionText;

    private ScrollViewSpawner codeLineSpawner;
    public int maxComplexityIncreasePerLevel = 1;

    private void Start()
    {
        codeLineSpawner = FindObjectOfType<ScrollViewSpawner>();
        codeLineSpawner.StartSpawning();
        StartCoroutine(LevelUpRoutine());
        UpdateUI();
    }

    private IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(levelIncreaseTime);
        codeLineSpawner.StopSpawning();
        codeLineSpawner.ClearCodeLines();
        SoundManager.Instance.PlayLevelUpSound();
        versionText.text = "Sending Version...";
        yield return new WaitForSeconds(timerBetweenLevels);   
        CurrentLevel++;
        codeLineSpawner.maxComplexity += maxComplexityIncreasePerLevel;
        codeLineSpawner.StartSpawning();
        UpdateUI();
        StartCoroutine(LevelUpRoutine());
    }

    private void UpdateUI()
    {
        versionText.text = "Version v" + CurrentLevel;
    }
}
