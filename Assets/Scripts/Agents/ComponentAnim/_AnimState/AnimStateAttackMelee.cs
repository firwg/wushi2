using UnityEngine;

public class AnimStateAttackMelee : AnimState
{
    enum E_State
    {
        E_PREPARING,
        E_ATTACKING,
        E_FINISHED,
    }


    AgentActionAttack Action;
    AnimAttackData AnimAttackData;

    Quaternion FinalRotation;
    Quaternion StartRotation;
    Vector3 StartPosition;
    Vector3 FinalPosition;
    float CurrentRotationTime;//当前转向所消耗的时间
    float RotationTime;//转向目标所需要消耗的总时间
    float MoveTime;//攻击的整个过程并非在原地进行，而是在攻击的过程中会产生位移，身体向前移动一段距离,
    float CurrentMoveTime;//当前身体位移的时间，用于计算当前应该偏移的位移量。

    float EndOfStateTime;//状态，结束的时间戳

    float HitTime;//伤害结算的时间戳

    float AttackPhaseTime;//连击攻击阶段结束的时间戳

    bool RotationOk = false;//标志是否转向完毕
    bool PositionOK = false;//表示是否
   // bool MovingToAttackPos;

    bool Critical = false;
    bool Knockdown = false;

    E_State State;

	public AnimStateAttackMelee(Animation anims, Agent owner)
		: base(anims, owner)
	{

	}

    override public void OnActivate(AgentAction action)
    {
        /*if(Owner.IsPlayer == false)
            Time.timeScale = 0.2f;*/
        base.OnActivate(action);
    }


    //FSM每一帧检测当前状态是否完成，IsFinished=true时的时候调用OnDeactivate，
    override public void OnDeactivate()
    {
        Action.SetSuccess();
        Action = null;
        
        base.OnDeactivate();
    }


    override public bool HandleNewAction(AgentAction action)
    {
		 if (action as AgentActionAttack != null)
		 {
             if (Action != null)
             {Action.AttackPhaseDone = true;
                 Action.SetSuccess();
             }

             Initialize(action);
			 return true;
		 }
		 return false;
    }

