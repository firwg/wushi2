using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSensorEyes : MonoBehaviour {

    Agent Owner;
    //Transform Transform;

    public float EyeRange = 6;
    public float FieldOfView = 120;

    float sqrEyeRange { get { return EyeRange * EyeRange; } }

    void Awake()
    {
        //Transform = transform;
        Owner = GetComponent("Agent") as Agent;

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (Owner.IsVisible == false)
            return;

        if (Mission.Instance.CurrentGameZone == null)
        {
            Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
            return;
        }

        List<Agent> enemies  = Mission.Instance.CurrentGameZone.Enemies;

        if (Owner.WorldState.GetWSProperty(E_PropKey.E_ALERTED).GetBool() == true)
        {
            if (enemies.Count == 0)
                Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if ((Owner.Position - enemies[i].Position).sqrMagnitude < sqrEyeRange)
                {
                    Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, true);
                    return;
                }
            }

            Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
        }
	}
}
