using UnityEngine;
using System.Collections;

public class SensorEyes : MonoBehaviour {

    Agent Owner;
    Agent MyEnemy;
  //  Transform Transform;

    public float EyeRange = 6;
    public float FieldOfView = 120;

    float sqrEyeRange { get { return EyeRange * EyeRange; } }

    void Awake()
    {
    //    Transform = transform;
        Owner = GetComponent("Agent") as Agent;
        

    }
	// Use this for initialization
	void Start () {
        Owner.BlackBoard.DesiredTarget = null;
        MyEnemy = Player.Instance.Agent;
	}

	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (Owner.IsVisible == false)
            return;


        if (MyEnemy.IsAlive == false)
        {
            Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
            Owner.WorldState.SetWSProperty(E_PropKey.E_ATTACK_TARGET, false);
            return;
        }

        //reset some shits
        Owner.WorldState.SetWSProperty(E_PropKey.E_LOOKING_AT_TARGET, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_AHEAD_OF_ENEMY, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_BEHIND_ENEMY, false);


        Vector3 dirToEnemy = MyEnemy.Position - Owner.Position;

        Owner.BlackBoard.DistanceToTarget = dirToEnemy.magnitude;

        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, Owner.BlackBoard.DistanceToTarget < Owner.BlackBoard.WeaponRange);
        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_COMBAT_RANGE, Owner.BlackBoard.DistanceToTarget < Owner.BlackBoard.CombatRange);

        Owner.WorldState.SetWSProperty(E_PropKey.E_BEHIND_ENEMY, Vector3.Angle(dirToEnemy, MyEnemy.Forward) > 180);

        if (Owner.WorldState.GetWSProperty(E_PropKey.E_ALERTED).GetBool() == false)
        {
            if(Owner.BlackBoard.DistanceToTarget < EyeRange &&  Vector3.Angle(Owner.Forward, dirToEnemy) < FieldOfView)
            {
                Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, true);
                Owner.WorldState.SetWSProperty(E_PropKey.E_ATTACK_TARGET, true);
                Owner.BlackBoard.DesiredTarget = MyEnemy;
            }
        }
        else
        {
            if (Vector3.Angle(Owner.Forward, dirToEnemy) < 10)
                Owner.WorldState.SetWSProperty(E_PropKey.E_LOOKING_AT_TARGET, true);
        }

        float angleToEnemyForward = Vector3.Angle(dirToEnemy, MyEnemy.Forward);

        if (angleToEnemyForward > 135 && angleToEnemyForward < 225)
            Owner.WorldState.SetWSProperty(E_PropKey.E_AHEAD_OF_ENEMY, true);
        else if (angleToEnemyForward > 315 || angleToEnemyForward < 45)
            Owner.WorldState.SetWSProperty(E_PropKey.E_BEHIND_ENEMY, true);
    }
}
