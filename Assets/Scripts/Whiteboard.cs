using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

    void ApplyTextures()
    {

    }
    void Start()
    {
        var r = GetComponent<Renderer>();

        r.material.mainTexture = CreateTexture();
    }
    public void ResetTexture()
    {
        var r = GetComponent<Renderer>();
        //texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = CreateTexture();
    }

    private Texture CreateTexture()
    {
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);

        Color[] colors = new Color[(int)textureSize.x * (int)textureSize.y];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.yellow;
        }

        texture.SetPixels(colors);
        texture.Apply();

        return texture;
    }
}

