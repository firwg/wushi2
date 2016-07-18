
using UnityEngine;
using System.Collections.Generic;

public class GuiElementData
{
    protected float _ScreenLeft;
    protected float _ScreenBottom;
    protected float _ScreenWidth;
    protected float _ScreenHeight;

    protected int _UvLeft;
    protected int _UvTop;
    protected int _UvWidth;
    protected int _UvHeight;

    public int ScreenLeft { get { return Mathf.CeilToInt(Screen.width * _ScreenLeft); } }
    public int ScreenBottom { get { return Mathf.CeilToInt(Screen.height * _ScreenBottom); } }
    public int ScreenWidth { get { return Mathf.CeilToInt(Screen.width * _ScreenWidth); } }
    public int ScreenHeight { get { return Mathf.CeilToInt(_ScreenHeight == -1 ? ScreenWidth: Screen.height * _ScreenHeight); } }

    public int UvLeft { get { return (int)(_UvLeft); } }
    public int UvTop { get { return (int)(_UvTop); } }
    public int UvWidth { get { return (int)(_UvWidth); } }
    public int UvHeight { get { return (int)(_UvHeight); } }

    public Sprite Sprite;

    public Vector2 Center { get { return new Vector2(ScreenLeft + ScreenWidth / 2, ScreenBottom + ScreenHeight / 2); } }

    public bool IsInside(Touch touch)
    {
        if (touch.position.x >= ScreenLeft && touch.position.x <= ScreenLeft + ScreenWidth &&
             touch.position.y >= _ScreenBottom && touch.position.y <= ScreenBottom + ScreenHeight)
        {
            return true;
        }
        return false;
    }
    
    public bool IsInside(Touch touch, float radius)
    {
        if ((touch.position - Center).magnitude < radius)
            return true;

        return false;
    }
}

class GuiButtonX: GuiElementData
{
    public GuiButtonX()
    {
        _ScreenLeft = 0.92f;
        _ScreenBottom = 0.35f;
        _ScreenWidth = 0.07f;
        _ScreenHeight = -1;

        _UvLeft = 364;
        _UvTop = 114; 
        _UvWidth = 114;
        _UvHeight = 114;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiButtonX(); return _Instance; } }

}
class GuiButtonY : GuiElementData
{
    public GuiButtonY()
    {
        _ScreenLeft = 0.84f;
        _ScreenBottom = 0.225f;
        _ScreenWidth = 0.07f;
        _ScreenHeight = -1;

        _UvLeft = 364;
        _UvTop = 228;
        _UvWidth = 114;
        _UvHeight = 114;

        _Instance = this;
    }

    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) new GuiButtonY(); return _Instance; } }

}

class GuiButtonRoll : GuiElementData
{
    public GuiButtonRoll()
    {
        _ScreenLeft = 0.80f;
        _ScreenBottom = 0.06f;
        _ScreenWidth = 0.07f;
        _ScreenHeight = -1f;

        _UvLeft = 364;
        _UvTop = 342;
        _UvWidth = 114;
        _UvHeight = 114;

        _Instance = this;
    }

    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) new GuiButtonRoll(); return _Instance; } }

}

class GuiButtonUse : GuiElementData
{
    public GuiButtonUse()
    {
        _ScreenLeft = 0.87f;
        _ScreenBottom = 0.14f;
        _ScreenWidth = 0.07f;
        _ScreenHeight = -1;

        _UvLeft = 484;
        _UvTop = 228;
        _UvWidth = 114;
        _UvHeight = 114;

        _Instance = this;
    }

    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) new GuiButtonUse(); return _Instance; } }
}

class GuiJoystick : GuiElementData
{
    public GuiJoystick()
    {
        _ScreenLeft = 0.08f;
        _ScreenBottom = 0.14f;
        _ScreenWidth = 0.12f;
        _ScreenHeight = -1;

        _UvLeft = 192;
        _UvTop = 170;
        _UvWidth = 170;
        _UvHeight = 170;

        _Instance = this;
    }

    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) new GuiJoystick(); return _Instance; } }
}

class GuiJoystickHat 
{
    static public Sprite Sprite;

    static public int ScreenLeft { get { return Mathf.CeilToInt(GuiJoystick.Instance.ScreenLeft + GuiJoystick.Instance.ScreenWidth / 2 - ScreenWidth/2); } }
    static public int ScreenBottom { get { return Mathf.CeilToInt(GuiJoystick.Instance.ScreenBottom + GuiJoystick.Instance.ScreenHeight /2 - ScreenHeight/2); } }
    static public int ScreenWidth { get { return Mathf.CeilToInt(Screen.width * 0.07f); } }
    static public int ScreenHeight { get { return ScreenWidth; } }

    static public int UvLeft = 484;
    static public int UvTop = 114;
    static public int UvWidth = 114;
    static public int UvHeight = 114;
}

static class GuiButtonSelect
{
    static public Sprite SpriteX;
    static public Sprite SpriteO;
    static public Sprite SpriteUse;
    static public Sprite SpriteRoll;
    static public Sprite SpriteJoystick;

    static public int UvLeft = 484;
    static public int UvTop = 342;
    static public int UvWidth = 114;
    static public int UvHeight = 114;
}


static class GuiFPS
{
    static public Sprite[] NumberSprites = new Sprite[2];

    static public float _ScreenLeft = 0.96f;
    static public float _ScreenBottom = 0.8f;
    static public float _ScreenWidth = .026f;
    static public float _ScreenHeight = -1;

    static public int ScreenLeft { get { return Mathf.CeilToInt(Screen.width * _ScreenLeft); } }
    static public int ScreenBottom { get { return Mathf.CeilToInt(Screen.height * _ScreenBottom); } }
    static public int ScreenWidth { get { return Mathf.CeilToInt(Screen.width * _ScreenWidth); } }
    static public int ScreenHeight { get { return Mathf.CeilToInt(_ScreenHeight == -1 ? ScreenWidth : Screen.height * _ScreenHeight); } }


