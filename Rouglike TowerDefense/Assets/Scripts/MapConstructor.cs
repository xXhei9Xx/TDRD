using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstructor
{
	public MapConstructor (GameHandler caller)
	{
		for (int x = 0; x < caller.GetGameGrid().length_x; x++)
		{
			for (int z = 0; z < caller.GetGameGrid().width_z; z++)
			{
				TerrainConstructor (x, z, caller, caller.GetGameGrid().TerrainTranslator (caller.GetGameGrid().GetValue (x, z, GameGrid.grid_parameter.terrain)));
			}
		}
	}

	public void TerrainConstructor (int x, int z, GameHandler caller, GameGrid.terrain terrain)
	{
		#region variable declarations

		GameObject grass_object = GameObject.Find ("Grass Template");
		GameObject sand_object = GameObject.Find ("Sand Template");
		GameObject rock_object = GameObject.Find ("Rock Template");
		GameObject forest_object = GameObject.Find ("Forest Template");
		GameObject water_object = GameObject.Find ("Water Template");
		GameObject swamp_object = GameObject.Find ("Swamp Template");

		#endregion

		GameObject terrain_object = new GameObject("terrain " + x + "," + z);
		terrain_object.transform.parent = GameObject.Find ("Terrain Initialized").transform;
		switch (terrain)
		{
			case GameGrid.terrain.grass:
			terrain_object.AddComponent<TerrainInstantiate>().SetTerrain (caller.GetGameGrid().GetWorldTileCenter (x, z, 0.05f), grass_object);
			break;

			case GameGrid.terrain.water:
			terrain_object.AddComponent<TerrainInstantiate>().SetTerrain (caller.GetGameGrid().GetWorldTileCenter (x, z, 0.05f), water_object);
			break;
			
			case GameGrid.terrain.sand:
			terrain_object.AddComponent<TerrainInstantiate>().SetTerrain (caller.GetGameGrid().GetWorldTileCenter (x, z, 0.05f), sand_object);
			break;

			case GameGrid.terrain.swamp:
			terrain_object.AddComponent<TerrainInstantiate>().SetTerrain (caller.GetGameGrid().GetWorldTileCenter (x, z, 0.05f), swamp_object);
			break;
		}
	}

	public class TerrainInstantiate : MonoBehaviour
	{
		private GameObject terrain;
		private Vector3 position;

		public void SetTerrain (Vector3 position, GameObject terrain)
		{
			this.terrain = terrain;
			this.position = position;
		}

		private void Start()
		{
			GameObject this_terrain_object = Instantiate (terrain);
			this_terrain_object.transform.position = position;
			this_terrain_object.transform.SetParent (transform, false);
			Destroy (this);
		}
	}
}
