using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ending : MonoBehaviour
{
    public GameObject EndUI;
    public GameObject ScoreUI;
    public GameObject TimeUI;  
    public GameObject CookingSteps;  
    public GameObject CookingSteps2;      
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _Ending(string score, string time)
    {  
        CookingSteps.SetActive(false);

        CookingSteps2.SetActive(false);

        EndUI.SetActive(true);

        ScoreUI.GetComponent<TextMeshProUGUI>().text = score;

        TimeUI.GetComponent<TextMeshProUGUI>().text = time;
    }
}
