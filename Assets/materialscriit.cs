using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialscriit : MonoBehaviour
{
    public Material Mat;
    public float alpha = 1;

    private void Update()
    {
        Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, alpha);
    }
}
