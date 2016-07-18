using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider))]
public class TriggerCamera: MonoBehaviour 
{
    public Transform CameraOffset;
    public float Time = 0.4f;

    public bool DisableAfterUse = false;


    // We'll draw a gizmo in the scene view, so it can be found....
    void OnDrawGizmos()
    {
        if (CameraOffset == null)
            return;

        Gizmos.color = Color.white;

        Gizmos.DrawSphere(CameraOffset.position, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.2f);

        Gizmos.DrawLine(transform.position, CameraOffset.position);

        Gizmos.DrawWireCube((GetComponent<Collider>() as BoxCollider).center + transform.position, (GetComponent<Collider>() as BoxCollider).size);
    }

    public void Disable()
    {
        gameObject.SetActiveRecursively(false);

    }

}
