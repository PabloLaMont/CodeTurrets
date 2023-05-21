using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerTurretSpawner : MonoBehaviour
{
    public GameObject coolerTurretPrefab;
    public int maxNumberOfTurrets = 3;

    public List<GameObject> coolerTurrets = new List<GameObject>();

    public void AddTurret()
    {
        if (coolerTurrets.Count < maxNumberOfTurrets)
        {
            GameObject newTurret = Instantiate(coolerTurretPrefab, transform);
            coolerTurrets.Add(newTurret);
        }
    }
}