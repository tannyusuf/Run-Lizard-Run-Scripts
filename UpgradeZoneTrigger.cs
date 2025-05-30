
using UnityEngine;

public class UpgradeZoneTrigger : MonoBehaviour
{
    public UpgradeUI upgradeUI;  // Inspector'dan atanacak

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeUI.ShowUpgradeOptions(); // Kartlarý göster
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu upgrade alanýndan çýktý.");
            upgradeUI.HideUpgradeOptions(); // paneli kapat
        }
    }
}

