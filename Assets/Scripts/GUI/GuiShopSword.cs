using UnityEngine;
using System.Collections;
using System;

public class GuiShopSword
{
    private GuiManager Gui;
    private SpriteUI DefaultSpriteUI;
    private Sprite SpriteB;
    private Sprite SpriteC;
    private Sprite SpriteL;
    //private GuiNumbers Level;
    private GuiNumbers Money;


    public void Start(GuiManager manager)
    {
        Gui = manager;
        DefaultSpriteUI = manager.DefaultSpriteUI;

        SpriteB = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.BScreenLeft(1), GuiShopComboButtons.BScreenBottom(1)), GuiShopComboButtons.BScreenWidth(1), GuiShopComboButtons.BScreenHeight(1), 9, GuiShopComboButtons.BUvLeft, GuiShopComboButtons.BUvBottom, GuiShopComboButtons.BUvWidth, GuiShopComboButtons.BUvHeight);
        
        

        if (Game.Instance.GameType != E_GameType.Survival)
        {
            SpriteC = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.CScreenLeft(1), GuiShopComboButtons.CScreenBottom(1)), GuiShopComboButtons.CScreenWidth(), GuiShopComboButtons.CScreenHeight(), 9, GuiShopComboButtons.CUvLeft[1], GuiShopComboButtons.CUvBottom[1], GuiShopComboButtons.CUvWidth, GuiShopComboButtons.CUvHeight);
            Money = new GuiNumbers()
            {
                UvLeft = GuiShopNumbers.UvLeft,
                UvTop = GuiShopNumbers.UvTop,
                UvWidth = GuiShopNumbers.UvWidth,
                UvHeight = GuiShopNumbers.UvHeight,
                Sprites = new Sprite[]{
                    DefaultSpriteUI.AddElement(new Vector2(GuiSwordNumbers.ScreenLeft + GuiSwordNumbers.ScreenWidth * 3, GuiSwordNumbers.ScreenBottom),GuiSwordNumbers.ScreenWidth, GuiSwordNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                    DefaultSpriteUI.AddElement(new Vector2(GuiSwordNumbers.ScreenLeft + GuiSwordNumbers.ScreenWidth * 2, GuiSwordNumbers.ScreenBottom),GuiSwordNumbers.ScreenWidth, GuiSwordNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                    DefaultSpriteUI.AddElement(new Vector2(GuiSwordNumbers.ScreenLeft + GuiSwordNumbers.ScreenWidth , GuiSwordNumbers.ScreenBottom),GuiSwordNumbers.ScreenWidth, GuiSwordNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                    DefaultSpriteUI.AddElement(new Vector2(GuiSwordNumbers.ScreenLeft, GuiSwordNumbers.ScreenBottom),GuiSwordNumbers.ScreenWidth, GuiSwordNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                   // DefaultSpriteUI.AddElement(new Vector2(GuiSwordNumbers.ScreenLeft[hardware] + GuiSwordNumbers.ScreenWidth[hardware] * 4, GuiSwordNumbers.ScreenBottom[hardware]),GuiSwordNumbers.ScreenWidth[hardware], GuiSwordNumbers.ScreenHeight[hardware], 9, GuiShopNumbers.UvLeft[hardware], GuiShopNumbers.UvTop[hardware], GuiShopNumbers.UvWidth[hardware], GuiShopNumbers.UvHeight[hardware]),
                }
            };
        }
        else
            SpriteL = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.CLScreenLeft, GuiShopComboButtons.CLScreenBottom), GuiShopComboButtons.CLScreenWidth, GuiShopComboButtons.CLScreenHeight, 9, GuiShopIconBigLock.UvLeft, GuiShopIconBigLock.UvTop, GuiShopIconBigLock.UvWidth, GuiShopIconBigLock.UvHeight);

       /* Level = new GuiNumbers()
        {
            UvLeft = GuiShopNumbers.UvLeft[hardware],
            UvTop = GuiShopNumbers.UvTop[hardware],
            UvWidth = GuiShopNumbers.UvWidth[hardware],
            UvHeight = GuiShopNumbers.UvHeight[hardware],
            Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiSwordLevel.ScreenLeft[hardware], GuiSwordLevel.ScreenBottom[hardware]),GuiSwordLevel.ScreenWidth[hardware], GuiSwordLevel.ScreenHeight[hardware], 9, GuiShopNumbers.UvLeft[hardware], GuiShopNumbers.UvTop[hardware], GuiShopNumbers.UvWidth[hardware], GuiShopNumbers.UvHeight[hardware]),
            }
        };*/

        //Hide();
    }

    public void Reset()
    {
        Hide();
    }

    public void Show()
    {
        DefaultSpriteUI.ShowSprite(SpriteB);

        if(SpriteC != null)
            DefaultSpriteUI.ShowSprite(SpriteC);
        if (SpriteL != null)
            DefaultSpriteUI.ShowSprite(SpriteL);
        //Gui.ShowNumbers(Level, (int)Game.Instance.SwordLevel, 5);

        if (Game.Instance.SwordLevelMaxed() == false && Money != null)
        {
            Gui.ShowNumbers(Money, (int)Game.PriceSword[(int)Game.Instance.SwordLevel], 9999);
            if (Game.Instance.CouldBuySwordLevel())
                SetColor(Money.Sprites, Color.white);
            else
                SetColor(Money.Sprites, GuiManager.DisabledColor);
        }
        else
            HideMoney();
    }


    public void Hide()
    {
        DefaultSpriteUI.HideSprite(SpriteB);

        if (SpriteC != null)
            DefaultSpriteUI.HideSprite(SpriteC);

        if (SpriteL != null)
            DefaultSpriteUI.HideSprite(SpriteL);

        HideMoney();

        ///DefaultSpriteUI.HideSprite(Level.Sprites[0]);
    }

    void SetColor(Sprite[] Sprites, Color color)
    {
        for (int i = 0; i < Sprites.Length; i++)
            Sprites[i].SetColor(color);
    }

    void HideMoney()
    {
        if (Money == null)
            return;

        for (int i = 0; i < Money.Sprites.Length; i++)
            DefaultSpriteUI.HideSprite(Money.Sprites[i]);
    }
}

