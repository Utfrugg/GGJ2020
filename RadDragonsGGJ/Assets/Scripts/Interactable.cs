using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Interactable : MonoBehaviour
{
    public bool canTarget = true;
    public bool canPickup = false;

    void Start()
    {
        OnStart();
    }

    public virtual void OnStart()
    {
        Train.Instance.interactables.Add(this);
    }

    public virtual void OnInteract(Interactable itemUsed, bool holding = false)
    {

    }

    public virtual void OnUse()
    {

    }

    public virtual Interactable OnPickup()
    {
        if (canPickup)
        {
            canTarget = false;
            canPickup = false;

            return this;
        }
        return null;
    }

    public virtual void OnDrop(Interactable pickup = null)
    {
        canTarget = true;
        canPickup = true;
    }
}
