using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private LevelManager levelManager;
    public int ProjectHealth { get; private set; } = 100;
    public int MoneyAvailable { get; private set; } = 0;

    public TMP_Text healthText;
    public TMP_Text moneyText;

    private ScrollViewSpawner codeLineSpawner;

    private void Start()
    {
        codeLineSpawner = FindObjectOfType<ScrollViewSpawner>();
        levelManager = FindObjectOfType<LevelManager>();
        UpdateUI();
    }

    public void DecreaseHealth(int damage)
    {
        ProjectHealth = Mathf.Max(ProjectHealth - damage, 0);
        UpdateUI();
    }

    public void IncreaseMoney(int amount)
    {
        amount += levelManager.CurrentLevel / 10;

        MoneyAvailable += amount;
        UpdateUI();
    }

    public void CureProject(int healthPoints)
    {
        ProjectHealth = Mathf.Min(ProjectHealth + healthPoints, 100); 
        UpdateUI();
    }

    public void DecreaseMoney(int amount)
    {
        MoneyAvailable = Mathf.Max(MoneyAvailable - amount, 0);
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthText.text = $"Health: {ProjectHealth} ({GetHealthState()})";
        moneyText.text = $"Money: {MoneyAvailable}";
    }

    public bool IsGameOver()
    {
        codeLineSpawner.StopSpawning();
        codeLineSpawner.ClearCodeLines();
        GameOver.instance.Restart();
        return ProjectHealth <= 0;       
    }

    public string GetHealthState()
    {
        if (ProjectHealth >= 80)
        {
            return "Fine";
        }
        else if (ProjectHealth >= 51)
        {
            return "Crunch Time";
        }
        else if (ProjectHealth >= 35)
        {
            return "Not Gonna Make It";
        }
        else
        {
            return "Find Another Job";
        }
    }
}
