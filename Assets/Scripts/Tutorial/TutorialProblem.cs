using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
public class TutorialProblem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Problems>().AddProblem(gameObject);
    }

    // Update is called once per frame
   void Update()
    {
        if(GetComponent<XRGrabInteractable>() != null)
        {
            if(GetComponent<XRGrabInteractable>().selectEntered.GetPersistentEventCount() == 0)
            {
                GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnHoverEnter);
                GetComponent<XRGrabInteractable>().selectExited.AddListener(OnHoverExit);
            }
        }
    }
    public void OnHoverEnter(SelectEnterEventArgs args)
    {
        gameObject.GetComponent<Problems>().SetActiveUI(true);
    }
    public void OnHoverExit(SelectExitEventArgs args)
    {
        gameObject.GetComponent<Problems>().SetActiveUI(false);
    }
}
