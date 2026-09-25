using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DeveloperOptions : MonoBehaviour
{
    public GameObject firstUI;
    public GameObject secondUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenDeveloperOptions(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDeveloperOptions(bool open)
    {
        firstUI.SetActive(!open);
        secondUI.SetActive(open);
    }
    
    public void SpawnIng()
    {
        CurrentRecipe currentRecipe = GameObject.Find("Recipe").GetComponent<CurrentRecipe>();

        for(int i = 0; i < currentRecipe.currentRecipe.ingredients.Count; i++)
        {
            GameObject.Find("Spawn").GetComponent<Spawning>().Spawn2(currentRecipe.currentRecipe.ingredients[i]);
        }
        for(int i = 0; i < currentRecipe.secondRecipe.ingredients.Count; i++)
        {
            GameObject.Find("Spawn").GetComponent<Spawning>().Spawn2(currentRecipe.secondRecipe.ingredients[i]);
        }
    }

    public void SkipCooking()
    {
        GameObject rec = GameObject.Find("Recipe").GetComponent<CurrentRecipe>().currentRecipe.gameObject;

        if(rec.name == "Gemüsehirse")
        {
            rec.GetComponent<Gemüsehirse>().step = 10;
        }
        else if (rec.name == "Hirsebällchen")
        {
            rec.GetComponent<Hirsebällchen>().step = 13;
        }
        else if (rec.name == "Gemüsegulasch")
        {
            rec.GetComponent<Gemüsegulasch>().step = 8;
        }
    }
    
    public void SkipPre()
    {
        GameObject.Find("GameScript").GetComponent<GameScript>().step = 6;
        WashVegetables();
    }
    public void WashVegetables()
    {
        WashVegetables[] wv = FindObjectsByType<WashVegetables>();

        foreach(WashVegetables v in wv)
        {
            v.washed = true;
        }
    }
}
