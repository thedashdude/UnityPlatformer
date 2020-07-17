using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientWidget : MonoBehaviour
{
    public Transform cameraAngle;
    public Transform widget;
    public Transform widgetAnchor;
    public Transform widgetGoal;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Vector3 Blocked(Vector3 v)
    {
        float max = Mathf.Abs(v.x);
        int maxi = 0;
        if (max < Mathf.Abs(v.y))
        {
            max = Mathf.Abs(v.y);
            maxi = 1;
        }
        if (max < Mathf.Abs(v.z))
        {
            max = Mathf.Abs(v.z);
            maxi = 2;
        }
        Vector3 vr = Vector3.zero;
        vr[maxi] = Mathf.Sign(v[maxi]);
        return vr;
    }
    // Update is called once per frame
    public Vector3 GetAxis()
    {
        return Blocked(cameraAngle.forward);
    }
    public Vector3 GetAxisRight()
    {
        return Blocked(cameraAngle.right);
    }
    void Update()
    {
        widgetGoal.position = widgetAnchor.position + GetAxis();
        widget.LookAt(widgetGoal);
    }
}
