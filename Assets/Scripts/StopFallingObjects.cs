using UnityEngine;

public class StopFallingObjects : MonoBehaviour
{
    public Transform Spawnpoint;
    public bool Boden = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(Boden)
        {
            if(other.gameObject.GetComponent<Rigidbody>()!= null && other.gameObject.transform.parent == null)
            {
                other.gameObject.transform.position = Spawnpoint.position;
            }
        }
        else
        {
            other.gameObject.transform.position = Spawnpoint.position;
        }
    }
}
