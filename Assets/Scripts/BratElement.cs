
using UnityEngine;


public class BratElement : MonoBehaviour
{
    public int  bratzustand = 0; // 0 bis 10000
    public Material Test;
    public bool gebraten = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bratzustand > 1000)
        {   
            Renderer renderer =  gameObject.GetComponent<Renderer>();

            Material[] materials = new Material[renderer.materials.Length];
            for(int i = 0; i < renderer.materials.Length; i++)
            {
                materials[i] = Test;
            }
            renderer.materials = materials;
            gebraten = true;
        }
    }
}
