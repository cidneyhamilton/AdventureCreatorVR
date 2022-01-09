/**
 * VREvents.cs
 * Event manager class for VR
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;

namespace CC.VR
{
    public static class VREvents
    {

	// Compass events
	public static System.Action OnAfterSetCompass;
	public static System.Action OnMakeCompassInactive;

	public static System.Action OnDrop;
	
	// Cursor cycling
	public static System.Action OnEquipNext;
	public static System.Action OnEquipPrevious;

	// Item toggling
	public static System.Action OnHideItem;
	public static System.Action OnShowItem;

	// Locomotion toggling
	public static System.Action OnLocomotionEnable;
	public static System.Action OnLocomotionDisable;

	// Snap turn toggling
	public static System.Action OnRotationEnable;
	public static System.Action OnRotationDisable;
	
	public static System.Action OnPlayDefaultLook;
	public static System.Action OnPlayDefaultUse;
	
	// On resetting the cursor to the default
	public static System.Action OnResetCursor;
       
	// On setting the cursor or movement method
	public static System.Action OnSetLook;      
	public static System.Action OnSetRun;
	public static System.Action OnSetUse;       
	public static System.Action OnSetWalk;

	// On unequipping the active inventory item
	public static System.Action OnUnequip;

	// Delegate to equip an inventory item
	public delegate void Delegate_Equip_Item(InvItem item);
	public static Delegate_Equip_Item OnEquip;

	// Error log delegates
	public delegate void Delegate_Log_Message(string message);
	public static Delegate_Log_Message OnDebugLog;

	// Hotspot delegates
        public delegate void Delegate_Change_Hotspot(Hotspot hotspot);
        public static Delegate_Change_Hotspot OnPlaceHotspotMenu;
        public static Delegate_Change_Hotspot OnPlaceInteractMenu;

	// Speech handling
	public delegate void Delegate_StartSpeech(AC.Char speakingCharacter, string speechText, int lineId);
	public static Delegate_StartSpeech OnPlaceSubtitleMenu;		

	// Raycast handling
	public delegate void Delegate_Raycast(RaycastHit hit);
	public static Delegate_Raycast OnPathfindToRaycast;

	// Static functions to fire events

	// Compass events
	public static void AfterSetCompass() {
	    if (OnAfterSetCompass != null) {
		OnAfterSetCompass();
	    }
	}

		
	public static void MakeCompassInactive() {
	    if (OnMakeCompassInactive != null) {
		OnMakeCompassInactive();
	    }
	}
	
	// Drop the selected inventory item
	public static void Drop() {
	    if (OnDrop != null) {
		OnDrop();
	    }
	}
	
	// Cycle inventory items to the right
	public static void EquipNext() {
	    if (OnEquipNext != null) {
		OnEquipNext();
	    }
	}
	
	// Cycle inventory items to the left
	public static void EquipPrevious() {
	    if (OnEquipPrevious != null) {
		OnEquipPrevious();
	    }
	}


	// Item toggling
	
	// Hide the selected inventory item
	public static void HideItem() {
	    if (OnHideItem != null) {
		OnHideItem();
	    }
	}

	
	// Hide the selected inventory item
	public static void ShowItem() {
	    if (OnShowItem != null) {
		OnShowItem();
	    }
	}

	// Locomotion toggling
	public static void EnableLocomotion() {
	    if (OnLocomotionEnable != null) {
		OnLocomotionEnable();
	    }
	}

	public static void DisableLocomotion() {
	    if (OnLocomotionDisable != null) {
		OnLocomotionDisable();
	    }
	}
	
	public static void EnableRotation() {
	    if (OnRotationEnable != null) {
		OnRotationEnable();
	    }
	}

	public static void DisableRotation() {
	    if (OnRotationDisable != null) {
		OnRotationDisable();
	    }
	}
	// Default voice over events
	
	public static void PlayDefaultLook() {
	    if (OnPlayDefaultLook != null) {
		OnPlayDefaultLook();
	    }
	}
	
	public static void PlayDefaultUse() {
	    if (OnPlayDefaultUse != null) {
		OnPlayDefaultUse();
	    }
	}
	
	public static void ResetCursor() {
	    if (OnResetCursor != null) {
		OnResetCursor();
	    }
	}

	
	// On setting the cursor or movement method
	public static void SetLook() {
	    if (OnSetLook != null) {
		OnSetLook();
	    }
	}
	
	public static void SetRun() {
	    if (OnSetRun != null) {
		OnSetRun();
	    }
	}
	
	public static void SetUse() {
	    if (OnSetUse != null) {
		OnSetUse();
	    }
	}
	
	public static void SetWalk() {
	    if (OnSetWalk != null) {
		OnSetWalk();
	    }
	}

	// Unequip the selected inventory item
	public static void Unequip() {
	    if (OnUnequip != null) {
		OnUnequip();
	    }
	}

	// Equip an inventory item
	public static void Equip(InvItem item) {
	    if (OnEquip != null) {
		OnEquip(item);
	    }
	}
	
	public static void DebugLog(string message) {
	    if (OnDebugLog != null) {
		// Debug.Log(message);
		OnDebugLog(message);
	    }
	}
	
	// Hotspot events
	
	public static void PlaceHotspotMenu(Hotspot hotspot)
        {
            if (OnPlaceHotspotMenu != null)
            {
                OnPlaceHotspotMenu(hotspot);
            }
        }

        public static void PlaceInteractMenu(Hotspot hotspot)
        {
            if (OnPlaceInteractMenu != null)
            {
                OnPlaceInteractMenu(hotspot);
            }
        }

	public static void PlaceSubtitleMenu(AC.Char speakingCharacter, string speechText, int lineId) {
	    if (OnPlaceSubtitleMenu != null) {
		OnPlaceSubtitleMenu(speakingCharacter, speechText, lineId);
	    }
	}

	public static void PathfindToRaycast(RaycastHit hit) {
	    if (OnPathfindToRaycast != null) {
		OnPathfindToRaycast(hit);
	    }
	}

    }

}
