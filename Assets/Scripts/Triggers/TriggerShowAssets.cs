using UnityEngine;using System.Collections;public class TriggerShowAssets: MonoBehaviour {
    public GameObject[] GameObjectsList;    
    void OnTriggerEnter(Collider other)
    {
        if (other != Player.Instance.Agent.CharacterController)
            return;

        for (int i = 0; i < Mission.Instance.ManagedGameObject.Length; i++)
        {
            Mission.Instance.ManagedGameObject[i].SetActiveRecursively(IsInList(Mission.Instance.ManagedGameObject[i]));
        }
            }

    bool IsInList(GameObject gameObject)
    {
        for (int ii = 0; ii < GameObjectsList.Length; ii++)
        {
            if (gameObject == GameObjectsList[ii])
                return true;
        }

        return false;
    }}