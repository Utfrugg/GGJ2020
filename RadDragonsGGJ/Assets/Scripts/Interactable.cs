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

    public virtual bool OnPickup()
    {
        if (canPickup)
        {
            canTarget = false;
            canPickup = false;

            return true;
        }
        return false;
    }

    public virtual void OnDrop()
    {
        canTarget = true;
        canPickup = true;
    }
}
