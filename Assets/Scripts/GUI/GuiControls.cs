using UnityEngine;
using System.Collections;
using System;

    
[System.Serializable]
public class GuiControls
{
	public AudioClip[] ButtonSounds = new AudioClip[1];

    private SpriteUI DefaultSpriteUI = null;

    private GuiManager  GuiManager;

    private bool On = false;

    public void Start (GuiManager manager)
	{
        GuiManager = manager;
        DefaultSpriteUI = GuiManager.DefaultSpriteUI;

        GuiButtonX.Instance.Sprite = DefaultSpriteUI.AddElement(Game.Instance.ButtonXPositon, GuiButtonX.Instance.ScreenWidth, GuiButtonX.Instance.ScreenHeight, 9, GuiButtonX.Instance.UvLeft, GuiButtonX.Instance.UvTop, GuiButtonX.Instance.UvWidth, GuiButtonX.Instance.UvHeight);
        GuiButtonY.Instance.Sprite = DefaultSpriteUI.AddElement(Game.Instance.ButtonOPositon, GuiButtonY.Instance.ScreenWidth, GuiButtonY.Instance.ScreenHeight, 9, GuiButtonY.Instance.UvLeft, GuiButtonY.Instance.UvTop, GuiButtonY.Instance.UvWidth, GuiButtonY.Instance.UvHeight);
        GuiButtonUse.Instance.Sprite = DefaultSpriteUI.AddElement(Game.Instance.ButtonOPositon, GuiButtonUse.Instance.ScreenWidth, GuiButtonUse.Instance.ScreenHeight, 9, GuiButtonUse.Instance.UvLeft, GuiButtonUse.Instance.UvTop, GuiButtonUse.Instance.UvWidth, GuiButtonUse.Instance.UvHeight);
        GuiButtonRoll.Instance.Sprite = DefaultSpriteUI.AddElement(Game.Instance.ButtonRPositon, GuiButtonRoll.Instance.ScreenWidth, GuiButtonRoll.Instance.ScreenHeight, 9, GuiButtonRoll.Instance.UvLeft, GuiButtonRoll.Instance.UvTop, GuiButtonRoll.Instance.UvWidth, GuiButtonRoll.Instance.UvHeight);

        GuiJoystick.Instance.Sprite = DefaultSpriteUI.AddElement(Game.Instance.JoystickPositon, GuiJoystick.Instance.ScreenWidth, GuiJoystick.Instance.ScreenHeight, 9, GuiJoystick.Instance.UvLeft, GuiJoystick.Instance.UvTop, GuiJoystick.Instance.UvWidth, GuiJoystick.Instance.UvHeight);
        GuiJoystickHat.Sprite = DefaultSpriteUI.AddElement(Game.Instance.JoystickHatPositon, GuiJoystickHat.ScreenWidth, GuiJoystickHat.ScreenHeight, 9, GuiJoystickHat.UvLeft, GuiJoystickHat.UvTop, GuiJoystickHat.UvWidth, GuiJoystickHat.UvHeight);

        GuiButtonInGameMenu.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiButtonInGameMenu.Instance.ScreenLeft, GuiButtonInGameMenu.Instance.ScreenBottom), GuiButtonInGameMenu.Instance.ScreenWidth, GuiButtonInGameMenu.Instance.ScreenHeight, 9, GuiButtonInGameMenu.Instance.UvLeft, GuiButtonInGameMenu.Instance.UvTop, GuiButtonInGameMenu.Instance.UvWidth, GuiButtonInGameMenu.Instance.UvHeight);

        GuiButtonSelect.SpriteX = DefaultSpriteUI.AddElement(Game.Instance.ButtonXPositon, GuiButtonX.Instance.ScreenWidth, GuiButtonX.Instance.ScreenHeight, 9, GuiButtonSelect.UvLeft, GuiButtonSelect.UvTop, GuiButtonSelect.UvWidth, GuiButtonSelect.UvHeight);
        GuiButtonSelect.SpriteO = DefaultSpriteUI.AddElement(Game.Instance.ButtonOPositon, GuiButtonY.Instance.ScreenWidth, GuiButtonY.Instance.ScreenHeight, 9, GuiButtonSelect.UvLeft, GuiButtonSelect.UvTop, GuiButtonSelect.UvWidth, GuiButtonSelect.UvHeight);
        GuiButtonSelect.SpriteUse = DefaultSpriteUI.AddElement(Game.Instance.ButtonOPositon, GuiButtonUse.Instance.ScreenWidth, GuiButtonUse.Instance.ScreenHeight, 9, GuiButtonSelect.UvLeft, GuiButtonSelect.UvTop, GuiButtonSelect.UvWidth, GuiButtonSelect.UvHeight);
        GuiButtonSelect.SpriteRoll = DefaultSpriteUI.AddElement(Game.Instance.ButtonRPositon, GuiButtonRoll.Instance.ScreenWidth, GuiButtonRoll.Instance.ScreenHeight, 9, GuiButtonSelect.UvLeft, GuiButtonSelect.UvTop, GuiButtonSelect.UvWidth, GuiButtonSelect.UvHeight);
        GuiButtonSelect.SpriteJoystick = DefaultSpriteUI.AddElement(Game.Instance.JoystickPositon, GuiJoystick.Instance.ScreenWidth, GuiJoystick.Instance.ScreenHeight, 9, GuiButtonSelect.UvLeft, GuiButtonSelect.UvTop, GuiButtonSelect.UvWidth, GuiButtonSelect.UvHeight);

        // Hide();
	}

	public void Update()
	{
	}       

    public void Reset()
    {
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiJoystickHat.Sprite, 0, 0, new Vector2(-GuiJoystickHat.ScreenWidth, GuiJoystickHat.ScreenBottom)));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiJoystick.Instance.Sprite, 0, 0, new Vector2(-GuiJoystick.Instance.ScreenWidth, GuiJoystick.Instance.ScreenBottom)));

        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonX.Instance.Sprite, 0, 0, new Vector2(Screen.width + GuiButtonX.Instance.ScreenWidth, GuiButtonX.Instance.ScreenBottom)));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonY.Instance.Sprite, 0, 0, new Vector2(Screen.width + GuiButtonY.Instance.ScreenWidth, GuiButtonY.Instance.ScreenBottom)));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonRoll.Instance.Sprite, 0, 0, new Vector2(Screen.width + GuiButtonRoll.Instance.ScreenWidth, GuiButtonRoll.Instance.ScreenBottom)));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonUse.Instance.Sprite, 0, 0, new Vector2(Screen.width + GuiButtonUse.Instance.ScreenWidth, GuiButtonUse.Instance.ScreenBottom)));

        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteX);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteO);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteUse);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteRoll);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteJoystick);

        DefaultSpriteUI.HideSprite(GuiButtonInGameMenu.Instance.Sprite);
