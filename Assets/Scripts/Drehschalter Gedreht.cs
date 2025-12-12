using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Drehschalter2 : MonoBehaviour
{
    private HingeJoint hinge;
    //private int steps = 5;
    private float rotation = 180;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }
    //void Update(){}

    public void SetPos()
    {
        //transform.localEulerAngles = new Vector3(rotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    public void LockPos()
    {
        Debug.Log($"{transform.eulerAngles.x}, {transform.eulerAngles.y}, {transform.eulerAngles.z}");
        float x = transform.eulerAngles.x;
        float y = transform.eulerAngles.y;
        float z = transform.eulerAngles.z;

        //transform.eulerAngles = new Vector3(90f, y, z);
        switch ((y, y))
        {
            case ( <= 45, <= 45):
            case ( >= 316, >= 316):
                rotation = 0f;
                transform.eulerAngles = new Vector3(x, rotation, z);
                break;

            case ( <= 135, >= 46):
                rotation = 90f;
                transform.eulerAngles = new Vector3(x, rotation, z);
                break;

            case ( <= 225, >= 136):
                rotation = 180f;
                transform.eulerAngles = new Vector3(x, rotation, z);
                break;


            case ( <= 315, >= 226):
                rotation = 270f;
                transform.eulerAngles = new Vector3(x, rotation, z);
                break;


        }
        /*case ( <= 215, >= 144):
            rotation = 180;
            transform.localEulerAngles = new Vector3(rotation, y, z);
            break;

        case ( <= 143, >= 72):
            rotation = 108;
            transform.localEulerAngles = new Vector3(rotation, y, z);
            break;

        case ( <= 71, >= 0):
            rotation = 36;
            transform.localEulerAngles = new Vector3(rotation, y, z);
            break;


        case ( <= 359, >= 288):
            rotation = 324;
            transform.localEulerAngles = new Vector3(rotation, y, z);
            break;

        case ( <= 287, >= 216):
            rotation = 252;
            transform.localEulerAngles = new Vector3(rotation, y, z);
            break;*/


        //float nearestStep = Mathf.Round((angle - hinge.limits.min) / stepSize) * stepSize + hinge.limits.min;
    }

}
