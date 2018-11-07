using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CornellTech
{
	public class Raycaster : MonoBehaviour
	{
		//Enums

		//Structs/classes

		//Readonly/const

		//Serialized
		[SerializeField]
		protected LayerMask layerMask;
		[SerializeField]
		protected float distance = 30f;
		
		/////Protected/////
		//References
		//Primitives
		
		//Actions/Funcs

		//Properties

		///////////////////////////////////////////////////////////////////////////
		//
		// Inherited from MonoBehaviour
		//
		
		protected void Awake ()
		{

		}
		
		protected void Start ()
		{	

		}
		
		protected void Update ()
		{	

			if (Input.GetMouseButtonDown (0))
			{
				Vector3 mousePosition = Input.mousePosition;

				Debug.Log ("Mouse position: " + mousePosition.ToString ());

				mousePosition.z = distance;

				Ray ray = Camera.main.ScreenPointToRay (mousePosition);

				RaycastHit raycastHit;

				if (Physics.Raycast (ray, out raycastHit, distance, this.layerMask.value))
				{
					string name = raycastHit.collider.gameObject.name;
					Debug.Log ("We hit: " + name);
					Debug.Log ("It was " + raycastHit.distance + " meters away.");
				}
			}
		}

		protected void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay (Camera.main.transform.position, Camera.main.transform.forward);
		}
		
		///////////////////////////////////////////////////////////////////////////
		//
		// Raycaster Functions
		//

		
		////////////////////////////////////////
		//
		// Event Functions

	}
}