//        DefaultSpriteUI.HideSprite(GuiButtonShop.Sprite);
    }

    public void ResetControls()
    {
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteX);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteO);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteUse);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteRoll);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteJoystick);

        DefaultSpriteUI.SetSpritePosition(GuiJoystickHat.Sprite, Game.Instance.JoystickHatPositon);
    }

    public void Show()
    {
     //   Debug.Log("show");

        FadeIn();
        On = true;
    }

    public void Hide()
    {
     //   Debug.Log("hide");
        FadeOut();
        On = false;
    }

    public void ShowForCustomize()
    {
        DefaultSpriteUI.SetSpritePosition(GuiJoystickHat.Sprite, Game.Instance.JoystickHatPositon);
        DefaultSpriteUI.SetSpritePosition(GuiJoystick.Instance.Sprite, Game.Instance.JoystickPositon);
        DefaultSpriteUI.SetSpritePosition(GuiButtonX.Instance.Sprite, Game.Instance.ButtonXPositon);
        DefaultSpriteUI.SetSpritePosition(GuiButtonY.Instance.Sprite, Game.Instance.ButtonOPositon);
        DefaultSpriteUI.SetSpritePosition(GuiButtonRoll.Instance.Sprite, Game.Instance.ButtonRPositon);
    }

    public void HideAfterCustomize()
    {

        DefaultSpriteUI.SetSpritePosition(GuiButtonSelect.SpriteX, Game.Instance.ButtonXPositon);
        DefaultSpriteUI.SetSpritePosition(GuiButtonSelect.SpriteO, Game.Instance.ButtonOPositon);
        DefaultSpriteUI.SetSpritePosition(GuiButtonSelect.SpriteRoll, Game.Instance.ButtonRPositon);
        DefaultSpriteUI.SetSpritePosition(GuiButtonSelect.SpriteUse, Game.Instance.ButtonOPositon);
        DefaultSpriteUI.SetSpritePosition(GuiButtonSelect.SpriteJoystick, Game.Instance.JoystickPositon);

        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteX);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteO);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteUse);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteRoll);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteJoystick);

        DefaultSpriteUI.SetSpritePosition(GuiJoystickHat.Sprite, new Vector2(-GuiJoystickHat.ScreenWidth, GuiJoystickHat.ScreenBottom));
        DefaultSpriteUI.SetSpritePosition(GuiJoystick.Instance.Sprite, new Vector2(-GuiJoystick.Instance.ScreenWidth, GuiJoystick.Instance.ScreenBottom));
        DefaultSpriteUI.SetSpritePosition(GuiButtonX.Instance.Sprite, new Vector2(Screen.width + GuiButtonX.Instance.ScreenWidth, GuiButtonX.Instance.ScreenBottom));
        DefaultSpriteUI.SetSpritePosition(GuiButtonY.Instance.Sprite, new Vector2(Screen.width + GuiButtonY.Instance.ScreenWidth, GuiButtonY.Instance.ScreenBottom));
        DefaultSpriteUI.SetSpritePosition(GuiButtonRoll.Instance.Sprite, new Vector2(Screen.width + GuiButtonRoll.Instance.ScreenWidth, GuiButtonRoll.Instance.ScreenBottom));
    }

    public void FadeIn()
    {
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiJoystickHat.Sprite, 0.1f, 0, Game.Instance.JoystickHatPositon));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiJoystick.Instance.Sprite, 0.1f, 0, Game.Instance.JoystickPositon));

        if (Player.Instance.InUseMode == false)
        {
            GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonX.Instance.Sprite, 0.1f, 0, Game.Instance.ButtonXPositon));
            GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonY.Instance.Sprite, 0.1f, 0, Game.Instance.ButtonOPositon));
            GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonRoll.Instance.Sprite, 0.1f, 0, Game.Instance.ButtonRPositon));
        }
        else
        {
            GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonUse.Instance.Sprite, 0.1f, 0, Game.Instance.ButtonOPositon));
        }

         GuiManager.StartCoroutine(ShowSprite(GuiButtonInGameMenu.Instance.Sprite, 0.2f));
   //     GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonShop.Sprite, 0.1f, 0, new Vector2(GuiButtonShop.ScreenLeft[Game.Instance.HardwareTypeIndex], GuiButtonShop.ScreenBottom[Game.Instance.HardwareTypeIndex])));

    }


    public void FadeOut()
    {
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiJoystickHat.Sprite, 0.1f, 0, new Vector2(-GuiJoystickHat.ScreenWidth, GuiJoystickHat.ScreenBottom)));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiJoystick.Instance.Sprite, 0.1f, 0, new Vector2(-GuiJoystick.Instance.ScreenWidth, GuiJoystick.Instance.ScreenBottom)));

        if (Player.Instance.InUseMode == false)
        {
            GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonX.Instance.Sprite, 0.1f, 0, new Vector2(Screen.width + GuiButtonX.Instance.ScreenWidth, GuiButtonX.Instance.ScreenBottom)));
            GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonY.Instance.Sprite, 0.1f, 0, new Vector2(Screen.width + GuiButtonY.Instance.ScreenWidth, GuiButtonY.Instance.ScreenBottom)));
            GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonRoll.Instance.Sprite, 0.1f, 0, new Vector2(Screen.width + GuiButtonRoll.Instance.ScreenWidth, GuiButtonRoll.Instance.ScreenBottom)));
        }
        else 
            GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonUse.Instance.Sprite, 0.1f, 0, new Vector2(Screen.width + GuiButtonUse.Instance.ScreenWidth, GuiButtonUse.Instance.ScreenBottom)));

        GuiManager.StartCoroutine(DisappearSprite(GuiButtonInGameMenu.Instance.Sprite, 0.2f));

       // GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonShop.Sprite, 0.1f, 0, new Vector2(-GuiButtonShop.ScreenWidth[Game.Instance.HardwareTypeIndex], GuiButtonShop.ScreenBottom[Game.Instance.HardwareTypeIndex])));
    }

    public void SwitchToUseMode()
    {
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonX.Instance.Sprite, 0.1f, 0, new Vector2(Screen.width + GuiButtonX.Instance.ScreenWidth, GuiButtonX.Instance.ScreenBottom)));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonY.Instance.Sprite, 0.1f, 0, new Vector2(Screen.width + GuiButtonY.Instance.ScreenWidth, GuiButtonY.Instance.ScreenBottom)));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonRoll.Instance.Sprite, 0.1f, 0, new Vector2(Screen.width + GuiButtonRoll.Instance.ScreenWidth, GuiButtonRoll.Instance.ScreenBottom)));

        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonUse.Instance.Sprite, 0.1f, 0.2f, Game.Instance.ButtonOPositon));

        DefaultSpriteUI.ShowSprite(GuiButtonInGameMenu.Instance.Sprite);

        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteX);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteO);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteUse);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteRoll);
    }

    public void SwitchToCombatMode()
    {
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonUse.Instance.Sprite, 0.1f, 0, new Vector2(Screen.width + GuiButtonUse.Instance.ScreenWidth, GuiButtonUse.Instance.ScreenBottom)));

        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonX.Instance.Sprite, 0.1f, 0.2f, Game.Instance.ButtonXPositon));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonY.Instance.Sprite, 0.1f, 0.2f, Game.Instance.ButtonOPositon));
        GuiManager.StartCoroutine(DefaultSpriteUI.MoveSprite(GuiButtonRoll.Instance.Sprite, 0.1f, 0.2f, Game.Instance.ButtonRPositon));

        DefaultSpriteUI.ShowSprite(GuiButtonInGameMenu.Instance.Sprite);

        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteX);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteO);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteUse);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteRoll);
    }

    public void SwitchToIngameMenu()
    {
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteX);

        DefaultSpriteUI.HideSprite(GuiButtonX.Instance.Sprite);
        DefaultSpriteUI.HideSprite(GuiButtonY.Instance.Sprite);
        DefaultSpriteUI.HideSprite(GuiButtonRoll.Instance.Sprite);
        DefaultSpriteUI.HideSprite(GuiButtonUse.Instance.Sprite);

        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteX);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteO);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteUse);
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteRoll);
        DefaultSpriteUI.HideSprite(GuiButtonInGameMenu.Instance.Sprite);
    }

    public void JoystickDown()
    {
        DefaultSpriteUI.ShowSprite(GuiButtonSelect.SpriteJoystick);
        DefaultSpriteUI.SetSpritePosition(GuiJoystickHat.Sprite, Game.Instance.JoystickPositon);
    }

    public void JoystickUpdate(Vector2 dir)
    {
        dir.x += GuiJoystickHat.ScreenLeft  + Game.Instance.ControlsJoystickOffset.x;
        dir.y += GuiJoystickHat.ScreenBottom + Game.Instance.ControlsJoystickOffset.y;
        DefaultSpriteUI.SetSpritePosition(GuiJoystickHat.Sprite, dir);
    }

    public void JoystickUp()
    {
        DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteJoystick);
        DefaultSpriteUI.SetSpritePosition(GuiJoystickHat.Sprite, Game.Instance.JoystickHatPositon);
