using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    [Range(0, 180)][SerializeField][Tooltip("Field of View Angle")] private float m_FOV;

    private GameObject m_Target;
    public GameObject playerHead;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LookAt")
        {
            InFOV(other.gameObject);
        }
    }

    private void InFOV(GameObject targetObject)
    {
        Debug.Log("Can see target");
        Vector3 toTarget = (targetObject.transform.position - transform.position).normalized;

        float angle = Vector3.Angle(transform.forward, toTarget);

        Debug.Log(angle);

        if ( angle > m_FOV/2.0f)
        {
            Debug.LogError("Not in FOV");

            return;
        }

        
        playerHead.transform.LookAt(Vector3.ProjectOnPlane(targetObject.transform.position, Vector3.up));
    }
}
