using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerTurret : CPUTurret
{
    private const float reduction = 0.2f;

    public override IEnumerator Compile(CodeLine codeLine, GameObject item)
    {
        float originalTime = spawner.timeBetweenSpawns;

        spawner.timeBetweenSpawns += reduction;

        yield return base.Compile(codeLine, item);

        spawner.timeBetweenSpawns = originalTime;
    }
}
