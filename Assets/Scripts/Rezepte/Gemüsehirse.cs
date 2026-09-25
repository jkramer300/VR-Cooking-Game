using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Rendering;
using UnityEngine.AI;
using TMPro;


public class Gemüsehirse : MonoBehaviour
{   
    public bool ExamMode = true;
    public TextMeshProUGUI stepUi;
    public int step = 0;
    public Dose dose;
    public GameObject schüssel;
    public GameObject pfanne;
    private Dictionary<string, float> cutVegetables = new Dictionary<string, float>();
    private int numberOfMillets = 3;
    private int numberOfPeas = 6;
    private float kochzeit = 180f;
    public GameObject newUI;
    public GameObject oldUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateDict();
        ExamMode = true;
    }

    void CreateDict()
    {
        cutVegetables.Add("Paprika", 12-2); // 24
        cutVegetables.Add("Zucchini", 6-1); //24
        cutVegetables.Add("Carrot", 4); //8
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
                DrainAndDryChickPeas();
                break;
            case 2:
                WashVegetables();
                break;
            case 3:
                CutVegetables();
                break;
            case 4:
                AddOilToPan();
                break;
            case 5:
                AddVegetablesToPan();
                break;
            case 6:
                CookVegetables();
                break;
            case 7:
                WashMillet();
                break;
            case 8:
                AddRestToPan();
                break;
            case 9:
                Cook();
                break;
            case 10:
                End();
                break;
        }
    }

    void StepZero()
    {
        if(GameObject.Find("GameScript").GetComponent<GameScript>().currentStep.id == 6 && GameObject.Find("Recipe").GetComponent<CurrentRecipe>().currentRecipe == gameObject.GetComponent<Recipe>())
        {
            step+=1;
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoLegumen");
        }
    }
    void AddDose()
    {
        if(dose == null)
        {
            dose = GameObject.FindWithTag("Dose").GetComponent<Dose>();
        }
    }
    void DrainAndDryChickPeas()
    {   
        AddDose();
        // Kichererbsen
        // Dose Öffnen
        // Dose Abgießen Lassen
        if(dose.open > 1)
        {
            StepUp();
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

            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoWashVegetables");
            StepUp();
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
                StepUp();
            }
        // hirse in schüssel schütten 
        // hirse waschen 
    } 

    void AddRestToPan()
    {   
        int sumOfMillets = 0;
        int sumOfPeas = 0;
        foreach(GameObject inhalt in pfanne.GetComponent<Braten>().Inhalt)
        {
            Ingredient ing = inhalt.GetComponent<Ingredient>();
            if(ing != null)
            {
                if(ing._name == "MilletBall")
                {
                    sumOfMillets++;
                }
                if(ing._name == "Pea")
                {
                    sumOfPeas++;
                }
            }
        }
        if(numberOfMillets <= sumOfMillets && numberOfPeas <= sumOfPeas)
        {
            StepUp();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("Frying");
        }
        // kicherebsen in pfanne legen
        // hirse in pfanne legen
    }

    void Cook()
    {   
        if(kochzeit > 0 && pfanne.GetComponent<Braten>().temperatur > 130)
        {
            kochzeit -= 0.1f;
        }
        else
        {
            StepUp();
        }
        // Aufkoche
        // 10 minuten köcheln
    }
    void End()
    {
        /**if (ExamMode)
        {
            GameObject.Find("ScoreTime").GetComponent<TimerLogic>().StopTimer();
            string time = GameObject.Find("ScoreTime").GetComponent<TimerLogic>().zeitAnzeige.text;
            GameObject.Find("Scoreboard").GetComponent<Scorboard>().Save("Test", time, 10);
        }
        step+=1;**/
        CurrentRecipe currentRecipe = GameObject.Find("Recipe").GetComponent<CurrentRecipe>();
        for(int i = 0; i < currentRecipe.secondRecipe.ingredients.Count; i++)
        {
            GameObject.Find("Spawn").GetComponent<Spawning>().Spawn2(currentRecipe.secondRecipe.ingredients[i]);
        }
        
        oldUI.transform.localPosition = oldUI.transform.localPosition + new Vector3(0,0f,100f);
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
