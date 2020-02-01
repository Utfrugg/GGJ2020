using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public virtual void OnInteract()
    {

    }

    public virtual void OnUse()
    {

    }

    public bool OnPickup()
    {
        if (canPickup)
        {
            canTarget = false;
            canPickup = false;

            return true;
        }
        return false;
    }

    public void OnDrop()
    {
        canTarget = true;
        canPickup = true;
    }
}
