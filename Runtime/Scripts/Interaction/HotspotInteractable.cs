using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using AC;

namespace CC.VR
{

    /// <summary>
    /// Add an interactable that contains a hotspot.
    /// Integrates <see cref="XRBaseInteractable"/> with Adventure Creator.
    /// </summary>
    public class HotspotInteractable : XRBaseInteractable
    {
        // The hotspot we're triggering events on
        Hotspot hotspot;
	
	// TODO: Define these from the Cursor Manager
	const int USE_CURSOR = 0;
	const int LOOK_CURSOR = 2;
	
        void Start()
        {
	    Setup();
        }

	// Called when the Look input action is used
	public void OnLook() {
	    if (isHovered) {
		HandleLook();
	    }
	}

	// Called when the Use input action is used
	public void OnUse() {
	    if (isHovered) {
		HandleUse();
	    }
	}

	// Performs an arbitrary interaction on a cursor
	public void HandleCursor(int cursor)
	{
	    RunInteraction(cursor);
	}

	void HandleUse() {
	    RunInteraction(USE_CURSOR);
	}
	
	void HandleLook() {
	    hotspot.RunUseInteraction(LOOK_CURSOR);
	}
	
	// Run automatically
	void RunInteraction(int cursor) {
	    // Get a reference to the currently selected item
	    InvInstance invItem = GetSelectedItem();
	    Debug.Log("Current selected inventory item " + invItem);
	    if (invItem == null) {
		hotspot.RunUseInteraction(cursor);
	    } else {
		hotspot.RunInventoryInteraction(invItem.ItemID);
	    }
	}
	
	InvInstance GetSelectedItem() {
	    return AC.KickStarter.runtimeInventory.SelectedInstance;
	}
	
	// Make sure that this is on AC's defined hotspot layer
	void Setup() {	 
	    
	    gameObject.layer = LayerMask.NameToLayer("Hotspot");
	    hotspot = GetComponent<Hotspot>();
	    if (hotspot == null) {
		hotspot = GetComponentInChildren<Hotspot>();
	    }
	    if (hotspot == null)
	    {
		Debug.LogWarning("No hotspot attached to " + gameObject.name);
	    }
	    else
	    {
		hotspot.gameObject.layer = LayerMask.NameToLayer("Hotspot");
		
		// For VR, make sure that this is a NOT a trigger
		
		if (BaseXRToggle.IsXRPresent())
		{
		    hotspot.gameObject.GetComponent<BoxCollider>().isTrigger = false;
		}
	    }
	}
    }
    
}
