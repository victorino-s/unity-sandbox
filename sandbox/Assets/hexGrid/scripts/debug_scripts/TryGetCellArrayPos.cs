using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryGetCellArrayPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log("Hit position : " + hit.point);
                Debug.Log("Hit InverseTransformPoint : " + transform.InverseTransformPoint(hit.point));
            }
        }		
	}
}
