using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject target;
    void Update()
    {
        IHealth entity = target ? target.GetComponent<IHealth>() : GetComponentInParent<IHealth>();
        float healthFraction = (float)entity.GetHealth() / entity.GetMaxHealth();
        transform.localScale = new Vector3(healthFraction, 1, 1);
    }
}
