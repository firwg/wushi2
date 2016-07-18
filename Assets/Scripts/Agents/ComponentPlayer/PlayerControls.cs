using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PlayerControls
{

    public enum  E_ButtonStatus
    {
        UpFirst,
        Up,
        DownFirst,
        Down,
        Cancel
    }

    public enum E_ButtonsName
    {
        None = -1,
        AttackX,
        AttackO,
        Roll,
        Use,
        Shop,
        InGameMenu,
        ShopOk,
        ShopCancel,
        ShopHealth,
        ShopSword,
        ShopCombo1,
        ShopCombo2,
        ShopCombo3,
        ShopCombo4,
        ShopCombo5,
        ShopCombo6,
        ShopInfoBuy,
        ShopInfoBack,
        Max
    }

    public class Button
    {
        public int FingerId = -1;
        public E_ButtonStatus Status = E_ButtonStatus.Up;
        public bool On = true;
        public float PressTime = -1;
        
        bool useRadius;
        Vector2 Center;
        float Left, Bottom, Width, Height;

        public Button(Vector2 center) {
            Center = center; useRadius = true; 
        }
        
        public Button(float left, float bottom, float witdth, float height) { Left = left; Bottom = bottom; Width = witdth; Height = height; useRadius = false; }

        public void SetCenter(Vector2 center) { Center = center; }


        public bool IsInsideButton(Touch touch)
        {
            if (useRadius)
            {
                if ((touch.position - Center).magnitude < 0.07f * Screen.width)
                    return true;
            }
            else
            {
                if (touch.position.x > Left && touch.position.x < Left + Width &&
                 touch.position.y > Bottom&& touch.position.y < Bottom + Height)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class JoystickControl
    {
        public int FingerID = -1;
        public float DeadZone { get { return 0.01f * Screen.width; } }
        public float Radius {get { return 0.075f * Screen.width;}}
        public bool On = true;

        public Vector3 Direction = new Vector2();
        public float Force;

        public Vector2 Center;

        public JoystickControl(Vector2 center) { Center = center; }
        public void SetCenter(Vector2 center) { Center = center; }

    }

    Transform _Temp;

    public Button[] Buttons;

    public JoystickControl Joystick = null;

    Transform MainCameraTransfom;

    public void Start()
    {
        GameObject t = new GameObject();
        _Temp = t.transform;
        MainCameraTransfom = Camera.main.transform;

       Buttons = new Button[] {
            new Button(Game.Instance.ButtonXPositon + new Vector2(GuiButtonX.Instance.ScreenWidth * 0.5f, GuiButtonX.Instance.ScreenHeight *0.5f)), 
            new Button(Game.Instance.ButtonOPositon + new Vector2(GuiButtonY.Instance.ScreenWidth * 0.5f, GuiButtonY.Instance.ScreenHeight *0.5f)), 
            new Button(Game.Instance.ButtonRPositon + new Vector2(GuiButtonRoll.Instance.ScreenWidth * 0.5f, GuiButtonRoll.Instance.ScreenHeight *0.5f)), 
            new Button(Game.Instance.ButtonOPositon + new Vector2(GuiButtonUse.Instance.ScreenWidth * 0.5f, GuiButtonUse.Instance.ScreenHeight *0.5f)), 
            new Button(GuiButtonShop.Instance.Center),
            new Button(GuiButtonInGameMenu.Instance.Center),
            new Button(GuiShopButtonOk.Instance.ScreenLeft, GuiShopButtonOk.Instance.ScreenBottom - GuiShopButtonOk.Instance.ScreenHeight, GuiShopButtonOk.Instance.ScreenWidth, GuiShopButtonOk.Instance.ScreenHeight * 2), 
            new Button(GuiShopButtonBack.Instance.ScreenLeft, GuiShopButtonBack.Instance.ScreenBottom - GuiShopButtonBack.Instance.ScreenHeight, GuiShopButtonBack.Instance.ScreenWidth, GuiShopButtonBack.Instance.ScreenHeight * 2), 
            new Button(GuiShopComboButtons.BScreenLeft(0), GuiShopComboButtons.BScreenBottom(0), GuiShopComboButtons.BScreenWidth(0), GuiShopComboButtons.BScreenHeight(0)), 
            new Button(GuiShopComboButtons.BScreenLeft(1), GuiShopComboButtons.BScreenBottom(1), GuiShopComboButtons.BScreenWidth(1), GuiShopComboButtons.BScreenHeight(1)), 
            new Button(GuiShopComboButtons.BScreenLeft(2), GuiShopComboButtons.BScreenBottom(2), GuiShopComboButtons.BScreenWidth(2), GuiShopComboButtons.BScreenHeight(2)), 
            new Button(GuiShopComboButtons.BScreenLeft(3), GuiShopComboButtons.BScreenBottom(3), GuiShopComboButtons.BScreenWidth(3), GuiShopComboButtons.BScreenHeight(3)), 
            new Button(GuiShopComboButtons.BScreenLeft(4), GuiShopComboButtons.BScreenBottom(4), GuiShopComboButtons.BScreenWidth(4), GuiShopComboButtons.BScreenHeight(4)), 
            new Button(GuiShopComboButtons.BScreenLeft(5), GuiShopComboButtons.BScreenBottom(5), GuiShopComboButtons.BScreenWidth(5), GuiShopComboButtons.BScreenHeight(5)), 
            new Button(GuiShopComboButtons.BScreenLeft(6), GuiShopComboButtons.BScreenBottom(6), GuiShopComboButtons.BScreenWidth(6), GuiShopComboButtons.BScreenHeight(6)), 
            new Button(GuiShopComboButtons.BScreenLeft(7), GuiShopComboButtons.BScreenBottom(7), GuiShopComboButtons.BScreenWidth(7), GuiShopComboButtons.BScreenHeight(7)), 
            new Button(GuiShopBuyInfo.OkScreenLeft, GuiShopBuyInfo.OkScreenBottom -  GuiShopBuyInfo.OkScreenHeight, GuiShopBuyInfo.OkScreenWidth, GuiShopBuyInfo.OkScreenHeight * 2), 
            new Button(GuiShopBuyInfo.CancelScreenLeft, GuiShopBuyInfo.CancelScreenBottom - GuiShopBuyInfo.CancelScreenHeight, GuiShopBuyInfo.CancelScreenWidth, GuiShopBuyInfo.CancelScreenHeight * 2), 
        };

       Joystick = new JoystickControl(Game.Instance.JoystickPositon + new Vector2(GuiJoystick.Instance.ScreenWidth * 0.5f, GuiJoystick.Instance.ScreenHeight * 0.5f));
    }

    public void UpdateControlsPosition()
    {
        Buttons[(int)E_ButtonsName.AttackX].SetCenter(Game.Instance.ButtonXPositon + new Vector2(GuiButtonX.Instance.ScreenWidth * 0.5f, GuiButtonX.Instance.ScreenHeight *0.5f));
        Buttons[(int)E_ButtonsName.AttackO].SetCenter(Game.Instance.ButtonOPositon + new Vector2(GuiButtonY.Instance.ScreenWidth * 0.5f, GuiButtonY.Instance.ScreenHeight *0.5f));
        Buttons[(int)E_ButtonsName.Use].SetCenter(Game.Instance.ButtonOPositon + new Vector2(GuiButtonUse.Instance.ScreenWidth * 0.5f, GuiButtonUse.Instance.ScreenHeight *0.5f));
        Buttons[(int)E_ButtonsName.Roll].SetCenter(Game.Instance.ButtonRPositon + new Vector2(GuiButtonRoll.Instance.ScreenWidth * 0.5f, GuiButtonRoll.Instance.ScreenHeight *0.5f));

        Joystick.SetCenter(Game.Instance.JoystickPositon + new Vector2(GuiJoystick.Instance.ScreenWidth * 0.5f, GuiJoystick.Instance.ScreenHeight * 0.5f));
    }

    public void Update()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i].Status == E_ButtonStatus.UpFirst)
                Buttons[i].Status = E_ButtonStatus.Up;
            else if (Buttons[i].Status == E_ButtonStatus.DownFirst)
                Buttons[i].Status = E_ButtonStatus.Down;

            if ( i < (int)E_ButtonsName.Shop && Buttons[i].PressTime != -1 && Time.timeSinceLevelLoad - Buttons[i].PressTime > 0.1f)
            {
                Buttons[i].Status = E_ButtonStatus.UpFirst;
                Buttons[i].FingerId = -1;
                Buttons[i].PressTime = -1;
                GuiManager.Instance.ButtonUp((E_ButtonsName)i);
            }
        }

        if (Input.touchCount == 0)
            return;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                TouchBegin(touch);
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                TouchUpdate(touch);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                TouchEnd(touch);
            }
        }
    }

    public void TouchBegin(Touch touch)
    {
        //Debug.Log("testing TouchBegin : " + touch.position);

        if (FingerIdInUse(touch))
            return;

        if (Joystick.On && Joystick.FingerID == -1)
        {
            Vector2 dir = touch.position - Joystick.Center;
            float dist = dir.magnitude;
            // //Debug.Log("testing TouchBegin on Joystick : " + dist);
            if (dist < Joystick.Radius * 1.5f)
            {
                Joystick.FingerID = touch.fingerId;

                GuiManager.Instance.JoystickDown();

                if (dist > Joystick.DeadZone)
                {
                    Joystick.Direction.x = dir.y;
                    Joystick.Direction.z = -dir.x;
                    Joystick.Direction.Normalize();

                    _Temp.eulerAngles = new Vector3(0, MainCameraTransfom.eulerAngles.y, 0);
                    Joystick.Direction = _Temp.TransformDirection(Joystick.Direction);

                    Joystick.Force = (dist - Joystick.DeadZone) / (Joystick.Radius - Joystick.DeadZone);

                    dir.Normalize();
                    dir *= Joystick.Force;
                    GuiManager.Instance.JoystickUpdate(dir);
                }
                else
                {
                    Joystick.Direction = Vector3.zero;
                    Joystick.Force = 0;

                    GuiManager.Instance.JoystickUpdate(Vector2.zero);
                }

                //Debug.Log("Joystick aquired");


                return;
            }
        }

        for (int i = 0; i < Buttons.Length; i++)
        {
            ////Debug.Log("testing TouchBegin on button (" + i + " ) : " + (touch.position - Buttons[i].Center).magnitude);

            if (Game.Instance.GameType == E_GameType.Survival && i >= (int)E_ButtonsName.ShopHealth)
                break;

            if (Buttons[i].On && Buttons[i].FingerId == -1 && Buttons[i].IsInsideButton(touch))
            {
                Buttons[i].FingerId = touch.fingerId;
                Buttons[i].Status = E_ButtonStatus.DownFirst;
                Buttons[i].PressTime = Time.timeSinceLevelLoad;

                GuiManager.Instance.ButtonDown((E_ButtonsName)i);

                return;
            }
        }
    }

    public void TouchUpdate(Touch touch)
    {
        if (Joystick.On && Joystick.FingerID == -1)
        {
            Vector2 dir = touch.position - Joystick.Center;
            float dist = dir.magnitude;
            // //Debug.Log("testing TouchBegin on Joystick : " + dist);
            if (dist < Joystick.Radius * 1.5f)
            {
                Joystick.FingerID = touch.fingerId;
                GuiManager.Instance.JoystickDown();

                if (dist > Joystick.DeadZone)
                {
                    Joystick.Direction.x = dir.y;
                    Joystick.Direction.z = -dir.x;
                    Joystick.Direction.Normalize();

                    _Temp.eulerAngles = new Vector3(0, MainCameraTransfom.eulerAngles.y, 0);
                    Joystick.Direction = _Temp.TransformDirection(Joystick.Direction);

                    Joystick.Force = (dist - Joystick.DeadZone) / (Joystick.Radius - Joystick.DeadZone);

                    dir.Normalize();
                    dir *= Joystick.Force;
                    GuiManager.Instance.JoystickUpdate(dir);
                }
                else
                {
                    Joystick.Direction = Vector3.zero;
                    Joystick.Force = 0;

                    GuiManager.Instance.JoystickUpdate(Vector2.zero);
                }

                //Debug.Log("Joystick aquired");


                return;
            }
        }

        if (Joystick.On && Joystick.FingerID == touch.fingerId)
        {
            Vector2 dir = touch.position - Joystick.Center;
            float dist = dir.magnitude;
            //Debug.Log("joystick updated");
            Joystick.FingerID = touch.fingerId;

            if (dist > Joystick.Radius)
            {
                dir.Normalize();
                dir *= Joystick.Radius;
            }
            GuiManager.Instance.JoystickUpdate(dir);


            if (dist > Joystick.DeadZone)
            {
                Joystick.Direction.x = dir.x;
                Joystick.Direction.z = dir.y;
                Joystick.Direction.Normalize();

                _Temp.transform.eulerAngles = new Vector3(0, MainCameraTransfom.eulerAngles.y, 0);
                Joystick.Direction = _Temp.transform.TransformDirection(Joystick.Direction);
                Joystick.Force = (dist - Joystick.DeadZone) / (Joystick.Radius - Joystick.DeadZone);
                if (Joystick.Force > 1)
                    Joystick.Force = 1;
                return;
            }
            Joystick.Direction = Vector3.zero;
            Joystick.Force = 0;
            return;
        }
    }

    public void TouchEnd(Touch touch)
    {
        if (Joystick.FingerID == touch.fingerId)
        {
            if (Joystick.On)
            {
                Joystick.FingerID = -1;
                Joystick.Direction = Vector3.zero;
                Joystick.Force = 0;
            }
            GuiManager.Instance.JoystickUp();
            //Debug.Log("Joystick releaed");
            return;
        }

        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i].On && touch.fingerId == Buttons[i].FingerId)
            {
                Buttons[i].Status = E_ButtonStatus.UpFirst;
                Buttons[i].FingerId = -1;
                Buttons[i].PressTime = -1;

                GuiManager.Instance.ButtonUp((E_ButtonsName)i);
                return;
            }
        }
    }

    bool FingerIdInUse(Touch touch)
    {
        if (Joystick.FingerID == touch.fingerId)
            return true;

        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i].On && touch.fingerId == Buttons[i].FingerId)
                return true;
        }

        return false;
    }

    void EnableShopInput(bool enable)
    {
        for (E_ButtonsName i = E_ButtonsName.ShopOk; i <= E_ButtonsName.ShopCombo6; i++)
            Buttons[(int)i].On = enable;

       // Debug.Log("EnableShopInput " + enable);
    }

    void EnableShopInfoInput(bool buyEnabled, bool enable)
    {
        Buttons[(int)E_ButtonsName.ShopInfoBuy].On = enable && buyEnabled;
        Buttons[(int)E_ButtonsName.ShopInfoBack].On = enable;
    }

    public void SwitchToShopMode()
    {
        Reset();

        Buttons[(int)E_ButtonsName.AttackX].On = false;
        Buttons[(int)E_ButtonsName.AttackO].On = false;
        Buttons[(int)E_ButtonsName.Roll].On = false;
        Buttons[(int)E_ButtonsName.Use].On = false;
        Buttons[(int)E_ButtonsName.Shop].On = false;
        Buttons[(int)E_ButtonsName.InGameMenu].On = false;
        Joystick.On = false;

        EnableShopInput(true);
        EnableShopInfoInput(false, false);
    }

    public void SwitchToShopBuyMode(bool buyEnabled)
    {
        //Debug.Log("SwitchToShopBuyMode");
        Buttons[(int)E_ButtonsName.AttackX].On = false;
        Buttons[(int)E_ButtonsName.AttackO].On = false;
        Buttons[(int)E_ButtonsName.Roll].On = false;
        Buttons[(int)E_ButtonsName.Use].On = false;
        Buttons[(int)E_ButtonsName.Shop].On = false;
        Buttons[(int)E_ButtonsName.InGameMenu].On = false;
        Joystick.On = false;

        EnableShopInput(false);
        EnableShopInfoInput(buyEnabled, true);
    }

    public void SwitchToUseMode()
    {
        Buttons[(int)E_ButtonsName.AttackX].On = false;
        Buttons[(int)E_ButtonsName.AttackO].On = false;
        Buttons[(int)E_ButtonsName.Roll].On = false;
        Buttons[(int)E_ButtonsName.Use].On = true;
        Buttons[(int)E_ButtonsName.Shop].On = true;
        Buttons[(int)E_ButtonsName.InGameMenu].On = true;
        Joystick.On = true;

        EnableShopInput(false);
        EnableShopInfoInput(false, false);
    }
    public void SwitchToCombatMode()
    {
        Buttons[(int)E_ButtonsName.AttackX].On = true;
        Buttons[(int)E_ButtonsName.AttackO].On = true;
        Buttons[(int)E_ButtonsName.Roll].On = true;
        Buttons[(int)E_ButtonsName.Use].On = false;
        Buttons[(int)E_ButtonsName.Shop].On = true;
        Buttons[(int)E_ButtonsName.InGameMenu].On = true;
        Joystick.On = true;

        EnableShopInput(false);
        EnableShopInfoInput(false, false);
    }

    public void DisableInput()
    {
        Reset();

        Buttons[(int)E_ButtonsName.AttackX].On = false;
        Buttons[(int)E_ButtonsName.AttackO].On = false;
        Buttons[(int)E_ButtonsName.Roll].On = false;
        Buttons[(int)E_ButtonsName.Use].On = false;
        Buttons[(int)E_ButtonsName.Shop].On = false;
        Buttons[(int)E_ButtonsName.InGameMenu].On = false;
        Joystick.On = false;

        EnableShopInput(false);
        EnableShopInfoInput(false, false);
    }

    public void EnableInput()
    {
        SwitchToCombatMode();
        Joystick.On = true;
    }

    public void Reset()
    {
        for(int i = 0; i < Buttons.Length;i++)
        {
            Buttons[i].FingerId = -1;
            Buttons[i].Status = E_ButtonStatus.Up;
        }
          
        Joystick.FingerID = -1;
        Joystick.Direction = Vector3.zero;
        Joystick.Force = 0;

        GuiManager.Instance.ResetControls();
    }
}