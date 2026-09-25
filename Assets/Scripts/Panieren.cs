using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Panieren : MonoBehaviour
{
    public int state = 0;
    public int hammer = 5;
    public bool schnitzel = true;
    public Material newMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 3)
        {
            gameObject.AddComponent<BratElement>();
            gameObject.GetComponent<BratElement>().Test = newMaterial;
            state++;
        }
        if(gameObject.transform.parent == null && !schnitzel && state == -1)
        {
            state = 0;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Hammer>()!= null && state == -1 && schnitzel)
        {
            hammer--;
            if(hammer <= 0)
            {
                state = 0;
            }
        }
    }
}
