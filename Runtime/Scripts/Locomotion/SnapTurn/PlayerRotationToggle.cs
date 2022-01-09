/**
 * PlayerRotationToggle.cs
 *
 * Handles enabling and disabling the VR rotation system, 
 * so the player can't move when a menu is open.
 *
 */
using UnityEngine;

namespace CC.VR {

    public class PlayerRotationToggle : MonoBehaviour
    {

								[SerializeField]
								ActionBasedSnapTurnProvider turn;
	
								void OnEnable() {	    
												VREvents.OnRotationDisable += DisableRotation;
												VREvents.OnRotationEnable += EnableRotation;
								}

								void OnDisable() {
												VREvents.OnRotationDisable -= DisableRotation;
												VREvents.OnRotationEnable -= EnableRotation;
								}

								void Start() {
												if (turn == null) {
																turn = GetComponent<ActionBasedSnapTurnProvider>();
												}

												// TO TEST: disable rotation on start
												// DisableRotation();
								}
	
								void DisableRotation() {
												Debug.Log("Disabling rotation system on player.");
												turn.enabled = false;
								}

								void EnableRotation () {
												Debug.Log("Enabling rotation system on player.");
												turn.enabled = true;
								}
    }


}
