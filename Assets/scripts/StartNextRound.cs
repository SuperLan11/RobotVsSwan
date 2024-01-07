using UnityEngine;

public class StartNextRound : MonoBehaviour
{
    public void OnPress()
    {
        RoundManager.instance.StartNextRound();
    }
}
