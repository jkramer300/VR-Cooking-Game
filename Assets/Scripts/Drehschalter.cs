using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Drehschalter : MonoBehaviour
{
    private HingeJoint hinge;
    //private int steps = 5;
    public float rotation = 180f;

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
        Debug.Log($"{transform.localEulerAngles.x}, {transform.localEulerAngles.y}, {transform.localEulerAngles.z}");
        float x = transform.localEulerAngles.x;
        float y = transform.localEulerAngles.y;
        float z = transform.localEulerAngles.z;

        //transform.eulerAngles = new Vector3(90f, y, z);
        switch ((x, x))
        {

            case ( <= 135, >= 46):
                rotation = 90f;
                transform.localEulerAngles = new Vector3(rotation, y, z);
                break;

            case ( <= 225, >= 136):
                rotation = 180f;
                transform.localEulerAngles = new Vector3(rotation, y, z);
                break;


            case ( <= 315, >= 226):
                rotation = 270f;
                transform.localEulerAngles = new Vector3(rotation, y, z);
                break;

            case ( <= 45, <= 45):
            case ( >= 316, >= 316):
                if (transform.localEulerAngles.y == 180f)
                {
                    rotation = 180f;
                    transform.localEulerAngles = new Vector3(0f, y, z);
                    break;
                }
                else
                {
                    rotation = 0f;
                    transform.localEulerAngles = new Vector3(rotation, y, z);
                    break;
                }


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
