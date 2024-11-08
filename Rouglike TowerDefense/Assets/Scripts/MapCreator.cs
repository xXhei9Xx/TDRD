using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameGrid;
using static GameHandler;

public class MapCreator : MonoBehaviour
{
    #region variable declarations

	bool spawn_zone_visibility = true;
	GameObject terrain_template;
	GameObject spawn_zone_template;
	GameObject spawner_button, core_button, grass_button, water_button, sand_button, rock_button, swamp_button, forest_button;
	GameGrid grid;
	new GameObject camera;
	(float x, float y, float z) board_center_position_tuple;
	terrain chosen_terrain = terrain.grass;
	spawn_zone chosen_spawn_zone = spawn_zone.empty;
	[SerializeField] Material empty_material;
	[SerializeField] Material spawner_material;
	[SerializeField] Material core_material;

	[SerializeField] Material grass_material;
	[SerializeField] Material sand_material;
	[SerializeField] Material water_material;
	[SerializeField] Material rock_material;
	[SerializeField] Material swamp_material;
	[SerializeField] Material forest_material;

	#endregion

    void Start()
    {
        #region templates

		terrain_template = GameObject.Find ("Terrain Template");
		spawn_zone_template = GameObject.Find ("Spawn Zone Template");

		#endregion
		#region buttons
		
			spawner_button = GameObject.Find ("Spawner Button");
			spawner_button.GetComponent<Button_UI>().ClickFunc = () => {
			if (spawn_zone_visibility == false)
			{
				ToggleSpawnZone (true);
				spawn_zone_visibility = true;
			}
			chosen_spawn_zone = spawn_zone.spawner;
			Debug.Log ("changed");
			};
			core_button = GameObject.Find ("Core Button");
			core_button.GetComponent<Button_UI>().ClickFunc = () => {
			if (spawn_zone_visibility == false)
			{
				ToggleSpawnZone (true);
				spawn_zone_visibility = true;
			}
			chosen_spawn_zone = spawn_zone.core;
			Debug.Log ("changed");
			};
			grass_button = GameObject.Find ("Grass Button");
			grass_button.GetComponent<Button_UI>().ClickFunc = () => {
			if (spawn_zone_visibility == true)
			{
				ToggleSpawnZone (false);
				spawn_zone_visibility = false;
			}
			chosen_terrain = terrain.grass;
			Debug.Log ("changed");
			};
			water_button = GameObject.Find ("Water Button");
			water_button.GetComponent<Button_UI>().ClickFunc = () => {
			if (spawn_zone_visibility == true)
			{
				ToggleSpawnZone (false);
				spawn_zone_visibility = false;
			}
			chosen_terrain = terrain.water;
			Debug.Log ("changed");
			};
			sand_button = GameObject.Find ("Sand Button");
			sand_button.GetComponent<Button_UI>().ClickFunc = () => {
			if (spawn_zone_visibility == true)
			{
				ToggleSpawnZone (false);
				spawn_zone_visibility = false;
			}
			chosen_terrain = terrain.sand;
			Debug.Log ("changed");
			};
			swamp_button = GameObject.Find ("Swamp Button");
			swamp_button.GetComponent<Button_UI>().ClickFunc = () => {
			if (spawn_zone_visibility == true)
			{
				ToggleSpawnZone (false);
				spawn_zone_visibility = false;
			}
			chosen_terrain = terrain.swamp;
			Debug.Log ("changed");
			};

		#endregion
		grid = new GameGrid (10, 10, 1, 1, true);
		for (int x = 0; x < grid.length_x; x++)
		{
			for (int z = 0; z < grid.width_z; z++)
			{
				GameObject terrain = Instantiate (terrain_template);
				terrain.transform.parent = GameObject.Find ("Terrain").transform;
				terrain.transform.position = grid.GetWorldTileCenter (x, z, 0);
				terrain.name = "terrain " + x.ToString() + "," + z.ToString();
				terrain.AddComponent<TerrainTile>();
				GameObject spawn_zone = Instantiate (spawn_zone_template);
				spawn_zone.transform.parent = GameObject.Find ("Spawn Zone").transform;
				spawn_zone.transform.position = grid.GetWorldTileCenter (x, z, 0);
				spawn_zone.name = "spawn_zone " + x.ToString() + "," + z.ToString();
				spawn_zone.AddComponent<SpawnZoneTile>();
			}
		}
		#region board center tupple

		if (grid.length_x % 2 == 0)
		{
			board_center_position_tuple.x = (((float) grid.length_x / 2) * grid.cell_length_x);
		}
		else
		{
			board_center_position_tuple.x = (((float) grid.length_x / 2 + 0.5f) * grid.cell_length_x);
		}
		if (grid.length_x >= grid.width_z)
		{
			board_center_position_tuple.y = grid.length_x;
		}
		else
		{
			board_center_position_tuple.y = grid.width_z;
		}
		if (grid.width_z % 2 == 0)
		{
			board_center_position_tuple.z = (((float) grid.width_z / 2) * grid.cell_width_z);
		}
		else
		{
			board_center_position_tuple.z = (((float) grid.width_z / 2 + 0.5f) * grid.cell_width_z);
		}

		#endregion
		camera = GameObject.Find("Main Camera");
		camera.transform.position = new Vector3 (board_center_position_tuple.x, board_center_position_tuple.y, board_center_position_tuple.z);
    }

