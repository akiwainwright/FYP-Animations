using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Actions m_PlayerActions;
    [SerializeField] private Camera m_PlayerCamera;

    private Animator m_Animator;
    private Rigidbody m_Rb;

    private Vector2 m_MovementInput;

    private Vector3 m_MovementVector;
    private Vector3 m_FacingDirection;
    private Vector3 m_RightMovement;
    private Vector3 m_ForwardMovement;
    private Vector3 m_InputVelocity;

    [SerializeField] private float m_MovementSpeed = 5.0f;
    [SerializeField] private float m_RotateRate = 5.0f;
    [SerializeField] private float m_LocomotionDamp = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rb = this.GetComponent<Rigidbody>();
        m_Animator = this.GetComponent<Animator>();

        Cursor.visible = false;
        m_InputVelocity = Vector3.zero;
    }

    private void Awake()
    {
        m_PlayerActions = new Actions();
    }


    // Update is called once per frame
    void Update()
    {
        m_MovementInput = m_PlayerActions.PlayerActions.Move.ReadValue<Vector2>();

        m_Animator.SetFloat("Locomotion", m_MovementInput.magnitude, m_LocomotionDamp, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (m_MovementInput.magnitude > 0.0f)
        {
            Move();

            AdjustFacingDirection(m_MovementVector);
        }       
    }

    private void OnEnable()
    {
        m_PlayerActions.Enable();
    }

    private void OnDisable()
    {
        m_PlayerActions.Disable();
    }

    private void Move()
    {
        m_RightMovement = m_PlayerCamera.transform.right * m_MovementInput.x;

        m_ForwardMovement = Vector3.ProjectOnPlane(m_PlayerCamera.transform.forward, Vector3.up) * m_MovementInput.y;

        m_MovementVector = (m_RightMovement + m_ForwardMovement).normalized;

        m_Rb.AddForce(m_MovementVector * m_MovementSpeed, ForceMode.VelocityChange);

        m_InputVelocity.x = m_Rb.velocity.x;
        m_InputVelocity.z = m_Rb.velocity.z;
    }


    private void SetMoveAnimation(bool moving)
    {
        //stop trying to set animation bool multiple times
        if(m_Animator.GetBool("Moving") != moving)
        {
            m_Animator.SetBool("Moving", moving);
        }
    }

    private void AdjustFacingDirection(Vector3 movementDir)
    {
        Quaternion newLookRotation = Quaternion.LookRotation(movementDir);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, newLookRotation, m_RotateRate);
    }
}
