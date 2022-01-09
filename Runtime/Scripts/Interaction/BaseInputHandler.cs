using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CC.VR {
    
    public class BaseInputHandler : MonoBehaviour
    {

	// Is a press in progress?
	protected bool pressInProgress;

	// Time spend before a debounce
	float waitTime = 0.5f;
	
	// Threshold before an input is counted 
	const float THRESHOLD = 0.5f;

	protected void HandleInput(Vector2 rawInput) {
	    Vector2 input = Normalize(rawInput);
	    if (input == Vector2.zero) {
		pressInProgress = false;
		// Debounce(waitTime);
	    } else if (pressInProgress) {
		// Haven't released the controls
	    } else {
		pressInProgress = true;
		ProcessAction(input);
	    } 
	}

	protected virtual void ProcessAction(Vector2 input) {
	    // Process action
	}       

	
	protected IEnumerator Debounce(float waitTime) {
	    yield return new WaitForSeconds(waitTime);
	    pressInProgress = false;
	}

	protected Vector2 Normalize(Vector2 input) {
	    if (input.y > THRESHOLD && input.x < THRESHOLD && input.x > -THRESHOLD) {
		return Vector2.up;
	    } else if (input.y < -THRESHOLD && input.x < THRESHOLD && input.x > -THRESHOLD) {
		return Vector2.down;
	    } else if (input.x > THRESHOLD && input.y < THRESHOLD && input.y > -THRESHOLD) {
		return Vector2.right;
	    } else if (input.x < -THRESHOLD && input.y < THRESHOLD && input.y > -THRESHOLD) {
		return Vector2.left;
	    } else {
		return Vector2.zero;
	    }
	}


    }

}
