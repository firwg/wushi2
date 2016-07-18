using UnityEngine;
using System.Collections;

public class UIManagerAdapter : UIManager
{
	public virtual void OnMouseDown(Sprite sprite, Vector2 pos){}
	public virtual void OnMouseUp(Sprite sprite, Vector2 pos){}
	public virtual void OnMouseMoved(Sprite sprite, Vector2 pos){}
	public virtual void OnMouseExit(Sprite sprite){}
	
	public virtual void OnTouchDown(Sprite sprite, Vector2 pos, int touch){}
	public virtual void OnTouchUp(Sprite sprite, Vector2 pos, int touch){}
	public virtual void OnTouchMoved(Sprite sprite, Vector2 pos, int touch){}
	public virtual void OnTouchExit(Sprite sprite, int touch){}
}
