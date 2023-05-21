using System.Collections.Generic;
using UnityEngine;




public class ObjectPool : MonoBehaviour
{

    public static ObjectPool instance;

    public GameObject codeLinePrefab;
    public GameObject errorPrefab;
    public GameObject warningPrefab;
    public int poolSize = 20;
    private Dictionary<string, Queue<GameObject>> itemPool;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }


        itemPool = new Dictionary<string, Queue<GameObject>>
        {
            { "CodeLine", new Queue<GameObject>() },
            { "Error", new Queue<GameObject>() },
            { "Warning", new Queue<GameObject>() }
        };

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject("CodeLine", codeLinePrefab);
            CreateNewObject("Error", errorPrefab);
            CreateNewObject("Warning", warningPrefab);
        }
    }

    private void CreateNewObject(string type, GameObject prefab)
    {
        GameObject item = Instantiate(prefab);
        item.SetActive(false);
        item.transform.SetParent(this.transform);
        itemPool[type].Enqueue(item);
    }

    public GameObject GetItem(string type)
    {
        if (itemPool[type].Count == 0)
        {
            CreateNewObject(type, GetPrefab(type));
        }

        return itemPool[type].Dequeue();
    }

    private GameObject GetPrefab(string type)
    {
        switch (type)
        {
            case "CodeLine":
                return codeLinePrefab;
            case "Error":
                return errorPrefab;
            case "Warning":
                return warningPrefab;
            default:
                return null;
        }
    }

    public void ReturnItem(string type, GameObject item)
    {
        item.SetActive(false);
        item.transform.SetParent(this.transform);
        itemPool[type].Enqueue(item);
    }
}

