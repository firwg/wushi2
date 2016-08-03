using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public static class Player
{
    public static ComponentPlayer Instance;

}

[RequireComponent(typeof(Agent))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(AnimSetPlayer))]
[RequireComponent(typeof(CameraOffsetBehaviour))]
[RequireComponent(typeof(AnimComponent))]



//基础属性设置，处理玩家操作
public class ComponentPlayer : MonoBehaviour, IActionHandler
{
	enum E_TouchCommandType
	{
		E_TC_UNKNOWN,
		E_TC_TAP,
		E_TC_DOUBLE_TAP,
		E_TC_LEFT_TO_RIGHT,
		E_TC_RIGHT_TO_LEFT,
		E_TC_UP,
		E_TC_DOWN,
		E_TC_TOUCH_START,
		E_TC_MOVING,
	}

    #region 基础属性设置
    private Agent Owner;
    public Agent Agent { get { return Owner; } }

    private Transform Transform;
    private float StepTime;
    private int Experience;
    private AnimSetPlayer AnimSet;
    private bool UseMode = false;
    public bool InUseMode { get { return UseMode; } }
    #endregion

    #region 玩家操作
    public class ComboStep
    {
        public E_AttackType AttackType;
        public E_ComboLevel ComboLevel;
        public AnimAttackData Data;
    }
    public class Combo
    {
        public E_SwordLevel SwordLevel;
        public ComboStep[] ComboSteps;
    }

    public Combo[] PlayerComboAttacks = new Combo[6];
    private List<E_AttackType> ComboProgress = new List<E_AttackType>();
    private Queue<AgentOrder> BufferedOrders = new Queue<AgentOrder>();//玩家操作的暂存堆栈
    private Agent LastAttacketTarget;
    //private PlayerControls Controls = new PlayerControls();
    private AgentActionAttack CurrentAttackAction;

    private Vector3 InputDirection;

    #endregion

   

    void Awake()
    {
        Player.Instance = this;
        Transform = transform;
        Owner = GetComponent<Agent>();
        AnimSet = GetComponent<AnimSetPlayer>();
    }


