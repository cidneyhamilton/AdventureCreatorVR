using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace CC.VR {

    public abstract class BaseInteractorReticle : MonoBehaviour
    {
	
								public float maxLength = 5.0f;
	
								// Specify a minimum distance, so it doesn't make the player cross-eyed
								public float minDistance = 1.0f;
	
								protected bool isActive;
								private ILineRenderable m_LineRenderable;
	
								private Vector3 m_ReticlePos, m_ReticleNormal;
								private int m_EndPositionInLine;

								LayerMask layerMask;

								protected virtual void Start() {
												layerMask = LayerMask.GetMask("Default");
												gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
												m_LineRenderable = transform.parent.GetComponent<ILineRenderable>();
								}
	
								protected virtual void MakeReticleActive() {
												// Debug.Log("Making active.");
												isActive = true;
								}
       
								protected virtual void MakeReticleInactive() {
												// Debug.Log("Making inactive.");
												isActive = false;
								}

								// Update the position of the reticle based on distance
								// Called from the Update function
								protected void UpdatePosition()
								{
												RaycastHit hit;

            if (Physics.Raycast(transform.parent.position, transform.parent.TransformDirection(Vector3.forward), out hit, maxLength, layerMask)) {
																// Use as distance if over the minimum distance
																Debug.Log("Hit something; setting distance to " + hit.distance);
																if (hit.distance > minDistance) {
																				transform.position = hit.point;
																} else {
																				transform.localPosition = new Vector3(0, 0, minDistance);
																}
												} else {
																Debug.Log("Did not hit something; setting distance to " + maxLength);
																transform.localPosition = new Vector3(0, 0, maxLength);
												}

        }
	
    }

}
