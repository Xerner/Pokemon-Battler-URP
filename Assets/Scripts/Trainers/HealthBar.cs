using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar
{
    private float totalHealth;
    private float currentHealth;
    private RectTransform transform;

    public HealthBar(RectTransform transform)
    {
        this.transform = transform;
        totalHealth = transform.rect.width;
    }

    public void Ouch(float damage)
    {
        transform.sizeDelta = new Vector2(transform.localPosition.y, transform.localPosition.x-damage);
    }

    public void Heal(float heal)
    {
        transform.sizeDelta = new Vector2(transform.localPosition.y, transform.localPosition.x + heal);
    }
}