    public class TerrainTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		#region variable declarations

		private MapCreator caller;
		private bool mouse_on_card = false;

		#endregion

		private void Start()
		{
			caller = GameObject.Find ("MapCreator").GetComponent<MapCreator>();
			terrain terrain = caller.grid.TerrainTranslator (caller.grid.GetValue (caller.grid.GetXZ(transform.position).x,
			caller.grid.GetXZ(transform.position).z, grid_parameter.terrain));
			switch (terrain)
			{
				case terrain.grass: gameObject.GetComponent<MeshRenderer>().material = caller.grass_material; break;

				case terrain.water: gameObject.GetComponent<MeshRenderer>().material = caller.water_material; break;

				case terrain.swamp: gameObject.GetComponent<MeshRenderer>().material = caller.swamp_material; break;

				case terrain.sand: gameObject.GetComponent<MeshRenderer>().material = caller.sand_material; break;
			}
		}

		private void Update()
		{
			if (mouse_on_card == true && Input.GetMouseButtonDown (0))
			{
				caller.grid.SetValue (caller.grid.GetXZ(transform.position).x, caller.grid.GetXZ(transform.position).z,
				grid_parameter.terrain, caller.grid.EnumTranslator(caller.chosen_terrain));
				switch (caller.chosen_terrain)
				{
					case terrain.grass: gameObject.GetComponent<MeshRenderer>().material = caller.grass_material; break;

					case terrain.water: gameObject.GetComponent<MeshRenderer>().material = caller.water_material; break;

					case terrain.swamp: gameObject.GetComponent<MeshRenderer>().material = caller.swamp_material; break;

					case terrain.sand: gameObject.GetComponent<MeshRenderer>().material = caller.sand_material; break;
				}
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Debug.Log ("terrain");
			mouse_on_card = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			mouse_on_card = false;
		}
	}

	public class SpawnZoneTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		#region variable declarations

		private MapCreator caller;
		private bool mouse_on_card = false;

		#endregion

		private void Start()
		{
			caller = GameObject.Find ("MapCreator").GetComponent<MapCreator>();
			spawn_zone spawn_zone = caller.grid.SpawnZoneTranslator (caller.grid.GetValue (caller.grid.GetXZ(transform.position).x,
			caller.grid.GetXZ(transform.position).z, grid_parameter.spawn_zone));
			switch (spawn_zone)
			{
				case spawn_zone.empty: gameObject.GetComponent<MeshRenderer>().material = caller.empty_material; break;

				case spawn_zone.spawner: gameObject.GetComponent<MeshRenderer>().material = caller.spawner_material; break;

				case spawn_zone.core: gameObject.GetComponent<MeshRenderer>().material = caller.core_material; break;
			}
		}

		private void Update()
		{
			if (mouse_on_card == true && Input.GetMouseButtonDown (0))
			{
				caller.grid.SetValue (caller.grid.GetXZ(transform.position).x, caller.grid.GetXZ(transform.position).z,
				grid_parameter.spawn_zone, caller.grid.EnumTranslator(caller.chosen_spawn_zone));
				switch (caller.chosen_spawn_zone)
			{
				case spawn_zone.empty: gameObject.GetComponent<MeshRenderer>().material = caller.empty_material; break;

				case spawn_zone.spawner: gameObject.GetComponent<MeshRenderer>().material = caller.spawner_material; break;

				case spawn_zone.core: gameObject.GetComponent<MeshRenderer>().material = caller.core_material; break;
			}
			}
			if (mouse_on_card == true && Input.GetMouseButtonDown (1))
			{
				caller.grid.SetValue (caller.grid.GetXZ(transform.position).x, caller.grid.GetXZ(transform.position).z,
				grid_parameter.spawn_zone, caller.grid.EnumTranslator(spawn_zone.empty));
				gameObject.GetComponent<MeshRenderer>().material = caller.empty_material;
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Debug.Log ("spawn_zone");
			mouse_on_card = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			mouse_on_card = false;
		}
	}

	private void ToggleSpawnZone (bool state)
	{
		for (int x = 0; x < grid.length_x; x++)
		{
			for (int z = 0; z < grid.width_z; z++)
			{
				GameObject spawn_zone = GameObject.Find ("spawn_zone " + x.ToString() + "," + z.ToString());
				spawn_zone.SetActive (state);
			}
		}
	}
}
