using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 10;

    private bool isInteracting = false;
    private Interactable target;
    private Interactable pickup;
    ContactFilter2D interactableFilter;
    public LayerMask layer;
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
        target.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
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
            target.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    void FixedUpdate()
    {
        if (pickup != null)
        {
            pickup.transform.position = transform.position;
            pickup.transform.rotation = transform.rotation;
        }
        
    }

    public void OnInteractPress()
    {
        if (target != null)
        {
            var array = target.GetComponents<Interactable>();
            
            foreach (var comp in array)
            {
                Debug.Log(comp.GetType().Name);
                comp.OnInteract();
            }
        }

        if (pickup != null)
        {
            var array = pickup.GetComponents<Interactable>();

            foreach (var comp in array)
            {
                Debug.Log(comp.GetType().Name);
                comp.OnUse();
            }
        }
    }

    public void OnPickupPress()
    {
        if (pickup != null)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), pickup.GetComponent<Collider2D>(), false);
            pickup.OnDrop();
            pickup = null;
        }
        else if (target != null)
        {
            if (target.OnPickup())
            {
                pickup = target;
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), pickup.GetComponent<Collider2D>(), true);
            }
        }
    }
}
