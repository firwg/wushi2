using UnityEngine;
using System.Collections.Generic;

public class CameraOffsetBehaviour : MonoBehaviour
{
    public Vector3 DefaultOffset = new Vector3(0, 2, -5);

    GameObject Offset;
    Transform OffsetTransform;
    Transform MyTransform;

	// Use this for initialization
	void Awake () 
    {
        MyTransform = transform;

        Offset = new GameObject("CameraOffset");
        OffsetTransform = Offset.transform;
        Debug.Log("DefaultOffset="+ DefaultOffset);

        OffsetTransform.position = DefaultOffset;//OffsetTransform.TransformPoint(DefaultOffset);
        Debug.Log("OffsetTransform.position="+OffsetTransform.position);
	}
	

    public Vector3 GetCameraPosition()
    {
        return OffsetTransform.position + MyTransform.position; //* 0.9f + MyTransform.position;
    }


    public void Rotation()
    {
        Quaternion q = new Quaternion(0, 1, 0, 0.1f);
        MyTransform.position = q * MyTransform.position;
    }




    void OnTriggerEnter(Collider other)
    {
        //is trigger camera volume ?
        TriggerCamera cameraVolume = other.GetComponent("TriggerCamera") as TriggerCamera;

        if (cameraVolume)
        {
            List<Vector3> pos = new List<Vector3>();

            if (cameraVolume.CameraOffset == null)
            {
                pos.Add(DefaultOffset);
                iTween.moveToBezier(Offset, cameraVolume.Time, 0, pos);
            }
            else
            {// get position from camera volume
                if (cameraVolume.Time == 0)
                {
                    OffsetTransform.position = cameraVolume.CameraOffset.localPosition;
                    //CameraBehaviour.Instance.Reset();
                }
                else
                {
                    pos.Add(cameraVolume.CameraOffset.localPosition);
                    iTween.moveToBezier(Offset, cameraVolume.Time, 0, pos);
                }
                iTween.moveTo(Offset, 0.5f, 0, cameraVolume.CameraOffset.transform.localPosition);
            }
        }

            
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log(this.ToString() + " exit " + other.ToString());
    }

    /// 
    void Activate(Transform t)
    {
        //Debug.Log("activate " + t.position);
        //OffsetTransform.position = t.TransformDirection(DefaultOffset);
        CameraBehaviour.Instance.Reset();
        //CameraBehaviour.Instance.Activate(t.position + Vector3.up, t.position + t.forward);
    }

    void Deactivate()
    {

    }

}