    static public int UvLeft = 254;
    static public int UvTop = 785;
    static public int UvWidth = 26;
    static public int UvHeight = 42;
}

class GuiShopKarmaCaption : GuiElementData
{
    public GuiShopKarmaCaption()
    {
        _ScreenLeft = 0.16f;
        _ScreenBottom = 0.865f;
        _ScreenWidth = 0.140f;
        _ScreenHeight = 0.050f;

        _UvLeft = 348;
        _UvTop = 824; 
        _UvWidth = 140;
        _UvHeight = 40;
        
        _Instance = this;
    }

    static GuiShopKarmaCaption _Instance;
    static public GuiShopKarmaCaption Instance { get { if (_Instance == null) _Instance = new GuiShopKarmaCaption(); return _Instance; } }

    public float _NScreenLeft = 0.444f;
    public float _NScreenBottom = 0.865f;
    public float _NScreenWidth =  0.030f;
    public float _NScreenHeight = 0.034f;

    public int NScreenLeft { get { return Mathf.CeilToInt(Screen.width * _NScreenLeft); } }
    public int NScreenBottom { get { return Mathf.CeilToInt(Screen.height * _NScreenBottom); } }
    public int NScreenWidth { get { return Mathf.CeilToInt(Screen.width * _NScreenWidth); } }
    public int NScreenHeight { get { return Mathf.CeilToInt(_ScreenHeight == -1 ? NScreenWidth : Screen.height * _NScreenHeight); } }


}

class GuiShopComboCaption : GuiElementData
{
        public GuiShopComboCaption()
    {
        _ScreenLeft = 0.16f;
        _ScreenBottom = 0.648f;
        _ScreenWidth = 0.220f;
        _ScreenHeight = 0.040f;

        _UvLeft = 0;
        _UvTop = 824; 
        _UvWidth = 230;
        _UvHeight = 40;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopComboCaption(); return _Instance; } }
}

class GuiShopBackgroundTop: GuiElementData
{
        public GuiShopBackgroundTop()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.925f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.006f;

        _UvLeft = 0;
        _UvTop = 1010; 
        _UvWidth = 800;
        _UvHeight = 6;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundTop(); return _Instance; } }
}


class GuiShopBackgroundMiddle: GuiElementData
{
        public GuiShopBackgroundMiddle()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.116f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.815f;

        _UvLeft = 0;
        _UvTop = 1016; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundMiddle(); return _Instance; } }
}

class GuiShopBackgroundBottom: GuiElementData
{
        public GuiShopBackgroundBottom()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.112f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.006f;

        _UvLeft = 0;
        _UvTop = 1024; 
        _UvWidth = 800;
        _UvHeight = 6;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundBottom(); return _Instance; } }
}


class GuiShopBackgroundTopTop: GuiElementData
{
    public GuiShopBackgroundTopTop()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.92f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.002f;

        _UvLeft = 0;
        _UvTop = 1004; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundTopTop(); return _Instance; } }
}


class GuiShopBackgroundTopMiddle: GuiElementData
{
        public GuiShopBackgroundTopMiddle()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.71f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.210f;

        _UvLeft = 0;
        _UvTop = 998; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundTopMiddle(); return _Instance; } }
}

class GuiShopBackgroundTopBottom: GuiElementData
{
        public GuiShopBackgroundTopBottom()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.710f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.002f;

        _UvLeft = 0;
        _UvTop = 1004; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundTopBottom(); return _Instance; } }
}


class GuiShopBackgroundMiddleTop: GuiElementData
{
        public GuiShopBackgroundMiddleTop()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.698f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.002f;

        _UvLeft = 0;
        _UvTop = 1004; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundMiddleTop(); return _Instance; } }
}


class GuiShopBackgroundMiddleMiddle: GuiElementData
{
    public GuiShopBackgroundMiddleMiddle()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.196f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.502f;

        _UvLeft = 0;
        _UvTop = 998; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundMiddleMiddle(); return _Instance; } }
}

class GuiShopBackgroundMiddleBottom: GuiElementData
{
        public GuiShopBackgroundMiddleBottom()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.194f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.002f;

        _UvLeft = 0;
        _UvTop = 1004; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundMiddleBottom(); return _Instance; } }
}

class GuiShopBackgroundDownTop: GuiElementData
{
        public GuiShopBackgroundDownTop()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.184f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.002f;

        _UvLeft = 0;
        _UvTop = 1004; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundDownTop(); return _Instance; } }
}


class GuiShopBackgroundDownMiddle: GuiElementData
{
        public GuiShopBackgroundDownMiddle()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.126f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.058f;

        _UvLeft = 0;
        _UvTop = 998; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundDownMiddle(); return _Instance; } }
}

class GuiShopBackgroundDownBottom: GuiElementData
{
        public GuiShopBackgroundDownBottom()
    {
        _ScreenLeft = 0.112f;
        _ScreenBottom = 0.124f;
        _ScreenWidth = 0.800f;
        _ScreenHeight = 0.002f;

        _UvLeft = 0;
        _UvTop = 1004; 
        _UvWidth = 800;
        _UvHeight = 2;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopBackgroundDownBottom(); return _Instance; } }
}

static class GuiShopComboButtons
{
    //static public Sprite Sprite;
    enum E_Button
    {
        Money,
        SwordLv,
        Combo1,
        Combo2,
        Combo3,
        Combo4,
        Combo5,
        Combo6,
    }

    static public Sprite[] Buttons = new Sprite[8];
    static public Sprite[] Captions = new Sprite[8];

    static public float[] _BScreenLeft = {0.158f,0.524f,0.158f,0.524f,0.158f,0.524f,0.158f,0.524f};
    static public float[] _BScreenBottom = {0.716f,0.716f,0.488f,0.488f,0.346f,0.346f,0.204f,0.204f};
    static public float _BScreenWidth = 0.346f;
    static public float _BScreenHeight = 0.14f;

