using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpriteButton
{
	public int TouchCount;
	
	private Vector2 _screenPosition;
    private Vector2 _LeftDown;
	private Vector2 _RightUp;
	private Sprite _sprite;
	
	public Sprite sprite
	{
		get { return _sprite; }
		set { _sprite = value; }
	}
	public Vector2 screenPosition
	{
		get { return _screenPosition; }
		set { _screenPosition = value; }
	}
    public Vector2 RightUp
	{
        get { return _RightUp; }
        set { _RightUp = value; }
	}

    public Vector2 LeftDown
    {
        get { return _LeftDown; }
        set { _LeftDown = value; }
    }

    public bool Hidden
	{
		get { return ( sprite.hidden ); }
		set
		{
			sprite.hidden = value;
		}
	}
	/*
	public float left
	{
		get { return _screenPosition.x - 20; }
	}
	public float right
	{
        get { return _RightUp.x + 20; }
	}
	public float top
	{
		get { return _screenPosition.y - 20; }
	}
	public float bottom
	{
        get { return _RightUp.y + 20; }
	}*/
}

public class SpriteUI : SpriteManager
{
	public int DrawDepth = 100;

	public enum ZeroLocationEnum
	{
		LowerLeft = -1,
		UpperLeft = 1
	}
	
	public LayerMask UILayer = 0;
	public ZeroLocationEnum ZeroLocation = ZeroLocationEnum.LowerLeft; 	
	public ScreenOrientation ScreenOrientation = ScreenOrientation.Portrait;
	
	private Camera _uiCamera;
	private GameObject _uiCameraHolder;
	private float _xOffset;
	private float _yOffset;

//    private List<SpriteButton> _buttons = new List<SpriteButton>();

	protected override void Awake()
	{
		base.Awake();
		_uiCameraHolder = new GameObject("UI Camera");
		_uiCameraHolder.AddComponent<Camera>(  );
		_uiCamera = _uiCameraHolder.GetComponent<Camera>();
		_uiCamera.clearFlags = CameraClearFlags.Depth;
		_uiCamera.nearClipPlane = 0.3f;
		_uiCamera.farClipPlane = 100.0f;
		_uiCamera.depth = DrawDepth;
		_uiCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f );
		_uiCamera.orthographic = true;
        _uiCamera.orthographicSize = Screen.height * 0.5f;
		_uiCamera.cullingMask = UILayer;
		_uiCamera.transform.position = new Vector3( 0.0f, 0.0f, -10.0f );
		UpdateUISize();

        //Debug.Log("Starting UI for screen " +  Screen.width + "x" + Screen.currentResolution.height);
	}
	
	public void UpdateUISize()
	{
     	_xOffset = - Screen.width / 2.0f;
		_yOffset = Screen.height / 2.0f;
	}
	
	public Sprite AddElement( Vector2 leftDown, float width, float height, float depth, int leftPixelX, int bottomPixelY, int pixelWidth, int pixelHeight)
    {
        return AddElement(leftDown, width, height, depth, PixelCoordToUVCoord(leftPixelX, bottomPixelY), PixelSpaceToUVSpace(pixelWidth, pixelHeight));
    }

    public Sprite AddElement(Vector2 leftDown, float width, float height, float depth, Vector2 lowerLeftUV, Vector2 uvSize)
	{
        return AddUIElement(leftDown, width, height, depth, lowerLeftUV, uvSize, null, false);
	}

  /*  public void RemoveElement( Sprite sprite )
	{
		SpriteButton selected = GetSpriteButton( sprite );
		GameObject obj = sprite.client;
		if ( selected != null )
		{
			_buttons.Remove( selected );
		}
		RemoveSprite( sprite );
		GameObject.Destroy( obj );
	}*/

	
	/*public void SetHidden( Sprite sprite, bool hidden )
	{
		SpriteButton selected = GetSpriteButton( sprite );
		if ( selected != null )
		{
			selected.Hidden = hidden;
		}
	}*/
