using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuTurretSpawner : MonoBehaviour
{
    public GameObject cpuTurretPrefab;
    public int maxNumberOfTurrets = 3;

    public List<GameObject> cpuTurrets = new List<GameObject>();

    private void Start()
    {
        AddTurret();
    }

    public void AddTurret()
    {
        if (cpuTurrets.Count < maxNumberOfTurrets)
        {
            GameObject newTurret = Instantiate(cpuTurretPrefab, transform);
            cpuTurrets.Add(newTurret);
        }
    }
}