using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GuiShopCombo
{
    private GuiManager Gui;
    private SpriteUI DefaultSpriteUI;
    private Sprite SpriteB;
    private Sprite SpriteC;
    private Sprite SpriteL;
    private GuiNumbers Level;
    private GuiNumbers Money;
    private int ComboIndex;
    ComponentPlayer.Combo ComboData;

    private List<Sprite> ProgressSprites = new List<Sprite>();


    public void Start(GuiManager manager, int Combo)
    {
        ComboIndex = Combo;
        ComboData = Player.Instance.PlayerComboAttacks[ComboIndex];

        if (ComboData == null)
            Debug.LogError("ComboData is Null " + ComboIndex);
        Gui = manager;
        DefaultSpriteUI = manager.DefaultSpriteUI;

        int buttonIndex = 2  + ComboIndex;

        SpriteB = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.BScreenLeft(buttonIndex), GuiShopComboButtons.BScreenBottom(buttonIndex)), GuiShopComboButtons.BScreenWidth(buttonIndex), GuiShopComboButtons.BScreenHeight(buttonIndex), 9, GuiShopComboButtons.BUvLeft, GuiShopComboButtons.BUvBottom, GuiShopComboButtons.BUvWidth, GuiShopComboButtons.BUvHeight);
        SpriteC = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.CScreenLeft(buttonIndex), GuiShopComboButtons.CScreenBottom(buttonIndex)), GuiShopComboButtons.CScreenWidth(), GuiShopComboButtons.CScreenHeight(), 9, GuiShopComboButtons.CUvLeft[buttonIndex], GuiShopComboButtons.CUvBottom[buttonIndex], GuiShopComboButtons.CUvWidth, GuiShopComboButtons.CUvHeight);
        SpriteL = DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.LScreenLeft(ComboIndex), GuiShopComboButtons.LScreenBottom(ComboIndex)), GuiShopComboButtons.LScreenWidth(), GuiShopComboButtons.LScreenHeight(), 9, GuiShopIconBigLock.UvLeft, GuiShopIconBigLock.UvTop, GuiShopIconBigLock.UvWidth, GuiShopIconBigLock.UvHeight);

        if (Game.Instance.GameType == E_GameType.SinglePlayer)
        {
            Money = new GuiNumbers()
            {
                UvLeft = GuiShopNumbers.UvLeft,
                UvTop = GuiShopNumbers.UvTop,
                UvWidth = GuiShopNumbers.UvWidth,
                UvHeight = GuiShopNumbers.UvHeight,
                Sprites = new Sprite[]{
                DefaultSpriteUI.AddElement(new Vector2(GuiComboNumbers.ScreenLeft(ComboIndex) + GuiComboNumbers.ScreenWidth * 3, GuiComboNumbers.ScreenBottom(ComboIndex)),GuiComboNumbers.ScreenWidth, GuiComboNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiComboNumbers.ScreenLeft(ComboIndex) + GuiComboNumbers.ScreenWidth * 2, GuiComboNumbers.ScreenBottom(ComboIndex)),GuiComboNumbers.ScreenWidth, GuiComboNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiComboNumbers.ScreenLeft(ComboIndex) + GuiComboNumbers.ScreenWidth , GuiComboNumbers.ScreenBottom(ComboIndex)),GuiComboNumbers.ScreenWidth, GuiComboNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
                DefaultSpriteUI.AddElement(new Vector2(GuiComboNumbers.ScreenLeft(ComboIndex), GuiComboNumbers.ScreenBottom(ComboIndex)),GuiComboNumbers.ScreenWidth, GuiComboNumbers.ScreenHeight, 9, GuiShopNumbers.UvLeft, GuiShopNumbers.UvTop, GuiShopNumbers.UvWidth, GuiShopNumbers.UvHeight),
            }
            };
        }

        for (int i = 0; i < ComboData.ComboSteps.Length; i++)
        {
            if (ComboData.ComboSteps[i].AttackType == E_AttackType.X)
                ProgressSprites.Add(DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.IScreenLeft(ComboIndex) + GuiShopComboButtons.IScreenWidth() * i, GuiShopComboButtons.IScreenBottom(ComboIndex)), GuiShopComboButtons.IScreenWidth(), GuiShopComboButtons.IScreenHeight(), 9, GuiShopIconX.UvLeft, GuiShopIconX.UvTop, GuiShopIconX.UvWidth, GuiShopIconX.UvHeight));
            else
                ProgressSprites.Add(DefaultSpriteUI.AddElement(new Vector2(GuiShopComboButtons.IScreenLeft(ComboIndex) + GuiShopComboButtons.IScreenWidth() * i, GuiShopComboButtons.IScreenBottom(ComboIndex)), GuiShopComboButtons.IScreenWidth(), GuiShopComboButtons.IScreenHeight(), 9, GuiShopIconY.UvLeft, GuiShopIconY.UvTop, GuiShopIconY.UvWidth, GuiShopIconY.UvHeight));
        }
    }

    public void Reset()
    {
        Hide();
    }

    public void Show()
    {
        DefaultSpriteUI.ShowSprite(SpriteB);


        if (ComboData.SwordLevel > Game.Instance.SwordLevel)
        {
            DefaultSpriteUI.ShowSprite(SpriteL);
            return;
        }
        else
        {
            DefaultSpriteUI.HideSprite(SpriteL);
        }

        DefaultSpriteUI.ShowSprite(SpriteC);

        for (int i = 0; i < ProgressSprites.Count; i++)
            DefaultSpriteUI.ShowSprite(ProgressSprites[i]);

        if (Game.Instance.ComboLevelMaxed(ComboIndex) == false && Money != null)
        {
            Gui.ShowNumbers(Money, (int)Game.PriceCombo[(int)Game.Instance.ComboLevel[ComboIndex]], 9999);

            if (Game.Instance.CouldBuyComboLevel(ComboIndex))
                SetColor(Money.Sprites, Color.white);
            else
                SetColor(Money.Sprites, GuiManager.DisabledColor);
        }
        else
            HideMoney();

        UpdateComboProgress();
    }

    public void Hide()
    {
        DefaultSpriteUI.HideSprite(SpriteB);
        DefaultSpriteUI.HideSprite(SpriteC);
        DefaultSpriteUI.HideSprite(SpriteL);

        HideMoney();

        for(int i = 0; i < ProgressSprites.Count;i++)
            DefaultSpriteUI.HideSprite(ProgressSprites[i]);
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

    public void UpdateComboProgress()
    {
        E_ComboLevel level = Game.Instance.ComboLevel[ComboIndex];

        for (int i = 0; i < ComboData.ComboSteps.Length; i++)
        {
            if (ComboData.ComboSteps[i].ComboLevel > level)
                ProgressSprites[i].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiShopIconLock.UvLeft, GuiShopIconLock.UvTop);
            else if (ComboData.ComboSteps[i].AttackType == E_AttackType.X)
                ProgressSprites[i].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiShopIconX.UvLeft, GuiShopIconX.UvTop); 
            else
                ProgressSprites[i].lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiShopIconY.UvLeft, GuiShopIconY.UvTop); 
        }

    }
}

