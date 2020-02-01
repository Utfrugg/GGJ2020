using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        var controller = other.attachedRigidbody.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.losingOxygen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var controller = other.attachedRigidbody.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.losingOxygen = false;
        }
    }
}
