using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    public string nameOfTheStep;
    public int id;
    public List<string> instructions = new List<string>();
    public bool gatheringIngredients = false;
    public bool gatheringEquipment = false;
    GameScript gameScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameScript = GameObject.Find("GameScript").GetComponent<GameScript>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    
    
    public void SelectStep()
    {
        if (!gameScript.ExamMode)
        {
            gameScript.step = id;
        }
    }
}

//List<string> SelectRecipe = new List<string>(){"Select a Recepie"};
 //   List<string> washingHands = new List<string>(){"Wet Hands", "Use soap", "wash soap", "dry hands"};
  //  List<string> cleaningWorkspace = new List<string>(){"Wet Towel", "Use wet-towel to clean workspace", "Use a dry-towel to dry the workspace"};
   // List<string> gatherTools = new List<string>(){};
    //List<string> gatherIngredients = new List<string>(){"Putt the Ingredients on the Workspace"};
    //List<string> cooking = new List<string>(){"Follow the steps on the Left"};