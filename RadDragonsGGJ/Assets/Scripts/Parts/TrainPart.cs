using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PartState
{
    GOOD,
    OVERHEATED,
    BURNING,
    BROKEN
}

public class TrainPart : Interactable
{
    private PartState currentState = PartState.GOOD;

    public float heat = 0.0f;
    public float health = 100.0f;

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case PartState.GOOD:
                break;
            case PartState.OVERHEATED:
                break;
            case PartState.BURNING:
                break;
            case PartState.BROKEN:
                break;
        }
    }

    public override void OnInteract()
    {
        Debug.Log("PartBoi");
    }
}