	// Use this for initialization
	void Start()
<<<<<<< HEAD
	{
        #region PlayerComboAttacks
=======
    {
        #region PlayerComboAttacks

>>>>>>> 5a46e3dca1bc9fcea4754e8f64d30d15576038a5
        PlayerComboAttacks[0] = new Combo() // FAST   Raisin Wave
        {
            SwordLevel = E_SwordLevel.One,
            ComboSteps = new ComboStep[]{new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[0]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[1]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[2]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Two, Data = AnimSet.AttackData[3]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.Three, Data = AnimSet.AttackData[4]},
            }
        };
        PlayerComboAttacks[1] = new Combo() // BREAK BLOCK  half moon
        {
            SwordLevel = E_SwordLevel.One,
            ComboSteps = new ComboStep[]{new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[5]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[6]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[7]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Two, Data = AnimSet.AttackData[8]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Three, Data = AnimSet.AttackData[9]},
            }
        };
        PlayerComboAttacks[2] = new Combo() // CRITICAL  cloud cuttin
        {
            SwordLevel = E_SwordLevel.Two,
            ComboSteps = new ComboStep[]{new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[5]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[6]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[17]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Two, Data = AnimSet.AttackData[18]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Three, Data = AnimSet.AttackData[19]},
            }
        };
        PlayerComboAttacks[3] = new Combo()  // flying dragon
        {
            SwordLevel = E_SwordLevel.Three,
            ComboSteps = new ComboStep[]{new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[0]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[10]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[11]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Two, Data = AnimSet.AttackData[12]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Three, Data = AnimSet.AttackData[13]},
            }
        };
        PlayerComboAttacks[4] = new Combo() // KNCOK //walking death
        {
            SwordLevel = E_SwordLevel.Four,
            ComboSteps = new ComboStep[]{new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[0]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[1]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[14]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Two, Data = AnimSet.AttackData[15]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.Three, Data = AnimSet.AttackData[16]},
            }
        };
        PlayerComboAttacks[5] = new Combo() // HEAVY, AREA  shogun death
        {
            SwordLevel = E_SwordLevel.Five,
            ComboSteps = new ComboStep[]{new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[5]},
                                         new ComboStep(){AttackType = E_AttackType.X, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[20]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.One, Data = AnimSet.AttackData[21]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.Two, Data = AnimSet.AttackData[22]},
                                         new ComboStep(){AttackType = E_AttackType.O, ComboLevel = E_ComboLevel.Three, Data = AnimSet.AttackData[23]},
            }
        };
        #endregion

        #endregion

        Owner.BlackBoard.IsPlayer = true;
        Owner.BlackBoard.Rage = 0;
        Owner.BlackBoard.Dodge = 0;
        Owner.BlackBoard.Fear = 0;


		//InputComponent.Instance.AddReceiver(this);

        SpriteEffectsManager.Instance.CreateShadow(Transform.FindChild("root").gameObject, 1.3f, 1.3f);

        #region GOAPAction
        Agent.AddGOAPAction(E_GOAPAction.gotoPos);
        Agent.AddGOAPAction(E_GOAPAction.move);
        Agent.AddGOAPAction(E_GOAPAction.gotoMeleeRange);
        Agent.AddGOAPAction(E_GOAPAction.weaponShow);
        Agent.AddGOAPAction(E_GOAPAction.weaponHide);
        Agent.AddGOAPAction(E_GOAPAction.orderAttack);
        //Agent.AddGOAPAction(E_GOAPAction.orderAttackJump);
        Agent.AddGOAPAction(E_GOAPAction.orderDodge);
        Agent.AddGOAPAction(E_GOAPAction.rollToTarget);
        Agent.AddGOAPAction(E_GOAPAction.useLever);
        Agent.AddGOAPAction(E_GOAPAction.playAnim);
        Agent.AddGOAPAction(E_GOAPAction.teleport);
        Agent.AddGOAPAction(E_GOAPAction.injury);
        Agent.AddGOAPAction(E_GOAPAction.death);

        Agent.AddGOAPGoal(E_GOAPGoals.E_GOTO);
        Agent.AddGOAPGoal(E_GOAPGoals.E_ORDER_ATTACK);
        Agent.AddGOAPGoal(E_GOAPGoals.E_ORDER_DODGE);
        Agent.AddGOAPGoal(E_GOAPGoals.E_ORDER_USE);
        Agent.AddGOAPGoal(E_GOAPGoals.E_ALERT);
        Agent.AddGOAPGoal(E_GOAPGoals.E_CALM);
        Agent.AddGOAPGoal(E_GOAPGoals.E_USE_WORLD_OBJECT);
        Agent.AddGOAPGoal(E_GOAPGoals.E_PLAY_ANIM);
        Agent.AddGOAPGoal(E_GOAPGoals.E_TELEPORT);
        Agent.AddGOAPGoal(E_GOAPGoals.E_REACT_TO_DAMAGE);

        Agent.InitializeGOAP();

        Owner.BlackBoard.ActionHandlerAdd(this);
        #endregion

        //Controls.Start();
	}

    void Activate(Transform t)
    {
        LastAttacketTarget = null;
        //LastTapTime = 0;
        StepTime = 0;

        Owner.BlackBoard.Reset();

        #region WorldState
        Owner.WorldState.SetWSProperty(E_PropKey.E_ORDER, AgentOrder.E_OrderType.E_NONE);

        Owner.WorldState.SetWSProperty(E_PropKey.E_IDLING, true);
        Owner.WorldState.SetWSProperty(E_PropKey.E_AT_TARGET_POS, true);
        Owner.WorldState.SetWSProperty(E_PropKey.E_ATTACK_TARGET, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_LOOKING_AT_TARGET, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_USE_WORLD_OBJECT, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_PLAY_ANIM, false);

        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_DODGE, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_WEAPONS_RANGE, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_WEAPON_IN_HANDS, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_BLOCK, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_IN_COMBAT_RANGE, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_AHEAD_OF_ENEMY, false);
        Owner.WorldState.SetWSProperty(E_PropKey.E_BEHIND_ENEMY, false);
        Owner.WorldState.SetWSProperty(E_PropKey.MoveToRight, false);
        Owner.WorldState.SetWSProperty(E_PropKey.MoveToLeft, false);

        Owner.WorldState.SetWSProperty(E_PropKey.E_EVENT, E_EventTypes.None);
        #endregion

        ComboProgress.Clear();
        ClearBufferedOrder();

        //Controls.SwitchToCombatMode();

        GuiManager.Instance.ShowComboProgress(ComboProgress);
    }

    void Deactivate()
    {
        ClearBufferedOrder();
    }

    void Update()
    {
        if (Owner.BlackBoard.Stop)
        {
            LastAttacketTarget = null;
            ComboProgress.Clear();
            ClearBufferedOrder();
            CreateOrderStop();
            //Controls.Update();
            return;
        }

        if (BufferedOrders.Count > 0)
        {
            if(CouldAddnewOrder())
                Owner.BlackBoard.OrderAdd(BufferedOrders.Dequeue());
        }

        //Controls.Update();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //Controls.Joystick.Direction=new Vector3(h,0,v);
        //Controls.Joystick.Direction.Normalize();

        InputDirection = new Vector3(h, 0, v);
        InputDirection.Normalize();
        if (Input.GetKeyDown(KeyCode.K))
        {
            //CreateWeaponShow();
            CreateOrderAttack(E_AttackType.O);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //CreateWeaponShow();
            CreateOrderDodge();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //CreateWeaponShow();
            CreateOrderAttack(E_AttackType.X);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //CreateWeaponShow();
            CreateOrderUse();
        }


        #region  输入
        //if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.InGameMenu].Status == PlayerControls.E_ButtonStatus.UpFirst)
        //{
        //    GuiManager.Instance.PlayButtonSound();
        //    GuiManager.Instance.ShowIngameMenu();
        //}
        //else if (Agent.IsAlive == false)
        //{
        //    //no update for buttons when death
        //}
        //else if (Controls.Buttons[0].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //{
        //   // Debug.Log(Time.timeSinceLevelLoad + " Button X");

        //    CreateOrderAttack(E_AttackType.X);
        //}
        //else if (Controls.Buttons[1].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //{
        //    CreateOrderAttack(E_AttackType.O);
        //}
        //else if (Controls.Buttons[2].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //{
        //    CreateOrderDodge();
        //}
        //else if (Controls.Buttons[3].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //{
        //    CreateOrderUse();
        //}
        //else if (Controls.Buttons[4].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //{
        //    /*if (iPhoneUtils.isApplicationGenuine == false && iPhoneUtils.isApplicationGenuineAvailable == true)
        //    {
        //    }
        //    else*/
        //    {
        //        GuiManager.Instance.ShowShop();
        //        Controls.SwitchToShopMode();
        //    }
        //}
        //else if (Game.Instance.GameState == E_GameState.Shop)
        //{

        //    if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopOk].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        GuiManager.Instance.HideShop(true);
        //        Controls.SwitchToCombatMode();
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCancel].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        GuiManager.Instance.HideShop(false);
        //        Controls.SwitchToCombatMode();
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopHealth].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        GuiManager.Instance.ShowShopInfo(Game.Instance.CouldBuyHealthLevel(), 0);
        //        Controls.SwitchToShopBuyMode(Game.Instance.CouldBuyHealthLevel());
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopSword].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        GuiManager.Instance.ShowShopInfo(Game.Instance.CouldBuySwordLevel(), 1);
        //        Controls.SwitchToShopBuyMode(Game.Instance.CouldBuySwordLevel());
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCombo1].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        if (PlayerComboAttacks[0].SwordLevel <= Game.Instance.SwordLevel)
        //        {
        //            GuiManager.Instance.ShowShopInfo(Game.Instance.CouldBuyComboLevel(0), 2);
        //            Controls.SwitchToShopBuyMode(Game.Instance.CouldBuyComboLevel(0));
        //        }
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCombo2].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        if (PlayerComboAttacks[1].SwordLevel <= Game.Instance.SwordLevel)
        //        {
        //            GuiManager.Instance.ShowShopInfo(Game.Instance.CouldBuyComboLevel(1), 3);
        //            Controls.SwitchToShopBuyMode(Game.Instance.CouldBuyComboLevel(1));
        //        }
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCombo3].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        if (PlayerComboAttacks[2].SwordLevel <= Game.Instance.SwordLevel)
        //        {
        //            GuiManager.Instance.ShowShopInfo(Game.Instance.CouldBuyComboLevel(2), 4);
        //            Controls.SwitchToShopBuyMode(Game.Instance.CouldBuyComboLevel(2));
        //        }
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCombo4].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        if (PlayerComboAttacks[3].SwordLevel <= Game.Instance.SwordLevel)
        //        {
        //            GuiManager.Instance.ShowShopInfo(Game.Instance.CouldBuyComboLevel(3), 5);
        //            Controls.SwitchToShopBuyMode(Game.Instance.CouldBuyComboLevel(3));
        //        }
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCombo5].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        if (PlayerComboAttacks[4].SwordLevel <= Game.Instance.SwordLevel)
        //        {
        //            GuiManager.Instance.ShowShopInfo(Game.Instance.CouldBuyComboLevel(4), 6);
        //            Controls.SwitchToShopBuyMode(Game.Instance.CouldBuyComboLevel(4));
        //        }
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCombo6].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        //            Debug.Log("combo6 pressed ON " + Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCombo6].On + Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopCombo5].On);
        //        if (PlayerComboAttacks[5].SwordLevel <= Game.Instance.SwordLevel)
        //        {
        //            GuiManager.Instance.ShowShopInfo(Game.Instance.CouldBuyComboLevel(5), 7);
        //            Controls.SwitchToShopBuyMode(Game.Instance.CouldBuyComboLevel(5));
        //        }
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopInfoBuy].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        GuiManager.Instance.HideShopInfo(true);
        //        Controls.SwitchToShopMode();
        //    }
        //    else if (Controls.Buttons[(int)PlayerControls.E_ButtonsName.ShopInfoBack].Status == PlayerControls.E_ButtonStatus.DownFirst)
        //    {
        //        GuiManager.Instance.PlayButtonSound();
        //        GuiManager.Instance.HideShopInfo(false);
        //        Controls.SwitchToShopMode();
        //    }
        //}
        #endregion

        if (InputDirection !=Vector3.zero)//Controls.Joystick.Direction != Vector3.zero)
        {
            //Debug.DrawLine(Agent.Position + Vector3.up, Agent.Position + Vector3.up + Controls.Joystick.Direction * Controls.Joystick.Force * 4);
            CreateOrderGoTo();
        }
        else if(Agent.CurrentGOAPGoal != null && Agent.CurrentGOAPGoal.GoalType == E_GOAPGoals.E_GOTO)
        {
            CreateOrderStop();
        }

        GuiManager.Instance.SetHealthPercent(Agent.BlackBoard.Health, Agent.BlackBoard.RealMaxHealth);
 
    }






    void LateUpdate()
    {
        if (StepTime < Time.timeSinceLevelLoad)
        {
            if (Owner.BlackBoard.MotionType == E_MotionType.Run)
            {
                Owner.SoundPlayStep();
                StepTime = Time.timeSinceLevelLoad + Random.Range(0.25f, 0.28f);
            }
             else if (Owner.BlackBoard.MotionType == E_MotionType.Walk)
             {
                 Owner.SoundPlayStep();
                StepTime = Time.timeSinceLevelLoad + Random.Range(0.40f, 0.43f);
            }
        }

        if (CurrentAttackAction != null && CurrentAttackAction.IsActive() == false)
        {// no continue in combos !!!
            if (BufferedOrders.Count == 0 && Owner.WorldState.GetWSProperty(E_PropKey.E_ORDER).GetOrder() != AgentOrder.E_OrderType.E_ATTACK)
            {
                //Debug.Log("clear combo progress " + CurrentAttackAction.Data.AnimName);
                ComboProgress.Clear();
                GuiManager.Instance.ShowComboProgress(ComboProgress);
            }
            CurrentAttackAction = null;
        }
    }

    void FixedUpdate()
    {
        bool old = UseMode;
        UseMode = Mission.Instance.CurrentGameZone.IsInteractionObjectInRange(Owner.Position, 2);

        if (UseMode != old)
        {
            if (UseMode)
            {
                GuiManager.Instance.SwitchToUseMode();
                //Controls.SwitchToUseMode();
            }
            else
            {
                GuiManager.Instance.SwitchToCombatMode();
                //Controls.SwitchToCombatMode();
            }
        }
    }

    public void UpdateControlsPosition()
    {
       // Controls.UpdateControlsPosition();
    }

    void OnTriggerEnter(Collider other)
    {
        InteractionTrigger interaction = other.GetComponent<InteractionTrigger>();
        if (interaction != null)
        {
            AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_USE);
            order.InteractionObject = interaction;
            order.Position = order.InteractionObject.GetEntryTransform().position;
            order.Interaction = E_InteractionType.On;
            Owner.BlackBoard.OrderAdd(order);
            return;
        }
    }

    public void HandleAction(AgentAction a)
    {
        //Debug.Log("ComponentPlayer.HandleAction("+a);
        if (a is AgentActionAttack)
        {
            CurrentAttackAction = a as AgentActionAttack;
            Owner.WorldState.SetWSProperty(E_PropKey.E_ALERTED, true);
        }
        else if (a is AgentActionInjury)
        {
            Owner.WorldState.SetWSProperty(E_PropKey.E_ORDER, AgentOrder.E_OrderType.E_NONE);
            ComboProgress.Clear();
            ClearBufferedOrder();
            GuiManager.Instance.ShowComboProgress(ComboProgress);
            Game.Instance.Hits = 0;
            Game.Instance.NumberOfInjuries++;

        }
        else if (a is AgentActionDeath)
        {
            Owner.WorldState.SetWSProperty(E_PropKey.E_ORDER, AgentOrder.E_OrderType.E_NONE);
            ComboProgress.Clear();
            ClearBufferedOrder();
            GuiManager.Instance.ShowComboProgress(ComboProgress);
            Game.Instance.Hits = 0;
            Game.Instance.NumberOfDeath++;
            //Game.Instance.Score -= 50;
            Mission.Instance.EndOfMission(false);
			// of	unlockAchievement
			if(Game.Instance.NumberOfDeath >= 100) {
				Achievements.UnlockAchievement(2);
			} else if(Game.Instance.NumberOfDeath >= 50) {
				Achievements.UnlockAchievement(1);
			}
        }
         
    }


    private void CreateOrderGoTo()
    {
        if (CouldAddnewOrder() == false)
            return;

        AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_GOTO);
        Owner.BlackBoard.CameraDirection = CameraBehaviour.Instance.lookAt;
        order.Direction = InputDirection;// Controls.Joystick.Direction;
        order.MoveSpeedModifier = 0.6f; //Controls.Joystick.Force;
        //Debug.Log("order.MoveSpeedModifier=" + order.MoveSpeedModifier);
        Owner.BlackBoard.OrderAdd(order);
    }

    private void CreateOrderStop()
    {
        AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_STOPMOVE);
        Owner.BlackBoard.OrderAdd(order);
    }


    private void CreateOrderAttack(E_AttackType type)
    {
        if (CouldBufferNewOrder() == false && CouldAddnewOrder() == false)
        {
            //Debug.Log(Time.timeSinceLevelLoad + " attack order rejected, already buffered one ");
            return;
        }

        AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_ATTACK);

        if (InputDirection != Vector3.zero)//Controls.Joystick.Direction != Vector3.zero)
            order.Direction = InputDirection;//Controls.Joystick.Direction;
        else
            order.Direction = Transform.forward;
        
        
        order.AnimAttackData = ProcessCombo(type);

        order.Target = GetBestTarget(false);

        if (CouldAddnewOrder())
        {
            //Debug.Log("order " + (order.Target != null ? order.Target.name : "no target") + " " + order.AnimAttackData.AnimName);
            Owner.BlackBoard.OrderAdd(order);
        }
        else
        {
            //Debug.Log("order to queue " + (order.Target != null ? order.Target.name : "no target") + " " + order.AnimAttackData.AnimName);
            BufferedOrders.Enqueue(order);
        }
    }

    private void CreateOrderDodge()
	{
        if (Owner.BlackBoard.IsOrderAddPossible(AgentOrder.E_OrderType.E_DODGE) == false)
            return;

        Vector3 rollDir;

        if (InputDirection != Vector3.zero)//Controls.Joystick.Direction != Vector3.zero)
            rollDir = InputDirection;// Controls.Joystick.Direction;
        else
            rollDir = Owner.Forward;

        rollDir.Normalize();

        AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_DODGE);
        order.Direction = rollDir;

        
        Owner.BlackBoard.OrderAdd(order);

        ComboProgress.Clear();
        ClearBufferedOrder();
        GuiManager.Instance.ShowComboProgress(ComboProgress);
	}

    public void CreateOrderUse()
    {
        if (Owner.BlackBoard.IsOrderAddPossible(AgentOrder.E_OrderType.E_USE) == false)
            return;

        InteractionGameObject onObject = Mission.Instance.CurrentGameZone.GetNearestInteractionObject(Owner.Position, 2);

        if (onObject == null)
            return;

        if (onObject is InteractionLever)
        {
            InteractionLever lever = onObject as InteractionLever;
            if (lever.State != InteractionLever.E_State.E_OFF && lever.State != InteractionLever.E_State.E_OFF)
            {
                HandleBadUse();
                return;
            }

            AgentOrder order = AgentOrderFactory.Create(AgentOrder.E_OrderType.E_USE);
            order.InteractionObject = onObject;
            order.Position = order.InteractionObject.GetEntryTransform().position;
            order.Interaction = E_InteractionType.On;
            Owner.BlackBoard.OrderAdd(order);

            return;
        }

        ComboProgress.Clear();
        ClearBufferedOrder();
        GuiManager.Instance.ShowComboProgress(ComboProgress);
    }

    void ClearBufferedOrder()
    {
        while(BufferedOrders.Count > 0)
            AgentOrderFactory.Return(BufferedOrders.Dequeue());
    }

    public void HandleBadUse()
    {

    }

    public bool CouldBufferNewOrder()
    {
        return BufferedOrders.Count <= 0 && CurrentAttackAction != null;
    }

    public bool CouldAddnewOrder()
    {
        AgentOrder.E_OrderType order =  Owner.WorldState.GetWSProperty(E_PropKey.E_ORDER).GetOrder();

        if (order == AgentOrder.E_OrderType.E_DODGE || order == AgentOrder.E_OrderType.E_ATTACK || order == AgentOrder.E_OrderType.E_USE)
            return false;

        AgentAction action;

        for(int i = 0;i < Owner.BlackBoard.ActionCount(); i++)
        {
            action = Owner.BlackBoard.ActionGet(i);
            if (action is AgentActionAttack && (action as AgentActionAttack).AttackPhaseDone == false)
                return false;
            else if (action is AgentActionRoll)
                return false;
            else if (action is AgentActionUseLever)
                return false;
            else if (action is AgentActionGoTo && (action as AgentActionGoTo).Motion == E_MotionType.Sprint)
                return false;
        }
        return true;
    }

    public void StopMove(bool stop)
    {
        //if (stop)
            //Controls.DisableInput();
        //else
            //Controls.EnableInput();
    }

    public void Teleport(Teleport teleport)
    {
        Owner.BlackBoard.Stop = true;
        Owner.BlackBoard.TeleportDestination = teleport;
        Owner.WorldState.SetWSProperty(E_PropKey.E_TELEPORT, true);
       // Controls.Reset();
      
    }

    public Agent GetBestTarget(bool hasToBeKnockdown)
    {
        if (Mission.Instance.CurrentGameZone == null)
            return null;

        List<Agent> enemies = Mission.Instance.CurrentGameZone.Enemies;

        float[] EnemyCoeficient = new float[enemies.Count];
        Agent enemy;
        Vector3 dirToEnemy;
 
        for(int i = 0; i < enemies.Count; i++)
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

            EnemyCoeficient[i] += 0.2f - ((angle/180.0f) * 0.2f);

          //  Debug.Log("angle " + Mission.Instance.CurrentGameZone.GetEnemy(i).name + " : " + EnemyCoeficient[i]);

            if (InputDirection !=Vector3.zero)//Controls.Joystick.Direction != Vector3.zero)
            {
                angle = Vector3.Angle(dirToEnemy, InputDirection);//Controls.Joystick.Direction);
                EnemyCoeficient[i] += 0.5f - ((angle / 180.0f) * 0.5f);
            }
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


    public void AddExperience(int exp, float scoreModifier)
    {
        Game.Instance.Money += exp;
    }

   // static string[] ComboNames = { "Raising Waves", "Half Moon", "Could Cutting", "Flaying Dragon", "Walking Death", "Shogun Death" }; 

    private AnimAttackData ProcessCombo(E_AttackType attackType)
    {
        if (attackType != E_AttackType.O && attackType != E_AttackType.X)
            return null;

        ComboProgress.Add(attackType);

        for (int i = 0; i < PlayerComboAttacks.Length; i++)
        {// projedem vsechny attacky

            Combo combo = PlayerComboAttacks[i];

            if (combo.SwordLevel > Game.Instance.SwordLevel)
                continue; // nema combo...

            bool valid = ComboProgress.Count <= combo.ComboSteps.Length; // 
            for (int ii = 0; ii < ComboProgress.Count && ii < combo.ComboSteps.Length; ii++)
            {
                if (ComboProgress[ii] != combo.ComboSteps[ii].AttackType ||  combo.ComboSteps[ii].ComboLevel >  Game.Instance.ComboLevel[i])
                {// combo nepokracuje timto stepem... nebo step neni available
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                combo.ComboSteps[ComboProgress.Count - 1].Data.LastAttackInCombo = NextAttackIsAvailable(E_AttackType.X) == false && NextAttackIsAvailable(E_AttackType.O) == false;
                combo.ComboSteps[ComboProgress.Count - 1].Data.FirstAttackInCombo = false;
                combo.ComboSteps[ComboProgress.Count - 1].Data.ComboIndex = i;
                combo.ComboSteps[ComboProgress.Count - 1].Data.FullCombo = ComboProgress.Count == combo.ComboSteps.Length;
                combo.ComboSteps[ComboProgress.Count - 1].Data.ComboStep = ComboProgress.Count;

                //if (ComboProgress.Count == 3)
                    //FlurryrBinding.FlurryLogPerformedCombo(ComboNames[i]);

                GuiManager.Instance.ShowComboProgress(ComboProgress);
                return combo.ComboSteps[ComboProgress.Count - 1].Data;
            }
        }

        // takze zadny uspech

        //pokud ale je nabuferovano,tak nezacinat nove combo ?


        //je treba zacit od zacatku
        ComboProgress.Clear();
        ComboProgress.Add(attackType);

        for (int i = 0; i < PlayerComboAttacks.Length; i++)
        {// projedem vsechny prvni stepy
            if (PlayerComboAttacks[i].ComboSteps[0].AttackType == attackType)
            {
               // Debug.Log(Time.timeSinceLevelLoad + " New combo " + i + " step " + ComboProgress.Count);
                PlayerComboAttacks[i].ComboSteps[0].Data.FirstAttackInCombo = true;
                PlayerComboAttacks[i].ComboSteps[0].Data.LastAttackInCombo = false;
                PlayerComboAttacks[i].ComboSteps[0].Data.ComboIndex = i;
                PlayerComboAttacks[i].ComboSteps[0].Data.FullCombo = false;
                PlayerComboAttacks[i].ComboSteps[0].Data.ComboStep = 0;

                GuiManager.Instance.ShowComboProgress(ComboProgress);
                return PlayerComboAttacks[i].ComboSteps[0].Data;
            }
        }

        Debug.LogError("Could not find any combo attack !!! Some shit happens");

        return null;
    }

    private bool NextAttackIsAvailable(E_AttackType attackType)
    {
        if (attackType != E_AttackType.O && attackType != E_AttackType.X)
            return false;

        if (ComboProgress.Count == 5) // ehmm. proste je jich ted sest, tak bacha na to...
            return false;

        List<E_AttackType> progress = new List<E_AttackType>(ComboProgress);

        progress.Add(attackType);

        for (int i = 0; i < PlayerComboAttacks.Length; i++)
        {// projedem vsechny attacky

            Combo combo = PlayerComboAttacks[i];

            if (combo.SwordLevel > Game.Instance.SwordLevel)
                continue;

            bool valid = true;
            for (int ii = 0; ii < progress.Count; ii++)
            {
                if (progress[ii] != combo.ComboSteps[ii].AttackType || combo.ComboSteps[ii].ComboLevel > Game.Instance.ComboLevel[i])
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
                return true;
        }
        return false;
    }

    public void HealToFullHealth()
    {
        StartCoroutine(HealingUp());
    }

    IEnumerator HealingUp()
    {
        yield return new WaitForSeconds(1.5f);

        float healingHP = Owner.BlackBoard.RealMaxHealth - Owner.BlackBoard.Health + 1;

       // PlayHealingSound();

        while (healingHP > 0)
        {
            float h = 33 * Time.deltaTime;
            if (healingHP - h < 0)
                h = healingHP;

            healingHP -= h;
            Owner.BlackBoard.Health += h;

            GuiManager.Instance.SetHealthPercent(Owner.BlackBoard.Health, Owner.BlackBoard.RealMaxHealth);

            yield return new WaitForEndOfFrame();
        }

        if (Owner.BlackBoard.Health > Owner.BlackBoard.RealMaxHealth)
            Owner.BlackBoard.Health = Owner.BlackBoard.RealMaxHealth;

        GuiManager.Instance.SetHealthPercent(Owner.BlackBoard.Health, Owner.BlackBoard.RealMaxHealth);
    }

}
