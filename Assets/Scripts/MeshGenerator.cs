using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace CornellTech.View
{
	public class MeshGenerator : MonoBehaviour
	{
		//Enums

		//Structs/classes

		//Readonly/const

		//Serialized
		[SerializeField]
		protected GameObject meshGameObject;
		
		/////Protected/////
		//References
		protected List<Vector3> pendingVertices = new List<Vector3>();
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
			//True when the user first clicks down on the mouse
			if (Input.GetMouseButtonDown (0))
			{
				//Mouse position in pixel/screen coordinates
				Vector2 mousePosition = Input.mousePosition;

				//Give it a z-value (1 meter forward)
				Vector3 screenPosition = new Vector3 (mousePosition.x, mousePosition.y, 1f);

				//Let the camera convert it to a world position
				Vector3 worldPosition = Camera.main.ScreenToWorldPoint (screenPosition);

				pendingVertices.Add (worldPosition);

				if (pendingVertices.Count == 3)
				{
					AddTriangle (pendingVertices);
					pendingVertices.Clear ();
				}
			}
		}
		
		///////////////////////////////////////////////////////////////////////////
		//
		// MeshGenerator Functions
		//

		protected void AddTriangle(List<Vector3> newVertices)
		{
			MeshFilter meshFilter = meshGameObject.GetComponent<MeshFilter> ();
			Mesh mesh = meshFilter.mesh;

			//do we not have one yet? Create a new one
			if (mesh == null)
				mesh = new Mesh ();

			//Gets our current list of vertices (uses System.Linq extension ToList())
			List<Vector3> meshVertices = mesh.vertices.ToList();
			//Add our new ones
			for (int i = 0; i < newVertices.Count; i++)
			{
				meshVertices.Add (newVertices [i]);	
			}

			//Gets our current list of vertices (uses System.Linq extension ToList())
			List<int> meshTriangles = mesh.triangles.ToList();
			//Add a new triangle
			int triangleIndexOne = meshVertices.Count-3;
			int triangleIndexTwo = meshVertices.Count - 2;
			int triangleIndexThree = meshVertices.Count - 1;
			meshTriangles.Add (triangleIndexOne);
			meshTriangles.Add (triangleIndexTwo);
			meshTriangles.Add (triangleIndexThree);

			//Colors
			List<Color> meshColors = mesh.colors.ToList();
			//add a random color for each vertex
			meshColors.Add (Util.Utility.GetRandomColor ());
			meshColors.Add (Util.Utility.GetRandomColor ());
			meshColors.Add (Util.Utility.GetRandomColor ());

			//Set our new vertices, triangles, and colors
			mesh.vertices = meshVertices.ToArray();
			mesh.triangles = meshTriangles.ToArray();
			mesh.colors = meshColors.ToArray ();

			//Recalculate normals automatically
			mesh.RecalculateNormals();

			//No need to worry about UVs

			//Set the mesh filter's mesh
			meshFilter.mesh = mesh;
		}

		////////////////////////////////////////
		//
		// Event Functions

	}
}