using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if(BasicDialog.state == BasicDialog.State.Hide){
			if(GUI.Button(new Rect(100, 100, 200, 100), "显示")){
				BasicDialog.state = BasicDialog.State.Show;
			}
		}
	}
}
