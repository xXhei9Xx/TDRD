using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameGrid;
using TMPro;
using UnityEngine.UIElements;

public class MapCreator : MonoBehaviour
{
    #region variable declarations

	[SerializeField] string save_name;
	[SerializeField] bool grid_text = false;
	bool spawn_zone_visibility = true, save_chosen = false, new_save = false, save_loaded = false;
	GameObject save_selection, creator_hud, create_new_save_button;
	GameObject terrain_template, spawn_zone_template;
	GameObject terrain_collection, spawn_zone_collection;
	GameObject terrain_legend, spawn_zone_legend;
	GameObject save_button, terrain_button, spawn_zone_button;
	GameObject empty_button ,spawner_button, core_button, grass_button, water_button, sand_button, rock_button, swamp_button, forest_button;
	Collider previous_terrain, previous_spawn_tile;
	GameGrid grid;
	SaveHandler save_handler;
	new GameObject camera;
	(float x, float y, float z) board_center_position_tuple;
	[SerializeField] private int length_x = 10;
	[SerializeField] private int width_z = 10;
	(int length_x, int width_z) grid_dimensions;
	[SerializeField] private terrain chosen_terrain = terrain.grass;
	[SerializeField] private spawn_zone chosen_spawn_zone = spawn_zone.empty;
	RaycastHit rayCastHit;
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
		save_handler = new SaveHandler ();
		
		#region references

			save_selection = GameObject.Find ("Save Selection");
			creator_hud = GameObject.Find ("Creator Hud");
			terrain_template = GameObject.Find ("Terrain Template");
			spawn_zone_template = GameObject.Find ("Spawn Zone Template");
			camera = GameObject.Find("Main Camera");
			terrain_collection = GameObject.Find("Terrain");
			spawn_zone_collection = GameObject.Find("Spawn Zone");
			terrain_legend = GameObject.Find("Terrain Legend");
			spawn_zone_legend = GameObject.Find("Spawn Zone Legend");

			#endregion
		#region buttons
			
			create_new_save_button = GameObject.Find ("Create Save Button");
			create_new_save_button.GetComponent<Button_UI>().ClickFunc =()=> {
			grid_dimensions = (length_x, width_z);
			new_save = true;
			save_chosen = true;
			creator_hud.SetActive (true);
			save_selection.SetActive (false);
			};
			save_button = GameObject.Find ("Save Button");
			save_button.GetComponent<Button_UI>().ClickFunc = () => {
			save_handler.SaveTerrainCreate (this, save_name);
			};
			terrain_button = GameObject.Find ("Terrain Button");
			terrain_button.GetComponent<Button_UI>().ClickFunc = () => {
			ToggleSpawnTiles (false);
			};
			spawn_zone_button = GameObject.Find ("Spawn Zone Button");
			spawn_zone_button.GetComponent<Button_UI>().ClickFunc = () => {
			ToggleSpawnTiles (true);
			};
			spawner_button = GameObject.Find ("Spawner Button");
			spawner_button.GetComponent<Button_UI>().ClickFunc = () => {
			chosen_spawn_zone = spawn_zone.spawner;
			};
			core_button = GameObject.Find ("Core Button");
			core_button.GetComponent<Button_UI>().ClickFunc = () => {
			chosen_spawn_zone = spawn_zone.core;
			};
			empty_button = GameObject.Find ("Empty Button");
			empty_button.GetComponent<Button_UI>().ClickFunc = () => {
			chosen_spawn_zone = spawn_zone.empty;
			};
			grass_button = GameObject.Find ("Grass Button");
			grass_button.GetComponent<Button_UI>().ClickFunc = () => {
			chosen_terrain = terrain.grass;
			};
			water_button = GameObject.Find ("Water Button");
			water_button.GetComponent<Button_UI>().ClickFunc = () => {
			chosen_terrain = terrain.water;
			};
			sand_button = GameObject.Find ("Sand Button");
			sand_button.GetComponent<Button_UI>().ClickFunc = () => {
			chosen_terrain = terrain.sand;
			};
			swamp_button = GameObject.Find ("Swamp Button");
			swamp_button.GetComponent<Button_UI>().ClickFunc = () => {
			chosen_terrain = terrain.swamp;
			};