    static public int BScreenLeft(int index) { return Mathf.CeilToInt(Screen.width * _BScreenLeft[index]); }
    static public int BScreenBottom(int index)  { return Mathf.CeilToInt(Screen.height * _BScreenBottom[index]);}
    static public int BScreenWidth(int index) { return Mathf.CeilToInt(Screen.width * _BScreenWidth); }
    static public int BScreenHeight(int index) { return Mathf.CeilToInt(_BScreenHeight == -1.0f ? BScreenWidth(index) : Screen.height * _BScreenHeight);}

    static public int BUvLeft = 0;
    static public int BUvBottom =984;
    static public int BUvWidth = 346;
    static public int BUvHeight = 80;

    static public float[] _CScreenLeft = {0.166f,0.532f,0.166f,0.532f,0.166f,0.532f,0.166f,0.532f};
    static public float[] _CScreenBottom = {0.766f,0.766f,0.560f,0.560f,0.420f,0.420f,0.280f,0.280f};
    static public float _CScreenWidth = 0.248f;
    static public float _CScreenHeight = 0.040f;

    static public int CScreenLeft(int index) { return Mathf.CeilToInt(Screen.width * _CScreenLeft[index]); }
    static public int CScreenBottom(int index) { return Mathf.CeilToInt(Screen.height * _CScreenBottom[index]); }
    static public int CScreenWidth() { return Mathf.CeilToInt(Screen.width * _CScreenWidth); }
    static public int CScreenHeight() { return Mathf.CeilToInt(_CScreenHeight == -1.0f ? CScreenWidth() : Screen.height * _CScreenHeight); }

    static public float[] _IScreenLeft = {0.178f,0.546f,0.178f,0.546f,0.178f,0.546f};
    static public float[] _IScreenBottom = {0.516f,0.516f,0.376f,0.376f,0.236f,0.236f};
    static public float _IScreenWidth = 0.024f;
    static public float _IScreenHeight = -1;

    static public int IScreenLeft(int index) { return Mathf.CeilToInt(Screen.width * _IScreenLeft[index]); }
    static public int IScreenBottom(int index) { return Mathf.CeilToInt(Screen.height * _IScreenBottom[index]); }
    static public int IScreenWidth() { return Mathf.CeilToInt(Screen.width * _IScreenWidth); }
    static public int IScreenHeight() { return Mathf.CeilToInt(_IScreenHeight == -1.0f ? IScreenWidth() : Screen.height * _IScreenHeight); }

// big lock coordinaty
    static public float _CLScreenLeft = 0.668f;
    static public float _CLScreenBottom = 0.550f;
    static public float _CLScreenWidth = 0.06f;
    static public float _CLScreenHeight = -1;

    static public int CLScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CLScreenLeft); } }
    static public int CLScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CLScreenBottom); } }
    static public int CLScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CLScreenWidth); } }
    static public int CLScreenHeight { get { return Mathf.CeilToInt(_CLScreenHeight == -1 ? CLScreenWidth : Screen.height * _CLScreenHeight); } }


    static public float[] _LScreenLeft = {0.0f,0.0f,0.304f,0.668f,0.304f,0.668f};
    static public float[] _LScreenBottom = { 0, 0, 0.38f, 0.38f, 0.240f, 0.24f};
    static public float _LScreenWidth = 0.064f;
    static public float _LScreenHeight = -1;

    static public int LScreenLeft(int index) { return Mathf.CeilToInt(Screen.width * _LScreenLeft[index]); }
    static public int LScreenBottom(int index) { return Mathf.CeilToInt(Screen.height * _LScreenBottom[index]); }
    static public int LScreenWidth() { return Mathf.CeilToInt(Screen.width * _LScreenWidth); }
    static public int LScreenHeight() { return Mathf.CeilToInt(_LScreenHeight == -1 ? LScreenWidth() : Screen.height * _LScreenHeight); }

    static public int[] CUvLeft = {348,348,0,0,0,0,0,0};
    static public int[] CUvBottom = {956, 984, 634,754,724, 694, 664, 784 };
    static public int CUvWidth = 248;
    static public int CUvHeight = 30;

    static public Vector2 GetCenter(int buttonIndex) { return new Vector2(BScreenLeft(buttonIndex) * Screen.width + BScreenWidth(buttonIndex) * Screen.width * 0.5f, BScreenBottom(buttonIndex) * Screen.height + BScreenHeight(buttonIndex) * Screen.height * 0.5f); }
}

static class GuiShopIconX
{
    static public int UvLeft = 346;
    static public int UvTop = 924;
    static public int UvWidth = 32;
    static public int UvHeight = 30;
}

static class GuiShopIconY
{
    static public int UvLeft = 378;
    static public int UvTop = 924;
    static public int UvWidth = 32;
    static public int UvHeight = 30;
}

static class GuiShopIconLock
{
    static public int UvLeft = 410;
    static public int UvTop = 924;
    static public int UvWidth = 32;
    static public int UvHeight = 30;
}

static class GuiShopIconBigLock
{
    static public int UvLeft = 254;
    static public int UvTop = 744;
    static public int UvWidth = 62;
    static public int UvHeight = 64;
}

class GuiShopButtonOk: GuiElementData
{
        public GuiShopButtonOk()
    {
        _ScreenLeft = 0.698f;
        _ScreenBottom = 0.136f;
        _ScreenWidth = 0.180f;
        _ScreenHeight = 0.05f;

        _UvLeft = 330;
        _UvTop = 694; 
        _UvWidth = 180;
        _UvHeight = 40;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopButtonOk(); return _Instance; } }
}

class GuiShopButtonBack: GuiElementData
{
    public GuiShopButtonBack()
    {
        _ScreenLeft = 0.160f;
        _ScreenBottom = 0.136f;
        _ScreenWidth = 0.160f;
        _ScreenHeight = 0.05f;

        _UvLeft = 528;
        _UvTop = 762; 
        _UvWidth = 160;
        _UvHeight = 40;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiShopButtonBack(); return _Instance; } }
}

