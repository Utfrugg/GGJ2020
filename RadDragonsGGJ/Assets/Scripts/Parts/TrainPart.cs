using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartState
{
    GOOD,
    WARMINGUP,
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
                if (Random.Range(0, 500) == 0)
                    currentState = PartState.WARMINGUP;
                break;
            case PartState.WARMINGUP:
                heat += 0.005f;
                if (heat > 1)
                {
                    heat = 1;
                    currentState = PartState.BURNING;
                }
                break;
            case PartState.BURNING:
                health -= 0.005f;
                if (health < 0)
                {
                    health = 0;
                    currentState = PartState.BROKEN;
                }
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

    public override void OnInteract(Interactable itemUsed, bool holding)
    {
        if (itemUsed is Water && holding) {
            switch (currentState)
            {
                case PartState.BURNING:
                    break;
                case PartState.BROKEN:
                    break;
                default:
                    heat -= 0.05f;
                    if (heat < 0) {
                        heat = 0;
                        currentState = PartState.GOOD;
                    }
                    break;
            }
        }
        if (itemUsed is Extinguisher && holding)
        {
            switch (currentState)
            {
                case PartState.BURNING:
                    heat -= 0.05f;
                    if (heat < 0)
                    {
                        heat = 0;
                        currentState = PartState.GOOD;
                    }
                    break;
            }
        }

        if (itemUsed is Hammer && !holding && currentState != PartState.BURNING)
        {
            health += 0.2f;
            
            if (health > 1)
            {
                health = 1;
                if (currentState == PartState.BROKEN)
                currentState = PartState.GOOD;
            }
        }
    }
}
