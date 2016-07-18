using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class UiRect
{
    public UiRect(int l, int b, int w, int h) { Left = l; Bottom = b; Width = w; Height = h; }

    public int Left;
    public int Bottom;
    public int Width;
    public int Height;
}

public class GuiNumbers
{
    public Sprite[] Sprites = new Sprite[6];
    public Sprite Minus;

    public int UvLeft;
    public int UvTop;
    public int UvWidth;
    public int UvHeight;

    public int MinusLeftScreen;
    public int MinusBottomScreen;
}


public class GuiManager : MonoBehaviour
{
	public enum E_HudMessageType
	{
		E_NONE,
		E_DEATH,
		E_DOJO_START,
		E_AREA_CLEAR,
		E_GO,
		E_NEW_COMBO,
	}

    enum E_PressedButton
    {
        E_NONE,
        E_INGAME_MENU,
        E_COMBO,
        E_SHOP,
        E_RESUME,
        E_QUIT,
    }
	public SpriteUI DefaultSpriteUI = null;
	// Alpha start value
	public float startAlpha = 1;
	// Default time a fade takes in seconds
	public float fadeDuration = 1;
	// Fade into scene at start
	public bool fadeIntoScene = true;
	// Current alpha of the texture
	private float currentAlpha = 1;
	// Current duration of the fade
	private float currentDuration;
	// Direction of the fade
	private int fadeDirection = -1;
	// Fade alpha to
	private float targetAlpha = 0;
	// Alpha difference
	private float alphaDifference = 0;
	// Color object for alpha setting
    private Color alphaColor = new Color();

    public static Color DisabledColor = new Color(0.5f, 0.5f, 0.5f, 1);

    public static GuiManager Instance;
	private Sprite Fade;

	class MessageUI
	{
		public Sprite MessageSprite;
		public E_HudMessageType CurrentMessage;
	}

    GuiControls GuiControls = new GuiControls();
    GuiHud GuiHud = new GuiHud();
    GuiShop GuiShop = new GuiShop();
    IngameMenu IngameMenu = new IngameMenu();

	private MessageUI Message = new MessageUI();

    private AudioSource Audio;

	void Awake()
	{
		Instance = this;
        Audio = GetComponent<AudioSource>();
	}

    void Start ()
	{
        DefaultSpriteUI.SetMaterial(Resources.Load("Gui/Gui4G") as Material);
        
        GuiControls.Start(this);
        GuiHud.Start(this);
        
        

        Message.MessageSprite = DefaultSpriteUI.AddElement(new Vector2(50, 206), 222, 52, 3, 290, 106, 222, 52);
		Message.CurrentMessage = E_HudMessageType.E_NONE;

//        GuiFPS.NumberSprites[0] = DefaultSpriteUI.AddElement(new Vector2(GuiFPS.ScreenLeft, GuiFPS.ScreenBottom), GuiFPS.ScreenWidth, GuiFPS.UvHeight, 10, GuiFPS.UvLeft, GuiFPS.UvTop, GuiFPS.UvWidth, GuiFPS.UvHeight);
//        GuiFPS.NumberSprites[1] = DefaultSpriteUI.AddElement(new Vector2(GuiFPS.ScreenLeft - GuiFPS.ScreenWidth, GuiFPS.ScreenBottom), GuiFPS.ScreenWidth, GuiFPS.UvHeight, 10, GuiFPS.UvLeft, GuiFPS.UvTop, GuiFPS.UvWidth, GuiFPS.UvHeight);

       Fade = DefaultSpriteUI.AddElement(new Vector2(0, 0), Screen.width, Screen.height, 10, 404 * 2, 510 * 2, 1, 1);

        GuiShop.Start(this);
        IngameMenu.Start(this);

		ShowMessage(E_HudMessageType.E_NONE);
		currentAlpha = startAlpha;
		if (fadeIntoScene)
			FadeIn();

	}

	void Update()
	{
        GuiControls.Update();
        GuiHud.Update();
        GuiShop.Update();
        IngameMenu.Update();
	}

	public void Reset()
	{
		//Debug.Log("GuiManager reset");
        StopAllCoroutines();

        GuiControls.Reset();
        GuiHud.Reset();
        GuiShop.Reset();
        IngameMenu.Reset();

        GuiControls.Show();
        GuiHud.Show();
	}

	public void ShowIngameMenu()
	{
        GuiHud.Hide();
        GuiControls.Hide();
        IngameMenu.Show();
	}

	public void HideIngameMenu()
	{
        GuiHud.Show();
        IngameMenu.Hide();
        GuiControls.Show();
	}

    public void ShowShop()
    {
        GuiControls.Hide();
        GuiHud.Hide();
        GuiShop.Show();
    }

    public void HideShop(bool saveResult)
    {
        GuiControls.Show();
        GuiHud.Show();

        GuiShop.Hide(saveResult);
    }

    public void ShowControlsForCustomize(bool show)
    {
        if (show)
            GuiControls.ShowForCustomize();
        else
            GuiControls.HideAfterCustomize();
    }

    public void UpdateControlsForCustomize()
    {
        GuiControls.ShowForCustomize();
    }
    
