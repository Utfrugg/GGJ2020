using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    public float Durability =  1;
    public float DamagePerUse = 0.1f;
    public bool holdToUse = false;
    public bool broken = false;


     public override void OnUse(bool holding)
    {
        if (holding)
        {
            Durability -= (holdToUse) ? DamagePerUse * Time.deltaTime : 0;
        } else
        {
            Durability -= (holdToUse) ? DamagePerUse * Time.deltaTime : DamagePerUse;
        }


        if (Durability < 0) {
            broken = true;
            transform.GetChild(0).GetComponent<SpriteRenderer>().material.color =  new Color(0.3f, 0.3f, 0.3f, 1);
        }
    }
}
