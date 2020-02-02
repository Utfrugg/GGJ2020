using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Image healthbar;
    [SerializeField] private Image heatbar;

    private float heat = 0.0f;
    private float health = 1.0f;

    public float TimeToBurn = 3f;
    public float TimeToBreak = 3f;
    public float TimeToDouse = 1.5f;
    public float HitsToRepair = 6f;

    public ParticleSystem Smoke;
    public ParticleSystem Fire;
    public ParticleSystem Explode;

    //public Camera cameraShake;

    private float BurnPerSecond;
    private float DamagePerSecond;
    private float DousePerSecond;
    private float HealthPerHammerHit;

    TutorialIcons tutorialIcons;

    void Start()
    {
        Train.Instance.trainParts.Add(this);
        UpdateBars();
        tutorialIcons = GetComponent<TutorialIcons>();
        BurnPerSecond = 1.0f / TimeToBurn;
        DamagePerSecond = 1.0f / TimeToBreak;
        DousePerSecond = 1.0f / TimeToDouse + BurnPerSecond;
        HealthPerHammerHit = 1.0f / HitsToRepair;
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
        heatbar.fillAmount = heat;
        healthbar.fillAmount = health;
    }

    public void SwitchState(PartState newState)
    {
        tutorialIcons.DisableIcons();

        Smoke.GetComponent<ParticleSystem>().Stop();
        Fire.GetComponent<ParticleSystem>().Stop();
        Explode.GetComponent<ParticleSystem>().Stop();

        switch (newState)
        {
            case PartState.GOOD:
                if (health < 1)
                tutorialIcons.EnableIcons(TutorialIcons.TutorialState.HAMMER);
                break;
            case PartState.WARMINGUP:
                tutorialIcons.EnableIcons(TutorialIcons.TutorialState.COOL);
                
                //particles smoke
                Smoke.GetComponent<ParticleSystem>().Play();
                ParticleSystem.EmissionModule em1 = Smoke.GetComponent<ParticleSystem>().emission;
                em1.enabled = true;


                break;
            case PartState.BURNING:
                tutorialIcons.EnableIcons(TutorialIcons.TutorialState.EXTINGUISH);
                heat = 1;

                //particles fire
                Fire.GetComponent<ParticleSystem>().Play();
                ParticleSystem.EmissionModule em2 = Fire.GetComponent<ParticleSystem>().emission;
                em2.enabled = true;

                break;
            case PartState.BROKEN:
                tutorialIcons.EnableIcons(TutorialIcons.TutorialState.DEPOSIT);
                health = 0;
                heat = 0;

                //particles explosion
                Explode.GetComponent<ParticleSystem>().Play();
                ParticleSystem.EmissionModule em3 = Explode.GetComponent<ParticleSystem>().emission;
                em3.enabled = true;

                // Need to call shake behaviour in the camera, but can't get it to work. Reference?
                //Camera.GetComponent ShakeBehaviour.shakeBehaviour();

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

        if (itemUsed is Hammer && !holding && currentState != PartState.BURNING && currentState != PartState.BROKEN)
        {
            health += HealthPerHammerHit;
            health = Mathf.Min(1, health);
            if (health == 1 && currentState == PartState.GOOD)
                tutorialIcons.DisableIcons();

        }
    }


    public override void OnDrop(Interactable pickup)
    {
        if (pickup is RepairPart && currentState == PartState.BROKEN)
        {
            health = 0.1f;
            heat = 0;
            SwitchState(PartState.GOOD);
            Destroy(pickup.gameObject);
        }
    }
}
