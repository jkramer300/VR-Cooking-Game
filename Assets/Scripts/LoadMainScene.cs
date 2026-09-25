using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadGermanScene()
    {
        SceneManager.LoadScene("MainDeutsch");
    }
    public void LoadExamScene()
    {
        SceneManager.LoadScene("ExamMode");
    }
}
