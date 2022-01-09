/**
 * ActionToggleXRRotation.cs
 *
 * Enable or disable the XR Rotation system
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using CC.VR;

namespace AC {
    
    public class ActionToggleXRRotation : Action
    {
								// Declare variables here
								public bool enableRotation;
	
								public override ActionCategory Category { get { return ActionCategory.Player; }}
								public override string Title { get { return "Toggle XR Rotation"; }}
								public override string Description { get { return "Toggle XR snap turning system."; }}

								public override float Run() {
												if (enableRotation) {
																VREvents.EnableRotation();
												} else {
																VREvents.DisableRotation();
												}
												return 0f;
								}

		#if UNITY_EDITOR	
								public override void ShowGUI ()
								{
												enableRotation = EditorGUILayout.Toggle ("Turn On Rotation?:", enableRotation);
								}
		
		
								public override string SetLabel ()
								{
												if (enableRotation)
												{
																return "Turn On Rotation";
												}
												else
												{
																return "Turn Off Rotation";
												}
								}

		#endif
    }

}
