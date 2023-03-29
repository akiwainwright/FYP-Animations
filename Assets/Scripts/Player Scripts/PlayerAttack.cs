using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Actions m_PlayerActions;

    private Animator m_Animator;

    private bool m_bWaving = false;
    private bool m_bHitting = false;
  


    private void Awake()
    {
        m_PlayerActions = new Actions();

    }

    private void Start()
    {
        m_Animator = GetComponent<Animator>();

        m_PlayerActions.PlayerActions.Wave.performed += OnWave;
        m_PlayerActions.PlayerActions.Hit.performed += OnHit;
    }

    public void OnEnable()
    {
        m_PlayerActions.Enable();

        m_PlayerActions.PlayerActions.Wave.performed += OnWave;
        m_PlayerActions.PlayerActions.Hit.performed += OnHit;
    }

    public void OnDisable()
    {
        m_PlayerActions.Disable();

        m_PlayerActions.PlayerActions.Wave.performed -= OnWave;
        m_PlayerActions.PlayerActions.Hit.performed -= OnHit;
    }

    public void OnHit(InputAction.CallbackContext context)
    {
        if (!m_Animator.GetBool("Hitting"))
        {
            m_Animator.SetBool("Hitting", true);
            m_Animator.SetLayerWeight(2, 1);
            m_Animator.SetLayerWeight(1, 0);
        }
    }

    public void OnWave(InputAction.CallbackContext context)
    {
        m_Animator.SetBool("Waving", !m_Animator.GetBool("Waving"));

        if (m_Animator.GetBool("Waving"))
        {
            m_Animator.SetLayerWeight(1, 1);
        }
        else
        {
            m_Animator.SetLayerWeight(1, 0);
        }
    }

    public void StopHit()
    {
        m_Animator.SetBool("Hitting", false);
        m_Animator.SetLayerWeight(2, 0);
    }
}
