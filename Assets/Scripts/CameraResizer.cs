using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour {
    public GameObject initialSize = null;

    // Use this for initialization
    void Start()
    {
        if (initialSize != null)
            Resize(initialSize);
    }

    public void Resize(GameObject bg)
    {
        Vector3 vcam = -Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        Vector3 vbg = bg.GetComponent<Renderer>().bounds.extents;
        float fx = vbg.x / vcam.x;
        float fy = vbg.y / vcam.y;

        Camera.main.orthographicSize *= Mathf.Max(fx, fy);
    }
}
