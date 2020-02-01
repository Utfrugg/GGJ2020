using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialIcons : MonoBehaviour
{
    public enum TutorialState
    {
        COOL,
        EXTINGUISH,
        HAMMER,
        DEPOSIT
    }

    public GameObject XIcon;
    public GameObject AIcon;
    public GameObject WaterIcon;
    public GameObject ExtinguisherIcon;
    public GameObject HammerIcon;
    public GameObject PartIcon;

    void Start()
    {
        DisableIcons();
    }

    public void DisableIcons()
    {
        XIcon.SetActive(false);
        AIcon.SetActive(false);
        WaterIcon.SetActive(false);
        ExtinguisherIcon.SetActive(false);
        HammerIcon.SetActive(false);
        PartIcon.SetActive(false);
    }


    public void EnableIcons(TutorialState request)
    {
        TutorialFlash XIconFlash = XIcon.GetComponent<TutorialFlash>();
        TutorialFlash AIconFlash = AIcon.GetComponent<TutorialFlash>();
        switch (request)
        {
            case TutorialState.COOL:
                XIcon.SetActive(true);
                WaterIcon.SetActive(true);
                XIconFlash.sprite1Time = 0.5f;
                XIconFlash.sprite2Time = 1.5f;
                break;
            case TutorialState.EXTINGUISH:
                XIcon.SetActive(true);
                ExtinguisherIcon.SetActive(true);
                XIconFlash.sprite1Time = 0.5f;
                XIconFlash.sprite2Time = 1.5f;
                break;
            case TutorialState.HAMMER:
                XIcon.SetActive(true);
                HammerIcon.SetActive(true);
                XIconFlash.sprite1Time = 0.25f;
                XIconFlash.sprite2Time = 0.25f;
                break;
            case TutorialState.DEPOSIT:
                AIcon.SetActive(true);
                PartIcon.SetActive(true);
                AIconFlash.sprite1Time = 1f;
                AIconFlash.sprite2Time = 1f;
                break;
        }
    }
}
