using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Documentation : MonoBehaviour
{
    [System.Serializable]
    public class Document
    {
        public string mainText;
        public string[] answers;
    }

    [System.Serializable]
    public class DocumentList
    {
        public List<Document> documents;
    }

    public TMP_InputField searchField;
    public TextMeshProUGUI docDisplay;

    private void Start()
    {
        searchField.onEndEdit.AddListener(OnSearchFieldChanged);

        LoadDocumentationData();
    }

    public DocumentList docData; 

    private void LoadDocumentationData()
    {
        string path = Path.Combine(Application.dataPath, "Resources/DataDocumentation.json");
        string jsonString = File.ReadAllText(path);
        docData = JsonUtility.FromJson<DocumentList>(jsonString); 

    }

    private void DisplayDocument(Document doc)
    {
        string underlinedAnswers = string.Join("\n", doc.answers.Select(a => $"<u>{a}</u>"));
        docDisplay.text = $"{doc.mainText}\n{underlinedAnswers}";
    }

    private void OnSearchFieldChanged(string value)
    {
        Search(value);
    }

    private void Search(string value)
    {
        foreach (var doc in docData.documents)
        {
            if (doc.mainText.ToLower().Contains(value.ToLower()) || doc.answers.Any(a => a.ToLower().Contains(value.ToLower())))
            {
                DisplayDocument(doc);
                return;
            }
        }

        docDisplay.text = "No results found.";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Search(searchField.text);
        }
    }
}