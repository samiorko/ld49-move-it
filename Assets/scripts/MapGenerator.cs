using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Texture2D map;
    public GameObject invalidPrefab;
    public int prefabSize;

    public Color startColor;
    public Color baseColor;

    public List<MapSquare> definitions;
        
    void Start()
    {
        var startOffset = Vector3.zero;

        var container = new GameObject("Map");
        var sizeVectorX = new Vector3(prefabSize, 0, 0);
        var sizeVectorY = new Vector3(0, 0, prefabSize);
        
        for (var mapX = 0; mapX < map.width; mapX++)
        {
            for (var mapY = 0; mapY < map.height; mapY++)
            {
                var color = map.GetPixel(mapX, mapY);

                if (color == baseColor)
                {
                    continue;
                }

                var vector = (sizeVectorX * mapX) + (sizeVectorY * mapY);
                
                if (color == startColor)
                {
                    startOffset = vector;
                }

                var prefab = GetPrefabForColor(color);
                var obj = Instantiate(prefab, container.transform);

                obj.transform.localPosition = vector;
            }
        }

        foreach (Transform child in container.transform)
        {
            child.localPosition -= startOffset;
        }
    }

    private GameObject GetPrefabForColor(Color color)
    {
        var def = definitions.FirstOrDefault(x => CompareColor(x.color, color));

        if (def != null)
        {
            return def.prefab;
        }
        
        Debug.LogWarning( $"Color not found: {color.ToString()}");
        return invalidPrefab;
    }

    private static bool CompareColor(Color a, Color b)
    {
        return CompareColorComponent(a.r, b.r)
               && CompareColorComponent(a.g, b.g)
               && CompareColorComponent(a.b, b.b);
    }

    private static bool CompareColorComponent(float a, float b)
    {
        return Mathf.Abs(a - b) < (1f/255f);
    }
}

[Serializable]
public class MapSquare
{
    public Color color;
    public GameObject prefab;
}
