using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class Gemüsegulasch : MonoBehaviour
{
    public bool ExamMode = false;
    public TextMeshProUGUI stepUi;
    public int step = 0;
    public GameObject schüssel;
    public GameObject pfanne;
    private Dictionary<string, float> cutVegetables = new Dictionary<string, float>();
    private int numberOfMillets = 1;
    private int numberOfPeas = 1;
    private float kochzeit = 10f;
    public GameObject newUI;
    public GameObject oldUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cutVegetables.Add("Carrot", 4);//8
        cutVegetables.Add("Paprika", 6); // 24
        cutVegetables.Add("Zucchini", 6); //24
        cutVegetables.Add("Leek", 7-1); //7

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
                AddVegetableBroth();
                break;
            case 7:
                AddRest();
                break;
            case 8:
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
        StepUp();
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
            StepUp();
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
            StepUp();
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
            StepUp();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoFrying");
        }
        // gemüse von schüssel in pfanne legen 
    }
    void CookVegetables()
    {
        foreach(GameObject bratobjekt in pfanne.GetComponent<Braten>().Inhalt)
        {
            if(bratobjekt.GetComponent<BratElement>() != null)
            {
                if (bratobjekt.GetComponent<BratElement>().gebraten == false)
                {
                    return;
                }
            }
        }
        if(pfanne.GetComponent<Braten>().Inhalt.Count > 0)
        {
            StepUp();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoVegetables");
        }
    }
    void AddVegetableBroth()
    {
        if (pfanne.GetComponent<Braten>().veg)
        {
            if(kochzeit > 0 && pfanne.GetComponent<Braten>().temperatur > 130)
            {
                kochzeit -= 0.1f;
            }
            else
            {
                StepUp();
            }
        }
    }
    void AddRest()
    {
        if (pfanne.GetComponent<Braten>().sour && pfanne.GetComponent<Braten>().soy && pfanne.GetComponent<Braten>().tomato)
        {
            StepUp();
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

    void StepUp()
    {
        step+=1;
        GetComponent<AudioSource>().Play();
        GameObject.Find("UI_Manager").GetComponent<BildschirmUI>().AddSteps(step-1);
    }
}
