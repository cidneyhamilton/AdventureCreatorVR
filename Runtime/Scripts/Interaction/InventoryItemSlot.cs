/**
	* InventoryItemSlot.cs
	*
 * Shows a 3D inventory object in the player's hand, on top of the controller
	* 
	* by Cidney Hamilton
	*
 */
using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace CC.VR {

    public class InventoryItemSlot : BaseInteractorReticle
    {
								// Reference to the currently held inventory item
								public GameObject currentItem;

								// Turn on to enable forced grab, and display the itme in the player's hand
								public bool forceGrab;
	
								int currentItemID = 0;

								// Bind to events
								void OnEnable() {
												VREvents.OnEquipPrevious += EquipPrevious;
												VREvents.OnEquipNext += EquipNext;
												VREvents.OnEquip += Equip;
												VREvents.OnUnequip += Unequip;
												VREvents.OnDrop += Drop;
												VREvents.OnHideItem += HideItem;
												VREvents.OnShowItem += ShowItem;
												// EventManager.OnMenuTurnOn += UnequipOnPause;
												EventManager.OnInventoryDeselect += Unequip;
								}
	
								void OnDisable() {
												VREvents.OnUnequip -= Unequip;
												VREvents.OnEquipPrevious -= EquipPrevious;
												VREvents.OnEquipNext -= EquipNext;
												VREvents.OnEquip -= Equip;
												VREvents.OnDrop -= Drop;
												VREvents.OnHideItem -= HideItem;
												VREvents.OnShowItem -= ShowItem;
												EventManager.OnInventoryDeselect -= Unequip;
								}

								void Update() {
												if (currentItem == null) {
																// do nothing
												} else if (forceGrab) {
																// Size up
																transform.localPosition = new Vector3(0, 0, minDistance);
																transform.localScale = new Vector3(2f, 2f,2f);
												} else {
																UpdatePosition();
																UpdateSize();
												}
								}
								
								void UpdateSize()
								{
												if (transform.localPosition.z > 1f)
            {
																transform.localScale = new Vector3(transform.localPosition.z, transform.localPosition.z, transform.localPosition.z);
            } else {
																transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

								// Show AC's selected item
								void ShowItem() {
												InvItem item;
												if (KickStarter.runtimeInventory.SelectedInstance == null) {
																// No selected item; do nothing
																return;
												}
												item = KickStarter.runtimeInventory.SelectedInstance.InvItem;
												Destroy(currentItem);
												Debug.Log("Loading " + item.label);
												currentItem = Instantiate(Resources.Load("InventoryCursors/" + item.label) as GameObject, transform);
												if (currentItem == null) {
																Debug.LogWarning("Item not found.");
												}

												currentItem.transform.parent = gameObject.transform;
	    
												MakeReticleActive();
												currentItemID = GetIDOfItem(item);
												// ToggleHand(true);

								}
	
								void HideItem() {
												if (currentItem != null) {
																Debug.Log("Hiding the current item.");
																Destroy(currentItem);
												}
												ToggleHand(false);
												MakeReticleInactive();
								}
	
								// Unequip the current item when entering the Pause menu
								void UnequipOnPause(Menu menu, bool isInstant) {
												if (menu.title == "Pause" && currentItem != null) {
																Unequip();
												}
								}

								void Unequip() {
												KickStarter.runtimeInventory.SetNull();
												currentItemID = -1;
												HideItem();
												if (currentItem != null) {
																Debug.Log("Current item exists; destroying it.");
																Destroy(currentItem);
												}
								}
	
								// Unequip the current item from hand. Does not remove from inventory.
								void Unequip(InvItem item) {
												Unequip();
								}
	
								// Run drop action for this item
								void Drop() {
												Unequip();	    
												InvInstance itemInstance = KickStarter.runtimeInventory.SelectedInstance;
												// itemInstance.Use(DROP_CURSOR);
								}
	
								// If HideControllerOnSelect is set, show the hand again
								void ToggleHand(bool showItem) {
												if (transform.parent.GetComponent<XRBaseControllerInteractor>().hideControllerOnSelect) {
																transform.parent.GetComponent<XRBaseController>().hideControllerModel = showItem;
												}
								}

								// Equip the last inventory item
								void EquipLast() {
												Equip(KickStarter.runtimeInventory.SelectedInstance.InvItem);
								}
	
								// Equip the inventory item with the given ID
								void Equip(int id) {
												List<InvItem> items = KickStarter.runtimeInventory.localItems;
												int numItems = items.Count;
												if (numItems == 0) {
																// Do nothing
												} else if (id >= numItems) {
																Debug.LogWarning("Out of range; can't equip." + id);
																Equip(items[0]);		
												} else if (id < 0) {
																Debug.LogWarning("Out of range; can't equip." + id);
																Equip(items[numItems - 1]);
												} else {
																Debug.Log("Equipping item " + id);
																Equip(items[id]);
												}
								}

								// Cycle equipped item to the previous inventory item 
								void EquipPrevious() {
												Unequip();
												Equip(currentItemID - 1);
								}

								// Cycle equipped item to the next inventory item
								void EquipNext() {
												Unequip();
												Equip(currentItemID + 1);
								}

								// Equip the given inventory item
								void Equip(InvItem item) {
												item.Select();
												ShowItem();
								}
	
								// remove hotspot interactables from currentItem, so player doesn't attempt to interact with them
								void RemoveHotspotInteractables()  {
												if (currentItem.GetComponent<HotspotInteractable>() != null) {
																Destroy(currentItem.GetComponent<HotspotInteractable>());
												} else if (currentItem.GetComponentInChildren<HotspotInteractable>() != null) {
																Destroy(currentItem.GetComponentInChildren<HotspotInteractable>());
												}
								}

								// Reduce length of the line ray so this item appears at the end
								void ToggleLineLength(bool isOverriden) {
												transform.parent.GetComponent<XRInteractorLineVisual>().overrideInteractorLineLength = isOverriden;
								}

								// Convert an inventory item to an ID
								int GetIDOfItem(InvItem item) {
												for (int i = 0; i < KickStarter.runtimeInventory.localItems.Count; i++) {
																InvItem testItem = KickStarter.runtimeInventory.localItems[i];
																if (item.id == testItem.id) {
																				return i;
																}
												}
												return -1;
								}
	
    }
    
}
