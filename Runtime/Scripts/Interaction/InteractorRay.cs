using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using AC;
    
namespace CC.VR {

    [RequireComponent(typeof(XRRayInteractor))]
    public class InteractorRay : MonoBehaviour
    {       
								
								// Cache the hotspot interactable this interactor ray hovered over
								HotspotInteractable hotspotInteractable;
        Hotspot hotspot;
								
        private float timeStarted = 0f;
								public float debounceTime = 0.25f;
								
								void Start() {
												KickStarter.playerInput.InputMousePositionDelegate = CustomGetMousePosition;
								}
								
								public LineCursor GetCursor() {
												return GetComponent<ToggleCursor>().activeCursor;
								}
								
								public void OnUse() {
												// Try to use the selected hotspot
												hotspot = GetCurrentHotspot();
            LineCursor cursor = GetCursor();
												if (cursor == LineCursor.Use) {
                if (hotspot != null) {
                    if (KickStarter.runtimeInventory.SelectedItem != null) {
                        InteractionHelpers.HandleHotspotUseInventory(hotspot);
                    } else {
                        InteractionHelpers.HandleHotspotUse(hotspot);
                    }
                } else {
																				// Trying to use a selected item without a hotspot
																				VREvents.PlayDefaultUse();
																}
												} else if (cursor == LineCursor.Look) {
																if (hotspot != null) {
																				InteractionHelpers.HandleHotspotLook(hotspot);
																} else {
																				PlayRoomDescription();
																}
												} else if (cursor == LineCursor.Compass || cursor == LineCursor.Select) {
																// No action when the compass is selected
												} else {
																Debug.LogError("Unhandled cursor: " + cursor);
												}				
								}
	
								public void OnToggleMenu() {
												// Don't allow the user to accidentally toggle the menu too many times
												if (timeStarted + debounceTime >= Time.time) {
																Debug.LogWarning("Can't open menu.");
																return;
												}	    
												timeStarted = Time.time;		
												// Debug.Log("Opening menu.");		
												ToggleMenu("Pause");
								}
	
								private Vector2 CustomGetMousePosition(bool cursorIsLocked) {
												RaycastHit raycastHit;
												XRRayInteractor rayInteractor = GetComponent<XRRayInteractor>();
												if (rayInteractor == null) {
																// XRRayInteractor was destroyed; don't use it
																return Vector2.zero;
												} else {
																bool isValid = rayInteractor.TryGetCurrent3DRaycastHit(out raycastHit);
																return KickStarter.CameraMain.WorldToScreenPoint(raycastHit.point);
												}
								}

								private Hotspot GetCurrentHotspot() {
            Hotspot hotspot = null;
            RaycastHit raycastHit;
												XRRayInteractor rayInteractor = GetComponent<XRRayInteractor>();
												if (rayInteractor == null) {
																// XRRayInteractor was destroyed; don't use it
																return null;
												} else {
																bool isValid = rayInteractor.TryGetCurrent3DRaycastHit(out raycastHit);
																// Debug.Log("Hitting a trigger.");
																if (isValid && raycastHit.collider != null)
																{
																				hotspot = InteractionHelpers.GetHotspotFromRaycastHit(raycastHit);
																}	    
																return hotspot;
												}
												
        }
	    
								void ToggleMenu(string menuName) {
												// Get the menu corresponding to this string
												Menu menu = PlayerMenus.GetMenuWithName(menuName);
	    
												// Now toggle the menu
												if (menu.IsOn()) {
																menu.TurnOff();
																Debug.Log("Resetting cursor; menu off.");
																VREvents.ResetCursor();
												} else {
																menu.TurnOn();
																Debug.Log("Resetting cursor; menu on.");
																VREvents.ResetCursor();
												}
								}
	
								void PlayRoomDescription() {
												string triggerName = CC_Resources.CC_RoomDescriptions.findRoomDescription(KickStarter.player.transform.position);
												GameObject triggerGO = GameObject.Find(triggerName);
												if (triggerGO != null) {
																AC_Trigger trigger = triggerGO.GetComponent<AC_Trigger>();
																if (trigger != null) {
																				Debug.Log("Playing look for trigger.");
																				trigger.Interact();
																} else {
																				Debug.Log("Playing default look.");
																				VREvents.PlayDefaultLook();
																}
												} else {
																Debug.Log("Playing default look.");
																VREvents.PlayDefaultLook();
												}
								}
    }
    
}
