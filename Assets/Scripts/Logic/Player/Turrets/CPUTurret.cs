using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUTurret : MonoBehaviour, ITurret
{
    public Image progressBar;
    public PlayerStats playerStats;
    public bool IsCompiling { get; protected set; } = false;

    public ScrollViewSpawner spawner;
    public ObjectPool codeLinePool;

    private void Start() 
    {
        playerStats = FindObjectOfType<PlayerStats>();
        spawner = FindObjectOfType<ScrollViewSpawner>();
        codeLinePool = FindObjectOfType<ObjectPool>();
    }

    public virtual IEnumerator Compile(CodeLine codeLine, GameObject item)
    {
        IsCompiling = true;
        progressBar.fillAmount = 0;

        float compileTime = codeLine.Complexity;
        SoundManager.Instance.PlayCompileSound();
        float timePassed = 0;
        while (timePassed < compileTime)
        {
            timePassed += Time.deltaTime;
            progressBar.fillAmount = timePassed / compileTime;

            yield return null;
        }

        progressBar.fillAmount = 1;
        IsCompiling = false;

        spawner.currentPool.Remove(item);
        codeLinePool.ReturnItem(codeLine.Type, item);

        item.SetActive(false);

        playerStats.IncreaseMoney((int)codeLine.Complexity);
        playerStats.CureProject((int)codeLine.Complexity);
    }
}


