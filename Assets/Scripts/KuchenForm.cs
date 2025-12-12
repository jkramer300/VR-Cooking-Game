using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Globalization;
using UnityEngine.UIElements;
public class KuchenForm : MonoBehaviour
{
    // Start is called before the first frame update

    public float contentWeight = 0;
    public float flourWeight = 0;
    public float weight = 1f;
    public CurrentRecipe recipe;
    public GameObject cake;
    public Material doug;
    public GameObject dougCap;
    (float, float) capHeight = (-0.00979f, 0.0077f);
    (float, float) capSize = (0.01f, 0.025f);
    bool addDoug = false;
    ClipShader clipShader = new ClipShader();
    float factor = 0;
    public GameObject mixer;
    int state = 0;
    public Material cakeFinishedMaterial;
    bool finished = false;
    GameObject backedCake = null;

    void Start()
    {
        //addFlour = true;
        doug.SetFloat("_RevealAmount", doug.GetFloat("_Min"));
        clipShader.ChangeShader(factor, (0, 0), (0, 0), (0, 0), capHeight, dougCap, doug, -1, 0.9f, 0.9f, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        if (addDoug && factor <= 1 && mixer != null && mixer.GetComponent<PuttInMixxer>().GetMixxed())
        {
            AddDoug(1);
        }
        else if (factor > 1 && state == 0)
        {
            state = 1;
        }
        if (finished)
        {
            RemoveCake();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Behälter"))
        {
            foreach (float i in recipe.currentRecipe.quantity)
            {
                if (i > 0)
                {
                    return;
                }
            }
            addDoug = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Behälter"))
        {
            addDoug = false;
            //mixer = null;
        }
    }
    public void AddDoug(int i)
    {
        if (factor < 1 && mixer.GetComponent<PuttInMixxer>().factor > 0)
        {
            factor += 0.001f * i;
            mixer.GetComponent<PuttInMixxer>().RemoveDoug();
            clipShader.ChangeShader(factor, (0, 0), (0, 0), (0, 0), capHeight, dougCap, doug, -1, 0.9f, 0.9f, 0.02f);
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

    public void CakeFinished()
    {
        Renderer renderer = new Renderer();
        state = 2;
        backedCake = Instantiate(cake, cake.transform.position, cake.transform.rotation, cake.transform.parent);
        renderer = backedCake.GetComponent<Renderer>();
        renderer.material = cakeFinishedMaterial;
        factor = 0;
        clipShader.ChangeShader(factor, (0, 0), (0, 0), (0, 0), capHeight, dougCap, doug, -1, 0.9f, 0.9f, 0.02f);
        finished = true;
    }

    public void RemoveCake()
    {
        Renderer renderer = new Renderer();
        if (InputSystem.actions.FindAction("Open Egg").WasPressedThisFrame() && GetComponent<XRGrabInteractable>().isSelected)
        {
            if (backedCake != null)
            {
                backedCake.transform.SetParent(null, true);
                backedCake.AddComponent<BoxCollider>();
                backedCake.AddComponent<Rigidbody>();
                backedCake.AddComponent<XRGrabInteractable>();
                backedCake = null;
                finished = false;
            }
        }
    }

}