    override public void Update()
    {
        if (State == E_State.E_PREPARING)
        {
            Debug.Log("State == E_State.E_PREPARING");
            //在准备阶段设置位置 和旋转角度
            bool dontMove = false;
            if (RotationOk == false)
            {
                //Debug.Log("rotate");
                CurrentRotationTime += Time.deltaTime;

                if (CurrentRotationTime >= RotationTime)
                {
                    CurrentRotationTime = RotationTime;
                    RotationOk = true;
                }

                float progress = CurrentRotationTime / RotationTime;
                Quaternion q = Quaternion.Lerp(StartRotation, FinalRotation, progress);
                Owner.Transform.rotation = q;

                if (Quaternion.Angle(q, FinalRotation) > 20.0f)
                    dontMove = true;
            }

            if (dontMove == false && PositionOK == false)
            {
                CurrentMoveTime += Time.deltaTime;
                if (CurrentMoveTime >= MoveTime)
                {
                    CurrentMoveTime = MoveTime;
                    PositionOK = true;
                }

                if (CurrentMoveTime > 0)
                {
                    float progress = CurrentMoveTime / MoveTime;
                    Vector3 finalPos = Mathfx.Hermite(StartPosition, FinalPosition, progress);
                    //if (MoveToCollideWithEnemy(finalPos, Transform.forward) == false)
                    if (Move(finalPos - Transform.position) == false)
                    {
                        PositionOK = true;
                    }
                }
            }

            if (RotationOk && PositionOK)
            {
                State = E_State.E_ATTACKING;
                PlayAnim();
            }
        }
        else if (State == E_State.E_ATTACKING)
        {
            //开始移动的时间戳更新
            CurrentMoveTime += Time.deltaTime;

            //检测是否完成攻击：随着timeSinceLevelLoad超过AttackPhaseTime的时候，标志着攻击流程的完成，进入下一个攻击状态
            if (AttackPhaseTime < Time.timeSinceLevelLoad)
            {
                //Debug.Log(Time.timeSinceLevelLoad + " attack phase done");
                Action.AttackPhaseDone = true;
                State = E_State.E_FINISHED;
            }

            if (CurrentMoveTime >= MoveTime)
               CurrentMoveTime = MoveTime;

            //位移移动
            if (CurrentMoveTime > 0 && CurrentMoveTime <= MoveTime)
            {
                float progress = Mathf.Min(1.0f, CurrentMoveTime / MoveTime);
                Vector3 finalPos = Mathfx.Hermite(StartPosition, FinalPosition, progress);
                //if (MoveToCollideWithEnemy(finalPos, Transform.forward) == false)
                if (Move(finalPos - Transform.position, false) == false)
                {
                    CurrentMoveTime = MoveTime;
                }

               // Debug.Log(Time.timeSinceLevelLoad + " moving");
            }

            //当没有进行攻击结算，并且时间到了的时候 进行攻击结算
            if(Action.Hit == false && HitTime <= Time.timeSinceLevelLoad)
            {
                Action.Hit = true;

                if(Owner.IsPlayer && AnimAttackData.FullCombo)
                GuiManager.Instance.ShowComboMessage(AnimAttackData.ComboIndex);

                Mission.Instance.CurrentGameZone.BreakBreakableObjects(Owner);
                
                if(Action.AttackType == E_AttackType.Fatality)
                    Mission.Instance.CurrentGameZone.DoDamageFatality(Owner, Action.Target, Owner.BlackBoard.WeaponSelected, AnimAttackData);
                else 
                    Mission.Instance.CurrentGameZone.DoMeleeDamage(Owner, Action.Target, Owner.BlackBoard.WeaponSelected, AnimAttackData, Critical, Knockdown);



                if (AnimAttackData.LastAttackInCombo || AnimAttackData.ComboStep == 3 )
                    CameraBehaviour.Instance.ComboShake(AnimAttackData.ComboStep - 3);

                if (AnimAttackData.LastAttackInCombo)
                    Owner.StartCoroutine(ShowTrail(AnimAttackData, 1, 0.3f, Critical, MoveTime - Time.timeSinceLevelLoad));
                else
                    Owner.StartCoroutine(ShowTrail(AnimAttackData, 2, 0.1f, Critical, MoveTime - Time.timeSinceLevelLoad));

                //Debug.Log("DoMeleeDamage  " + (Action.AttackTarget != null ? Action.AttackTarget.name : "no target"));
            }
        }
        else if (State == E_State.E_FINISHED && EndOfStateTime <= Time.timeSinceLevelLoad)
        {
            Debug.Log("State == E_State.E_FINISHED");

            Action.AttackPhaseDone = true;
            //Debug.Log(Time.timeSinceLevelLoad + " attack finished");
            Release();
        }
    }

    private void PlayAnim()
    {
        //切换动画
        CrossFade(AnimAttackData.AnimName, 0.2f);

        // 计算 攻击结算时间
        HitTime = Time.timeSinceLevelLoad + AnimAttackData.HitTime;

        //记录起始位置和最终应该到达位置
        StartPosition = Transform.position;
        FinalPosition = StartPosition + Transform.forward * AnimAttackData.MoveDistance;

        //计算状态中 位移移动的时间总长度 以及 标记移动的时间戳（负数，0开始移动）
        MoveTime = AnimAttackData.AttackMoveEndTime - AnimAttackData.AttackMoveStartTime;
        CurrentMoveTime = -AnimAttackData.AttackMoveStartTime; // move a little bit later


        //状态结束的时间戳
        EndOfStateTime = Time.timeSinceLevelLoad + AnimEngine[AnimAttackData.AnimName].length * 0.9f;

        //连招时间戳的计算
        if (AnimAttackData.LastAttackInCombo)
            AttackPhaseTime = Time.timeSinceLevelLoad + AnimEngine[AnimAttackData.AnimName].length * 0.9f;
        else
            AttackPhaseTime = Time.timeSinceLevelLoad + AnimAttackData.AttackEndTime;

        //镜头动画的计算
        if (Action.Target && Action.Target.IsAlive)
        {
            if(Critical)
            {
                CameraBehaviour.Instance.InterpolateTimeScale(0.25f, 0.5f);
                CameraBehaviour.Instance.InterpolateFov(25, 0.5f);
                CameraBehaviour.Instance.Invoke("InterpolateScaleFovBack", 0.7f);
            }
            else if (Action.AttackType == E_AttackType.Fatality)
            {
                CameraBehaviour.Instance.InterpolateTimeScale(0.25f, 0.7f);
                CameraBehaviour.Instance.InterpolateFov(25, 0.65f);
                CameraBehaviour.Instance.Invoke("InterpolateScaleFovBack", 0.8f);
            }
        }

    }