		#endregion
		creator_hud.SetActive (false);
		var info = new DirectoryInfo (Application.dataPath + "/Map Saves/");
		var fileInfo = info.GetFiles();
		int counter = 0;
		foreach (FileInfo file in fileInfo)
		{
			if (!file.Name.Contains (".meta"))
			{
				GameObject save_button = Instantiate (GameObject.Find ("Save Button Template"));
				save_button.name = file.Name.Replace (".json", "");
				save_button.GetComponentInChildren<TextMeshProUGUI>().text = save_button.name;
				save_button.transform.parent = save_selection.transform;
				save_button.transform.localPosition = new Vector3 (0, 225f - (counter * 62.5f), 0);
				save_button.GetComponent<Button_UI>().ClickFunc = () => {
				grid_dimensions = save_handler.GetGridDimensions (this, save_button.name);
				save_name = save_button.name;
				save_chosen = true;
				creator_hud.SetActive (true);
				save_selection.SetActive (false);
				};
				counter++;
			}
		}
    }

	private void Update()
	{
		if (save_chosen == true && save_loaded == false)
		{
			grid = new GameGrid (grid_dimensions.length_x, grid_dimensions.width_z, 1, 1, grid_text);
			if (new_save == false)
			{
				save_handler.SaveTerrainLoad (this, save_name);
			}
			for (int x = 0; x < grid.length_x; x++)
			{
				for (int z = 0; z < grid.width_z; z++)
				{
					GameObject terrain = Instantiate (terrain_template);
					terrain.transform.parent = terrain_collection.transform;
					terrain.transform.position = grid.GetWorldTileCenter (x, z, 0.025f);
					terrain.name = "terrain " + x.ToString() + "," + z.ToString();
					terrain.AddComponent<TerrainTile>();
					GameObject spawn_zone = Instantiate (spawn_zone_template);
					spawn_zone.transform.parent = spawn_zone_collection.transform;
					spawn_zone.transform.position = grid.GetWorldTileCenter (x, z, 0.075f);
					spawn_zone.name = "spawn_zone " + x.ToString() + "," + z.ToString();
					spawn_zone.AddComponent<SpawnTile>();
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
			camera.transform.position = new Vector3 (board_center_position_tuple.x, board_center_position_tuple.y, board_center_position_tuple.z);
			camera.AddComponent<CameraMovement>().SetVariables (grid, grid_text);
			ToggleSpawnTiles (false);
			save_loaded = true;
		}
		if (save_loaded == true)
		{
			Ray mouse_world_ray = camera.GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (mouse_world_ray, out rayCastHit);
			if (spawn_zone_visibility == false)
			{
				if (rayCastHit.collider != null)
				{
					if (rayCastHit.collider.gameObject.tag == "terrain" && rayCastHit.collider != previous_terrain)
					{
						if (previous_terrain != null)
						{
							previous_terrain.GetComponent<MeshRenderer>().material = previous_terrain.GetComponent<TerrainTile>().previous_material;
						}
						switch (chosen_terrain)
						{
							case terrain.grass:
							rayCastHit.collider.GetComponent<MeshRenderer>().material = grass_material;
							break;

							case terrain.water:
							rayCastHit.collider.GetComponent<MeshRenderer>().material = water_material;
							grid.SetValue (grid.GetXZ (rayCastHit.collider.gameObject.transform.position), grid_parameter.spawn_zone, grid.EnumTranslator (spawn_zone.empty));
							UpdateSpawnTiles ();
							break;

							case terrain.swamp:
							rayCastHit.collider.GetComponent<MeshRenderer>().material = swamp_material;
							break;

							case terrain.sand:
							rayCastHit.collider.GetComponent<MeshRenderer>().material = sand_material;
							break;
						}
						previous_terrain = rayCastHit.collider;
					}
				}
				if (rayCastHit.collider.gameObject.tag == "terrain" && Input.GetMouseButton (0))
				{
					if (previous_terrain != null)
					{
						previous_terrain.GetComponent<TerrainTile>().previous_material = previous_terrain.GetComponent<MeshRenderer>().material;
						grid.SetValue (grid.GetXZ (rayCastHit.collider.gameObject.transform.position), grid_parameter.terrain, grid.EnumTranslator (chosen_terrain));
						grid.GridTextUpdate ();
					}
				}
			}
			else
			{
				if (rayCastHit.collider != null)
				{
					if (rayCastHit.collider.gameObject.tag == "spawn zone" && rayCastHit.collider != previous_spawn_tile &&
					grid.CheckIfValidSpawnZone (grid.GetXZ (rayCastHit.collider.gameObject.transform.position)) == true)
					{
						if (previous_spawn_tile != null)
						{
							previous_spawn_tile.GetComponent<MeshRenderer>().material = previous_spawn_tile.GetComponent<SpawnTile>().previous_material;
						}
						switch (chosen_spawn_zone)
						{
							case spawn_zone.empty:
							rayCastHit.collider.GetComponent<MeshRenderer>().material = empty_material;
							break;

							case spawn_zone.spawner:
							rayCastHit.collider.GetComponent<MeshRenderer>().material = spawner_material;
							break;

							case spawn_zone.core:
							rayCastHit.collider.GetComponent<MeshRenderer>().material = core_material;
							break;
						}
						previous_spawn_tile = rayCastHit.collider;
					}
					if (rayCastHit.collider.gameObject.tag == "spawn zone" && Input.GetMouseButton (0) &&
					grid.CheckIfValidSpawnZone (grid.GetXZ (rayCastHit.collider.gameObject.transform.position)) == true)
					{
						if (previous_spawn_tile != null)
						{
							previous_spawn_tile.GetComponent<SpawnTile>().previous_material = previous_spawn_tile.GetComponent<MeshRenderer>().material;
							grid.SetValue (grid.GetXZ (rayCastHit.collider.gameObject.transform.position), grid_parameter.spawn_zone, grid.EnumTranslator (chosen_spawn_zone));
							grid.GridTextUpdate ();
						}
					}
				}
			}
		}
	}

	public class TerrainTile : MonoBehaviour
	{
		#region variable declarations

		private MapCreator caller;
		private bool mouse_on_card = false;
		new GameObject camera;
		public Material previous_material;

		#endregion

		private void Start()
		{
			camera = GameObject.Find("Main Camera");
			caller = GameObject.Find ("MapCreator").GetComponent<MapCreator>();
			terrain terrain = caller.grid.TerrainTranslator (caller.grid.GetValue (caller.grid.GetXZ(transform.position).x,
			caller.grid.GetXZ(transform.position).z, grid_parameter.terrain));
			switch (terrain)
			{
				case terrain.grass:
				GetComponent<MeshRenderer>().material = caller.grass_material;
				break;

				case terrain.water:
				GetComponent<MeshRenderer>().material = caller.water_material;
				break;

				case terrain.swamp:
				GetComponent<MeshRenderer>().material = caller.swamp_material;
				break;

				case terrain.sand:
				GetComponent<MeshRenderer>().material = caller.sand_material;
				break;
			}
			previous_material = GetComponent<MeshRenderer>().material;
		}
	}

	public class SpawnTile : MonoBehaviour
	{
		#region variable declarations

		private MapCreator caller;
		private bool mouse_on_card = false;
		new GameObject camera;
		public Material previous_material;

		#endregion

		private void Start()
		{
			camera = GameObject.Find("Main Camera");
			caller = GameObject.Find ("MapCreator").GetComponent<MapCreator>();
			spawn_zone terrain = caller.grid.SpawnZoneTranslator (caller.grid.GetValue (caller.grid.GetXZ(transform.position).x,
			caller.grid.GetXZ(transform.position).z, grid_parameter.spawn_zone));
			switch (terrain)
			{
				case spawn_zone.empty:
				GetComponent<MeshRenderer>().material = caller.empty_material;
				break;

				case spawn_zone.spawner:
				GetComponent<MeshRenderer>().material = caller.spawner_material;
				break;

				case spawn_zone.core:
				GetComponent<MeshRenderer>().material = caller.core_material;
				break;
			}
			previous_material = GetComponent<MeshRenderer>().material;
		}
	}

	private void ToggleSpawnTiles (bool state)
	{
		spawn_zone_collection.SetActive (state);
		if (state == true)
		{
			terrain_legend.gameObject.SetActive (false);
			spawn_zone_legend.gameObject.SetActive (true);
			spawn_zone_visibility = true;
		}
		else
		{
			terrain_legend.gameObject.SetActive (true);
			spawn_zone_legend.gameObject.SetActive (false);
			spawn_zone_visibility = false;
		}
	}

	public void UpdateSpawnTiles ()
	{
		foreach (SpawnTile child in spawn_zone_collection.GetComponentsInChildren<SpawnTile>())
		{
			if (grid.GetValue (grid.GetXZ (child.transform.position), grid_parameter.spawn_zone) == grid.EnumTranslator (spawn_zone.empty))
			{
				child.GetComponent<MeshRenderer>().material = empty_material;
			}
		}
	}

	public GameGrid GetGameGrid ()
	{
		return grid;
	}
}
