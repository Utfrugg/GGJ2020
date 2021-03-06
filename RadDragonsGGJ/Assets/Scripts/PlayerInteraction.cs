﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 10;

    private bool isInteracting = false;
    public bool isHoldingButton = false;
    private Interactable target;
    public Interactable pickup;
    ContactFilter2D interactableFilter;
    public LayerMask layer;
    [SerializeField] private float throwForce = 1000f;

    public bool inputEnabled { get; set; } = true;

    void Start()
    {
       interactableFilter = new ContactFilter2D();
       interactableFilter.SetLayerMask(layer);
    }

    public void UpdateTarget(Vector2 playerPos)
    {
        List<Collider2D> foundInteractables = new List<Collider2D>();
        Physics2D.OverlapCircle(playerPos, interactionRange, interactableFilter, foundInteractables);
        if (target != null)
        {
            target.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
            
        target = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D coll in foundInteractables)
        {
            if (coll.GetComponent<Interactable>().canTarget)
            {
                Vector2 toInteractable = coll.transform.position - transform.position;
                if (Vector2.Dot(transform.up, toInteractable) > 0)
                {
                    float sqrDistance = toInteractable.sqrMagnitude;
                    if (sqrDistance < shortestDistance)
                    {
                        shortestDistance = sqrDistance;
                        target = coll.GetComponent<Interactable>();
                    }
                }
            }
        }
        if (target != null)
        {
            target.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    void Update()
    {
        if (pickup != null)
        {
            pickup.transform.position = transform.position + transform.up;
            pickup.transform.rotation = transform.rotation;
        }

        if (target != null && isHoldingButton)
        {
            var array = target.GetComponents<Interactable>();

            foreach (var comp in array)
            {
                if (pickup != null) {
                    comp.OnInteract((pickup.GetComponent<Pickup>().broken) ? null : pickup, true);
                } else {
                    comp.OnInteract(pickup, true);
                }

            }
        }

        if (pickup != null && isHoldingButton)
        {
            var array = pickup.GetComponents<Pickup>();

            foreach (var comp in array)
            {
                if (!comp.broken)
                comp.OnUse(isHoldingButton);
            }
        }

    }

    public void OnInteractPress()
    {
        if (inputEnabled)
        {
            if (target != null)
            {
                var array = target.GetComponents<Interactable>();

                foreach (var comp in array)
                {
                    Debug.Log(comp.GetType().Name);
                    if (pickup != null)
                    {
                        comp.OnInteract((pickup.GetComponent<Pickup>().broken) ? null : pickup);
                    }
                    else
                    {
                        comp.OnInteract(pickup);
                    }
                }
            }

            if (pickup != null)
            {
                var array = pickup.GetComponents<Pickup>();

                foreach (var comp in array)
                {
                    if (!comp.broken)
                    comp.OnUse(isHoldingButton);
                }
            }
        }
    }

    public void OnPickupPress()
    {
        if (inputEnabled)
        {
            if (pickup != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), pickup.GetComponent<Collider2D>(), false);
                if (target != null)
                {
                    target.OnDrop(pickup);
                }
                pickup.OnDrop();
                pickup = null;
            }
            else if (target != null)
            {
                Interactable newPickup = target.OnPickup();
                if (newPickup != null)
                {
                    pickup = newPickup;
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), pickup.GetComponent<Collider2D>(), true);
                }
            }
        }
    }

    public void OnThrow()
    {
        if (inputEnabled)
        {
            if (pickup != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), pickup.GetComponent<Collider2D>(), false);
                pickup.OnDrop();
                var dir = (Vector2)((Quaternion.Euler(0, 0, GetComponent<Rigidbody2D>().rotation) * Vector2.up));
                pickup.GetComponent<Rigidbody2D>().AddForce(dir * throwForce);
                
                pickup = null;
            }
        }
    }
}
