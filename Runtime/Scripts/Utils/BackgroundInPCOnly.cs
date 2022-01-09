using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CC.VR {

				[RequireComponent (typeof(Image))]
				public class BackgroundInPCOnly : MonoBehaviour
				{

								Image img;
								
								void Start() {
												img = GetComponent<Image>();
												if (img == null) {
																Debug.LogWarning("No image component found.");
												}
												if (BaseXRToggle.IsXRPresent()) {
																img.enabled = false;
												} else {
																img.enabled = true;
												}
								}
				}
}
