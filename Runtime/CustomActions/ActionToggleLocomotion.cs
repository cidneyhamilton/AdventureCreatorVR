/**
 * ActionToggleLocomotion.cs
 *
 * Enable or disable the VR locomotion system
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using CC.VR;

namespace AC {
    
    public class ActionToggleLocomotion : Action
    {
	// Declare variables here
	public bool enableLocomotion;
	
	public override ActionCategory Category { get { return ActionCategory.Player; }}
	public override string Title { get { return "Toggle XR Locomotion"; }}
	public override string Description { get { return "Toggle XR custom movement system."; }}

	public override float Run() {
	    if (enableLocomotion) {
		VREvents.EnableLocomotion();
	    } else {
		VREvents.DisableLocomotion();
	    }
	    return 0f;
	}

		#if UNITY_EDITOR	
	public override void ShowGUI ()
	{
	    enableLocomotion = EditorGUILayout.Toggle ("Turn On Locomotion?:", enableLocomotion);
	}
		
		
	public override string SetLabel ()
	{
	    if (enableLocomotion)
	    {
		return "Turn On Locomotion";
	    }
	    else
	    {
		return "Turn Off Locomotion";
	    }
	}

		#endif
    }

}
