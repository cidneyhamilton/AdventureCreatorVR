using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using AC;

namespace CC.VR {
    
    public enum LineCursor {
	Select = 0, Look = 1, Use = 2 , Compass = 3, Drop = 4
    }
    
    public class ToggleCursor : BaseInputHandler
    {
	
        public LineCursor activeCursor = LineCursor.Look;
        public AudioClip selectClip, lookClip, useClip, compassClip, dropClip;

	public float selectLength = 10f;
	public float useLength = 3f;
	public float lookLength = 30f;
	public float compassLength = 10f;

	public GameObject InventorySlot;
	public float useCursorLength = 1.4f;
	public float selectCursorLength = 0.5f;	
	
        // Per Roberta, this is the WHITE gradient, narrated as SELECT
        [SerializeField]
	Gradient SelectGradient;
	
	// Per Roberta, this is the RED gradient, narrated as LOOK
	[SerializeField]
	Gradient LookGradient;
	
	// Per Roberta, this is the GREEN gradient, narrated as USE
	[SerializeField]
	Gradient UseGradient;
	
	// Per Roberta, this is the YELLOW gradient, narrated as COMPASS
	[SerializeField]
	Gradient CompassGradient;
	
	[SerializeField]
	XRInteractorLineVisual lineVisual;
	
	// The Vector2 input for interactions
	Vector2 interactionVal;
	
	void OnInteract(InputValue value) {
	    interactionVal = value.Get<Vector2>();
	}

	void OnEnable() {
	    VREvents.OnResetCursor += ResetCursor;
	    VREvents.OnSetLook += AssignLookCursor;
	    VREvents.OnSetUse += AssignUseCursor;
	    EventManager.OnAfterChangeScene += AfterSceneChange;
	}

	void OnDisable() {
            VREvents.OnResetCursor -= ResetCursor;
	    VREvents.OnSetLook -= AssignLookCursor;
	    VREvents.OnSetUse -= AssignUseCursor;
	    EventManager.OnAfterChangeScene -= AfterSceneChange;
        }

	void AfterSceneChange(LoadingGame loadingGame) {
	    ResetCursor();
	}
	
	void ResetCursor() {
	    if (IsMenuOpen()) {
		AssignSelectCursorSilent();
	    } else if (IsSelectOnly()) {
		AssignSelectCursorSilent();
            } else {
                AssignUseCursorSilent();
            }
	}

	bool IsCompassHidden() {
	    InvVar hideCompassAttribute = KickStarter.sceneSettings.GetAttribute(1);
	    if (hideCompassAttribute != null) {
		return CheckCondition(hideCompassAttribute);
	    } else {
		return false;
	    }
	}

	// Returns true if this Scene has the Scene Attribute of SelectRayOnly
	bool IsSelectOnly() {
	    InvVar selectAttribute = KickStarter.sceneSettings.GetAttribute(0);
	    if (selectAttribute != null) {
		return CheckCondition(selectAttribute);
	    } else {
		Debug.LogWarning("Cannot find a scene attribute with an id of " + 0);
		return false;
	    }
	}

	// Check the given IntVar and see if it's true
	bool CheckCondition(InvVar attribute) {
	    if (attribute == null) {
		return false;
	    } else 
		return attribute.IntegerValue == 1;
	}
	
	void Start()
	{
	    // Assign the line visual if none is assigned
	    if (lineVisual == null)
	    {
		lineVisual = GetComponent<XRInteractorLineVisual>();
	    }

	    ResetCursor();
	}
	
	void Update() {
	    HandleInput(interactionVal);
	}
	
	protected override void ProcessAction(Vector2 input) {
	    if (input == Vector2.left) {
		CycleCursorLeft();
	    } else if (input == Vector2.right) {
		CycleCursorRight();
	    }
	}

	void AssignSelectCursorSilent() {
	    // VREvents.HideItem();
	    AssignCursor(LineCursor.Select, SelectGradient, null, selectLength);
	    // InventorySlot.transform.localPosition = new Vector3(0, 0, selectCursorLength);
	    VREvents.MakeCompassInactive();
	}
	
