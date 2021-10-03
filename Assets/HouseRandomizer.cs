using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRandomizer : MonoBehaviour
{
    public AnimationCurve heightCurve;
    public float minHeight;
    public float maxHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        var t = Random.Range(0f, 1f);

        var height = minHeight + heightCurve.Evaluate(t) * maxHeight;
        transform.localScale += Vector3.up * height;
        transform.localPosition = Vector3.up * height / 2;
    }

}
