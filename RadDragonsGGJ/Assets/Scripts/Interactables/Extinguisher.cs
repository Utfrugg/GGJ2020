using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : Interactable
{
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

    public override void OnInteract()
    {
        Debug.Log("DIE FLAME!");
    }
}
