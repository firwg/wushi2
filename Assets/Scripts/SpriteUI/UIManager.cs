using UnityEngine;
using System.Collections;

public interface UIManager {
	void OnMouseDown(Sprite sprite, Vector2 pos);
	void OnMouseUp(Sprite sprite, Vector2 pos);
	void OnMouseMoved(Sprite sprite, Vector2 pos);
	void OnMouseExit(Sprite sprite);
	
	void OnTouchDown(Sprite sprite, Vector2 pos, int touch);
	void OnTouchUp(Sprite sprite, Vector2 pos, int touch);
	void OnTouchMoved(Sprite sprite, Vector2 pos, int touch);
	void OnTouchExit(Sprite sprite, int touch);
}
