using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartState
{
    GOOD,
    WARMINGUP,
    OVERHEATED,
    BURNING,
    BROKEN
}

public class TrainPart : Interactable
{
    public PartState currentState = PartState.GOOD;

    [SerializeField] private Healthbar healthbar;
    [SerializeField] private Healthbar heatbar;

    private float heat = 0.0f;
    private float health = 1.0f;

    void Start()
    {
        Train.Instance.trainParts.Add(this);
        UpdateBars();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case PartState.GOOD:
                break;
            case PartState.WARMINGUP:
                heat += 0.01f;
                break;
            case PartState.OVERHEATED:
                break;
            case PartState.BURNING:
                break;
            case PartState.BROKEN:
                break;
        }

        UpdateBars();
    }

    private void UpdateBars()
    {
        heatbar.SetSize(heat);
        healthbar.SetSize(health);
    }

    public override void OnInteract()
    {
        Debug.Log("PartBoi");
    }
}