static class GuiShopNumbers
{
    static public int UvLeft = 346;
    static public int UvTop = 894;
    static public int UvWidth = 24;
    static public int UvHeight = 26;
}

static class GuiHealthLevel
{
    static public float _ScreenLeft = 0.342f;
    static public float _ScreenBottom = 0.778f;
    static public float _ScreenWidth = 0.022f;
    static public float _ScreenHeight = 0.030f;

    static public int ScreenLeft { get { return Mathf.CeilToInt(Screen.width * _ScreenLeft); } }
    static public int ScreenBottom { get { return Mathf.CeilToInt(Screen.height * _ScreenBottom); } }
    static public int ScreenWidth { get { return Mathf.CeilToInt(Screen.width * _ScreenWidth); } }
    static public int ScreenHeight { get { return Mathf.CeilToInt(_ScreenHeight == -1 ? ScreenWidth : Screen.height * _ScreenHeight); } }
}

static class GuiHealthNumbers
{
    static public float _ScreenLeft = 0.388f;
    static public float _ScreenBottom = 0.778f;
    static public float _ScreenWidth = 0.024f;
    static public float _ScreenHeight = 0.03f;

    static public int ScreenLeft { get { return Mathf.CeilToInt(Screen.width * _ScreenLeft); } }
    static public int ScreenBottom { get { return Mathf.CeilToInt(Screen.height * _ScreenBottom); } }
    static public int ScreenWidth { get { return Mathf.CeilToInt(Screen.width * _ScreenWidth); } }
    static public int ScreenHeight { get { return Mathf.CeilToInt(_ScreenHeight == -1 ? ScreenWidth : Screen.height * _ScreenHeight); } }
}

static class GuiSwordLevel
{
    static public float _ScreenLeft = 0.698f;
    static public float _ScreenBottom = 0.778f;
    static public float _ScreenWidth = 0.020f;
    static public float _ScreenHeight = -1;

    static public int ScreenLeft { get { return Mathf.CeilToInt(Screen.width * _ScreenLeft); } }
    static public int ScreenBottom { get { return Mathf.CeilToInt(Screen.height * _ScreenBottom); } }
    static public int ScreenWidth { get { return Mathf.CeilToInt(Screen.width * _ScreenWidth); } }
    static public int ScreenHeight { get { return Mathf.CeilToInt(_ScreenHeight == -1 ? ScreenWidth : Screen.height * _ScreenHeight); } }
}

static class GuiSwordNumbers
{
    static public float _ScreenLeft = 0.754f;
    static public float _ScreenBottom = 0.778f;
    static public float _ScreenWidth = 0.024f;
    static public float _ScreenHeight = -1;

    static public int ScreenLeft { get { return Mathf.CeilToInt(Screen.width * _ScreenLeft); } }
    static public int ScreenBottom { get { return Mathf.CeilToInt(Screen.height * _ScreenBottom); } }
    static public int ScreenWidth { get { return Mathf.CeilToInt(Screen.width * _ScreenWidth); } }
    static public int ScreenHeight { get { return Mathf.CeilToInt(_ScreenHeight == -1 ? ScreenWidth : Screen.height * _ScreenHeight); } }
}

static class GuiComboNumbers
{
    static public float[] _ScreenLeft = {0.388f,0.754f,0.388f,0.754f,0.388f,0.754f};
    static public float[] _ScreenBottom = { 0.516f, 0.516f, 0.376f, 0.376f, 0.236f, 0.236f };
    static public float _ScreenWidth = 0.024f;
    static public float _ScreenHeight = -1;

    static public int ScreenLeft(int index) { return Mathf.CeilToInt(Screen.width * _ScreenLeft[index]); }
    static public int ScreenBottom(int index) {  return Mathf.CeilToInt(Screen.height * _ScreenBottom[index]); }
    static public int ScreenWidth { get { return Mathf.CeilToInt(Screen.width * _ScreenWidth); } }
    static public int ScreenHeight { get { return Mathf.CeilToInt(_ScreenHeight == -1 ? ScreenWidth : Screen.height * _ScreenHeight); } }
}


static class GuiShopBuyInfo
{
    static public float _SScreenLeft = 0.0f;
    static public float _SScreenBottom = 0.0f;
    static public float _SScreenWidth = 1;
    static public float _SScreenHeight = -1;

    static public int SScreenLeft { get { return Mathf.CeilToInt(Screen.width * _SScreenLeft);}}
    static public int SScreenBottom { get { return Mathf.CeilToInt(Screen.height * _SScreenBottom); }}
    static public int SScreenWidth { get { return Mathf.CeilToInt(Screen.width * _SScreenWidth); } }
    static public int SScreenHeight { get { return Mathf.CeilToInt(_SScreenHeight == -1 ? SScreenWidth : Screen.height * _SScreenHeight); } }

    static public int SUvLeft = 816;
    static public int SUvTop = 1022;
    static public int SUvWidth = 1;
    static public int SUvHeight = 1;

    static public float _BScreenLeft = 0.250f;
    static public float _BScreenBottom = 0.35f;
    static public float _BScreenWidth = 0.500f;
    static public float _BScreenHeight = 0.300f;

    static public int BScreenLeft { get { return Mathf.CeilToInt(Screen.width * _BScreenLeft);}}
    static public int BScreenBottom { get { return Mathf.CeilToInt(Screen.height * _BScreenBottom); }}
    static public int BScreenWidth { get { return Mathf.CeilToInt(Screen.width * _BScreenWidth); } }
    static public int BScreenHeight { get { return Mathf.CeilToInt(_BScreenHeight == -1 ? BScreenWidth : Screen.height * _BScreenHeight); } }


    static public int BUvLeft = 512;
    static public int BUvTop = 784;
    static public int BUvWidth = 512;
    static public int BUvHeight = 230;

    static public float _CScreenLeft = 0.375f;
    static public float _CScreenBottom = 0.564f;
    static public float _CScreenWidth = 0.250f;
    static public float _CScreenHeight = 0.040f;
    
