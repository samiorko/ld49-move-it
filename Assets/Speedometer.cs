using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public float maxSpeed;
    private Slider _slider;

    [Range(0f, 1f)]
    public float middle;
    public Image fill;
    public Color normalColor;
    public Color middleColor;
    public Color highColor;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        var speed = Mathf.Abs(Car.Instance.speed);
        var t = Mathf.Clamp01(speed / maxSpeed);
        
        var color = Color.magenta;
        

        if (t < middle)
        {
            color = Color.Lerp(normalColor, middleColor, t / middle);
        }
        else
        {
            color = Color.Lerp(middleColor, highColor, (t-middle)/(1-middle));
        }

        fill.color = color;
        _slider.value = t;
    }
}
