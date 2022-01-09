using System;
using System.Collections.Generic;
using UnityEngine;
using AC;

namespace CC.VR {

    // Hide this gameobject if VR is running
    public class HideInVR : MonoBehaviour
    {
	
								// Start is called before the first frame update
								void Awake()
								{
												SetValue();
								}

								void SetValue() {
												// Override based on LowOrHighPoly variable
												if (BaseXRToggle.IsXRPresent()) {
																gameObject.SetActive(false);
												} else {
																gameObject.SetActive(true);
												}
								}

    }

}