//        DefaultSpriteUI.HideSprite(GuiJoystickHat.Sprite);
    }


    public void ButtonDown(PlayerControls.E_ButtonsName button)
    {
        if (On == false)
            return;

        if (button == PlayerControls.E_ButtonsName.AttackX)
            DefaultSpriteUI.ShowSprite(GuiButtonSelect.SpriteX);
        else if (button == PlayerControls.E_ButtonsName.AttackO)
            DefaultSpriteUI.ShowSprite(GuiButtonSelect.SpriteO);
        else if (button == PlayerControls.E_ButtonsName.Use)
            DefaultSpriteUI.ShowSprite(GuiButtonSelect.SpriteUse);
        else if (button == PlayerControls.E_ButtonsName.Roll)
            DefaultSpriteUI.ShowSprite(GuiButtonSelect.SpriteRoll);
    }

    public void ButtonUp(PlayerControls.E_ButtonsName button)
    {
        if (On == false)
            return;

        if (button == PlayerControls.E_ButtonsName.AttackX)
            DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteX);
        else if (button == PlayerControls.E_ButtonsName.AttackO)
            DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteO);
        else if (button == PlayerControls.E_ButtonsName.Use)
            DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteUse);
        else if (button == PlayerControls.E_ButtonsName.Roll)
            DefaultSpriteUI.HideSprite(GuiButtonSelect.SpriteRoll);
        else
            GuiManager.Instance.PlayButtonSound();
    }

    IEnumerator ChangeButtonSize(Sprite s, float finalWidth, float Height, float speed)
    {
        float newWidth;
        float newHeight;
        float progress = 0;
        float width = s.width;
        float height = s.height;
        while (progress < 1)
        {
            progress += speed * Time.deltaTime;
            
            if (progress > 1)
                progress = 1;
            
            newWidth = Mathfx.Sinerp(width, finalWidth, progress);
            newHeight = Mathfx.Sinerp(height, finalWidth, progress);
            
            s.SetSizeXY(newWidth, newHeight);
            
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ShowSprite(Sprite s, float time)
    {
        DefaultSpriteUI.ShowSprite(s);

        float step = 1 / time;
        Color c = Color.white;

        c.a = 0;
        s.SetColor(c);

        while (s.color.a < 1)
        {
            c.a = Mathf.Min(1.0f, c.a + step * Time.deltaTime);
            s.SetColor(c);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DisappearSprite(Sprite s, float time)
    {
        float step = 1 / time;
        Color c = Color.white;

        while (s.color.a > 0)
        {
            c.a = Mathf.Max(0.0f, c.a - step * Time.deltaTime);
            s.SetColor(c);
            yield return new WaitForEndOfFrame();
        }

        DefaultSpriteUI.HideSprite(s);
    }
}

