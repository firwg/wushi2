using System;
using System.Collections.Generic;
using UnityEngine;

class GOAPActionOrderAttack : GOAPAction
{
    private AgentActionAttack Action;
    private Agent LastAttacketTarget = null;

    public GOAPActionOrderAttack(Agent owner) : base(E_GOAPAction.orderAttack, owner) { }


    public override void InitAction()
    {
        WorldPreconditions.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, true);
        WorldPreconditions.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, true);
        WorldEffects.SetWSProperty(E_PropKey.E_ATTACK_TARGET, true);

        Cost = 4;
    }


    public override void Activate()
    {
        base.Activate();
       // Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
        DoAttackAction();
    }

    public override void Deactivate()
    {
        
       // Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, false);
        LastAttacketTarget = null;

        base.Deactivate();
    }

    public override bool IsActionComplete()
    {
        if (Owner.WorldState.GetWSProperty(E_PropKey.E_ORDER).GetOrder() == AgentOrder.E_OrderType.E_ATTACK && Action.AttackPhaseDone)
        {// pokud uz ceka novy utok a je to pokracovani stavajiciho komba, tak skoncime o torchu drive !!
            if (Owner.BlackBoard.DesiredAttackPhase.FirstAttackInCombo == false)
            {
               //if (Owner.debugGOAP) Debug.Log(Time.timeSinceLevelLoad + " " + this.ToString() + " - IsActionComplete, next attack is waiting"); 
                return true;
            }
        }

        if (Action.IsActive() == false )
            return true;

        return false;
    }

    public override void Update()
    {
        /*WorldStateProp prop = Owner.WorldState.GetWSProperty(E_PropKey.E_ORDER);
        if (prop == null || prop.GetOrder() == AgentOrder.E_OrderType.E_ATTACK)
        {
            //pokud akce skoro skoncila a my uz mame informace o nove, tak vytvorime dalsi utok a poslem ho
            DoAttackAction();
        }*/
    }

    void DoAttackAction()
    {
        Action = AgentActionFactory.Create(AgentActionFactory.E_Type.E_ATTACK) as AgentActionAttack;
        Owner.BlackBoard.DesiredTarget = Action.Target = GetBestTarget(false);
        Action.AttackType = Owner.BlackBoard.DesiredAttackType;
        Action.AttackDir = Owner.BlackBoard.DesiredDirection;
        Action.Data = Owner.BlackBoard.DesiredAttackPhase;

        if (Action.Target != null)
        {
            if (Action.Target.BlackBoard.MotionType == E_MotionType.Knockdown)
            {
                Action.Data = Owner.AnimSet.GetFirstAttackAnim(Owner.BlackBoard.WeaponSelected, E_AttackType.Fatality);
                Action.AttackType = E_AttackType.Fatality;
            }
        }

        if (Action.Data.FullCombo || Action.AttackType == E_AttackType.Fatality)
            Owner.SoundPlayBerserk();
        else if (UnityEngine.Random.Range(0, 100) < 20)
            Owner.SoundPlayPrepareAttack();

        Action.Hit = false;
        Action.AttackPhaseDone = false;

        Owner.BlackBoard.ActionAdd(Action);

        if (Owner.WorldState.GetWSProperty(E_PropKey.E_ORDER).GetOrder() == AgentOrder.E_OrderType.E_ATTACK)
            Owner.WorldState.SetWSProperty(E_PropKey.E_ORDER, AgentOrder.E_OrderType.E_NONE);

        LastAttacketTarget = Action.Target;
    }


    public Agent GetBestTarget(bool hasToBeKnockdown)
    {
        if (Mission.Instance.CurrentGameZone == null)
            return null;

        List<Agent> enemies = Mission.Instance.CurrentGameZone.Enemies;

        float[] EnemyCoeficient = new float[enemies.Count];
        Agent enemy;
        Vector3 dirToEnemy;

        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyCoeficient[i] = 0;
            enemy = enemies[i];

            if (hasToBeKnockdown && enemy.BlackBoard.MotionType != E_MotionType.Knockdown)
                continue;

            if (enemy.BlackBoard.Invulnerable)
                continue;

            dirToEnemy = (enemy.Position - Owner.Position);

            float distance = dirToEnemy.magnitude;

            if (distance > 5.0f)
                continue;

            dirToEnemy.Normalize();

            float angle = Vector3.Angle(dirToEnemy, Owner.Forward);

            if (enemy == LastAttacketTarget)
                EnemyCoeficient[i] += 0.1f;

            //Debug.Log("LastTarget " + Mission.Instance.CurrentGameZone.GetEnemy(i).name + " : " + EnemyCoeficient[i]); 

            EnemyCoeficient[i] += 0.2f - ((angle / 180.0f) * 0.2f);

            //  Debug.Log("angle " + Mission.Instance.CurrentGameZone.GetEnemy(i).name + " : " + EnemyCoeficient[i]);

            angle = Vector3.Angle(dirToEnemy, Owner.BlackBoard.DesiredDirection);
            EnemyCoeficient[i] += 0.5f - ((angle / 180.0f) * 0.5f);
            //    Debug.Log(" joy " + Mission.Instance.CurrentGameZone.GetEnemy(i).name + " : " + EnemyCoeficient[i]); 

            EnemyCoeficient[i] += 0.2f - ((distance / 5) * 0.2f);

            //      Debug.Log(" dist " + Mission.Instance.CurrentGameZone.GetEnemy(i).name + " : " + EnemyCoeficient[i]); 
        }

        float bestValue = 0;
        int best = -1;
        for (int i = 0; i < enemies.Count; i++)
        {
            //     Debug.Log(Mission.Instance.CurrentGameZone.GetEnemy(i).name + " : " + EnemyCoeficient[i]); 
            if (EnemyCoeficient[i] <= bestValue)
                continue;

            best = i;
            bestValue = EnemyCoeficient[i];
        }

        if (best >= 0)
            return enemies[best];

        return null;
    }


}
