using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GuiShop
{
    private GuiManager GuiManager;
    //private AudioSource Audio;
    private SpriteUI DefaultSpriteUI;

    private GuiNumbers Karma = new GuiNumbers();
    private GuiShopHealth ButtonHealth = new GuiShopHealth();
    private GuiShopSword ButtonSword = new GuiShopSword();
    private GuiShopInfo BuyScreen = new GuiShopInfo();

    private GuiShopCombo[] ButtonCombo = new GuiShopCombo[] { new GuiShopCombo(), new GuiShopCombo(), new GuiShopCombo(), new GuiShopCombo(), new GuiShopCombo(), new GuiShopCombo() };
    
    private E_SwordLevel SaveSwordLevel;
    private E_HealthLevel SaveHealthlevel;
    private E_ComboLevel[] SaveComboLevels = new E_ComboLevel[6];
    private int SaveExperience;
    private bool Initialized = false;

    public void Start(GuiManager manager)
    {
        GuiManager = manager;
        //Audio = GuiManager.audio;
        DefaultSpriteUI = GuiManager.DefaultSpriteUI;
	}

	public void Update()
	{
        if (Input.touchCount == 0)
            return;
	}

    void Initialize()
    {
        Initialized = true;

        if (Game.Instance.GameType != E_GameType.Survival)
        {
            GuiShopBackgroundMiddle.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundMiddle.Instance.ScreenLeft, GuiShopBackgroundMiddle.Instance.ScreenBottom), GuiShopBackgroundMiddle.Instance.ScreenWidth, GuiShopBackgroundMiddle.Instance.ScreenHeight, 9, GuiShopBackgroundMiddle.Instance.UvLeft, GuiShopBackgroundMiddle.Instance.UvTop, GuiShopBackgroundMiddle.Instance.UvWidth, GuiShopBackgroundMiddle.Instance.UvHeight);
            GuiShopBackgroundTop.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundTop.Instance.ScreenLeft, GuiShopBackgroundTop.Instance.ScreenBottom), GuiShopBackgroundTop.Instance.ScreenWidth, GuiShopBackgroundTop.Instance.ScreenHeight, 9, GuiShopBackgroundTop.Instance.UvLeft, GuiShopBackgroundTop.Instance.UvTop, GuiShopBackgroundTop.Instance.UvWidth, GuiShopBackgroundTop.Instance.UvHeight);
            GuiShopBackgroundBottom.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundBottom.Instance.ScreenLeft, GuiShopBackgroundBottom.Instance.ScreenBottom), GuiShopBackgroundBottom.Instance.ScreenWidth, GuiShopBackgroundBottom.Instance.ScreenHeight, 9, GuiShopBackgroundBottom.Instance.UvLeft, GuiShopBackgroundBottom.Instance.UvTop, GuiShopBackgroundBottom.Instance.UvWidth, GuiShopBackgroundBottom.Instance.UvHeight);

            GuiShopBackgroundTopTop.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundTopTop.Instance.ScreenLeft, GuiShopBackgroundTopTop.Instance.ScreenBottom), GuiShopBackgroundTopTop.Instance.ScreenWidth, GuiShopBackgroundTopTop.Instance.ScreenHeight, 9, GuiShopBackgroundTopTop.Instance.UvLeft, GuiShopBackgroundTopTop.Instance.UvTop, GuiShopBackgroundTopTop.Instance.UvWidth, GuiShopBackgroundTopTop.Instance.UvHeight);
            GuiShopBackgroundTopMiddle.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundTopMiddle.Instance.ScreenLeft, GuiShopBackgroundTopMiddle.Instance.ScreenBottom), GuiShopBackgroundTopMiddle.Instance.ScreenWidth, GuiShopBackgroundTopMiddle.Instance.ScreenHeight, 9, GuiShopBackgroundTopMiddle.Instance.UvLeft, GuiShopBackgroundTopMiddle.Instance.UvTop, GuiShopBackgroundTopMiddle.Instance.UvWidth, GuiShopBackgroundTopMiddle.Instance.UvHeight);
            GuiShopBackgroundTopBottom.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundTopBottom.Instance.ScreenLeft, GuiShopBackgroundTopBottom.Instance.ScreenBottom), GuiShopBackgroundTopBottom.Instance.ScreenWidth, GuiShopBackgroundTopBottom.Instance.ScreenHeight, 9, GuiShopBackgroundTopBottom.Instance.UvLeft, GuiShopBackgroundTopBottom.Instance.UvTop, GuiShopBackgroundTopBottom.Instance.UvWidth, GuiShopBackgroundTopBottom.Instance.UvHeight);
        }
        else
        {
        }

        GuiShopBackgroundMiddleMiddle.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundMiddleMiddle.Instance.ScreenLeft, GuiShopBackgroundMiddleMiddle.Instance.ScreenBottom), GuiShopBackgroundMiddleMiddle.Instance.ScreenWidth, GuiShopBackgroundMiddleMiddle.Instance.ScreenHeight, 9, GuiShopBackgroundMiddleMiddle.Instance.UvLeft, GuiShopBackgroundMiddleMiddle.Instance.UvTop, GuiShopBackgroundMiddleMiddle.Instance.UvWidth, GuiShopBackgroundMiddleMiddle.Instance.UvHeight);
        GuiShopBackgroundMiddleTop.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundMiddleTop.Instance.ScreenLeft, GuiShopBackgroundMiddleTop.Instance.ScreenBottom), GuiShopBackgroundMiddleTop.Instance.ScreenWidth, GuiShopBackgroundMiddleTop.Instance.ScreenHeight, 9, GuiShopBackgroundMiddleTop.Instance.UvLeft, GuiShopBackgroundMiddleTop.Instance.UvTop, GuiShopBackgroundMiddleTop.Instance.UvWidth, GuiShopBackgroundMiddleTop.Instance.UvHeight);
        GuiShopBackgroundMiddleBottom.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundMiddleBottom.Instance.ScreenLeft, GuiShopBackgroundMiddleBottom.Instance.ScreenBottom), GuiShopBackgroundMiddleBottom.Instance.ScreenWidth, GuiShopBackgroundMiddleBottom.Instance.ScreenHeight, 9, GuiShopBackgroundMiddleBottom.Instance.UvLeft, GuiShopBackgroundMiddleBottom.Instance.UvTop, GuiShopBackgroundMiddleBottom.Instance.UvWidth, GuiShopBackgroundMiddleBottom.Instance.UvHeight);

        GuiShopBackgroundDownMiddle.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundDownMiddle.Instance.ScreenLeft, GuiShopBackgroundDownMiddle.Instance.ScreenBottom), GuiShopBackgroundDownMiddle.Instance.ScreenWidth, GuiShopBackgroundDownMiddle.Instance.ScreenHeight, 9, GuiShopBackgroundDownMiddle.Instance.UvLeft, GuiShopBackgroundDownMiddle.Instance.UvTop, GuiShopBackgroundDownMiddle.Instance.UvWidth, GuiShopBackgroundDownMiddle.Instance.UvHeight);
        GuiShopBackgroundDownTop.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundDownTop.Instance.ScreenLeft, GuiShopBackgroundDownTop.Instance.ScreenBottom), GuiShopBackgroundDownTop.Instance.ScreenWidth, GuiShopBackgroundDownTop.Instance.ScreenHeight, 9, GuiShopBackgroundDownTop.Instance.UvLeft, GuiShopBackgroundDownTop.Instance.UvTop, GuiShopBackgroundDownTop.Instance.UvWidth, GuiShopBackgroundDownTop.Instance.UvHeight);
        GuiShopBackgroundDownBottom.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopBackgroundDownBottom.Instance.ScreenLeft, GuiShopBackgroundDownBottom.Instance.ScreenBottom), GuiShopBackgroundDownBottom.Instance.ScreenWidth, GuiShopBackgroundDownBottom.Instance.ScreenHeight, 9, GuiShopBackgroundDownBottom.Instance.UvLeft, GuiShopBackgroundDownBottom.Instance.UvTop, GuiShopBackgroundDownBottom.Instance.UvWidth, GuiShopBackgroundDownBottom.Instance.UvHeight);

        if (Game.Instance.GameType != E_GameType.Survival)
            GuiShopKarmaCaption.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopKarmaCaption.Instance.ScreenLeft, GuiShopKarmaCaption.Instance.ScreenBottom), GuiShopKarmaCaption.Instance.ScreenWidth, GuiShopKarmaCaption.Instance.ScreenHeight, 9, GuiShopKarmaCaption.Instance.UvLeft, GuiShopKarmaCaption.Instance.UvTop, GuiShopKarmaCaption.Instance.UvWidth, GuiShopKarmaCaption.Instance.UvHeight);

        GuiShopComboCaption.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboCaption.Instance.ScreenLeft, GuiShopComboCaption.Instance.ScreenBottom), GuiShopComboCaption.Instance.ScreenWidth, GuiShopComboCaption.Instance.ScreenHeight, 9, GuiShopComboCaption.Instance.UvLeft, GuiShopComboCaption.Instance.UvTop, GuiShopComboCaption.Instance.UvWidth, GuiShopComboCaption.Instance.UvHeight);

        GuiShopButtonOk.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopButtonOk.Instance.ScreenLeft, GuiShopButtonOk.Instance.ScreenBottom), GuiShopButtonOk.Instance.ScreenWidth, GuiShopButtonOk.Instance.ScreenHeight, 9, GuiShopButtonOk.Instance.UvLeft, GuiShopButtonOk.Instance.UvTop, GuiShopButtonOk.Instance.UvWidth, GuiShopButtonOk.Instance.UvHeight);
        GuiShopButtonBack.Instance.Sprite = DefaultSpriteUI.AddElement(new Vector2(GuiShopButtonBack.Instance.ScreenLeft, GuiShopButtonBack.Instance.ScreenBottom), GuiShopButtonBack.Instance.ScreenWidth, GuiShopButtonBack.Instance.ScreenHeight, 9, GuiShopButtonBack.Instance.UvLeft, GuiShopButtonBack.Instance.UvTop, GuiShopButtonBack.Instance.UvWidth, GuiShopButtonBack.Instance.UvHeight);

        if (Game.Instance.GameType != E_GameType.Survival)
        {
            Karma = new GuiNumbers()
            {
                UvLeft = GuiShopNumbers.UvLeft,
                UvTop = GuiShopNumbers.UvTop,
                UvWidth = GuiShopNumbers.UvWidth,
                UvHeight = GuiShopNumbers.UvHeight,
                Sprites = new Sprite[]{
            DefaultSpriteUI.AddElement(new Vector2(GuiShopKarmaCaption.Instance.NScreenLeft, GuiShopKarmaCaption.Instance.NScreenBottom),GuiShopKarmaCaption.Instance.NScreenWidth, GuiShopKarmaCaption.Instance.NScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiShopKarmaCaption.Instance.NScreenLeft - GuiShopKarmaCaption.Instance.NScreenWidth , GuiShopKarmaCaption.Instance.NScreenBottom),GuiShopKarmaCaption.Instance.NScreenWidth, GuiShopKarmaCaption.Instance.NScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiShopKarmaCaption.Instance.NScreenLeft - GuiShopKarmaCaption.Instance.NScreenWidth * 2, GuiShopKarmaCaption.Instance.NScreenBottom),GuiShopKarmaCaption.Instance.NScreenWidth, GuiShopKarmaCaption.Instance.NScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiShopKarmaCaption.Instance.NScreenLeft - GuiShopKarmaCaption.Instance.NScreenWidth * 3, GuiShopKarmaCaption.Instance.NScreenBottom),GuiShopKarmaCaption.Instance.NScreenWidth, GuiShopKarmaCaption.Instance.NScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
            DefaultSpriteUI.AddElement(new Vector2(GuiShopKarmaCaption.Instance.NScreenLeft - GuiShopKarmaCaption.Instance.NScreenWidth * 4, GuiShopKarmaCaption.Instance.NScreenBottom),GuiShopKarmaCaption.Instance.NScreenWidth, GuiShopKarmaCaption.Instance.NScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
        }
            };
        }
        else
            Karma = null;

        for (int i = 0; i < ButtonCombo.Length; i++)
            ButtonCombo[i].Start(GuiManager, i);

        ButtonHealth.Start(GuiManager);
        ButtonSword.Start(GuiManager);
        BuyScreen.Start(GuiManager);

         Hide();
    }

    public void Reset()
    {
        if(Initialized)
            Hide();
    }


    public void Show()
    {
        if (Initialized == false)
            Initialize();

        GuiManager.StartCoroutine(_Show());
    }

    IEnumerator _Show()
    {
        GuiManager.FadeOut(0.2f, 0.7f);

        yield return new WaitForSeconds(0.3f);

        //Debug.Log("Show shop");
        
        if (Game.Instance.GameType != E_GameType.Survival)
        {
            DefaultSpriteUI.ShowSprite(GuiShopBackgroundTop.Instance.Sprite);
            DefaultSpriteUI.ShowSprite(GuiShopBackgroundMiddle.Instance.Sprite);
            DefaultSpriteUI.ShowSprite(GuiShopBackgroundBottom.Instance.Sprite);

            DefaultSpriteUI.ShowSprite(GuiShopBackgroundTopTop.Instance.Sprite);
            DefaultSpriteUI.ShowSprite(GuiShopBackgroundTopMiddle.Instance.Sprite);
            DefaultSpriteUI.ShowSprite(GuiShopBackgroundTopBottom.Instance.Sprite);
        }

        DefaultSpriteUI.ShowSprite(GuiShopBackgroundMiddleTop.Instance.Sprite);
        DefaultSpriteUI.ShowSprite(GuiShopBackgroundMiddleMiddle.Instance.Sprite);
        DefaultSpriteUI.ShowSprite(GuiShopBackgroundMiddleBottom.Instance.Sprite);

        DefaultSpriteUI.ShowSprite(GuiShopBackgroundDownTop.Instance.Sprite);
        DefaultSpriteUI.ShowSprite(GuiShopBackgroundDownMiddle.Instance.Sprite);
        DefaultSpriteUI.ShowSprite(GuiShopBackgroundDownBottom.Instance.Sprite);

        if (Game.Instance.GameType != E_GameType.Survival)
            DefaultSpriteUI.ShowSprite(GuiShopKarmaCaption.Instance.Sprite);

        DefaultSpriteUI.ShowSprite(GuiShopComboCaption.Instance.Sprite);

        DefaultSpriteUI.ShowSprite(GuiShopButtonOk.Instance.Sprite);
        DefaultSpriteUI.ShowSprite(GuiShopButtonBack.Instance.Sprite);

        if (Game.Instance.GameType != E_GameType.Survival)
        {
            ButtonHealth.Show();
            ButtonSword.Show();
        }

        for (int i = 0; i < ButtonCombo.Length; i++)
            ButtonCombo[i].Show();
        
        if (Game.Instance.GameType != E_GameType.Survival)
            GuiManager.ShowNumbers(Karma, Game.Instance.Money, 99999);

        SaveExperience = Game.Instance.Money;
        SaveSwordLevel = Game.Instance.SwordLevel;
        SaveHealthlevel = Game.Instance.HealthLevel;
        for (int i = 0; i < 6; i++)
            SaveComboLevels[i] = Game.Instance.ComboLevel[i];

        Time.timeScale = 0;

        Game.Instance.GameState = E_GameState.Shop;
    }

    public void ShowShopInfo(bool showBuy, int ButtonIndex)
    {
        BuyScreen.Show(showBuy, ButtonIndex);
    }

    public void HideShopInfo(bool ok)
    {
        BuyScreen.Hide(ok);

        if (ok)
            Refresh();
    }

    public void Hide(bool ok)
    {
        Time.timeScale = 1;
        if (ok == false)
        {
            //Debug.Log("Shop back - restore values !!!" + Game.Instance.Money + " " + SaveExperience);

            Game.Instance.Money = SaveExperience;
            Game.Instance.SwordLevel = SaveSwordLevel;
            Game.Instance.HealthLevel = SaveHealthlevel;
            for (int i = 0; i < 6; i++)
                Game.Instance.ComboLevel[i] = SaveComboLevels[i];

            //Debug.Log("Shop back - restore values !!!" + Game.Instance.Money);
        }
        Hide();

        Game.Instance.GameState = E_GameState.Game;

    }

    void Hide()
    {
        GuiManager.FadeIn();

        if (Game.Instance.GameType != E_GameType.Survival)
        {
            DefaultSpriteUI.HideSprite(GuiShopBackgroundTop.Instance.Sprite);
            DefaultSpriteUI.HideSprite(GuiShopBackgroundMiddle.Instance.Sprite);
            DefaultSpriteUI.HideSprite(GuiShopBackgroundBottom.Instance.Sprite);

            DefaultSpriteUI.HideSprite(GuiShopBackgroundTopTop.Instance.Sprite);
            DefaultSpriteUI.HideSprite(GuiShopBackgroundTopMiddle.Instance.Sprite);
            DefaultSpriteUI.HideSprite(GuiShopBackgroundTopBottom.Instance.Sprite);
        }

        DefaultSpriteUI.HideSprite(GuiShopBackgroundMiddleTop.Instance.Sprite);
        DefaultSpriteUI.HideSprite(GuiShopBackgroundMiddleMiddle.Instance.Sprite);
        DefaultSpriteUI.HideSprite(GuiShopBackgroundMiddleBottom.Instance.Sprite); 

        DefaultSpriteUI.HideSprite(GuiShopBackgroundDownTop.Instance.Sprite);
        DefaultSpriteUI.HideSprite(GuiShopBackgroundDownMiddle.Instance.Sprite);
        DefaultSpriteUI.HideSprite(GuiShopBackgroundDownBottom.Instance.Sprite);

        if (Game.Instance.GameType != E_GameType.Survival)
            DefaultSpriteUI.HideSprite(GuiShopKarmaCaption.Instance.Sprite);

        DefaultSpriteUI.HideSprite(GuiShopComboCaption.Instance.Sprite);

        DefaultSpriteUI.HideSprite(GuiShopButtonOk.Instance.Sprite);
        DefaultSpriteUI.HideSprite(GuiShopButtonBack.Instance.Sprite);

        ButtonHealth.Hide();
        ButtonSword.Hide();
        BuyScreen.Hide();

        for (int i = 0; i < ButtonCombo.Length; i++)
            ButtonCombo[i].Hide();

        if (Game.Instance.GameType != E_GameType.Survival)
        {
            for (int i = 0; i < Karma.Sprites.Length; i++)
                DefaultSpriteUI.HideSprite(Karma.Sprites[i]);
        }
    }

    void Refresh()
    {
        if (Game.Instance.GameType != E_GameType.Survival)
        {
            ButtonHealth.Show();
            ButtonSword.Show();
        }

        for (int i = 0; i < ButtonCombo.Length; i++)
            ButtonCombo[i].Show();

        if (Game.Instance.GameType != E_GameType.Survival)
            GuiManager.ShowNumbers(Karma, Game.Instance.Money, 99999);

    }
}

