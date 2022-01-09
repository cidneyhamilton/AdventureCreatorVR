/**
 * Dynamically adjusts the player's speed based on the slider in the Options menu
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;

namespace CC.VR {

    public class PlayerSpeedFactor : MonoBehaviour
    {

	// TODO: These are currently hardcoded, but should be the max and min values in the Speed Slider
	public float minSpeed = 2f;
	public float maxSpeed = 4f;

	const string SPEED_VAR = "Environment/walkSpeed";
	
	void OnEnable() {
	    VREvents.OnSetWalk += SetWalk;
	    VREvents.OnSetRun += SetRun;
	    EventManager.OnVariableChange += UpdateVariable;
	}
	
	void OnDisable() {	    
	    VREvents.OnSetWalk -= SetWalk;
	    VREvents.OnSetRun -= SetRun;
	    EventManager.OnVariableChange -= UpdateVariable;
	}
	
	void UpdateVariable(GVar variable) {
	    if (variable.label == SPEED_VAR) {
		float speed = Mathf.Clamp(variable.FloatValue, minSpeed, maxSpeed);
		Debug.Log("New player speed: " + speed);
		UpdatePlayerSpeed(speed);
	    }            
        }
	
	// Update the speed on this XR rig accordingly
	void UpdatePlayerSpeed(float speed) {
	    Debug.Log("New player speed: " + speed);
	    GlobalVariables.GetVariable(SPEED_VAR).FloatValue = speed;
	}

	// Set the player to a walk speed
	void SetWalk() {
	    UpdatePlayerSpeed(minSpeed);
	}

	// Set the player to a run speed
	void SetRun() {
	    UpdatePlayerSpeed(maxSpeed);
	}
    }
    
}
