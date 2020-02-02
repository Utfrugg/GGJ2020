using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float movementSpeed;
    public float extraOffset;
    public int copyAmount = 1;
    public bool oneOff = false;

    private float minX;
    private bool Original = true;
    // Start is called before the first frame update
    void Start()
    {
        float bound = (GetComponent<SpriteRenderer>() != null) ? GetComponent<SpriteRenderer>().bounds.size.x : 100;
        minX = transform.position.x - bound;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(-movementSpeed * Time.deltaTime, 0));
        if (transform.position.x < minX) {
            if (oneOff)
            {
                Destroy(this);
            }
            else
            {
                transform.Translate(new Vector2(GetComponent<SpriteRenderer>().bounds.size.x + extraOffset, 0));
            }
        }
    }
}
