using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;

namespace CC.VR {

    public class HideInPC : MonoBehaviour
    {

	
								// Start is called before the first frame update
								void Awake()
								{
												SetValue();
								}
       
								void SetValue() {
											
												 if (BaseXRToggle.IsXRPresent())
												{
																gameObject.SetActive(true );
												}
												else 
												{
																gameObject.SetActive(false);
												}
								}

    }

}
