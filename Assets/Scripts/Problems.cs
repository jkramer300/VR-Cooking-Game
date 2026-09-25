using UnityEngine;

public class Problems : MonoBehaviour
{

    public Material problemMaterial;
    public Material oldMaterial;
    public string problemName;
    public GameObject problemUI;
    bool activeProblem = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        problemUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddProblem(GameObject g)
    {   
        activeProblem = true;
        Debug.Log("AddProblem");
        Renderer r = GetComponent<Renderer>();
        

        if("HandContaminated" == problemName)
        {
            problemUI.SetActive(true);
            r = g.GetComponent<Renderer>();
        }
        oldMaterial = r.material;
        r.material = problemMaterial;
        
    }
    public void RemoveProblem(GameObject g)
    {
        activeProblem = false;
        Debug.Log("RemoveProblem");
        Renderer r = g.GetComponent<Renderer>();

        if("HandContaminated" != problemName)
        {
            r.material = oldMaterial;
        }

        problemUI.SetActive(false);
    }

    public void SetActiveUI(bool active)
    {   
        if(!(!activeProblem && active))
        {
            problemUI.SetActive(active);
        }
    }
}
