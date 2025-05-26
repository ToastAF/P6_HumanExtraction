using UnityEngine;
using System.Collections.Generic;

public class PosePoser : MonoBehaviour
{
    //GameObject rækkfølge i listen:  nose, neck, rShoulder, rElbow, rWrist, lShoulder, lElbow, lWrist, rHip, rKnee, rAnkle, rEye, lEye, rEar, lEar
    public List<GameObject> poses = new List<GameObject>();

    public List<float> rawXValues = new List<float>();
    public List<float> xValues = new List<float>();
    public List<float> rawYValues = new List<float>();
    public List<float> yValues = new List<float>();
    public List<float> depthValues = new List<float>();

    public bool pose;
    public float scalingValue, depthScalingValue;

    void Start()
    {
        for(int i = 0; i < rawXValues.Count; i++)
        {
            xValues.Add(rawXValues[i]);
            yValues.Add(rawYValues[i]);
        }
    }

    void Update()
    {
        if(pose == true)
        {
            ChangePose();
        }
    }

    public void ChangePose()
    {
        for (int i = 0; i < poses.Count; i++)
        {
            poses[i].transform.position = new Vector3(rawXValues[i] / scalingValue, -rawYValues[i] / scalingValue, depthValues[i] / depthScalingValue);
        }
    }
}
