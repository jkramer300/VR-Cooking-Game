using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Gemüse"))
        {
            StepUp();
        }
    
        else if (other.gameObject.CompareTag("Oil"))
        {
            StepUp();
        }
        else if (other.gameObject.CompareTag("GemüseBrühe"))
        {
            StepUp();
        }
        else if (other.gameObject.CompareTag("Egg"))
        {
            StepUp();
        }
        else if (other.gameObject.CompareTag("Tomatenmark"))
        {
            StepUp();
        }
        else if (other.gameObject.CompareTag("Soy"))
        {
            StepUp();
        }
        else if (other.gameObject.CompareTag("Sour"))
        {
            StepUp();
        }
    }
    public void StepUp()
    {
        GetComponent<AudioSource>().Play();
    }
}
