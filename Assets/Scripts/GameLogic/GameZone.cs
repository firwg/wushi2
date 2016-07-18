using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(BoxCollider))]
public class GameZone : GameZoneBaze
{
    public enum E_State
    {
        E_WAITING_FOR_START,
        E_IN_PROGRESS,
        E_ACCOMPLISHED,
        E_FINISHED,
    }
    private E_State State = E_State.E_WAITING_FOR_START;

    public SpawnZone[] SpawnZones = null;
    public InteractionGameObject[] InteractionObjects = null;

    public BreakableObject[] BreakableObjects = null;

    public LoadNextLevel LoadNextLevel;

	void Awake()
	{
        GameObject = gameObject;
	}

    void Start()
    {
        if (LoadNextLevel)
            LoadNextLevel.gameObject.SetActiveRecursively(false);
    }

    public override void Enable()
    {
        base.Enable();

        State = E_State.E_WAITING_FOR_START;

        for (int i = 0; i < SpawnZones.Length; i++)
            SpawnZones[i].Enable();
    }

    public override void SetInProgress()
    {
        base.SetInProgress();

        State = E_State.E_IN_PROGRESS;
    }

    // We'll draw a gizmo in the scene view, so it can be found....
    void OnDrawGizmos()
    {
        BoxCollider b = GetComponent("BoxCollider") as BoxCollider;
        if(b != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(b.transform.position + b.center, b.size );
        }
    }

	// Update is called once per frame
	void FixedUpdate()
	{
        for(int i = 0; i < Enemies.Count;i++)
        {
            if (Enemies[i].IsAlive == false)
            {
                //Debug.Log(Time.timeSinceLevelLoad + " remove enemy " + Enemies[i].gameObject.name );
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(State == E_State.E_IN_PROGRESS)
        {
            bool finished = true;
            for (int i = 0; i < SpawnZones.Length; i++)
            {
                if (SpawnZones[i].State == SpawnZone.E_State.E_FINISHED)
                    continue;

                finished = false;
            }
            if(finished)
            {
                //Debug.Log(Time.timeSinceLevelLoad + " game zone finished");

                State = E_State.E_ACCOMPLISHED;


                if (LoadNextLevel)
                {
                    LoadNextLevel.gameObject.SetActiveRecursively(true);
                }
                else
                {
                    Mission.Instance.UnlockNextGameZone();
                    Player.Instance.HealToFullHealth();
                }
            }
        }
	}
    

	public override void Reset()
	{
        base.Reset();

        StopAllCoroutines();

        if (LoadNextLevel)
            LoadNextLevel.gameObject.SetActiveRecursively(false);

        if (SpawnZones != null)
        {
            for (int i = SpawnZones.Length - 1; i >= 0; i--)
                SpawnZones[i].Restart();
        }


        if(InteractionObjects != null)
        {
            for (int i = InteractionObjects.Length - 1; i >= 0; i--)
                InteractionObjects[i].Restart();
        }

        if(BreakableObjects != null)
        {
            for (int i = BreakableObjects.Length - 1; i >= 0; i--)
                BreakableObjects[i].Restart();
        }
	}

    public override void BreakBreakableObjects(Agent attacker)
    {
        if(attacker.IsPlayer == false)
            return;

        BreakableObject bo = null;
        Vector3 dir;

        for (int i = 0; i < BreakableObjects.Length; i++)
        {
            bo = BreakableObjects[i];

            if (bo.IsActive == false || bo.enabled == false)
                continue;

            dir = bo.Position - attacker.Position;

            if (dir.sqrMagnitude > attacker.BlackBoard.sqrWeaponRange)
                continue;

            bo.Break();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (State != E_State.E_WAITING_FOR_START || other != Player.Instance.Agent.CharacterController)
            return;

        //Debug.Log(GameObject.name + " " + Time.timeSinceLevelLoad + " OnTriggerEnter");

        Mission.Instance.CurrentGameZone = this;

        Game.Instance.Save_Save();

        Game.Instance.SaveStatistics();

        Mission.Instance.LockPrevGameZone();
    }
    
    void OnTriggerExit(Collider other)
    {
        if (SpawnZones == null || SpawnZones.Length == 0)
        {
            Mission.Instance.UnlockNextGameZone();
            Player.Instance.HealToFullHealth();
        }
       // Mission.Instance.NewZoneDeactive(this);
    }

    public void EnableAllActiveInteraction(bool enable)
    {
        for (int i = InteractionObjects.Length - 1; i >= 0; i--)
        {
            InteractionGameObject o = InteractionObjects[i];
            if (o.IsActive == false)
                continue;

            o.Enable(enable);
        }
    }

    public override InteractionGameObject GetNearestInteractionObject(Vector3 center, float maxLen)
    {

        float len;
        float nearestLen = maxLen * maxLen;
        InteractionGameObject best = null;
        for (int i = InteractionObjects.Length - 1; i >= 0; i--)
        {
            InteractionGameObject o = InteractionObjects[i];
            if (o.IsActive == false || o.IsEnabled == false || (o is InteractionLever) == false)
                continue;

            len = (o.Position - center).sqrMagnitude;
            if (len < nearestLen)
            {
                nearestLen = len;
                best = o;
            }
        }

        return best;
    }

    public override bool IsInteractionObjectInRange(Vector3 center, float maxLen)
    {
        float nearestLen = maxLen * maxLen;
        for (int i = InteractionObjects.Length - 1; i >= 0; i--)
        {
            InteractionGameObject o = InteractionObjects[i];
            if (o.IsActive == false || o.IsEnabled == false || (o is InteractionLever) == false)
                continue;

            if ((o.Position - center).sqrMagnitude < nearestLen)
                return true;
        }

        return false;
    }
}

