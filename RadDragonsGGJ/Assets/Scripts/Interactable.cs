using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canTarget = true;
    public bool canPickup = false;

    void Start()
    {
        
    }

    void Update()
    {

            }

    public void OnInteract()
    {
        Debug.Log("Hey I got interacted with");
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
