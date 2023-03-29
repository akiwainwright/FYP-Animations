using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour
{
    private Animator m_Animator;

    [SerializeField] Vector3 m_BoxSize;

    //Feet Positions
    private Vector3 m_leftFootPosition;
    private Vector3 m_leftFootIKPosition; //Position relative to the left foot

    private Vector3 m_rightFootPosition;
    private Vector3 m_rightFootIKPosition; //Position relative to the left foot


    //Feet Rotations
    private Quaternion m_leftFootRotation;
    private Quaternion m_leftFootIKRotation; //Rotation relative to the left foot

    private Quaternion m_rightFootRotation;
    private Quaternion m_rightFootIKRotation; //Rotation relative to the right foot

    private LastYPositions m_lastYPositions;

    [SerializeField] private float m_GroundRayDistance;

    private float m_raycastStart; //point above the ground to start;
    private float m_raycastLength;

    [SerializeField]private LayerMask m_terrainLayer;

    public bool toggleIK;

    [SerializeField] private bool m_IsGrounded = false;


    struct LastYPositions
    {
        float pelvis;
        float leftFoot;
        float rightFoot;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        toggleIK = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position - transform.up * m_GroundRayDistance);

        Gizmos.DrawCube(transform.position + transform.up  - transform.up * m_GroundRayDistance, m_BoxSize);


    }

    //Needs to be in the same update as the players movement
    void FixedUpdate()
    {
        if(!CheckGrounded() || !toggleIK)
        {
            return;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //Only use IK if switched on
        if (!toggleIK)
        {
            return;
        }

        //Only use IK if player is on the floor
        if(!m_IsGrounded)
        {
            return;
        }

        //Set IK position weight for left and right foot to 1 to tell program to use IK position to override the animation
        m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        m_Animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        //Set IK rotation weight for left and right foot
        //Uses value from animation curve so that foot rotation is only adjusted when foot
        //is on the floor
        m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, m_Animator.GetFloat("LeftFoot"));
        m_Animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, m_Animator.GetFloat("RightFoot"));
    }

    private void AdjustToIkPosition(AvatarIKGoal foot, Vector3 ikPosition, Quaternion ikRotation)
    {

    }

    private bool CheckGrounded()
    {
        if (Physics.BoxCast(transform.position + transform.up, m_BoxSize/2, -transform.up, transform.rotation, m_GroundRayDistance))
        {
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
        }

        return m_IsGrounded;
    }

}
