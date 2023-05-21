using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public TMP_Text cpuTurretPriceText;
    public TMP_Text gpuTurretPriceText;
    public TMP_Text coolerTurretPriceText; 
    
    public Button buyCpuButton;
    public Button buyGpuButton;
    public Button buyCoolerButton;

    public PlayerStats playerStats;

    public CpuTurretSpawner cpuTurretSpawner;
    public GpuTurretSpawner gpuTurretSpawner;
    public CoolerTurretSpawner coolerTurretSpawner;

    private int cpuPrice = 10;
    private int gpuPrice = 15;
    private int coolerPrice = 20;

    private void Start()
    {
        UpdatePriceTexts();

        buyCpuButton.onClick.AddListener(() => BuyCpuTurret());
        buyGpuButton.onClick.AddListener(() => BuyGpuTurret());
        buyCoolerButton.onClick.AddListener(() => BuyCoolerTurret());
    }

    private void Update()
    {
        buyCpuButton.interactable = playerStats.MoneyAvailable >= cpuPrice && cpuTurretSpawner.cpuTurrets.Count < cpuTurretSpawner.maxNumberOfTurrets;
        buyGpuButton.interactable = playerStats.MoneyAvailable >= gpuPrice && gpuTurretSpawner.gpuTurrets.Count < gpuTurretSpawner.maxNumberOfTurrets;
        buyCoolerButton.interactable = playerStats.MoneyAvailable >= coolerPrice && coolerTurretSpawner.coolerTurrets.Count < coolerTurretSpawner.maxNumberOfTurrets;
    }

    private void BuyCpuTurret()
    {
        if (playerStats.MoneyAvailable >= cpuPrice && cpuTurretSpawner.cpuTurrets.Count < cpuTurretSpawner.maxNumberOfTurrets)
        {
            playerStats.DecreaseMoney(cpuPrice);
            cpuTurretSpawner.AddTurret();
        }
    }

    private void BuyGpuTurret()
    {
        if (playerStats.MoneyAvailable >= gpuPrice && gpuTurretSpawner.gpuTurrets.Count < gpuTurretSpawner.maxNumberOfTurrets)
        {
            playerStats.DecreaseMoney(gpuPrice);
            gpuTurretSpawner.AddTurret();
        }
    }

    private void BuyCoolerTurret()
    {
        if (playerStats.MoneyAvailable >= coolerPrice && coolerTurretSpawner.coolerTurrets.Count < coolerTurretSpawner.maxNumberOfTurrets)
        {
            playerStats.DecreaseMoney(coolerPrice);
            coolerTurretSpawner.AddTurret();
        }
    }

    private void UpdatePriceTexts()
    {
        cpuTurretPriceText.text = $" {cpuPrice}";
        gpuTurretPriceText.text = $" {gpuPrice}";
        coolerTurretPriceText.text = $" {coolerPrice}";
    }
}