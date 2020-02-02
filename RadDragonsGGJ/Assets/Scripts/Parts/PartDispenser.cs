using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDispenser : Interactable
{
    private bool partReady;
    private bool isGenerating;

    public float timeToGenerate = 2f;
    public Interactable partPrefab;
    public ProgressBar ProgressWheelPrefab;

    private ProgressBar ProgressWheel;
    [SerializeField] private GameObject partIcon;
    private float progressPerSecond;

    private float partProgress = 0f;

    void Start()
    {
        ProgressWheel = Instantiate(ProgressWheelPrefab);
        ProgressWheel.transform.SetParent(GameObject.Find("UICanvas").transform);
        ProgressWheel.SetPosition(transform);
        ProgressWheel.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        ProgressWheel.minimum = ProgressWheel.current = 0;
        ProgressWheel.maximum = 1;
        ProgressWheel.gameObject.SetActive(false);
        progressPerSecond = 1 / timeToGenerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGenerating) {
            partProgress += progressPerSecond * Time.deltaTime;
            ProgressWheel.current = partProgress;
            if (partProgress > 1f) {
                partReady = true;
                isGenerating = false;
                ProgressWheel.gameObject.SetActive(false);
                partIcon.SetActive(true);
            }
        }
    }

    public override void OnInteract(Interactable itemUsed, bool holding)
    {
        if (!partReady && !isGenerating && !holding) {
            ProgressWheel.gameObject.SetActive(true);
            ProgressWheel.current = 0;
            partProgress = 0;
            isGenerating = true;
        }
    }

    public override Interactable OnPickup()
    {
        if (partReady) {
            partIcon.SetActive(false);
            Interactable newPart = Instantiate(partPrefab, transform);
            newPart.OnPickup();
            partReady = false;
            return newPart;
        }
        return null;
    }
}
