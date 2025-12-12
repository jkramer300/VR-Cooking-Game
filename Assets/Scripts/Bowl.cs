using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Bowl : MonoBehaviour
{
    // Start is called before the first frame update
    public float contentWeight = 0;
    public float flourWeight = 0;
    public float weight = 1f;
    public Material doug;
    public GameObject dougCap;
    (float, float) capHeight = (-0.0089f, 0.0347f);
    (float, float) capSize = (0.01f, 0.025f);

    List<GameObject> content = new List<GameObject>();
    GameObject _parent;
    bool _test = true;
    bool addFlour = false;
    ClipShader clipShader = new ClipShader();
    float factor = 0;
    string type;
    public List<float> types = new List<float>();
    void Start()
    {
        doug.SetFloat("_RevealAmount", doug.GetFloat("_Min"));
        for (int i = 0; i < 5; i++)
            types.Add(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (addFlour && factor <= 1)
        {
            AddFlour(1);
        }
        ChangeLayer();

        if (InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected)
        {
            ResetBowl();
        }
    }
    void ResetBowl()
    {
        for (int i = 0; i < 5; i++)
        {
            types[i] = 0f;
        }
        flourWeight = 0f;
        contentWeight = 0f;
        factor = 0;
        clipShader.ChangeShader(factor, capSize, (0, 0), capSize, capHeight, dougCap, doug, -1, 0, 0.0001f, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {

            GameObject test = content.Find(x => x == other.gameObject);
            if (test != null)
            {
                return;
            }

            GameObject ingredient = other.gameObject;

            content.Add(ingredient);

            contentWeight += ingredient.GetComponent<Ingredient>().quantity;
        }
        if (other.gameObject.layer == 9)
        {
            addFlour = true;
            type = other.gameObject.GetComponent<Ingredient>()._name;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {

            GameObject test = content.Find(x => x == other.gameObject);
            if (test == null && _test)
            {
                //content.Remove(test);
                return;
            }

            GameObject ingredient = other.gameObject;

            content.Remove(ingredient);

            contentWeight -= ingredient.GetComponent<Ingredient>().quantity;
            Debug.Log("ExitBowl");
        }
        if (other.gameObject.layer == 9)
        {
            addFlour = false;
        }
    }
    public void Selected()
    {
        foreach (GameObject ingredient in content)
        {
            ingredient.GetComponent<Rigidbody>().isKinematic = true;
            //_parent = ingredient.transform.parent.gameObject;
            ingredient.transform.SetParent(transform, true);
        }
    }
    public void UnSelected()
    {
        foreach (GameObject ingredient in content)
        {
            ingredient.transform.SetParent(null, true);
            ingredient.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    public void AddFlour(int i)
    {
        contentWeight += 0.001f * i;
        flourWeight += 0.001f * i;
        factor += 0.001f * i;
        clipShader.ChangeShader(factor, capSize, (0, 0), capSize, capHeight, dougCap, doug, -1, 0, 0.0001f, 0);

        if ("Mehl" == type)
        {
            types[0] += 0.001f;
        }
        else if ("Backpulver" == type)
        {
            types[1] += 0.001f;
        }
        else if ("Oil" == type)
        {
            types[2] += 0.001f;
        }
        else if ("Zucker" == type)
        {
            types[3] += 0.001f;
        }
        else if ("Mandeln" == type)
        {
            types[4] += 0.001f;
        }

    }
    void ChangeLayer()
    {
        if (flourWeight > 0)
        {
            if (this.gameObject.layer != 9)
            {
                this.gameObject.layer = 9;
            }

        }
        else if (this.gameObject.layer == 9)
        {
            //this.gameObject.layer = 0;
        }
    }
}
