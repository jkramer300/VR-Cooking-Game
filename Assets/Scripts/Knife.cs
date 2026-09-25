using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Knife : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameScript gameScript;
    void Start()
    {
        gameScript = GameObject.Find("GameScript").GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectKnife()
    {
        if (GetComponent<XRGrabInteractable>().isSelected && !gameScript.KnifeSelected)
        {
            gameScript.SelectKnife(true);
        }
        else if(!GetComponent<XRGrabInteractable>().isSelected && gameScript.KnifeSelected)
        {
            gameScript.SelectKnife(false);
        }
    }
}
