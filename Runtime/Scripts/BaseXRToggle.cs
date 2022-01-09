using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace CC.VR {
    
    public class BaseXRToggle
    {    

								public static bool IsXRPresent() {
												// Check presence of VR display
												var xrDisplays = new List<XRDisplaySubsystem>();
												SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplays);
												foreach (var xrDisplay in xrDisplays) {
																if (xrDisplay.running) {
																				return true;
																}
												}
												return false;	    
								}

    }
}