    static public int CScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CScreenLeft);}}
    static public int CScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CScreenBottom); }}
    static public int CScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CScreenWidth); } }
    static public int CScreenHeight { get { return Mathf.CeilToInt(_CScreenHeight == -1 ? CScreenWidth : Screen.height * _CScreenHeight); } }


    static public float _IScreenLeft = 0.290f;
    static public float _IScreenBottom = 0.472f;
    static public float _IScreenWidth = 0.420f;
    static public float _IScreenHeight = 0.080f;

    static public int IScreenLeft { get { return Mathf.CeilToInt(Screen.width * _IScreenLeft);}}
    static public int IScreenBottom { get { return Mathf.CeilToInt(Screen.height * _IScreenBottom); }}
    static public int IScreenWidth { get { return Mathf.CeilToInt(Screen.width * _IScreenWidth); } }
    static public int IScreenHeight { get { return Mathf.CeilToInt(_IScreenHeight == -1 ? IScreenWidth : Screen.height * _IScreenHeight); } }

    static public float _OkScreenLeft = 0.652f;
    static public float _OkScreenBottom = 0.345f;
    static public float _OkScreenWidth = 0.080f;
    static public float _OkScreenHeight = 0.118f;

    static public int OkScreenLeft { get { return Mathf.CeilToInt(Screen.width * _OkScreenLeft);}}
    static public int OkScreenBottom { get { return Mathf.CeilToInt(Screen.height * _OkScreenBottom); }}
    static public int OkScreenWidth { get { return Mathf.CeilToInt(Screen.width * _OkScreenWidth); } }
    static public int OkScreenHeight { get { return Mathf.CeilToInt(_OkScreenHeight == -1 ? OkScreenWidth : Screen.height * _OkScreenHeight); } }


    static public float _CancelScreenLeft = 0.270f;
    static public float _CancelScreenBottom = 0.366f;
    static public float _CancelScreenWidth = 0.116f;
    static public float _CancelScreenHeight = 0.080f;
    
    static public int CancelScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CancelScreenLeft);}}
    static public int CancelScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CancelScreenBottom); }}
    static public int CancelScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CancelScreenWidth); } }
    static public int CancelScreenHeight { get { return Mathf.CeilToInt(_CancelScreenHeight == -1 ? CancelScreenWidth : Screen.height * _CancelScreenHeight); } }

    static public int IUvLeft = 602;
    static public int[] IUvTop = {406,464,58,116,174,232,290,348};
    static public int IUvWidth = 424;
    static public int IUvHeight = 58;

    static public int OkUvLeft = 514;
    static public int OkUvTop = 448;
    static public int OkUvWidth = 80;
    static public int OkUvHeight = 88;
}

class GuiButtonShop: GuiElementData
{
     public GuiButtonShop()
    {
        _ScreenLeft = 0.06f;
        _ScreenBottom = 0.85f;
        _ScreenWidth = 0.06f;
        _ScreenHeight = -1;

        _UvLeft = 364;
        _UvTop = 114; 
        _UvWidth = 114;
        _UvHeight = 114;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiButtonShop(); return _Instance; } }
}

class GuiButtonInGameMenu: GuiElementData
{
        public GuiButtonInGameMenu()
    {
        _ScreenLeft = 0.95f;
        _ScreenBottom = 0.90f;
        _ScreenWidth = 0.050f;
        _ScreenHeight = 0.1f;

        _UvLeft = 388;
        _UvTop = 624; 
        _UvWidth = 58;
        _UvHeight = 66;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiButtonInGameMenu(); return _Instance; } }
}

class GuiMoneyNumbers: GuiElementData
{
        public GuiMoneyNumbers()
    {
        _ScreenLeft = 0.272f;
        _ScreenBottom = 0.830f;
        _ScreenWidth = 0.026f;
        _ScreenHeight = 0.05f;

        _UvLeft = 252;
        _UvTop = 782; 
        _UvWidth = 26;
        _UvHeight = 36;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiMoneyNumbers(); return _Instance; } }
}

class GuiHitNumbers: GuiElementData
{
    public GuiHitNumbers()
    {
        _ScreenLeft = 0.802f;
        _ScreenBottom = 0.79f;
        _ScreenWidth = 0.050f;
        _ScreenHeight = 0.12f;

        _UvLeft = 0;
        _UvTop = 496; 
        _UvWidth = 50;
        _UvHeight = 76;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiHitNumbers(); return _Instance; } }
}

static class GuiHealthBar
{
    static public float _BScreenLeft = 0.028f;
    static public float _BScreenBottom = 0.820f;
    static public float _BScreenWidth = 0.360f;
    static public float _BScreenHeight = 0.166f;

    static public int BScreenLeft { get { return Mathf.CeilToInt(Screen.width * _BScreenLeft); } }
    static public int BScreenBottom { get { return Mathf.CeilToInt(Screen.height * _BScreenBottom); } }
    static public int BScreenWidth { get { return Mathf.CeilToInt(Screen.width * _BScreenWidth); } }
    static public int BScreenHeight { get { return Mathf.CeilToInt(_BScreenHeight == -1 ? BScreenWidth : Screen.height * _BScreenHeight); } }


    static public int BUvLeft = 0;
    static public int BUvTop = 594;
    static public int BUvWidth = 360;
    static public int BUvHeight = 96;


    static public float _HScreenLeft = 0.124f;
    static public float _HScreenBottom = 0.898f;
    static public float _HScreenWidth = 0.212f;
    static public float _HScreenHeight = 0.04f;

    static public int HScreenLeft { get { return Mathf.CeilToInt(Screen.width * _HScreenLeft); } }
    static public int HScreenBottom { get { return Mathf.CeilToInt(Screen.height * _HScreenBottom); } }
    static public int HScreenWidth { get { return Mathf.CeilToInt(Screen.width * _HScreenWidth); } }
    static public int HScreenHeight { get { return Mathf.CeilToInt(_HScreenHeight == -1 ? HScreenWidth : Screen.height * _HScreenHeight); } }


