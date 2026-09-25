using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameScript : MonoBehaviour
{
    public Canvas main;
    public List<GameObject> ui;
    public TextMeshProUGUI textField;
    public List<Step> steps = new List<Step>();
    public GameObject GatheringPoint;
    public GameObject GatheringPointVeg;
    public GameObject GatheringPoints;
    public int step = 0;
    public Step currentStep;
    public bool ExamMode = false;
    public bool KnifeSelected = false;
    public GameObject preparation;
    public GameObject cooking;
    public int score = 0;
    public GameObject ScoreUI;
    public int numberOfMistakes = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        AddSteps();
    }

    public void SelectKnife(bool selected)
    {
        KnifeSelected = selected;
    }
    
    // Update is called once per frame
    void Update()
    {   
        // step wurde abgeschlossen
        if(currentStep.id != step)
        {   
            if(step == 7)
            {
                GameObject.Find("UI_Manager").GetComponent<BildschirmUI>().ChoseRecipe("Gemüsegulasch");
                GameObject.Find("UI_Manager").GetComponent<BildschirmUI>().ChoseRecipe2("Schnitzel");
                step = 6;
            }
            currentStep = steps[step];
            GetComponent<AudioSource>().Play();
            AddSteps();
            ChangeInfo();
            if(currentStep.id == 0)
            {
                GameObject.Find("UI_Manager").GetComponent<BildschirmUI>().ResetRecipe();
            }
            else if(currentStep.id == 6)
            {
                ChangeScore(500);
            }

            // adds collider if nessesary
            switch (currentStep.id)
            {
            case 0:
                GameObject.Find("UI_Manager").GetComponent<BildschirmUI>().ResetRecipe();
                break;

            case 1:
                // in handScript
                break;    
            case 2:
                break;
            case 3:
                GatheringPoints.SetActive(true);
                break;
            case 4:
                GatheringPoints.SetActive(false);
                GatheringPointVeg.SetActive(true);
                break;
            case 5:
                GatheringPointVeg.SetActive(false);
                GatheringPoint.SetActive(true);
                if(GatheringPoint.GetComponent<GetheringIng>() == null)
                {
                    GatheringPoint.AddComponent<GetheringIng>();
                }
                break;
            case 6:
                GatheringPoint.SetActive(false);
                ChangeScore(500);
                preparation.SetActive(false);
                cooking.SetActive(true);
                break;
            }
        }

        if(currentStep.gatheringIngredients)
        {   
            GatheringPoint.SetActive(true);
            if(GatheringPoint.GetComponent<GetheringIng>() == null)
            {
                GatheringPoint.AddComponent<GetheringIng>();
            }
        }
        else if(GatheringPoint.activeSelf)
        {
            GatheringPoint.SetActive(false);
        }

        if (currentStep.gatheringEquipment)
        {
            GatheringPoints.SetActive(true);
        }
        else if(GatheringPoint.activeSelf)
        {
            GatheringPoints.SetActive(false);
        }
        if(currentStep.id == 4 && GatheringPointVeg.activeSelf == false)
        {
            GatheringPointVeg.SetActive(true);
        }
        else if(currentStep.id == 5 && GatheringPointVeg.activeSelf == true)
        {
            GatheringPointVeg.SetActive(false);
        }
        else if(currentStep.id == 6)
        {
            preparation.SetActive(false);
            cooking.SetActive(true);
        }
        // Check if ExamMode
        if(currentStep.id == 10)
        {
            EnterExamMode();
        }
    }

    public void ChangeScore(int number)
    {
        score += number;
        ScoreUI.GetComponent<TextMeshProUGUI>().text = score.ToString();

        if(number < 0)
        {
            numberOfMistakes++;
        }
    }
    public void EnterExamMode()
    {   
        SceneManager.LoadScene("ExamMode");
    }

    void AddSteps()
    {
        Debug.Log("AddSteps");
        foreach(GameObject UI in ui)
        {
            UI.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        for(int i = 0; i < step; i++)
        {
            ui[i].GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        ui[step].GetComponent<TextMeshProUGUI>().color = Color.blue;
        
        string text = "";
        for(int i = 0; i < currentStep.instructions.Count; i++)
        {
            text += currentStep.instructions[i];
            text += "\n";
        }
        textField.text = text;
    }

    void AddText(string text, GameObject ui, int fontSize)
    {
        GameObject textObject = new GameObject();
        textObject.transform.SetParent(ui.transform, false);
        TextMeshProUGUI tmpText = textObject.AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        tmpText.fontSize = fontSize;
        tmpText.alignment = TextAlignmentOptions.Left;
        tmpText.color = Color.black;
        RectTransform rect = textObject.GetComponent<RectTransform>();
    }
    void ChangeInfo()
    { 
        switch (currentStep.id)
        {
            case 2:
                GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoCleaningWorkspace");
                break;
            case 5:
                GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoRegionalität/Saisonalität");
                break;
            case 4:
                GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoWashVegetables");
                break;
        }
        //GameObject.FindWithTag("Info").GetComponent<Infos>().ChangeInfo("InfoCleaningWorkspace");
    }
}
