using UnityEngine;
using System.Collections.Generic;

public class TryPose : MonoBehaviour
{
    public Transform leftHandTarget;
    public Transform rightHandTarget;
    public Transform leftFootTarget;
    public Transform rightFootTarget;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("Animator component not found on this GameObject.");
    }

    // Called by the Animator component to handle IK
    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // Left Hand
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);

            // Right Hand
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);

            // Left Foot
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootTarget.position);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootTarget.rotation);

            // Right Foot
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootTarget.rotation);
        }
    }
}
