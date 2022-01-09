using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;
using UnityEngine.XR.Interaction.Toolkit;

namespace CC.VR {

    public class AssignInteractions : MonoBehaviour
    {
	XRInteractionManager interactionManager;
		
	void OnEnable() {
	    EventManager.OnOccupyPlayerStart += SetupPlayer;
	}

	void OnDisable() {
	    EventManager.OnOccupyPlayerStart -= SetupPlayer;
	}
	
	void SetupPlayer(Player player, PlayerStart playerStart) {;	
	    // Set player starting rotation
	    float turnAmount = playerStart.transform.rotation.eulerAngles.y;
	    player.GetComponent<XRRig>().RotateAroundCameraUsingRigUp(turnAmount);
	    	    
	    AssignInteractionManager(player);
	}	

	void AssignInteractionManager(Player player) {	
	    Debug.Log("Assigning interaction manager to all interactables in this scene.");
	    interactionManager = player.GetComponentInChildren<XRInteractionManager>();
	    XRBaseInteractable[] interactables = GameObject.FindObjectsOfType<XRBaseInteractable>();
	    foreach (XRBaseInteractable interactable in interactables) {
		Debug.Log("Assigning interaction manager to " + interactable.name);
		interactable.interactionManager = interactionManager;
	    }	    
	}
	
    }
    
}
