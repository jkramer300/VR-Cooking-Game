using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Hände : MonoBehaviour
{
    public int direction; // 1 left hand -1 right hand
    public Material normalMaterial;//1
    public Material wetMaterial;//2
    public Material soapMaterial;//3
    public Material wetSoapMaterial;//4
    public GameObject leftHand;
    public GameObject rightHand;
    public int materialState = 1;
    private int step = 0; // steps of washing hands step0: wet, step1 soap, step2 wet, step3 dry, step4 finished 
    public int contaminated = -1; //-1 not contaminated  0 contaminated 0 > contaminated and in the process of washing hands


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(contaminated < 0){
            switch (step)
            {
                case 0:
                    if(materialState == 2)
                    {
                        step = 1;
                    }
                    break;
                case 1:
                    if(materialState == 4)
                    {
                        step = 2;
                    }
                    break;
                case 2:
                    if(materialState == 2) step = 3;
                    break;
                case 3:
                    if(materialState == 1)
                    {
                        step = 4;
                    }
                    break;
                case 4:
                    step = 5;
                    GameObject GameScript = GameObject.Find("GameScript");
                    if (GameScript.GetComponent<GameScript>().currentStep.id == 1)
                    {
                        GameScript.GetComponent<GameScript>().step = 2;
                    }
                    break;
            }
        }
        else
        {
            switch (contaminated)
            {
                case 0:
                    if(materialState == 2)
                    {
                        contaminated = 1;
                    }
                    break;
                case 1:
                    if(materialState == 4)
                    {
                        contaminated = 2;
                    }
                    break;
                case 2:
                    if(materialState == 2)
                    {
                        contaminated = 3;
                    }
                    break;
                case 3:
                    if(materialState == 1)
                    {
                        contaminated = 4;
                    }
                    break;
                case 4:
                Debug.Log("direction:" + direction);
                    if (direction == 1)
                    {   
                        Debug.Log("dir1");
                        gameObject.GetComponent<Problems>().RemoveProblem(leftHand);
                    }
                    else if (direction == -1)
                    {
                        Debug.Log("dir-1");
                        gameObject.GetComponent<Problems>().RemoveProblem(rightHand);
                    }
                    contaminated = -1;
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        /**if (other.transform.CompareTag("Hand"))
        {
            Vector3 pos = other.transform.localPosition;
            pos.x = pos.x - (abs) * direction;
            other.transform.localPosition = pos;

            pos = transform.localPosition;
            pos.x = pos.x + (abs) * direction;
            transform.localPosition = pos;
        }**/

        if (other.transform.CompareTag("Schnitzel"))
        {
            if(other.gameObject.GetComponent<Ingredient>()._name == "Schnitzel")
            {
                if (direction == 1)
                {
                    gameObject.GetComponent<Problems>().AddProblem(leftHand);
                }
                else
                {
                    gameObject.GetComponent<Problems>().AddProblem(rightHand);
                }
                contaminated = 0;
                materialState = -1;
            }
        }
        else if (direction == 1) // LeftHand
        {
            if (other.transform.CompareTag("Seife"))
            {
                if (materialState == 1) //normal
                {

                    //leftHand.GetComponent<Renderer>().material = soapMaterial;
                    //materialState = 3;
                }
                else if (materialState == 2) //wet
                {

                    leftHand.GetComponent<Renderer>().material = wetSoapMaterial;
                    materialState = 4;
                }
            }
            else if (other.transform.CompareTag("Water"))
            {
                if (materialState == 1 || materialState == -1) //normal
                {

                    leftHand.GetComponent<Renderer>().material = wetMaterial;
                    materialState = 2;
                }
                else if (materialState == 3) //soap
                {

                    leftHand.GetComponent<Renderer>().material = wetMaterial;
                    materialState = 2;
                }
                else if (materialState == 4) //soap
                {

                    leftHand.GetComponent<Renderer>().material = wetMaterial;
                    materialState = 2;
                }
            }
            else if (other.transform.CompareTag("Lappen"))
            {
                leftHand.GetComponent<Renderer>().material = normalMaterial;
                materialState = 1;
            }
            else if (other.gameObject.GetComponent<Küchenrolle>()!= null || other.gameObject.GetComponent<Wasserhahn>()!= null || other.gameObject.GetComponent<Teller>()!= null)
            {
                
            }
            else if (other.gameObject.GetComponent<XRGrabInteractable>() && contaminated != -1)
            {
                GameObject.Find("GameScript").GetComponent<GameScript>().ChangeScore(-100);
            }
        }
        else if (direction == -1) // Righthand
        {
            if (other.transform.CompareTag("Seife"))
            {
                if (materialState == 1) //normal
                {
                    //rightHand.GetComponent<Renderer>().material = soapMaterial;
                    //materialState = 3;
                }
                else if (materialState == 2) //wet
                {
                    rightHand.GetComponent<Renderer>().material = wetSoapMaterial;

                    materialState = 4;
                }
            }
            else if (other.transform.CompareTag("Water"))
            {
                if (materialState == 1 || materialState == -1) //normal
                {
                    rightHand.GetComponent<Renderer>().material = wetMaterial;

                    materialState = 2;
                }
                else if (materialState == 3) //soap
                {
                    rightHand.GetComponent<Renderer>().material = wetMaterial;

                    materialState = 2;
                }
                else if (materialState == 4) //soap
                {
                    rightHand.GetComponent<Renderer>().material = wetMaterial;

                    materialState = 2;
                }
            }
            else if (other.transform.CompareTag("Lappen"))
            {
                rightHand.GetComponent<Renderer>().material = normalMaterial;
                materialState = 1;
            }
            else if (other.gameObject.GetComponent<Küchenrolle>()!= null || other.gameObject.GetComponent<Wasserhahn>()!= null || other.gameObject.GetComponent<Teller>()!= null)
            {
                
            }
            else if (other.gameObject.GetComponent<XRGrabInteractable>() && contaminated != -1)
            {
                GameObject.Find("GameScript").GetComponent<GameScript>().ChangeScore(-100);
            }
        }
    }


    /**private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Hand") && direction == 1 && false)
        {
            transform.localPosition = posL;
            other.transform.localPosition = posR;
        }
        if (other.transform.CompareTag("Hand") && direction == -1 && false)
        {
            transform.localPosition = posR;
            other.transform.localPosition = posL;
        }
        if (other.transform.CompareTag("Hand"))
        {
            if (direction == 1)
            {
                transform.localPosition = posL;
                other.transform.localPosition = posR;
            }
            else if (direction == -1)
            {
                transform.localPosition = posR;
                other.transform.localPosition = posL;
            }
            Debug.Log("Test222");
            //Vector3 pos = other.transform.localPosition;
            //pos.x = pos.x + (abs) * direction;
            //other.transform.localPosition = pos;

            //pos = transform.localPosition;
            //pos.x = pos.x - (abs) * direction;
            //transform.localPosition = pos;
        }
    }**/

    private void OnTriggerStay(Collider other)
    {
    }
    bool checkHands()
    {
        return true;
    }
}
