using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.SceneManagement;

public class Hirsebällchen : MonoBehaviour
{
    public bool ExamMode = true;
    public TextMeshProUGUI stepUi;
    public int step = 0;
    public GameObject schüssel;
    public GameObject pfanne;
    private Dictionary<string, float> cutVegetables = new Dictionary<string, float>();
    private int numberOfMillets = 6;
    //private int numberOfPeas = 1;
    private float kochzeit = 180f;
    public GameObject pfannenInhalt;
    private int numberOfMilletBalls = 5;
    public GameObject newUI;
    public GameObject oldUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cutVegetables.Add("Carrot", 8-1);
        cutVegetables.Add("Leek", 1);
        
        
        ExamMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case 0:
                StepZero();
                break;
            case 1:
                WashVegetables();
                break;
            case 2:
                CutVegetables();
                break;
            case 3:
                AddOilToPan();
                break;
            case 4:
                AddVegetablesToPan();
                break;
            case 5:
                CookVegetables();
                break;
            case 6:
                WashMillet();
                break;
            case 7:
                AddRestToPan();
                break;
            case 8:
                AddVegetableBroth();
                break;
            case 9:
                AddEgg();
                break;

            case 10:
                Cook();
                break;

            case 11:
                CreateDough();
                break;
            case 12:
                FryBalls();
                break;
            case 13:
                End();
                break;
        }
        stepUi.text = step.ToString();
    }

    void StepZero()
    {
        if(GameObject.Find("GameScript").GetComponent<GameScript>().currentStep.id == 6 && GameObject.Find("Recipe").GetComponent<CurrentRecipe>().currentRecipe == gameObject.GetComponent<Recipe>())
        {
            step+=1;
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoWashVegetables");
        }
    }
    void WashVegetables()
    {
        foreach (WashVegetables wash in GameObject.FindObjectsByType<WashVegetables>())
        {
            if(wash.washed == false)
            {
                return;
            }
        }
        StepUp(2);
        GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoCutting");
    }

    void CutVegetables()
    {   
        // create copy of cutVegetables
        Dictionary<string, float> cut = new Dictionary<string, float>();;
        foreach (KeyValuePair<string, float> entry in cutVegetables)
        {
            cut.Add(entry.Key, entry.Value);
        }
        // check if all cutVegatables are in the bowl
        foreach(GameObject inhalt in schüssel.GetComponent<Bowl2>().content)
        {
            if ( ! (inhalt.GetComponent<Ingredient>() == null))
            {   
                cut[inhalt.GetComponent<Ingredient>()._name] -= inhalt.GetComponent<Ingredient>().quantity;
                if(cut[inhalt.GetComponent<Ingredient>()._name] < 0)
                {
                    cut[inhalt.GetComponent<Ingredient>()._name] = 0;
                }
            }
        }
        float sum = 0;
        foreach(KeyValuePair<string, float> entry in cut)
        {
            sum += entry.Value;
        }
        if(sum <= 0)
        {
            StepUp(3);
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoFats");

        }
        // Möhre Schälen
        // Möhre, Zuchini und Paprika Schniedne
        // in schüssel legen
    }
    void AddOilToPan()
    {
        if (pfanne.GetComponent<Braten>().oil)
        {
            StepUp(4);
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoVegetables");

            FoodWaste[] foodWastes = FindObjectsByType<FoodWaste>();
            foreach(FoodWaste fw in foodWastes)
            {
                GameObject.Find("GameScript").GetComponent<GameScript>().ChangeScore(-50);
            }
        }
        // oil in pfanne hinzufügen
    }
    void AddVegetablesToPan()
    {
        Dictionary<string, float> cut = new Dictionary<string, float>();;
        foreach (KeyValuePair<string, float> entry in cutVegetables)
        {
            cut.Add(entry.Key, entry.Value);
        }
        // check if all cutVegatables are in the bowl
        foreach(GameObject inhalt in pfanne.GetComponent<Braten>().Inhalt)
        {
            if (! (inhalt.GetComponent<Ingredient>() == null))
            {   
                cut[inhalt.GetComponent<Ingredient>()._name] -= inhalt.GetComponent<Ingredient>().quantity;
                if(cut[inhalt.GetComponent<Ingredient>()._name] < 0)
                {
                    cut[inhalt.GetComponent<Ingredient>()._name] = 0;
                }
            }
        }
        float sum = 0;
        foreach(KeyValuePair<string, float> entry in cut)
        {
            sum += entry.Value;
        }
        if(sum <= 0)
        {
            Debug.Log("StepUP");
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoFrying");
            StepUp(5);
        }
        // gemüse von schüssel in pfanne legen 
    }
    void CookVegetables()
    {
        foreach(GameObject bratobjekt in pfanne.GetComponent<Braten>().Inhalt)
        {
            Debug.Log("Test0");
            if(bratobjekt.GetComponent<BratElement>() != null)
            {
                Debug.Log("Test1");

                if (bratobjekt.GetComponent<BratElement>().gebraten == false)
                {   
                    Debug.Log("Test2");
                    return;
                }
            }
        }
        if(pfanne.GetComponent<Braten>().Inhalt.Count > 0)
        {
            Debug.Log("Test3");
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoLegumen");
            StepUp(6);
        }
    }
    void WashMillet()
    {
        int sumOfMillets = 0;
        foreach(GameObject inhalt in schüssel.GetComponent<Bowl2>().content)
        {   
            Ingredient ing = inhalt.GetComponent<Ingredient>();
            if(ing != null)
            {
                if(ing._name == "MilletBall")
                {
                    sumOfMillets++;
                }
            }
        }
        if(numberOfMillets <= sumOfMillets && schüssel.GetComponent<Bowl2>().water)
        {
            StepUp(7);

        }
        // hirse in schüssel schütten 
        // hirse waschen 
    } 
    void AddRestToPan()
    {   
        int sumOfMillets = 0;
        foreach(GameObject inhalt in pfanne.GetComponent<Braten>().Inhalt)
        {
            Ingredient ing = inhalt.GetComponent<Ingredient>();
            if(ing != null)
            {
                if(ing._name == "MilletBall")
                {
                    sumOfMillets++;
                }
            }
        }
        if(numberOfMillets <= sumOfMillets)
        {
            StepUp(8);
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoVegetables");
        }
        // kicherebsen in pfanne legen
        // hirse in pfanne legen
    }
    void AddVegetableBroth()
    {
        if (pfanne.GetComponent<Braten>().veg)
        {
            StepUp(9);
        }
    }
    void AddEgg()
    {   
        bool egg = false;
        foreach(GameObject inhalt in pfanne.GetComponent<Braten>().Inhalt)
        {
            Ingredient ing = inhalt.GetComponent<Ingredient>();
            if(ing != null)
            {
                if(ing._name == "Egg")
                {
                    egg = true;
                }
            }
        }
        if(egg)
        {
            StepUp(10);
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoFrying");
        }
    }
    void Cook()
    {   
        if(kochzeit > 0 && pfanne.GetComponent<Braten>().temperatur > 130)
        {
            kochzeit -= 0.1f;
        }
        else
        {
            StepUp(11);
        }
        // Aufkoche
        // 10 minuten köcheln
    }
    void CreateDough()
    {
        foreach(GameObject inhalt in pfanne.GetComponent<Braten>().Inhalt)
        {
            Destroy(inhalt);
        }
        pfanne.GetComponent<Braten>().Inhalt.Clear();


        pfannenInhalt.SetActive(true);
        pfannenInhalt.AddComponent<BoxCollider>();
        pfannenInhalt.AddComponent<Rigidbody>();
        pfannenInhalt.AddComponent<XRGrabInteractable>();
        pfannenInhalt.transform.SetParent(null);

        StepUp(12);
        GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoFrying");
    }
    void FryBalls()
    {
        int sumOfMilletBalls = 0;
        foreach(GameObject bratobjekt in pfanne.GetComponent<Braten>().Inhalt)
        {
            Ingredient ing = bratobjekt.GetComponent<Ingredient>();
            if(bratobjekt.GetComponent<BratElement>() != null && ing != null)
            {
                if (bratobjekt.GetComponent<BratElement>().gebraten == false)
                {
                    break;
                }
                else if(ing._name == "Millet")  
                {
                    sumOfMilletBalls++;
                }
            }
        }
        if(numberOfMilletBalls <= sumOfMilletBalls)
        {
            StepUp(13);
        }
    }

    void End()
    {
        CurrentRecipe currentRecipe = GameObject.Find("Recipe").GetComponent<CurrentRecipe>();
        for(int i = 0; i < currentRecipe.secondRecipe.ingredients.Count; i++)
        {
            GameObject.Find("Spawn").GetComponent<Spawning>().Spawn2(currentRecipe.secondRecipe.ingredients[i]);
        }

        oldUI.transform.localPosition = oldUI.transform.localPosition + new Vector3(0f,0f,100f);
        newUI.SetActive(true);
        step+=1;
        GameObject.Find("Recipe").GetComponent<CurrentRecipe>().secondRecipe.gameObject.SetActive(true);
        Destroy(pfanne.transform.parent.gameObject);
        GameObject.Find("GameScript").GetComponent<GameScript>().ChangeScore(1000);
    }

    void StepUp(int i)
    {
        step+=1;
        GetComponent<AudioSource>().Play();
        GameObject.Find("UI_Manager").GetComponent<BildschirmUI>().AddSteps(i-1);
    }
}
