/**
 * PlayerMovementToggle.cs
 *
 * Handles enabling and disabling the VR locomotion system, 
 * so the player can't move when a menu is open.
 *
 */
using UnityEngine;

namespace CC.VR {

    public class PlayerMovementToggle : MonoBehaviour
    {

								[SerializeField]
								CustomMovement movement;
	
								void OnEnable() {	    
												VREvents.OnLocomotionDisable += DisableLocomotion;
												VREvents.OnLocomotionEnable += EnableLocomotion;
								}

								void OnDisable() {
												VREvents.OnLocomotionDisable -= DisableLocomotion;
												VREvents.OnLocomotionEnable -= EnableLocomotion;
								}

								void Start() {
												if (movement == null) {
																movement = GetComponent<CustomMovement>();
												}

												// TO TEST: disable locomotion on start
												// DisableLocomotion();
								}
	
								void DisableLocomotion() {
												Debug.Log("Disabling locomotion system on player.");
												movement.enabled = false;
								}

								void EnableLocomotion() {
												Debug.Log("Enabling locomotion system on player.");
												movement.enabled = true;
								}
    }


}
