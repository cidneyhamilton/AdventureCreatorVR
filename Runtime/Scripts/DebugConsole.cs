using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CC.VR {

    public class DebugConsole : MonoBehaviour
    {
	
	public Text text;
	
	void OnEnable() {
	    VREvents.OnDebugLog += Log;
	}
	
	void OnDisable() {
	    VREvents.OnDebugLog -= Log;
    }
	void Awake() {
	    text = GetComponentInChildren<Text>();
	    text.text = "";
	}
	
	// Update is called once per frame
	void Log(string message)
	{
	    text.text = message;
	}
    }
    
}
