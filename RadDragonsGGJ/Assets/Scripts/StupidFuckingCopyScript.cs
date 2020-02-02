using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidFuckingCopyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BackgroundScroll[] list = GetComponentsInChildren<BackgroundScroll>();

        foreach (BackgroundScroll thing in list) {
            for (int i = 0; i < thing.copyAmount; i++)
            {
                GameObject newThing = Instantiate(thing.gameObject, thing.transform.position, thing.transform.rotation);
                newThing.transform.Translate((thing.GetComponent<SpriteRenderer>().bounds.size.x + thing.extraOffset) * (i+1), 0, 0);
            }
        }
    }
}
