using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private Vector3 m_DefaultCameraOffset;
    private Vector3 m_CameraOffset;

    [SerializeField] private GameObject m_FocusPoint;

    [SerializeField] private Transform m_Camera;

    [SerializeField] private float m_Dampening;

    [SerializeField] private float m_Sensitivity;
    private float m_RotAngle;

    private Vector3 m_CurentVelocity = Vector3.zero;

    public InputAction CameraZoom;
    private void Awake()
    {
        transform.position = m_FocusPoint.transform.position;

        m_CameraOffset = m_DefaultCameraOffset;

        if (m_Camera)
        {
            m_Camera.position = transform.position + m_CameraOffset;
        }
        else
        {
            Debug.LogError("No Camera");
        }
    }

    private void Update()
    {
        m_RotAngle = UnityEngine.InputSystem.Mouse.current.delta.ReadValue().x;
    }

    private void FixedUpdate()
    {
        m_Camera.LookAt(transform.position);
        transform.position = Vector3.SmoothDamp(transform.position, m_FocusPoint.transform.position, ref m_CurentVelocity, m_Dampening);

        transform.Rotate(0.0f, UnityEngine.InputSystem.Mouse.current.delta.ReadValue().x * m_Sensitivity * Time.fixedDeltaTime, 0.0f);
    }

    private void Zoom()
    {

    }

    
    
}
