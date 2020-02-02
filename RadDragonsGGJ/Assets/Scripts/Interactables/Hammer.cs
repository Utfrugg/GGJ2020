using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Pickup
{
    public ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnInteract(Interactable itemUsed, bool holding)
    {
        Debug.Log("HAMMA");
    }


    public override void OnUse(bool holding)
    {
        base.OnUse(holding);

        if (!holding)
        particles.Play();
    }
}
