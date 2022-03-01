using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexScrollShader : MonoBehaviour
{
    public float Scrollx;
    public float Scrolly;
    void Update()
    {
        float offsetx = Time.time * Scrollx;
        float offsety = Time.time * Scrolly;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetx, offsety);    
    }
}
