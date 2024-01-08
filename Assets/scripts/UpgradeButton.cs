using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeButton : MonoBehaviour
{
    public GameObject costText;
    public int[] costs;
    public int[] statIncreases;
    private int currentLevel = 0;
    
    public void OnPress()
    {
        if (CanBuy())
        {
            Upgrade(statIncreases[currentLevel]);
            Robot.instance.eggs -= costs[currentLevel];
            currentLevel++;
            AudioManager.instance.Play("equip_upgrade");
        }
    }

    public abstract void Upgrade(int amount);

    protected bool CanBuy()
    {
        return currentLevel < costs.Length && Robot.instance.eggs >= costs[currentLevel];
    }

    void Update()
    {
        GetComponent<CanvasGroup>().alpha = CanBuy() ? 1 : 0.5f;
        GetComponent<CanvasGroup>().interactable = CanBuy();
        if (currentLevel < costs.Length) costText.GetComponent<TextMeshProUGUI>().text = costs[currentLevel].ToString();
    }
}
