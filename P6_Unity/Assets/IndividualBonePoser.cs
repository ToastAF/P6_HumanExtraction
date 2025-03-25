using UnityEngine;

public class IndividualBonePoser : MonoBehaviour
{
    public GameObject bone;

    void Update()
    {
        bone.transform.localPosition = transform.position; //Should set the bone's location as this empty object's location
    }
}