    static public int HUvLeft = 372;
    static public int HUvTop = 594;
    static public int HUvWidth = 1;
    static public int HUvHeight = 24;

    static public float _HBScreenLeft = 0.124f;
    static public float _HBScreenBottom = 0.898f;
    static public float _HBScreenWidth = 0.212f;
    static public float _HBScreenHeight = 0.04f;

    static public int HBScreenLeft { get { return Mathf.CeilToInt(Screen.width * _HBScreenLeft); } }
    static public int HBScreenBottom { get { return Mathf.CeilToInt(Screen.height * _HBScreenBottom); } }
    static public int HBScreenWidth { get { return Mathf.CeilToInt(Screen.width * _HBScreenWidth); } }
    static public int HBScreenHeight { get { return Mathf.CeilToInt(_HBScreenHeight == -1 ? HBScreenWidth : Screen.height * _HBScreenHeight); } }

    static public int HBUvLeft = 378;
    static public int HBUvTop = 594;
    static public int HBUvWidth = 1;
    static public int HBUvHeight = 24;

}

static class GuiCombatProgress
{
    static public float _CPScreenLeft = 0.896f;
    static public float _CPScreenBottom = 0.604f;
    static public float _CPScreenWidth = 0.030f;
    static public float _CPScreenHeight = -1;

    static public int CPScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CPScreenLeft); } }
    static public int CPScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CPScreenBottom); } }
    static public int CPScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CPScreenWidth); } }
    static public int CPScreenHeight { get { return Mathf.CeilToInt(_CPScreenHeight == -1 ? CPScreenWidth : Screen.height * _CPScreenHeight); } }

    static public int XUvLeft = 346;
    static public int XUvTop = 866;
    static public int XUvWidth = 30;
    static public int XUvHeight = 36;

    static public int OUvLeft = 376;
    static public int OUvTop = 866;
    static public int OUvWidth = 30;
    static public int OUvHeight = 36;

    static public int LUvLeft = 436;
    static public int LUvTop = 866;
    static public int LUvWidth = 30;
    static public int LUvHeight = 36;

    static public int FUvLeft = 406;
    static public int FUvTop = 866;
    static public int FUvWidth = 30;
    static public int FUvHeight = 36;

    static public float _CMScreenLeft = 0.712f;
    static public float _CMScreenBottom = 0.742f;
    static public float _CMScreenWidth = 0.170f;
    static public float _CMScreenHeight = 0.11f;

    static public int CMScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CMScreenLeft); } }
    static public int CMScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CMScreenBottom); } }
    static public int CMScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CMScreenWidth); } }
    static public int CMScreenHeight { get { return Mathf.CeilToInt(_CMScreenHeight == -1 ? CMScreenWidth : Screen.height * _CMScreenHeight); } }


    static public int[] CMUvLeft = {684, 854, 856, 514, 856, 686};
    static public int[] CMUvTop = {554, 554, 936, 554, 1024, 984};
    static public int CMUvWidth = 170;
    static public int CMUvHeight = 88;

    static public float _HScreenLeft = 0.730f;
    static public float _HScreenBottom = 0.68f;
    static public float _HScreenWidth = 0.16f;
    static public float _HScreenHeight = 0.16f;

    static public int HScreenLeft { get { return Mathf.CeilToInt(Screen.width * _HScreenLeft); } }
    static public int HScreenBottom { get { return Mathf.CeilToInt(Screen.height * _HScreenBottom); } }
    static public int HScreenWidth { get { return Mathf.CeilToInt(Screen.width * _HScreenWidth); } }
    static public int HScreenHeight { get { return Mathf.CeilToInt(_HScreenHeight == -1 ? HScreenWidth : Screen.height * _HScreenHeight); } }


    static public int HUvLeft = 366;
    static public int HUvTop = 416;
    static public int HUvWidth = 144;
    static public int HUvHeight = 72;
}


static class GuiInGameMenu
{
    static public Sprite SpriteResume;
    static public Sprite SpriteQuit;
    static public Sprite SpriteControls;

    static public Sprite SpriteControlsOk;
    static public Sprite SpriteControlsCancel;
    static public Sprite SpriteControlsReset;
    static public Sprite SpriteControlsBackground;

    static public float _COKScreenLeft = 0.810f;
    static public float _COKScreenBottom = 0.026f;
    static public float _COKScreenWidth = 0.164f;
    static public float _COKScreenHeight = 0.056f;

    static public int COKScreenLeft { get { return Mathf.CeilToInt(Screen.width * _COKScreenLeft); } }
    static public int COKScreenBottom { get { return Mathf.CeilToInt(Screen.height * _COKScreenBottom); } }
    static public int COKScreenWidth { get { return Mathf.CeilToInt(Screen.width * _COKScreenWidth); } }
    static public int COKScreenHeight { get { return Mathf.CeilToInt(_COKScreenHeight == -1 ? COKScreenWidth : Screen.height * _COKScreenHeight); } }

    static public int COKUvLeft = 326;
    static public int COKUvTop = 694;
    static public int COKUvWidth = 164;
    static public int COKUvHeight = 36;

    static public float _CCScreenLeft = 0.050f;
    static public float _CCScreenBottom = 0.028f;
    static public float _CCScreenWidth = 0.144f;
    static public float _CCScreenHeight = 0.056f;

