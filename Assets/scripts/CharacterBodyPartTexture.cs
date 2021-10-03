using System.Collections.Generic;
using UnityEngine;

public class CharacterBodyPartTexture : MonoBehaviour
{
    private Texture _texture;
    
    // Start is called before the first frame update

    void Start()
    {
        _texture = GetComponentInParent<Character>().texture;

        var renderer = GetComponent<Renderer>();
        var mesh = GetComponent<MeshFilter>().mesh;

        renderer.material.mainTexture = _texture;

        var uvMap = new Vector2[]
        {
            new Vector2(.375f, .75f),
            new Vector2(.5f, .75f),
            new Vector2(.375f, .875f),
            new Vector2(.5f, .875f),
            
            // new Vector2(.25f, .75f),
            // new Vector2(.125f, .75f),
            // new Vector2(.25f, .875f),
            // new Vector2(.125f, .875f),
            //
            new Vector2(.375f, .75f),
            new Vector2(.5f, .75f),
            new Vector2(.375f, .875f),
            new Vector2(.5f, .875f),

            
            new Vector2(),
            new Vector2(),
            new Vector2(),
            new Vector2(),
            
            new Vector2(),
            new Vector2(),
            new Vector2(),
            new Vector2(),
            
            new Vector2(),
            new Vector2(),
            new Vector2(),
            new Vector2(),
            
            new Vector2(),
            new Vector2(),
            new Vector2(),
            new Vector2(),
        };

        mesh.uv = uvMap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
