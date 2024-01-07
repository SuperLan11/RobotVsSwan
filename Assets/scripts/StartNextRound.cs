using TMPro;
using UnityEngine;

public class StartNextRound : MonoBehaviour
{
    public void OnPress()
    {
        RoundManager.instance.StartNextRound();
    }

    void Update()
    {
        GetComponentInChildren<TextMeshProUGUI>().text =
            RoundManager.instance.lastRoundWasWin ? "Start next round" : "Retry round";
    }
}
