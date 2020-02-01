using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Interactable
{
    public GameObject splashPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnUse()
    {
        GameObject Splash = Instantiate(splashPrefab, transform.position + transform.up * 1.0f, transform.rotation);
        Splash.GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(Random.value * 60 - 30, transform.forward) * transform.up * 10;
        //splashPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.transform.forward.x, this.transform.forward.z) * 3.0f);
    }
}
