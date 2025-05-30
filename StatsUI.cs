using UnityEngine;
using TMPro;

/// <summary>
/// Controls the UI elements that show player statistics
/// </summary>
public class StatsUI : MonoBehaviour
{
    // UI text elements
    public TextMeshProUGUI coinText;    // Text displaying coin count
    public TextMeshProUGUI damageText;  // Text displaying damage amount
    
    // Player reference
    public IguanaCharacter iguana;      // Reference to player character

    /// <summary>
    /// Update the UI text elements with current player stats
    /// </summary>
    void Update()
    {
        // Update coin display
        coinText.text = iguana.cointCount.ToString();
        
        // Update damage display, rounded to nearest integer
        damageText.text = iguana.damageAmount.ToString("F0");
    }
}

