using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public float minimum;
    public float maximum;
    public float current;
    public Image mask;
    public Image fill;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;

        mask.GetComponent<Image>().color = color;
    }

    public void SetPosition(Transform targetTransform)
    {
            transform.position = Camera.main.WorldToScreenPoint(targetTransform.position);
    }
}