using UnityEngine;

public class Lappen : MonoBehaviour
{
    bool wet;
    public Material _material;
    public Texture2D tex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCleaning();
    }

    void CheckCleaning()
    {
        Renderer myRenderer = GetComponent<Renderer>();
        tex = (Texture2D)myRenderer.material.GetTexture("_BaseMap");
        //Texture2D texture = GetComponent<Renderer>().material.texture;
        Color targetColor = Color.white;
        Color[] pixels = tex.GetPixels();

        int count = 0;

        foreach (Color c in pixels)
        {
            if (c == targetColor)
            {
                count++;
            }
        }

        if (count > (2048 * 1024) * 0.6)
        {
            Destroy(gameObject);
            GameObject GameScript = GameObject.Find("GameScript");
            GameScript.GetComponent<GameScript>().step +=1;
        }
    }
}
