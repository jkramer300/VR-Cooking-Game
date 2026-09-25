using UnityEngine;

public class OverlayUI : MonoBehaviour
{
    public GameObject ui;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseWindow(bool set)
    {
        ui.SetActive(set);
    }
}
