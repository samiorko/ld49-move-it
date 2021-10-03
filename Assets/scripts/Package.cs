using System;
using UnityEngine;

public class Package : MonoBehaviour
{

    public PackageDefinition definition;
    public Material sourceMaterial;

    public Color lightColor;
    public Color heavyColor;
    public float lightWeight;
    public float heavyWeight;

    public float weight;
    
    // Start is called before the first frame update
    void Start()
    {
        var child = transform.GetChild(0);

        weight = definition.weight != default
            ? definition.weight
            : definition.size.x * definition.size.y * definition.size.z * 100;

        child.transform.localScale = definition.size;
        
        GetComponent<Rigidbody>().mass = weight;
        child.transform.localScale = definition.size;
        
        var material = new Material(sourceMaterial);

        var t = Mathf.InverseLerp(lightWeight, heavyWeight, weight);
        material.color = Color.Lerp(lightColor, heavyColor, t);
        material.SetColor("_EmissionColor", material.color * .2f);
        
        child.GetComponent<Renderer>().material = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

[System.Serializable]
public class PackageDefinition
{
    public Vector3 size = Vector3.one;
    public Vector3 position = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public float weight = 0;
}
