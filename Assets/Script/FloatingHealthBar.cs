using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider slider;
    public Transform target;
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = (currentHealth / maxHealth);

        // remove the offset I did when I styled the slider
        if (slider.value != 1)
            slider.value -= 0.2f;

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + new Vector3(-0.1f, 6.5f, 0);
    }
}
