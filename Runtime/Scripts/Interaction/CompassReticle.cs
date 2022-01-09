using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AC;

namespace CC.VR {

    public class CompassReticle : BaseInteractorReticle
    {
	
	public GameObject compassCanvas;
	public Text canvasText;	
	
	protected override void Start() {
            base.Start();
            compassCanvas.gameObject.SetActive(false);
	}
	
	void OnEnable() {
	    VREvents.OnAfterSetCompass += MakeReticleActive;
	    VREvents.OnMakeCompassInactive += MakeReticleInactive;
	}
	
	void OnDisable() {
	    VREvents.OnAfterSetCompass -= MakeReticleActive;
	    VREvents.OnMakeCompassInactive -= MakeReticleInactive;
	}
	
	protected void Update() {
            if (isActive) {
		UpdatePosition();
		canvasText.text = GetDirection();
	    }
	}
	
	protected override void MakeReticleActive() {
            base.MakeReticleActive();
            compassCanvas.gameObject.SetActive(true);
	}
	
	protected override void MakeReticleInactive() {
            base.MakeReticleInactive();
            compassCanvas.gameObject.SetActive(false);
	}
	
	private string GetDirection() {
	    // Get letter from player angle
	    // TODO: Is this the correct angle, or do I need to apply an offset?
	    float angle = transform.parent.eulerAngles.y;
	    angle = angle % 360f;
	    // Debug.Log("Angle: " + angle);
	    if (angle < 27.5 && angle >= -27.5) {
		return "N";
	    } else if (angle < 72.5 && angle >= 27.5) {
		return "NE";
	    } else if (angle < 117.5 && angle >= 72.5) {
		return "E";
	    } else if (angle < 162.5 && angle >= 117.5) {
		return "SE";
	    } else if (angle < 205.5 && angle >= 162.5) {
		return "S";
	    } else if (angle < 250.5 && angle >= 205.5) {
		return "SW";
	    } else if (angle < 295.5 && angle >= 250.5) {
		return "W";
	    } else if (angle < 340.5 && angle >= 295.5) {
		return "NW";
	    } else if (angle < 385.5 && angle >= 340.5) {
		// Handle wraparound
		return "N";
	    } else {
		Debug.LogError("Angle not found.");
		return "X";
	    }
	    
	}
    }

}
