using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpuTurret : CPUTurret
{

    private const int MaxComplexity = 4;

    public override IEnumerator Compile(CodeLine codeLine, GameObject item)
    {
        if (codeLine.Complexity < 4)
        {
            yield break;
        }

        IsCompiling = true;
        progressBar.fillAmount = 0;

        float compileTime = codeLine.Complexity / 2f;
        SoundManager.Instance.PlayCompileGPUSound();
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
