using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKControlTerrain : MonoBehaviour
{
    private Animator m_Animator;

    public Transform  leftFootPosition, rightFootPosition;
    public Transform leftFootIKTartetPos, rightFootIKTargetPos;

    public Quaternion leftFootRotation, rightFootRotation;
    public Quaternion leftFootIKTargetRot, rightFootIKTargetRot;


    public bool bIKOn;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {

        RotateFoot(AvatarIKGoal.LeftFoot);
        
    }

    void RotateFoot(AvatarIKGoal foot)
    {
        m_Animator.GetBoneTransform(HumanBodyBones.LeftFoot).Rotate(0, 5, 0);
    }
}
