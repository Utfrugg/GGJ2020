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
    private SpriteRenderer heatbarRenderer;

    private float heat = 0.0f;
    private float health = 1.0f;

    public float TimeToBurn = 3f;
    public float TimeToBreak = 3f;
    public float TimeToDouse = 1.5f;
    public float HitsToRepair = 6f;

    private float BurnPerSecond;
    private float DamagePerSecond;
    private float DousePerSecond;
    private float HealthPerHammerHit;

    void Start()
    {
        Train.Instance.trainParts.Add(this);
        UpdateBars();

        BurnPerSecond = 1.0f / TimeToBurn;
        DamagePerSecond = 1.0f / TimeToBreak;
        DousePerSecond = 1.0f / TimeToDouse + BurnPerSecond;
        HealthPerHammerHit = 1.0f / HitsToRepair;

        heatbarRenderer = heatbar.transform.Find("Bar").GetChild(0).GetComponent<SpriteRenderer>();
}

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case PartState.GOOD:
                if (Random.Range(0, 500) == 0)
                    SwitchState(PartState.WARMINGUP);
                break;
            case PartState.WARMINGUP:
                heat += BurnPerSecond * Time.deltaTime;
                heatbarRenderer.color = new Color(1, 1f - heat * 0.5f, 0, 1);
                if (heat > 1)
                {
                    heat = 1;
                    SwitchState(PartState.BURNING);
                }
                break;
            case PartState.BURNING:
                health -= DamagePerSecond * Time.deltaTime;
                if (health < 0)
                {
                    health = 0;
                    SwitchState(PartState.BROKEN);
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

    private void SwitchState(PartState newState)
    {
        switch (newState)
        {
            case PartState.GOOD:
                break;
            case PartState.WARMINGUP:
                break;
            case PartState.BURNING:
                heatbarRenderer.color = new Color(1, 0, 0, 1);
                break;
            case PartState.BROKEN:
                    heat = 0;
                break;
        }
        currentState = newState;

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
                    heat -= DousePerSecond * Time.deltaTime;
                    if (heat < 0) {
                        heat = 0;
                        SwitchState(PartState.GOOD);
                    }
                    break;
            }
        }
        if (itemUsed is Extinguisher && holding)
        {
            switch (currentState)
            {
                case PartState.BURNING:
                    heat -= DousePerSecond * Time.deltaTime;
                    if (heat < 0)
                    {
                        heat = 0;
                        SwitchState(PartState.GOOD);
                    }
                    break;
            }
        }

        if (itemUsed is Hammer && !holding && currentState != PartState.BURNING)
        {
            health += HealthPerHammerHit;
            
            if (health > 1)
            {
                health = 1;
                if (currentState == PartState.BROKEN)
                    SwitchState(PartState.GOOD);
            }
        }
    }


}
