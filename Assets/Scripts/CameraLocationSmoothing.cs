using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocationSmoothing : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    public float y;
    public Transform player;
    public float yscale;
    public float minYScale;
    public float maxYScale;
    public float yscaleRate;
    void Start()
    {
        transform.position = player.position + offset + Vector3.up * y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset + Vector3.up * y * yscale;
        if (player.localScale.y > yscale)
        {
            yscale = Mathf.Clamp(yscale + yscaleRate * Time.deltaTime, minYScale, maxYScale);
        }
        else if (player.localScale.y < yscale)
        {
            yscale = Mathf.Clamp(yscale - yscaleRate * Time.deltaTime, minYScale, maxYScale);
        }
    }
}