/*
    private SpriteButton GetSpriteButton(Sprite sprite)
    {
       // GameObject obj = sprite.client;
        SpriteButton selected = null;


        for (int i = 0; i < _buttons.Count; i++)
        {
            if (_buttons[i].sprite == sprite)
            {
                selected = _buttons[i];
                break;
            }
        }
        
        return selected;
    }*/
    private Sprite AddUIElement(Vector2 leftDown, float width, float height, float depth, Vector2 lowerLeftUV, Vector2 uvSize, UIManager manager, bool isActive /*, bool pureSprite*/)
	{
        UpdateUISize();

		GameObject element = new GameObject("UI Element");
		Transform t = element.transform;
        float xPos = (leftDown.x + _xOffset + (width / 2.0f));
        float yPos = (int)ZeroLocation * (-leftDown.y + _yOffset - (height / 2.0f));
		t.position = new Vector3( xPos, yPos, depth );
		// UILayer.value is a mask, find which bit is set 
		for (int i = 0; i < sizeof(int) * 8; i++) 
		{ 
			if ((UILayer.value & (1 << i)) == (1 << i)) 
			{ 
				element.layer = i; 
				break; 
			} 
		}
		Sprite sprite = AddSprite( element, width, height, lowerLeftUV, uvSize, false );
		/*

		SpriteButton spriteButton = new SpriteButton();
		spriteButton.TouchCount = 0;
		spriteButton.sprite = sprite;
        spriteButton.screenPosition = leftDown;
        spriteButton.LeftDown = leftDown;
        spriteButton.RightUp = new Vector2(leftDown.x + width, leftDown.y + height);
        _buttons.Add(spriteButton);*/
		return sprite;
	}

    public void UpdateSpriteSize(Sprite sprite, Vector2 upperLeft, float width, float height)
    {
        float xPos = (upperLeft.x + _xOffset + (width / 2.0f));
        float yPos = (int)ZeroLocation * (-upperLeft.y + _yOffset - (height / 2.0f));
        sprite.clientTransform.position = new Vector3(xPos, yPos, sprite.clientTransform.position.z);
        sprite.SetSizeXY(width, height);
        Transform(sprite);
    }

	public void UpdateSpriteSize(Sprite sprite, Vector2 leftDown, float width, float height, int leftPixelX, int bottomPixelY, int pixelWidth, int pixelHeight)
	{
		UpdateSpriteSize(sprite, leftDown, width, height, PixelCoordToUVCoord(leftPixelX, bottomPixelY), PixelSpaceToUVSpace(pixelWidth, pixelHeight));
	}

    void UpdateSpriteSize(Sprite sprite, Vector2 leftDown, float width, float height, Vector2 lowerLeftUV, Vector2 uvSize)
	{
        float xPos = (leftDown.x + _xOffset + (width / 2.0f));
        float yPos = (int)ZeroLocation * (-leftDown.y + _yOffset - (height / 2.0f));
		sprite.clientTransform.position = new Vector3( xPos, yPos, sprite.clientTransform.position.z );
		sprite.SetSizeXY(width, height);
		sprite.uvDimensions = uvSize;
		sprite.lowerLeftUV = lowerLeftUV;
	}

    public void SetSpritePosition(Sprite s, Vector2 leftDown)
    {
        s.clientTransform.position = new Vector3((leftDown.x + _xOffset + (s.width / 2.0f)), (int)ZeroLocation * (-leftDown.y + _yOffset - (s.height / 2.0f)), 0); 
        Transform(s);
    }

    public IEnumerator MoveSprite(Sprite s, float time, float delay, Vector2 leftDown)
    {       
        yield return new WaitForSeconds(delay);

        float currentTime = 0;
        Vector3 start = s.clientTransform.position;
        Vector3 end = new Vector3((leftDown.x + _xOffset + (s.width / 2.0f)), (int)ZeroLocation * (-leftDown.y + _yOffset - (s.height / 2.0f)), 0);

        if (time == 0)
        {
            s.clientTransform.position = end;
            Transform(s);
            yield break;
        }

        float progress;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            if (currentTime > time)
                currentTime = time;

            progress = currentTime / time;
            s.clientTransform.position =  Mathfx.Hermite(start, end, progress);
            Transform(s);
            yield return new WaitForEndOfFrame();
        }

    }
}
