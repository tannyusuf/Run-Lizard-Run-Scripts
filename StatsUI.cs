using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI damageText;
    public IguanaCharacter iguana;

    void Update()
    {
        coinText.text = iguana.cointCount.ToString();
        damageText.text = iguana.damageAmount.ToString("F0");
    }
}

