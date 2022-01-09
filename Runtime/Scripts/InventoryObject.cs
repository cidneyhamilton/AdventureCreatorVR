using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CC.VR {
    
    public class InventoryObject : MonoBehaviour
    {
	public MeshRenderer mesh;

	void Start() {
	    mesh = GetComponentInChildren<MeshRenderer>();
	    if (mesh == null) {
		Debug.LogError("No mesh found on inventory object " + gameObject.name);
	    }
	}
   
    }

}
