
using UnityEngine;

public class UpgradeZoneTrigger : MonoBehaviour
{
    public UpgradeUI upgradeUI;  // Inspector'dan atanacak

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upgradeUI.ShowUpgradeOptions(); // Kartlar� g�ster
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu upgrade alan�ndan ��kt�.");
            upgradeUI.HideUpgradeOptions(); // paneli kapat
        }
    }
}

