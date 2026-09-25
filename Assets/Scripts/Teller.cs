using UnityEngine;

public class Teller : MonoBehaviour
{   
    public int inhalt = 0;
    public GameObject inhaltTexture;
    public Material mehlMaterial;
    public Material eiMaterial;
     public Material mehlEiMaterial;
    public Material panierMaterial;

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
        if(other.gameObject.layer == 9)
        {
            Debug.Log(other.gameObject.name);
            Debug.Log(other.gameObject.GetComponent<Ingredient>()._name);
            Renderer r = inhaltTexture.GetComponent<Renderer>();
            if(other.gameObject.GetComponent<Ingredient>()._name == "Flour")
            {
                inhalt = 1;
                r.material = mehlMaterial;
            }
            else if(other.gameObject.GetComponent<Ingredient>()._name == "Egg")
            {
                Debug.Log("EggEntered");
                inhalt = 2;
                r.material = eiMaterial;
                Destroy(other.gameObject);
            }
            else if(other.gameObject.GetComponent<Ingredient>()._name == "Breadcrumbs")
            {
                inhalt = 3;
                r.material = panierMaterial;
            }
        }
        else if(other.gameObject.GetComponent<Panieren>() != null)
        {
            Renderer r = other.gameObject.GetComponent<Renderer>();
            if(other.gameObject.GetComponent<Panieren>().state == 0 && inhalt == 1)
            {   
                r.material = mehlMaterial;
                other.gameObject.GetComponent<Panieren>().state = 1;
            }
            else if(other.gameObject.GetComponent<Panieren>().state == 1 && inhalt == 2)
            {
                r.material = eiMaterial;
                other.gameObject.GetComponent<Panieren>().state = 2;
            }
            else if(other.gameObject.GetComponent<Panieren>().state == 2 && inhalt == 3)
            {
                r.material = panierMaterial;
                other.gameObject.GetComponent<Panieren>().state = 3;
            }
        }
    }
}
