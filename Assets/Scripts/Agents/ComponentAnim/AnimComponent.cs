using UnityEngine;
using System.Collections;


public enum E_AnimFSMTypes
{
    Player,
    Archer,
    Swordsman,
    Peasant,
    DoubleSwordsman,
    Musketeer,
    MiniBoss01,
    MiniBoss02,
    Boss01,
    Boss02,
    Boss03,
    BossOrochi,
}
/*
public class AnimsData
{
	protected static string[][] Data = new string[(int)E_AnimTypes.E_E_COUNT][];
	static public string GetAnim(int i, int l) {return Data[i][l];}
}


public class PlayerAnims : AnimsData
{
	static PlayerAnims()
	{
		Data[(int)E_AnimTypes.E_STAND][0] = "test";
		Data[(int)E_AnimTypes.E_STAND][0] = "test2";
	}
}
*/

public class AnimComponent : MonoBehaviour, IActionHandler
{
	private AnimFSM FSM;
	//private Transform OwnerTransform;
	private Animation Animation;
	private Agent Owner;

	Vector3 RootPosition;

    public E_AnimFSMTypes TypeOfFSM;

	public void Awake()
	{
		Owner = GetComponent("Agent") as Agent;

		Animation = GetComponent<Animation>();
		//OwnerTransform = transform;

        switch (TypeOfFSM)
        {
            case E_AnimFSMTypes.Player:
                FSM = new AnimFSMPlayer(Animation, Owner);
                break;
            case E_AnimFSMTypes.Archer:
                FSM = new AnimFSMEnemyArcher(Animation, Owner);
                break;
            case E_AnimFSMTypes.Swordsman:
                FSM = new AnimFSMEnemySwordsman(Animation, Owner);
                break;
            case  E_AnimFSMTypes.Peasant:
                FSM = new AnimFSMEnemyPeasant(Animation, Owner);
                break;
            case E_AnimFSMTypes.MiniBoss01:
                FSM = new AnimFSMEnemyMiniBoss(Animation, Owner);
                break;
            case E_AnimFSMTypes.MiniBoss02:
                FSM = new AnimFSMEnemyMiniBoss(Animation, Owner);
                break;
            case E_AnimFSMTypes.DoubleSwordsman:
                FSM = new AnimFSMEnemyDoubleSwordsman(Animation, Owner);
                break;
            case E_AnimFSMTypes.Musketeer:
                FSM = new AnimFSMEnemyDoubleSwordsman(Animation, Owner);
                break;
            case E_AnimFSMTypes.Boss01:
                FSM = new AnimFSMEnemyBoss01(Animation, Owner);
                break;
            case E_AnimFSMTypes.Boss02:
                FSM = new AnimFSMEnemyBoss02(Animation, Owner);
                break;
            case E_AnimFSMTypes.Boss03:
                FSM = new AnimFSMEnemyBoss03(Animation, Owner);
                break;
            case E_AnimFSMTypes.BossOrochi:
                FSM = new AnimFSMEnemyBossOrochi(Animation, Owner);
                break;
            default:
                Debug.LogError(this.name + " unkown type of FSM");
                break;
        }
	}

	// Use this for initialization
	void Start()
	{
        FSM.Initialize();
		Owner.BlackBoard.ActionHandlerAdd(this);
	}

	// Update is called once per frame
    // Update is called once per frame
    void Update()
    {
  //      RootPosition = RootTransform.localPosition;
        FSM.UpdateAnimStates();
    }

 //   Vector3 localroot_lastframe;

    public void LateUpdate()
    {
       // Vector3 diff = RootTransform.localPosition - localroot_lastframe;
        //localroot_lastframe = RootTransform.localPosition;

    //    RootPosition = Vector3.zero;
      //  RootPosition.y = RootTransform.localPosition.y;
//        RootTransform.localPosition = RootPosition;

        //Vector3 fixPos = OwnerTransform.position + diff;
        //OwnerTransform.position = fixPos;
    }

	void UpdateRotation()
	{
		//        MovementData.Direction = OwnerTransform.forward;
	}

	public void HandleAction(AgentAction action)
	{
		if (action.IsFailed())
			return;

		FSM.DoAction(action);
	}



	public void Activate(Transform spawnTransform)
	{
        Animation.Stop();
        Animation.Rewind();
        FSM.Initialize();
	}

    public void Deactivate()
    {
        FSM.Reset();
    }
}




