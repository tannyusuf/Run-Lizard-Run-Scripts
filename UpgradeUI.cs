
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public GameObject upgradePanel;
    public Button card1Button;
    public Button card2Button;
    public GameObject healthBar;
    public GameObject statsBar;


    public int upgradeCost = 20;

    private IguanaCharacter player;

    private void Start()
    {
        upgradePanel.SetActive(false);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<IguanaCharacter>();

        if (healthBar == null)
            healthBar = GameObject.Find("HealthBar"); // objenin adýyla eþleþmeli

        if (statsBar == null)
            statsBar = GameObject.Find("StatsDisplay"); // yine doðru adla

        card1Button.onClick.AddListener(() => ApplyUpgrade(1));
        card2Button.onClick.AddListener(() => ApplyUpgrade(2));
    }

    public void ShowUpgradeOptions()
    {
        upgradePanel.SetActive(true);
        healthBar.SetActive(false);
        statsBar.SetActive(false);
        Debug.Log("Upgrade Panel Açýlýyor!");
    }


    private void ApplyUpgrade(int index)
    {
        if (player.cointCount < upgradeCost)
        {
            Debug.Log("Yeterli coin yok!");
            return;
        }

        player.cointCount -= upgradeCost;

        if (index == 1)
        {
            float increase = player.damageAmount * 0.2f;
            player.damageAmount += increase;
            Debug.Log("Damage upgraded to: " + player.damageAmount);
        }
        else if (index == 2)
        {
            player.healthSystem.IncreaseMaxHealth(0.2f);
            Debug.Log("Health upgraded to: " + player.healthSystem.GetMaxHealth());
        }

        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void HideUpgradeOptions()
    {
        upgradePanel.SetActive(false);
        healthBar.SetActive(true);
        statsBar.SetActive(true);
        Time.timeScale = 1f;
    }

}
