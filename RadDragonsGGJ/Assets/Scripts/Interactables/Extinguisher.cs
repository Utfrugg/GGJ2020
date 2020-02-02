using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : Interactable
{
    public float emitTimer = 0;
    private IEnumerator emitCoroutine;
    public ParticleSystem particles;
    public GameObject splashPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        emitTimer += Time.deltaTime;
        if (emitTimer > 0.2f) {
            particles.Stop();
        }
    }

    public override void OnInteract(Interactable itemUsed, bool holding)
    {

    }

    public override void OnUse(bool holding) {
        if (!particles.isPlaying)
        {
            particles.Play();
        }
        emitTimer = 0;
    }

}