    static public int CCScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CCScreenLeft); } }
    static public int CCScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CCScreenBottom); } }
    static public int CCScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CCScreenWidth); } }
    static public int CCScreenHeight { get { return Mathf.CeilToInt(_CCScreenHeight == -1 ? CCScreenWidth : Screen.height * _CCScreenHeight); } }

    static public int CCUvLeft = 528;
    static public int CCUvTop = 760;
    static public int CCUvWidth = 144;
    static public int CCUvHeight = 36;

    static public float _CRScreenLeft = 0.466f;
    static public float _CRScreenBottom = 0.028f;
    static public float _CRScreenWidth = 0.110f;
    static public float _CRScreenHeight = 0.056f;

    static public int CRScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CRScreenLeft); } }
    static public int CRScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CRScreenBottom); } }
    static public int CRScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CRScreenWidth); } }
    static public int CRScreenHeight { get { return Mathf.CeilToInt(_CRScreenHeight == -1 ? CRScreenWidth : Screen.height * _CRScreenHeight); } }

    static public int CRUvLeft = 236;
    static public int CRUvTop = 822;
    static public int CRUvWidth = 110;
    static public int CRUvHeight = 36;

    static public float _CBScreenLeft = 0;
    static public float _CBScreenBottom = 0.02f;
    static public float _CBScreenWidth = 1;
    static public float _CBScreenHeight = 0.068f;

    static public int CBScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CBScreenLeft); } }
    static public int CBScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CBScreenBottom); } }
    static public int CBScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CBScreenWidth); } }
    static public int CBScreenHeight { get { return Mathf.CeilToInt(_CBScreenHeight == -1 ? CBScreenWidth : Screen.height * _CBScreenHeight); } }

    static public int CBUvLeft = 808;
    static public int CBUvTop = 1022;
    static public int CBUvWidth = 1;
    static public int CBUvHeight = 1;

    static public float _RScreenLeft = 0.380f;
    static public float _RScreenBottom = 0.6f;
    static public float _RScreenWidth = 0.266f;
    static public float _RScreenHeight = 0.086f;

    static public int RScreenLeft { get { return Mathf.CeilToInt(Screen.width * _RScreenLeft); } }
    static public int RScreenBottom { get { return Mathf.CeilToInt(Screen.height * _RScreenBottom); } }
    static public int RScreenWidth { get { return Mathf.CeilToInt(Screen.width * _RScreenWidth); } }
    static public int RScreenHeight { get { return Mathf.CeilToInt(_RScreenHeight == -1 ? RScreenWidth : Screen.height * _RScreenHeight); } }

    static public int RUvLeft = 0;
    static public int RUvTop = 882;
    static public int RUvWidth = 266;
    static public int RUvHeight = 56;

    static public float _CScreenLeft = 0.380f;
    static public float _CScreenBottom = 0.48f;
    static public float _CScreenWidth = 0.266f;
    static public float _CScreenHeight = 0.086f;

    static public int CScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CScreenLeft); } }
    static public int CScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CScreenBottom); } }
    static public int CScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CScreenWidth); } }
    static public int CScreenHeight { get { return Mathf.CeilToInt(_CScreenHeight == -1 ? CScreenWidth : Screen.height * _CScreenHeight); } }

    static public int CUvLeft = 492;
    static public int CUvTop = 840;
    static public int CUvWidth = 266;
    static public int CUvHeight = 56;
	
	static public float _QScreenLeft = 0.380f;
    static public float _QScreenBottom = 0.36f;
    static public float _QScreenWidth = 0.266f;
    static public float _QScreenHeight = 0.086f;

    static public int QScreenLeft { get { return Mathf.CeilToInt(Screen.width * _QScreenLeft); } }
    static public int QScreenBottom { get { return Mathf.CeilToInt(Screen.height * _QScreenBottom); } }
    static public int QScreenWidth { get { return Mathf.CeilToInt(Screen.width * _QScreenWidth); } }
    static public int QScreenHeight { get { return Mathf.CeilToInt(_QScreenHeight == -1 ? QScreenWidth : Screen.height * _QScreenHeight); } }
    
    static public int QUvLeft = 758;
    static public int QUvTop = 840;
    static public int QUvWidth = 266;
    static public int QUvHeight = 56;
}

class GuiSaving: GuiElementData
{
        public GuiSaving()
    {
        _ScreenLeft = 0.04f;
        _ScreenBottom = 0.760f;
        _ScreenWidth = 0.154f;
        _ScreenHeight = 0.064f;

        _UvLeft = 336;
        _UvTop = 742; 
        _UvWidth = 154;
        _UvHeight = 44;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiSaving(); return _Instance; } }
}

class GuiMessageDeath: GuiElementData
{
        public GuiMessageDeath()
    {
        _ScreenLeft = 0.334f;
        _ScreenBottom = 0.508f;
        _ScreenWidth = 0.360f;
        _ScreenHeight = 0.148f;

        _UvLeft = 0;
        _UvTop = 418; 
        _UvWidth = 360;
        _UvHeight = 98;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiMessageDeath(); return _Instance; } }
}

class GuiMessageHajime: GuiElementData
{
    public GuiMessageHajime()
    {
        _ScreenLeft = 0.334f;
        _ScreenBottom = 0.508f;
        _ScreenWidth = 0.360f;
        _ScreenHeight = 0.118f;

        _UvLeft = 0;
        _UvTop = 320; 
        _UvWidth = 360;
        _UvHeight = 72;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiMessageHajime(); return _Instance; } }
}

class GuiMessageRoundDone: GuiElementData
{
    public GuiMessageRoundDone()
    {
         _ScreenLeft = 0.334f;
        _ScreenBottom = 0.508f;
        _ScreenWidth = 0.360f;
        _ScreenHeight = 0.118f;

        _UvLeft = 0;
        _UvTop = 246; 
        _UvWidth = 360;
        _UvHeight = 72;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiMessageRoundDone(); return _Instance; } }
}

static class GuiBlood
{
    static private float[] _BloodLeft = { 0.185f, 0.200f, 0.250f, 0.367f, 0.391f, 0.343f, 0.520f, 0.480f, 0.484f, 0.520f, 0.575f, 0.688f, 0.660f, 0.670f, 0.665f, 0.805f };
    static private float[] _BloodBottom = { 0.406f, 0.620f, 0.478f, 0.667f, 0.461f, 0.310f, 0.720f, 0.570f, 0.390f, 0.260f, 0.455f, 0.781f, 0.600f, 0.356f, 0.236f, 0.530f };
    static private float _Size = 0.084f;

