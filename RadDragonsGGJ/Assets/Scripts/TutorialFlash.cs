using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFlash : MonoBehaviour
{
    
    public Sprite sprite1;
    public Sprite sprite2;

    public float sprite1Time = 1f;
    public float sprite2Time = 1f;

    private SpriteRenderer rend;
    private bool isSprite1;
    private float TimeTillSwap;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        TimeTillSwap = sprite1Time;
        rend.sprite = sprite1;
    }

    // Update is called once per frame
    void Update()
    {
        TimeTillSwap -= Time.deltaTime;
        if (TimeTillSwap < 0) {
            rend.sprite = isSprite1 ? sprite2 : sprite1;
            TimeTillSwap = isSprite1 ? sprite2Time : sprite1Time;
            isSprite1 = !isSprite1;
        }
    }
}
