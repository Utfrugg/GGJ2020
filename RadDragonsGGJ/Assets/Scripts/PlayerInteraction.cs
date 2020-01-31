using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 10;

    private bool isInteracting = false;
    private Interactable target;
    ContactFilter2D interactableFilter;

    void Start()
    {
        interactableFilter = new ContactFilter2D();
        interactableFilter.layerMask = LayerMask.NameToLayer("Interactable");
    }

    void UpdateTarget(Vector2 playerPos, Vector2 forwardVector)
    {
        List<Collider2D> foundInteractables = new List<Collider2D>();
        Physics2D.OverlapCircle(playerPos, interactionRange, interactableFilter, foundInteractables);
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D coll in foundInteractables)
        {
            Vector2 toInteractable = coll.transform.position - transform.position;
            if (Vector2.Dot(forwardVector, toInteractable) > 0)
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

    void OnInteractPress() {
        target.OnInteract();
    }
}