    public void ShowShopInfo(bool showBuy, int index)
    {
        GuiShop.ShowShopInfo(showBuy, index);
    }

    public void HideShopInfo(bool ok)
    {
        GuiShop.HideShopInfo(ok);
    }


	public void SetHealthPercent(float currentHealth, float maxHealth)
	{
        GuiHud.SetHealthPercent(currentHealth, maxHealth);
	}

    public void ShowComboProgress(List<E_AttackType> comboProgress)
    {
        GuiHud.ShowComboProgress(comboProgress);
    }

    public void ShowComboMessage(int ComboIndex)
    {
        GuiHud.ShowComboMessage(ComboIndex);
    }

	public void SetHitsCount(int hits)
	{
        GuiHud.SetHitsCount(hits);
	}

	public void SetMoney(int oldExperience, int newExperince)
	{
        GuiHud.SetMoney(oldExperience, newExperince);
	}

    /*public void SetScore(int oldScore, int newScore)
    {
        GuiHud.SetScore(oldScore, newScore);
    }*/

    public void ShowSaveProgress()
    {
        GuiHud.ShowSaveProgress();
    }
	public void ShowBloodSplash()
	{
        GuiHud.ShowBloodSplash();
	}

    public void SwitchToUseMode()   
    {
        GuiControls.SwitchToUseMode();
    }

    public void SwitchToCombatMode()
    {
        GuiControls.SwitchToCombatMode();
    }

    public void JoystickDown()
    {
        GuiControls.JoystickDown();
    }

    public void JoystickUp()
    {
        GuiControls.JoystickUp();
    }

    public void JoystickUpdate(Vector2 dir)
    {
        GuiControls.JoystickUpdate(dir);
    }
    public void ButtonDown(PlayerControls.E_ButtonsName button)
    {
        GuiControls.ButtonDown(button);
    }

    public void ButtonUp(PlayerControls.E_ButtonsName button)
    {
        GuiControls.ButtonUp(button);
    }

    public void ResetControls()
    {
        GuiControls.ResetControls();
    }

    public void PlayButtonSound()
    {
        Audio.PlayOneShot(SoundDataManager.Instance.ControlSound);
    }

    public void SetFPS(int fps)
    {
       /* if (fps > 99)
            fps = 99;

        int number = fps % 10;
        int tents = fps / 10;

        DefaultSpriteUI.ShowSprite(GuiFPS.NumberSprites[0]);
        GuiFPS.NumberSprites[0].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiFPS.UvLeft + GuiFPS.UvWidth * number, GuiFPS.UvTop);

        if (fps > 9)
        {
            DefaultSpriteUI.ShowSprite(GuiFPS.NumberSprites[1]);
            GuiFPS.NumberSprites[1].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiFPS.UvLeft + GuiFPS.UvWidth * tents, GuiFPS.UvTop);
        }
        else
            DefaultSpriteUI.HideSprite(GuiFPS.NumberSprites[1]);
     */
    }

	public void ShowMessage(E_HudMessageType message)
	{
		if (message == E_HudMessageType.E_NONE)
		{
			DefaultSpriteUI.HideSprite(Message.MessageSprite);
			return;
		}

		//if (Message.CurrentMessage != message)
		{
			if (message == E_HudMessageType.E_DEATH)
			{
                DefaultSpriteUI.UpdateSpriteSize(Message.MessageSprite, new Vector2(GuiMessageDeath.Instance.ScreenLeft, GuiMessageDeath.Instance.ScreenBottom), GuiMessageDeath.Instance.ScreenWidth, GuiMessageDeath.Instance.ScreenHeight,
                    GuiMessageDeath.Instance.UvLeft, GuiMessageDeath.Instance.UvTop, GuiMessageDeath.Instance.UvWidth, GuiMessageDeath.Instance.UvHeight);
			}
			else if(message == E_HudMessageType.E_DOJO_START)
			{
                DefaultSpriteUI.UpdateSpriteSize(Message.MessageSprite, new Vector2(GuiMessageHajime.Instance.ScreenLeft, GuiMessageHajime.Instance.ScreenBottom), GuiMessageHajime.Instance.ScreenWidth, GuiMessageHajime.Instance.ScreenHeight,
                    GuiMessageHajime.Instance.UvLeft, GuiMessageHajime.Instance.UvTop, GuiMessageHajime.Instance.UvWidth, GuiMessageHajime.Instance.UvHeight);
			}
			else if(message == E_HudMessageType.E_AREA_CLEAR)
			{
                DefaultSpriteUI.UpdateSpriteSize(Message.MessageSprite, new Vector2(GuiMessageRoundDone.Instance.ScreenLeft, GuiMessageRoundDone.Instance.ScreenBottom), GuiMessageRoundDone.Instance.ScreenWidth, GuiMessageRoundDone.Instance.ScreenHeight,
                    GuiMessageRoundDone.Instance.UvLeft, GuiMessageRoundDone.Instance.UvTop, GuiMessageRoundDone.Instance.UvWidth, GuiMessageRoundDone.Instance.UvHeight);
			}
		}
		DefaultSpriteUI.ShowSprite(Message.MessageSprite);
	}