    //状态初始化，为攻击中状态的更新做好数据的准备，目标等
    override protected void Initialize(AgentAction action)
    {
        base.Initialize(action);
        SetFinished(false);

        State = E_State.E_PREPARING;
        Owner.BlackBoard.MotionType = E_MotionType.Attack;

        Action = action as AgentActionAttack;
        Action.AttackPhaseDone = false;
        Action.Hit = false;

        if (Action.Data == null)
            Action.Data = Owner.AnimSet.GetFirstAttackAnim(Owner.BlackBoard.WeaponSelected, Action.AttackType);

        AnimAttackData = Action.Data;

        if (AnimAttackData == null)
            Debug.LogError("AnimAttackData == null");

        StartRotation = Transform.rotation;
        Debug.Log("StartRotation=" + StartRotation);

        StartPosition = Transform.position;

        float angle = 0;

        bool backstab = false;//是否敌人是否背对着自己

        float distance = 0;


        //如果有目标
        if (Action.Target != null)
        {

            //计算与目标之间的直线距离
            Vector3 dir = Action.Target.Position - Transform.position;
            distance = dir.magnitude;

            if (distance > 0.1f)
            {
                dir.Normalize();
                angle = Vector3.Angle(Transform.forward, dir);

                //attacker uhel k cili je mensi nez 40 and uhel enemace a utocnika je mensi nez 80 stupnu
                if(angle < 40 && Vector3.Angle(Owner.Forward, Action.Target.Forward) < 80)
                    backstab = true;
            }
            else
            {
                dir = Transform.forward;
            }

            //设置最终的转向
            FinalRotation.SetLookRotation(dir);



            if (distance < Owner.BlackBoard.WeaponRange)
                FinalPosition = StartPosition;
            else
                FinalPosition = Action.Target.Transform.position - dir * Owner.BlackBoard.WeaponRange;
   
            MoveTime = (FinalPosition - StartPosition).magnitude / 20.0f;
            RotationTime = angle / 720.0f;
        }
        else
        {
            FinalRotation.SetLookRotation(Action.AttackDir);
            Debug.Log("FinalRotation=" + FinalRotation);

            RotationTime = Vector3.Angle(Transform.forward, Action.AttackDir) / 720.0f;
            MoveTime = 0;
        }



        RotationOk = RotationTime == 0;
        PositionOK = MoveTime == 0;


        CurrentRotationTime = 0;
        CurrentMoveTime = 0;

        if (Owner.IsPlayer && AnimAttackData.HitCriticalType != E_CriticalHitType.None && Action.Target && Action.Target.BlackBoard.CriticalAllowed && Action.Target.IsBlocking == false && Action.Target.IsInvulnerable == false && Action.Target.IsKnockedDown == false)
        {
            if (backstab)
                Critical = true;
            else
            {
     //           Debug.Log("critical chance" + Owner.GetCriticalChance() * AnimAttackData.CriticalModificator * Action.Target.BlackBoard.CriticalHitModifier);
                Critical = Random.Range(0, 100) < Owner.GetCriticalChance() * AnimAttackData.CriticalModificator * Action.Target.BlackBoard.CriticalHitModifier;
            }
        }
        else
            Critical = false;

        Knockdown = AnimAttackData.HitAreaKnockdown && Random.Range(0, 100) < 60 * Owner.GetCriticalChance();
    }
}
