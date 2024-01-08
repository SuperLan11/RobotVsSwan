using UnityEngine;
using UnityEngine.UI;

public class ChargeMeter : MonoBehaviour
{
    public GameObject[] chargeBars;

    public float progress;
    
    public static ChargeMeter instance;
    public int damageForFullBar;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        int bars = (int)(progress * 3);
        for (int i = 0; i < 3; i++)
        {
            chargeBars[i].SetActive(i >= bars);
        }

        Color color;
        if (bars == 0)
        {
            color = Color.red;
        }
        else
        {
            color = chargeBars[bars - 1].GetComponent<Image>().color;
        }
        GetComponent<Image>().color = color;

        transform.localScale = new Vector3(progress, 1, 1);
    }
    
    void AddProgress(float amount)
    {
        progress += amount;
        if (progress > 1)
        {
            progress = 1;
        }
    }

    public void RegisterDamage(int damage)
    {
        AddProgress((float)damage / damageForFullBar);
    }
}
