using UnityEngine;
using System.Collections;
using System;

public class GuiShopInfo
{
    //private GuiManager Gui;
    private SpriteUI DefaultSpriteUI;
    private Sprite SpriteC;
    private Sprite SpriteB;
    private Sprite SpriteS;
    private Sprite SpriteI;
    private Sprite SpriteBuy;

    private int ButtonIndex;

    public void Start(GuiManager manager)
    {
        //Gui = manager;
        DefaultSpriteUI = manager.DefaultSpriteUI;

        SpriteS = DefaultSpriteUI.AddElement(new Vector2(GuiShopBuyInfo.SScreenLeft, GuiShopBuyInfo.SScreenBottom), GuiShopBuyInfo.SScreenWidth, GuiShopBuyInfo.SScreenHeight, 9, GuiShopBuyInfo.SUvLeft, GuiShopBuyInfo.SUvTop, GuiShopBuyInfo.SUvWidth, GuiShopBuyInfo.SUvHeight);
        SpriteB = DefaultSpriteUI.AddElement(new Vector2(GuiShopBuyInfo.BScreenLeft, GuiShopBuyInfo.BScreenBottom), GuiShopBuyInfo.BScreenWidth, GuiShopBuyInfo.BScreenHeight, 9, GuiShopBuyInfo.BUvLeft, GuiShopBuyInfo.BUvTop, GuiShopBuyInfo.BUvWidth, GuiShopBuyInfo.BUvHeight);
        SpriteC = DefaultSpriteUI.AddElement(new Vector2(GuiShopBuyInfo.CScreenLeft, GuiShopBuyInfo.CScreenBottom), GuiShopBuyInfo.CScreenWidth, GuiShopBuyInfo.CScreenHeight, 9, GuiShopComboButtons.CUvLeft[0], GuiShopComboButtons.CUvBottom[0], GuiShopComboButtons.CUvWidth, GuiShopComboButtons.CUvHeight);
        SpriteI = DefaultSpriteUI.AddElement(new Vector2(GuiShopBuyInfo.IScreenLeft, GuiShopBuyInfo.IScreenBottom), GuiShopBuyInfo.IScreenWidth, GuiShopBuyInfo.IScreenHeight, 9, GuiShopBuyInfo.IUvLeft, GuiShopBuyInfo.IUvTop[0], GuiShopBuyInfo.IUvWidth, GuiShopBuyInfo.IUvHeight);

        SpriteBuy = DefaultSpriteUI.AddElement(new Vector2(GuiShopBuyInfo.OkScreenLeft, GuiShopBuyInfo.OkScreenBottom), GuiShopBuyInfo.OkScreenWidth, GuiShopBuyInfo.OkScreenHeight, 9,
            GuiShopBuyInfo.OkUvLeft, GuiShopBuyInfo.OkUvTop, GuiShopBuyInfo.OkUvWidth, GuiShopBuyInfo.OkUvHeight);
	}

    public void Reset()
    {
        Hide();
    }

    public void Show(bool showBuy, int buttonIndex)
    {
        ButtonIndex = buttonIndex;

        DefaultSpriteUI.ShowSprite(SpriteS);
        DefaultSpriteUI.ShowSprite(SpriteB);

        SpriteC.lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiShopComboButtons.CUvLeft[buttonIndex], GuiShopComboButtons.CUvBottom[buttonIndex]);
        SpriteI.lowerLeftUV = DefaultSpriteUI.PixelCoordToUVCoord(GuiShopBuyInfo.IUvLeft, GuiShopBuyInfo.IUvTop[buttonIndex]);

        DefaultSpriteUI.ShowSprite(SpriteC);
        DefaultSpriteUI.ShowSprite(SpriteI);

        if (showBuy)
            DefaultSpriteUI.ShowSprite(SpriteBuy);
    }

    public void Hide(bool buy)
    {
        if (buy)
        {
            switch (ButtonIndex)
            {
                case 0:
                    Game.Instance.BuyHealthLevel();
                    break;
                case 1:
                    Game.Instance.BuySwordLevel();
                    break;
                default:
                    Game.Instance.BuyComboLevel(ButtonIndex - 2);
                    break;
            }
        }
        Hide();
    }

    public void Hide()
    {
        DefaultSpriteUI.HideSprite(SpriteS);
        DefaultSpriteUI.HideSprite(SpriteB);
        DefaultSpriteUI.HideSprite(SpriteC);
        DefaultSpriteUI.HideSprite(SpriteI);
        DefaultSpriteUI.HideSprite(SpriteBuy);
    }
}

