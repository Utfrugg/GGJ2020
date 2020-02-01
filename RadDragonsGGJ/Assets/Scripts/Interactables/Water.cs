using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Interactable
{
    public GameObject splashPrefab;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnUse()
    {
        Instantiate(splashPrefab, transform.position + transform.up * 2.0f, transform.rotation);
        //splashPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.transform.forward.x, this.transform.forward.z) * 3.0f);
    }
}
