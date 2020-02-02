using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCan : Interactable
{
    public int targetLayer;
    

    public override void OnDrop(Interactable pickup)
    {
        BackgroundScroll scrollComponent = pickup.gameObject.AddComponent<BackgroundScroll>();

        pickup.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = targetLayer;
        pickup.gameObject.layer = 0;

        pickup.transform.rotation.SetEulerAngles(new Vector3(0,0,0));

        scrollComponent.extraOffset = 100;
        scrollComponent.movementSpeed = 20;
        scrollComponent.oneOff = true;
        Destroy(pickup.GetComponent<Collider2D>());
        Destroy(pickup.GetComponent<Rigidbody2D>());
    }
}
