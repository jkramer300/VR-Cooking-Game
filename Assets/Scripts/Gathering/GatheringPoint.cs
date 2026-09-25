using UnityEngine;

public class GatheringPoint : MonoBehaviour
{
    public GameObject test;
    public int set = 0;
    public string type;
    public GameObject wrongBoard;
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
        if(type == "brett" && other.gameObject.GetComponent<Brett>() != null)
        {
            set = -1;

            if(other.gameObject == test)
            {
                set = 2;

                if(wrongBoard != null)
                {
                    wrongBoard.GetComponent<Problems>().RemoveProblem(wrongBoard);
                    wrongBoard = null;
                }
            }

            if(set == -1 && wrongBoard == null)
            {
                other.gameObject.GetComponent<Problems>().AddProblem(other.gameObject);

                wrongBoard = other.gameObject;

                GameObject.Find("GameScript").GetComponent<GameScript>().ChangeScore(-50);
            }
        }
        else if(type == "bowl" && other.gameObject.GetComponent<Bowl2>() != null)
        {
            set = 1;
            if(other.gameObject == test)
            {
                set = 2;
            }
        }
        else if(type == "plate" && other.gameObject.GetComponent<Teller>() != null)
        {
            set = 1;
            if(other.gameObject == test)
            {
                set = 2;
            }
        }
        else if(type == "greenBowl" && other.gameObject.GetComponent<GreenBowl>() != null )
        {   
            if(GameObject.Find("GameScript").GetComponent<GameScript>().currentStep.id == 4)
            {
                int i = 0;
                foreach(GameObject veg in other.gameObject.GetComponent<GreenBowl>().content)
                {
                    if (!veg.GetComponent<WashVegetables>().washed)
                    {
                        return;
                    }
                    i++;
                }
                WashVegetables[] washVeg = FindObjectsByType<WashVegetables>();
                foreach(WashVegetables veg in washVeg)
                {
                    if (!veg.GetComponent<WashVegetables>().washed)
                    {
                        return;
                    }
                    i--;
                }
                if(i != 0)
                {
                    return;
                } 
                GameObject.Find("GameScript").GetComponent<GameScript>().step++;
            }
            else
            {
                set = 1;
            }
        }
        else if(type == "brownBowl" && other.gameObject.GetComponent<BrownBowl>() != null)
        {
            set = 1;
        }
        
    }
    void OnTriggerExit(Collider other)
    {   
        if(other.gameObject == test)
        {
            set = 1;
        }
    }
}
