using UnityEngine;

public class DrehschalterTutorial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Drehschalter drehschalter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(drehschalter.GetComponent<Drehschalter>().rotation == 90f)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    
}
