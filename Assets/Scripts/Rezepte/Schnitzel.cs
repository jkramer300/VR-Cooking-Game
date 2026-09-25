using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Schnitzel : MonoBehaviour
{
    public bool ExamMode = false;
    public GameObject pfanne;
    public GameObject gatheringPoint;
    public int step = 0;
    public GameObject schnitzel;
    public List<GameObject> plates;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                PrepareTheWorkspace();
                break;
            case 2:
                FlattenTheSchnitzel();
                break;
            case 3:
                PrepareBreadingStation();
                break;
            case 4:
                BreadingProcess();
                break;
            case 5:
                AddOilToPan();
                break;
            case 6:
                AddSchnitzelToPan();
                break;
            case 7:
                Cook();
                break;
            case 8:
                End();
                break;
        }
    }
    void AddSchnitzel()
    {
        if(schnitzel == null)
        {
            schnitzel = GameObject.FindWithTag("Schnitzel");
        }
    }
    void StepZero()
    {
        if(GameObject.Find("GameScript").GetComponent<GameScript>().currentStep.id == 6 && GameObject.Find("Recipe").GetComponent<CurrentRecipe>().secondRecipe == gameObject.GetComponent<Recipe>())
        {
            StepUp();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoMeatHygene");
        }
    }

    void PrepareTheWorkspace()
    {
        gatheringPoint.SetActive(true);
        if (gatheringPoint.GetComponent<Gathering3>().ready)
        {
            StepUp();
            gatheringPoint.SetActive(false);
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoMeat");   
        }
        
    }

    void FlattenTheSchnitzel()
    {
        AddSchnitzel();
        if (schnitzel.GetComponent<Panieren>().state == 0)
        {
            StepUp();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoBreading");
        }
    }

    void PrepareBreadingStation()
    {
        int i = 0;
        foreach(GameObject plate in plates)
        {
            if (plate.GetComponent<Teller>().inhalt == 1)
            {
                i++;
                break;                
            }
        }

        foreach(GameObject plate in plates)
        {
            if (plate.GetComponent<Teller>().inhalt == 2)
            {
                i++;
                break;                
            }
        }

        foreach(GameObject plate in plates)
        {
            if (plate.GetComponent<Teller>().inhalt == 3)
            {
                i++;
                break;                
            }
        }
        if(i >= 3)
        {
            StepUp();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoBreading");
        }
    }

    void BreadingProcess()
    {
        if(schnitzel.GetComponent<Panieren>().state >= 3)
        {
            StepUp();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoFats");
        }
    }

    void AddOilToPan()
    {
        if (pfanne.GetComponent<Braten>().oil)
        {
            StepUp();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoKäse");
        }
        // oil in pfanne hinzufügen
    }
    void AddSchnitzelToPan()
    {
        foreach(GameObject inhalt in pfanne.GetComponent<Braten>().Inhalt)
        {
            if(inhalt.GetComponent<Ingredient>()._name == "Schnitzel")
            {
                StepUp();
                GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoFrying");
            }
        }
    }
    void Cook()
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
            step+=1;
            GetComponent<AudioSource>().Play();
            GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("FoodWaste");
        }
    }
    void End()
    {
        if (ExamMode)
        {
            GameObject.Find("GameScript").GetComponent<GameScript>().ChangeScore(500);
            
            GameObject.Find("ScoreTime").GetComponent<TimerLogic>().StopTimer();
            
            string time = GameObject.Find("ScoreTime").GetComponent<TimerLogic>().zeitAnzeige.text;
            

            Scorboard sb = GameObject.Find("Scoreboard").GetComponent<Scorboard>();
            int score = GameObject.Find("GameScript").GetComponent<GameScript>().score;

            int numberOfMistakes = GameObject.Find("GameScript").GetComponent<GameScript>().numberOfMistakes;
            
            sb.SavePlayer("Player" + sb.number.ToString(), score, time, numberOfMistakes);

            FindAnyObjectByType<Ending>()._Ending(score.ToString(), time);
        }
        step+=1;
    }

    void StepUp()
    {
        step+=1;
        GetComponent<AudioSource>().Play();
        GameObject.Find("UI_Manager").GetComponent<BildschirmUI>().AddSteps2(step-1);
    }
}
