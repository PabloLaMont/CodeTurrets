using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CodeLine : MonoBehaviour, ICodeLine
{
    public string Text { get; set; }
    public string Type { get; set; }
    public float TimeToMove { get; set; }
    public float Complexity { get; set; }
    public string[] Answers { get; set; }

    public TMP_Text mainText;
    public Image mainImage;

    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public IEnumerator Move()
   {   
        yield return new WaitForSeconds(TimeToMove);
        Vector3 endPos = new Vector3(transform.localPosition.x, transform.localPosition.y + 40, transform.localPosition.z);
        transform.localPosition = endPos;
        if(this.gameObject.activeInHierarchy)StartCoroutine("Move");
   }

    public void Die()
    {
        StopAllCoroutines();
        ObjectPool.instance.ReturnItem(this.Type ,this.gameObject);
    }

    public void SetData(CodeLineData data)
    {
        Text = data.text;
        TimeToMove = data.timeToMove;
        Type = data.type;
        Complexity = data.complexity;
        Answers = data.answers;

        TimeToMove = Mathf.Max(0, TimeToMove - (0.2f * levelManager.CurrentLevel));
        Complexity = Mathf.Max(0, Complexity + (0.2f * levelManager.CurrentLevel));

        mainText.text = "["+ System.DateTime.Now.ToString("HH:mm:ss") +"] "+ Text;
    }

}

[System.Serializable]
public class CodeLineData
{
    public string text;
    public string type;
    public float timeToMove;
    public float complexity;
    public string[] answers;
}

[System.Serializable]
public class CodeLineDataCollection
{
    public CodeLineData[] items;

    public void LoadDataFromJson(string path)
    {
        TextAsset file = Resources.Load<TextAsset>(path);
        string json = file.text;
        CodeLineDataCollection codeLines = JsonUtility.FromJson<CodeLineDataCollection>(json);  
        items = codeLines.items;
    }
}