    public void ShowNumbers(GuiNumbers numbers, int number, int max )
    {
        if (number == -1)
        {
            if (numbers.Sprites.Length > 0)
                DefaultSpriteUI.HideSprite(numbers.Sprites[0]);
            if (numbers.Sprites.Length > 1)
                DefaultSpriteUI.HideSprite(numbers.Sprites[1]);
            if (numbers.Sprites.Length > 2)
                DefaultSpriteUI.HideSprite(numbers.Sprites[2]);
            if (numbers.Sprites.Length > 3)
                DefaultSpriteUI.HideSprite(numbers.Sprites[3]);
            if (numbers.Sprites.Length > 4)
                DefaultSpriteUI.HideSprite(numbers.Sprites[4]);
        }
        else
        {
            if (number > max)
                number = max;

            int one = number % 10;
            int tents = (number % 100) / 10;
            int hundreds = (number % 1000) / 100;
            int thousands = (number % 10000) / 1000;
            int hundredsthousands = number / 10000;

            //Debug.Log(ToString() + " " + hundredsthousands.ToString() + " "  + thousands.ToString() + " " + hundreds.ToString() + " " + tents.ToString() + " " + one.ToString());

            DefaultSpriteUI.ShowSprite(numbers.Sprites[0]);
            numbers.Sprites[0].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * one, numbers.UvTop);
            if (number > 9)
            {
                DefaultSpriteUI.ShowSprite(numbers.Sprites[1]);
                numbers.Sprites[1].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * tents, numbers.UvTop);
            }
            else if(numbers.Sprites.Length > 1)
                DefaultSpriteUI.HideSprite(numbers.Sprites[1]);

            if (number > 99)
            {
                DefaultSpriteUI.ShowSprite(numbers.Sprites[2]);
                numbers.Sprites[2].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * hundreds, numbers.UvTop);
            }
            else if (numbers.Sprites.Length > 2)
                DefaultSpriteUI.HideSprite(numbers.Sprites[2]);

            if (number > 999)
            {
                DefaultSpriteUI.ShowSprite(numbers.Sprites[3]);
                numbers.Sprites[3].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * thousands, numbers.UvTop);
            }
            else if (numbers.Sprites.Length > 3)
                DefaultSpriteUI.HideSprite(numbers.Sprites[3]);

            if (number > 9999)
            {
                DefaultSpriteUI.ShowSprite(numbers.Sprites[4]);
                numbers.Sprites[4].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(numbers.UvLeft + numbers.UvWidth * hundredsthousands, numbers.UvTop);
            }
            else if (numbers.Sprites.Length > 4)
                DefaultSpriteUI.HideSprite(numbers.Sprites[4]);
        }
    }


    public void ShowScoreTable()
    {
    }

	// ---------------------------------------- 
	//  FADE METHODS
	// ----------------------------------------

	public void FadeIn(float duration, float to)
	{
		// Set fade duration
		currentDuration = duration;
		// Set target alpha
		targetAlpha = to;
		// Difference
		alphaDifference = Mathf.Clamp01(currentAlpha - targetAlpha);
		// Set direction to Fade in
		fadeDirection = -1;
	}

    public void SetFadeOut(float fadeOut)
    {
        currentAlpha = fadeOut;
        targetAlpha = fadeOut;
        alphaColor.a = fadeOut;
        Fade.SetColor(alphaColor);

        if (currentAlpha == fadeOut)
			DefaultSpriteUI.HideSprite(Fade);
		else
			DefaultSpriteUI.ShowSprite(Fade);

    }

	public void FadeIn()
	{
		FadeIn(fadeDuration, 0);
	}

	public void FadeIn(float duration)
	{
		FadeIn(duration, 0);
	}

	public void FadeOut(float duration, float to)
	{
		// Set fade duration
		currentDuration = duration;
		// Set target alpha
		targetAlpha = to;
		// Difference
		alphaDifference = Mathf.Clamp01(targetAlpha - currentAlpha);
		// Set direction to fade out
		fadeDirection = 1;
	}

	public void FadeOut()
	{
		FadeOut(fadeDuration, 1);
	}

	public void FadeOut(float duration)
	{
		FadeOut(duration, 1);
	}

	void LateUpdate()
	{
		// Fade alpha if active
		if ((fadeDirection == -1 && currentAlpha > targetAlpha) ||
			 (fadeDirection == 1 && currentAlpha < targetAlpha))
		{
			// Advance fade by fraction of full fade time
			currentAlpha += (fadeDirection * alphaDifference) * (Time.deltaTime / currentDuration);
			// Clamp to 0-1
			currentAlpha = Mathf.Clamp01(currentAlpha);
		}

		// Draw only if not transculent
		if (currentAlpha >= 0 && currentAlpha <= 1)
		{
			alphaColor.a = currentAlpha;
			Fade.SetColor(alphaColor);
		}

		if (currentAlpha == 0)
			DefaultSpriteUI.HideSprite(Fade);
		else
			DefaultSpriteUI.ShowSprite(Fade);
	}
}

