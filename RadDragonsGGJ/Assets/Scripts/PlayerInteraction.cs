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

    public void UpdateTarget(Vector2 playerPos, Vector2 forwardVector)
    {
        List<Collider2D> foundInteractables = new List<Collider2D>();
        Physics2D.OverlapCircle(playerPos, interactionRange, interactableFilter, foundInteractables);
        if (target != null)
        target.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        target = null;
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
        if (target != null)
        {
            Debug.Log("boffe");
            target.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }

    public void OnInteractPress() {
        target.OnInteract();
    }

    
}
