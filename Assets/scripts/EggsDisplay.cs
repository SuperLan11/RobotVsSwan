using TMPro;
using UnityEngine;

public class EggsDisplay : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = Robot.instance.eggs.ToString();
    }
}