	void AssignSelectCursor() {
	    // VREvents.HideItem();
	    AssignCursor(LineCursor.Select, SelectGradient, selectClip, selectLength);
	    // InventorySlot.transform.localPosition = new Vector3(0, 0, selectCursorLength);
	    VREvents.MakeCompassInactive();
	}
	
	void AssignLookCursor() {
	    VREvents.HideItem();
	    AssignCursor(LineCursor.Look, LookGradient, lookClip, lookLength);
	    VREvents.MakeCompassInactive();
	}

	void AssignLookCursorSilent() {
	    // If there's an item selected, temporarily unequip it	    
	    VREvents.HideItem();
	    AssignCursor(LineCursor.Look, LookGradient, null, lookLength);
	    VREvents.MakeCompassInactive();
	}

	void AssignUseCursor() {
	    VREvents.ShowItem();
	    AssignCursor(LineCursor.Use, UseGradient, useClip, useLength);
	    //InventorySlot.transform.localPosition = new Vector3(0, 0, useCursorLength);
	    VREvents.MakeCompassInactive();
	}

	void AssignUseCursorSilent() {
	    VREvents.ShowItem();
	    AssignCursor(LineCursor.Use, UseGradient, null, useLength);
	    // InventorySlot.transform.localPosition = new Vector3(0, 0, useCursorLength);
	    VREvents.MakeCompassInactive();
	}
	
	void AssignCompassCursor() {
	    VREvents.HideItem();
	    AssignCursor(LineCursor.Compass, CompassGradient, compassClip, compassLength);
	    VREvents.AfterSetCompass();
	}

	bool IsMenuOpen() {
	    return PlayerMenus.GetMenuWithName("Pause").IsOn() || PlayerMenus.GetMenuWithName("Options").IsOn() || PlayerMenus.GetMenuWithName("RetroSaves").IsOn() || PlayerMenus.GetMenuWithName("Inventory").IsOn();
	}
	
	void CycleCursorLeft() {	    
	    if (IsMenuOpen() || IsSelectOnly()) {
		// Only show the select cursor if a menu is open
		AssignSelectCursor();
	    } else if (activeCursor == LineCursor.Select) {
		AssignUseCursor();
	    } else if (activeCursor == LineCursor.Look) {
		if (IsCompassHidden()) {
		    AssignUseCursor();
		} else {
		    AssignCompassCursor();
		}
	    } else if (activeCursor == LineCursor.Use) {
		AssignLookCursor();
	    } else if (activeCursor == LineCursor.Compass) {
		AssignUseCursor();
	    } 
	}

	// Cycle through the cursors in sequence
	void CycleCursorRight() {
	    if (IsMenuOpen() || IsSelectOnly()) {
		AssignSelectCursor();
	    } else if (activeCursor == LineCursor.Select) {
		AssignLookCursor();
	    } else if (activeCursor == LineCursor.Look) {
		AssignUseCursor();
	    } else if (activeCursor == LineCursor.Use) {
		if (IsCompassHidden()) {
		    AssignLookCursor();
		} else {
		    AssignCompassCursor();
		}
	    } else if (activeCursor == LineCursor.Compass) {
		AssignLookCursor();		
	    }
	}
	
	// Assign the given cursor as the current cursor
	void AssignCursor(LineCursor cursorType, Gradient validGradient,  AudioClip clip, float lineLength) {
	    activeCursor = cursorType;

	    // Set the gradients
	    lineVisual.validColorGradient = validGradient;
	    lineVisual.invalidColorGradient = validGradient;

	    // TEMP: For debugging, make it obvious when there's nothing to interact with
	    // lineVisual.invalidColorGradient = SelectGradient;

	    GetComponent<XRRayInteractor>().maxRaycastDistance = lineLength;

	    if (clip != null) {
		// Speak the line
		GetComponent<AudioSource>().PlayOneShot(clip);
	    }
	}

    }
}
