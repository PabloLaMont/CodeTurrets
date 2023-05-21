using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GpuTurretSpawner : MonoBehaviour
{
    public GameObject gpuTurretPrefab;
    public int maxNumberOfTurrets = 3;

    public List<GameObject> gpuTurrets = new List<GameObject>();

    public void AddTurret()
    {
        if (gpuTurrets.Count < maxNumberOfTurrets)
        {
            GameObject newTurret = Instantiate(gpuTurretPrefab, transform);
            gpuTurrets.Add(newTurret);
        }
    }
}