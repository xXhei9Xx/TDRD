using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Core
{
	#region variable declarations

	#endregion

	public Core (Vector3 position, GameHandler caller)
	{
		GameObject core = new GameObject("Core " + caller.GetSpawnerCounter ());
		caller.GetGameGrid().SetValue (caller.GetGameGrid().GetXZ(position).x, caller.GetGameGrid().GetXZ(position).z,
		GameGrid.grid_parameter.object_type, GameGrid.object_type.core);
		core.AddComponent<CoreObject>().position = position;
		core.transform.parent = GameObject.Find ("Core Initialized").transform;
	}

	public class CoreObject : MonoBehaviour
	{
		#region variable declarations

		public Vector3 position;
		private GameObject core_template, this_core;

		#endregion

		private void Start()
		{
			core_template = GameObject.Find("Core");
			this_core = Instantiate(core_template);
			this_core.transform.position = position;
			this_core.transform.parent = transform;
		}
	}
}
