using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Wasserhahn : MonoBehaviour
{
    public GameObject water;
    public GameObject waterJet;
    public ParticleSystem waterParticle;
    public GameObject drainSwitch;
    private float waterJetSize = 5f;
    bool tabIsOpen = false;
    bool drainIsOpen = false;
    bool semaphore1, semaphore2 = false;
    public AudioSource wasserSound;
    public AudioSource drainSound;

    // Start is called before the first frame update
    void Start()
    {
        waterParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
        CheckDrainSwitch();
    }

    void CheckRotation()
    {
        float xRoation = transform.eulerAngles.x;
        float yRoation = transform.eulerAngles.y;
        float zRoation = transform.eulerAngles.z;
        if ((xRoation > 270) && (yRoation > 135) && (zRoation > 0))
        {
            if (!tabIsOpen)
            {
                wasserSound.Play();
                waterJet.transform.localPosition += new Vector3(0f, waterJetSize * -1, 0f);
                waterJet.transform.localScale += new Vector3(0f, waterJetSize, 0f);
            }
            tabIsOpen = true;
            if (!semaphore1)
            {
                AddWater();
            }
            if (!waterParticle.isPlaying)
            {
                waterParticle.Play();
            }
        }
        else
        {
            if (tabIsOpen)
            {
                waterJet.transform.localPosition += new Vector3(0f, waterJetSize, 0f);
                waterJet.transform.localScale += new Vector3(0f, waterJetSize * -1, 0f);
                waterParticle.Stop();
                wasserSound.Stop();
            }
            tabIsOpen = false;
        }
    }
    void AddWater()
    {
        semaphore1 = true;
        if ((tabIsOpen) && (!drainIsOpen) && (water.transform.localPosition.y < 0.5f))
        {
            float factor = ((transform.localEulerAngles.x - 270f) / (315f - 270f) + 1f) * 2f;
            water.transform.localPosition += new Vector3(0f, 0.001f * factor, 0f);

            waterJet.transform.localPosition += new Vector3(0f, 0.0015f * factor, 0f);
            waterJet.transform.localScale += new Vector3(0f, 0.0015f * factor * -1, 0f);
            waterJetSize -= 0.0015f * factor;
        }
        semaphore1 = false;
    }
    void RemoveWater()
    {
        semaphore2 = true;
        if (drainIsOpen && !tabIsOpen && water.transform.localPosition.y > -1.28)
        {
            float factor = ((transform.localEulerAngles.x - 270f) / (315f - 270f) + 1f) * 2f;
            water.transform.localPosition += new Vector3(0f, -0.0015f * factor, 0f);

            //waterJet.transform.localPosition += new Vector3(0f, -0.001f * factor, 0f);
            //waterJet.transform.localScale += new Vector3(0f, 0.001f * factor, 0f);
            waterJetSize += 0.001f * factor;
        }
        semaphore2 = false;

    }
    void CheckDrainSwitch()
    {
        if (drainSwitch.transform.eulerAngles.y == 90f && !semaphore2 && (water.transform.localPosition.y > -1.28))
        {
            if (!drainIsOpen)
            {
                drainSound.Play();
            }
            drainIsOpen = true;
            RemoveWater();
        }
        else
        {
            if (drainSound.isPlaying)
            {
                drainSound.Stop();
            }
            drainIsOpen = false;
        }
    }

}
