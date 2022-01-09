using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AC;

namespace CC.VR {

    public class InventoryInput :BaseInputHandler
    {
	
	// The Vector2 input for inventory
	Vector2 inventoryVal;
	
	void OnInventory(InputValue value) {
	    inventoryVal = value.Get<Vector2>();
	}
       
	void Update() {
	    HandleInput(inventoryVal);
	}
	
	protected override void ProcessAction(Vector2 input) {
	    Debug.Log("Processing inventory action.");
	    Menu inventory = PlayerMenus.GetMenuWithName("Inventory");
	    // Debug.Log("Normalized input value: " + input);
	    if (input == Vector2.up) {	   
		VREvents.DebugLog("Turning on inventory.");
		VREvents.Unequip();
		inventory.TurnOn();
	    } else if (input == Vector2.down) {
		Debug.Log("Turning off inventory.");
		inventory.TurnOff();

		// Unequipping inventory item
		VREvents.Unequip();
	    } else if (input == Vector2.left) {
		VREvents.DebugLog("Cycling to left.");
		// cycle inventory items to left
		VREvents.EquipPrevious();
	    } else if (input == Vector2.right) {
		VREvents.DebugLog("Cycling to right.");
		VREvents.EquipNext();
	    } else {
		VREvents.DebugLog("Inventory value: " + inventoryVal);
	    }
	}
    }

}