    static public int BloodLeft(int index) { return Mathf.CeilToInt(Screen.width * _BloodLeft[index]); }
    static public int BloodBottom(int index) { return Mathf.CeilToInt(Screen.height * _BloodBottom[index]); }
    static public int BloodSize { get { return Mathf.CeilToInt(Screen.width * _Size); } }

    static public int[] UvLeft = {0,0,84,84};
    static public int[] UvTop = {84,168,84,168};
    static public int UvWidth = 84;
    static public int UvHeight = 84;
}


class GuiScore: GuiElementData
{
    public GuiScore()
    {
        _ScreenLeft = 0.0f;
        _ScreenBottom = 0.0f;
        _ScreenWidth = 1.0f;
        _ScreenHeight = 1.0f;

        _UvLeft = 0;
        _UvTop = 766; 
        _UvWidth = 1024;
        _UvHeight = 768;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiScore(); return _Instance; } }
}

class GuiScoreAchievements: GuiElementData
{
    public GuiScoreAchievements()
    {
        _ScreenLeft = 0.405f;
        _ScreenBottom = 0.038f;
        _ScreenWidth = 0.227f;
        _ScreenHeight = 0.47f;

        _UvLeft = 405;
        _UvTop = 940; 
        _UvWidth = 227;
        _UvHeight = 47;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiScoreAchievements(); return _Instance; } }
}

class GuiScoreLeaderboard: GuiElementData
{
        public GuiScoreLeaderboard()
    {
        _ScreenLeft = 0.016f;
        _ScreenBottom = 0.038f;
        _ScreenWidth = 0.227f;
        _ScreenHeight = 0.047f;

        _UvLeft = 16;
        _UvTop = 940; 
        _UvWidth = 227;
        _UvHeight = 47;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiScoreLeaderboard(); return _Instance; } }
}

class GuiScoreContinue: GuiElementData
{
    public GuiScoreContinue()
    {
        _ScreenLeft = 0.799f;
        _ScreenBottom = 0.038f;
        _ScreenWidth = 0.227f;
        _ScreenHeight = 0.077f;

        _UvLeft = 799;
        _UvTop = 940; 
        _UvWidth = 227;
        _UvHeight = 47;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiScoreContinue(); return _Instance; } }
}

static class GuiScoreNumbers
{
    static public int UvLeft = 2;
    static public int UvTop = 837;
    static public int UvWidth = 16;
    static public int UvHeight = 32;
}

static class GuiScoreNumbersFinal
{
    static public int UvLeft = 2;
    static public int UvTop = 805;
    static public int UvWidth = 24;
    static public int UvHeight = 35;
}

static class GuiScoreTable
{
    static public float _CountScreenLeft = 0.580f;
    static public float _CountScreenBottom = 0.685f;
    static public float _CountScreenWidth = 0.016f;
    static public float _CountScreenHeight = 0.041f;

    static public int CountScreenLeft { get { return Mathf.CeilToInt(Screen.width * _CountScreenLeft); } }
    static public int CountScreenBottom { get { return Mathf.CeilToInt(Screen.height * _CountScreenBottom); } }
    static public int CountScreenWidth { get { return Mathf.CeilToInt(Screen.width * _CountScreenWidth); } }
    static public int CountScreenHeight { get { return Mathf.CeilToInt(_CountScreenHeight == -1 ? CountScreenWidth : Screen.height * _CountScreenHeight); } }

    static public float _ScoreScreenLeft = 0.764f;
    static public float _ScoreScreenBottom = 0.685f;
    static public float _ScoreScreenWidth = 0.016f;
    static public float _ScoreScreenHeight = 0.041f;

    static public int ScoreScreenLeft { get { return Mathf.CeilToInt(Screen.width * _ScoreScreenLeft); } }
    static public int ScoreScreenBottom { get { return Mathf.CeilToInt(Screen.height * _ScoreScreenBottom); } }
    static public int ScoreScreenWidth { get { return Mathf.CeilToInt(Screen.width * _ScoreScreenWidth); } }
    static public int ScoreScreenHeight { get { return Mathf.CeilToInt(_ScoreScreenHeight == -1 ? ScoreScreenWidth : Screen.height * _ScoreScreenHeight); } }

    static public float _FinalScreenLeft = 0.764f;
    static public float _FinalScreenBottom = 0.27f;
    static public float _FinalScreenWidth = 0.020f;
    static public float _FinalScreenHeight = 0.05f;

    static public int FinalScreenLeft { get { return Mathf.CeilToInt(Screen.width * _FinalScreenLeft); } }
    static public int FinalScreenBottom { get { return Mathf.CeilToInt(Screen.height * _FinalScreenBottom); } }
    static public int FinalScreenWidth { get { return Mathf.CeilToInt(Screen.width * _FinalScreenWidth); } }
    static public int FinalScreenHeight { get { return Mathf.CeilToInt(_FinalScreenHeight == -1 ? FinalScreenWidth : Screen.height * _FinalScreenHeight); } }

    static public float _RowHeight = 0.041f;
    static public int RowHeight { get { return Mathf.CeilToInt(Screen.height * _RowHeight); } }
}

class GuiScoreLoading : GuiElementData
{
    public GuiScoreLoading()
    {
        _ScreenLeft = 0.324f;
        _ScreenBottom = 0.396f;
        _ScreenWidth = 0.398f;
        _ScreenHeight = 0.118f;

        _UvLeft = 586;
        _UvTop = 88; 
        _UvWidth = 398;
        _UvHeight = 118;
        
        _Instance = this;
    }
    
    static GuiElementData _Instance;
    static public GuiElementData Instance { get { if (_Instance == null) _Instance = new GuiScoreLoading(); return _Instance; } }
}

static class GuiScoreBlack
{
    static public int UvLeft = 1020;
    static public int UvTop = 828;
    static public int UvWidth = 1;
    static public int UvHeight = 1;
}

class GuiObjectsData { }
