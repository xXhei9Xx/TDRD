using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UIElements;

public class GameGrid 
{
	#region variable declarations

	public bool display_text;
	public int length_x, width_z;
	public float cell_length_x, cell_width_z;
	private int [,,] grid_array;
	public List<GameObject> [,] array_of_characters_on_tile_lists;
	private GameObject [,] text_objects_array;

	#endregion
	#region enum declarations and methods

	#region grid parameter
	public enum grid_parameter
	{
		object_type,
		object_id,
		enemy,
		simulated,
		terrain,
		spawn_zone,
		mana,
		tile_effect,
		amount_of_dead,
		prev_x_empty,
		prev_z_empty,
		prev_x_any_object,
		prev_z_any_object,
		prev_x_any_terrain,
		prev_z_any_terrain,
		prev_x_any_tile,
		prev_z_any_tile
	}

	public int EnumTranslator (grid_parameter parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case grid_parameter.object_type: return 0;

			case grid_parameter.object_id: return 1;

			case grid_parameter.enemy: return 2;

			case grid_parameter.simulated: return 3;

			case grid_parameter.terrain: return 4;

			case grid_parameter.spawn_zone: return 5;

			case grid_parameter.mana: return 6;

			case grid_parameter.tile_effect: return 7;

			case grid_parameter.amount_of_dead: return 8;

			case grid_parameter.prev_x_empty: return 9;

			case grid_parameter.prev_z_empty: return 10;

			case grid_parameter.prev_x_any_object: return 11;

			case grid_parameter.prev_z_any_object: return 12;

			case grid_parameter.prev_x_any_terrain: return 13;

			case grid_parameter.prev_z_any_terrain: return 14;

			case grid_parameter.prev_x_any_tile: return 15;

			case grid_parameter.prev_z_any_tile: return 16;
		}
	}
	#endregion
	#region object type
	public enum object_type
	{
		empty,
		spawner,
		tower,
		traversable_tower,
		core,
		tree,
		rock
	}

	public int EnumTranslator (object_type parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case object_type.empty: return 0;

			case object_type.spawner: return 1;

			case object_type.tower: return 2;

			case object_type.traversable_tower: return 3;

			case object_type.core: return 4;

			case object_type.tree: return 5;

			case object_type.rock: return 6;
		}
	}

	public object_type ObjectTypeTranslator (int parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case 0: return object_type.empty;

			case 1: return object_type.spawner;

			case 2: return object_type.tower;

			case 3: return object_type.traversable_tower;

			case 4: return object_type.core;

			case 5: return object_type.tree;

			case 6: return object_type.rock;
		}
	}
	#endregion
	#region terrain
	public enum terrain
	{
		grass,
		sand,
		water,
		swamp
	}

	public int EnumTranslator (terrain parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case terrain.grass: return 0;

			case terrain.sand: return 1;

			case terrain.water: return 2;

			case terrain.swamp: return 3;
		}
	}

	public terrain TerrainTranslator (int parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case 0: return terrain.grass;

			case 1: return terrain.sand;

			case 2: return terrain.water;

			case 3: return terrain.swamp;
		}
	}
	#endregion
	#region spawn zone
	public enum spawn_zone
	{
		empty,
		spawner,
		core
	}

	public int EnumTranslator (spawn_zone parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case spawn_zone.empty: return 0;

			case spawn_zone.spawner: return 1;

			case spawn_zone.core: return 2;
		}
	}

	public spawn_zone SpawnZoneTranslator (int parameter)
	{
		switch (parameter)
		{
			default: return spawn_zone.empty;

			case 0: return spawn_zone.empty;

			case 1: return spawn_zone.spawner;

			case 2: return spawn_zone.core;
		}
	}
	#endregion
	#region enemy
	public enum enemy
	{
		empty,
		occupied
	}

	public int EnumTranslator (enemy parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case enemy.empty: return 0;

			case enemy.occupied: return 1;
		}
	}

	public enemy EnemyTranslator (int parameter)
	{
		switch (parameter)
		{
			default: return enemy.empty;

			case 0: return enemy.empty;

			case 1: return enemy.occupied;
		}
	}
	#endregion
	#region grid direction
	public enum grid_direction
	{
		up,
		down,
		left,
		right,
		top_left,
		top_right,
		bottom_left,
		bottom_right
	}

	public grid_direction GetMovementDirection (Vector3 starting_position, (int x, int z) destination)
	{
		(int x, int z) direction = (destination.x - (int) Math.Floor (starting_position.x), destination.z - (int) Math.Floor (starting_position.z));
		switch (direction.x)
		{
			case 0:
			switch (direction.z)
			{
				case > 0:
				return grid_direction.up;

				case <0:
				return grid_direction.down;
			}
			break;

			case > 0:
			switch (direction.z)
			{
				case 0:
				return grid_direction.right;

				case > 0:
				return grid_direction.top_right;

				case < 0:
				return grid_direction.bottom_right;
			}

			case < 0:
			switch (direction.z)
			{
				case 0:
				return grid_direction.left;

				case > 0:
				return grid_direction.top_left;

				case < 0:
				return grid_direction.bottom_left;
			}
		}
		return grid_direction.up;
	}

	public grid_direction GetMovementDirection ((int x, int z) starting_position, (int x, int z) destination)
	{
		(int x, int z) direction = (destination.x - starting_position.x, destination.z - starting_position.z);
		switch (direction.x)
		{
			case 0:
			switch (direction.z)
			{
				case > 0:
				return grid_direction.up;

				case <0:
				return grid_direction.down;
			}
			break;

			case > 0:
			switch (direction.z)
			{
				case 0:
				return grid_direction.right;

				case > 0:
				return grid_direction.top_right;

				case < 0:
				return grid_direction.bottom_right;
			}

			case < 0:
			switch (direction.z)
			{
				case 0:
				return grid_direction.left;

				case > 0:
				return grid_direction.top_left;

				case < 0:
				return grid_direction.bottom_left;
			}
		}
		return grid_direction.up;
	}
	#endregion
	#region mana
	public enum mana
	{
		connected,
		unconnected
	}

	public int EnumTranslator (mana parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case mana.unconnected: return 0;

			case mana.connected: return 1;
		}
	}

	public mana ManaTranslator (int parameter)
	{
		switch (parameter)
		{
			default: return mana.unconnected;

			case 0: return mana.unconnected;

			case 1: return mana.connected;
		}
	}
	#endregion
	#region tile effect
	public enum tile_effect
	{
		ice,
		oil,
		fire,
	}

	public int EnumTranslator (tile_effect parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case tile_effect.ice: return 0;

			case tile_effect.oil: return 1;

			case tile_effect.fire: return 2;
		}
	}

	public tile_effect TileEffectTranslator (int parameter)
	{
		switch (parameter)
		{
			default: return tile_effect.ice;

			case 0: return tile_effect.ice;

			case 1: return tile_effect.oil;

			case 2: return tile_effect.fire;
		}
	}
	#endregion

	#endregion

	public GameGrid (int length_x, int width_z, float cell_length_x, float cell_width_z, bool display_text = true)
	{

		#region saving arguments

		this.length_x = length_x;
		this.width_z = width_z;
		this.cell_length_x = cell_length_x;
		this.cell_width_z = cell_width_z;
		this.display_text = display_text;

		#endregion

		text_objects_array = new GameObject [length_x, width_z];

		grid_array = new int [length_x, width_z, Enum.GetNames(typeof(grid_parameter)).Length];
		array_of_characters_on_tile_lists = new List<GameObject> [length_x, width_z];
		GameObject.Find("Text Collection").GetComponent<RectTransform>().sizeDelta = new Vector2 (length_x, width_z);

		for (int i = 0; i < length_x; i++)
		{
			for (int j = 0; j < width_z; j++)
			{
				Debug.DrawLine (GetWorldPosition (i, j, 0) , GetWorldPosition (i + 1, j, 0), Color.white, 10000);
				Debug.DrawLine (GetWorldPosition (i, j, 0) , GetWorldPosition (i, j + 1, 0), Color.white, 10000);
				if (display_text == true)
				{
					array_of_characters_on_tile_lists [i, j] = new List<GameObject> ();
					text_objects_array[i, j] = new GameObject("text " + i + "," + j);
					text_objects_array[i, j].transform.SetParent(GameObject.Find("Text Collection").transform);
					text_objects_array [i, j].AddComponent<TextMeshProUGUI>().fontSize = 0.05f;
					text_objects_array[i, j].transform.position = GetWorldTileCenter(i, j, 0);
					text_objects_array[i, j].transform.Rotate (90, 0, 0);
					text_objects_array [i, j].GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.CenterGeoAligned;
					text_objects_array [i, j].GetComponent<RectTransform>().sizeDelta = new Vector2 (1, 1);
				}
			}
		}

		Debug.DrawLine (GetWorldPosition (length_x, 0, 0) , GetWorldPosition (length_x, width_z, 0), Color.white, 10000);
		Debug.DrawLine (GetWorldPosition (0, width_z, 0) , GetWorldPosition (length_x, width_z, 0), Color.white, 10000);
		GridTextUpdate ();

	}

	public GameGrid ((int length_x, int width_z) dimentions, float cell_length_x, float cell_width_z, bool display_text = true)
	{

		#region saving arguments

		this.length_x = dimentions.length_x;
		this.width_z = dimentions.width_z;
		this.cell_length_x = cell_length_x;
		this.cell_width_z = cell_width_z;
		this.display_text = display_text;

		#endregion

		text_objects_array = new GameObject [length_x, width_z];

		grid_array = new int [length_x, width_z, Enum.GetNames(typeof(grid_parameter)).Length];
		array_of_characters_on_tile_lists = new List<GameObject> [length_x, width_z];
		GameObject.Find("Text Collection").GetComponent<RectTransform>().sizeDelta = new Vector2 (length_x, width_z);

		for (int i = 0; i < length_x; i++)
		{
			for (int j = 0; j < width_z; j++)
			{
				Debug.DrawLine (GetWorldPosition (i, j, 0) , GetWorldPosition (i + 1, j, 0), Color.white, 10000);
				Debug.DrawLine (GetWorldPosition (i, j, 0) , GetWorldPosition (i, j + 1, 0), Color.white, 10000);
				if (display_text == true)
				{
					array_of_characters_on_tile_lists [i, j] = new List<GameObject> ();
					text_objects_array[i, j] = new GameObject("text " + i + "," + j);
					text_objects_array[i, j].transform.SetParent(GameObject.Find("Text Collection").transform);
					text_objects_array [i, j].AddComponent<TextMeshProUGUI>().fontSize = 0.05f;
					text_objects_array[i, j].transform.position = GetWorldTileCenter(i, j, 0);
					text_objects_array[i, j].transform.Rotate (90, 0, 0);
					text_objects_array [i, j].GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.CenterGeoAligned;
					text_objects_array [i, j].GetComponent<RectTransform>().sizeDelta = new Vector2 (1, 1);
				}
			}
		}

		Debug.DrawLine (GetWorldPosition (length_x, 0, 0) , GetWorldPosition (length_x, width_z, 0), Color.white, 10000);
		Debug.DrawLine (GetWorldPosition (0, width_z, 0) , GetWorldPosition (length_x, width_z, 0), Color.white, 10000);
		GridTextUpdate ();
	}

	#region GridTextToggle, GridTextUpdate, GridTextTileUpdate

	public void GridTextUpdate ()
	{
		if (display_text == true)
		{
			for (int i = 0; i < length_x; i++)
			{
				for (int j = 0; j < width_z; j++)
				{
					GridTextTileUpdate (i, j);
				}
			}
		}
	}

	public void GridTextTileUpdate (int x, int z)
	{
		text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text = "tile - " + x + "," + z + System.Environment.NewLine;
		var parameter_names = Enum.GetNames(typeof(grid_parameter));
		for(int k = 0; k < parameter_names.Length; k++)
		{
			if (parameter_names[k] == grid_parameter.prev_x_empty.ToString())
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += "prev_tile_empty - " + grid_array [x,z,k] + "," + grid_array [x,z,k + 1] + System.Environment.NewLine +
				"prev_tile_any_object - " + grid_array [x,z,k + 2] + "," + grid_array [x,z,k + 3] + System.Environment.NewLine +
				"prev_tile_any_terrain - " + grid_array [x,z,k + 4] + "," + grid_array [x,z,k + 5] + System.Environment.NewLine +
				"prev_tile_any_tile - " + grid_array [x,z,k + 6] + "," + grid_array [x,z,k + 7];
				break;
			}
			if (parameter_names[k] == grid_parameter.object_type.ToString())
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += parameter_names[k] + " - " + ObjectTypeTranslator (grid_array [x,z,k]).ToString() + System.Environment.NewLine;
			}
			if (parameter_names[k] == grid_parameter.terrain.ToString())
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += parameter_names[k] + " - " + TerrainTranslator (grid_array [x,z,k]).ToString() + System.Environment.NewLine;
			}
			if (parameter_names[k] == grid_parameter.spawn_zone.ToString())
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += parameter_names[k] + " - " + SpawnZoneTranslator (grid_array [x,z,k]).ToString() + System.Environment.NewLine;
			}
			if (parameter_names[k] == grid_parameter.enemy.ToString())
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += parameter_names[k] + " - " + EnemyTranslator (grid_array [x,z,k]).ToString() + System.Environment.NewLine;
			}
			if (parameter_names[k] == grid_parameter.mana.ToString())
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += parameter_names[k] + " - " + ManaTranslator (grid_array [x,z,k]).ToString() + System.Environment.NewLine;
			}
			if (parameter_names[k] == grid_parameter.tile_effect.ToString())
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += parameter_names[k] + " - " + TileEffectTranslator (grid_array [x,z,k]).ToString() + System.Environment.NewLine;
			}
			if (parameter_names[k] == grid_parameter.amount_of_dead.ToString() || parameter_names[k] == grid_parameter.simulated.ToString() || parameter_names[k] == grid_parameter.object_id.ToString())
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += parameter_names[k] + " - " + grid_array [x,z,k] + System.Environment.NewLine;
			}
		}
	}

	#endregion

	#region GetXZ, GetWorldPosition, GetWordTileCenter

	public (int x, int z) GetXZ (Vector3 position)
	{
		return ((int)Math.Floor(position.x / cell_length_x), (int)Math.Floor(position.z / cell_width_z));
	}

	public Vector3 GetWorldPosition (int x, int z)
	{
		return new Vector3 (x * cell_length_x, 0.5f, z * cell_width_z);
	}

	public Vector3 GetWorldPosition (int x, int z, float object_y)
	{
		return new Vector3 (x * cell_length_x, object_y / 2, z * cell_width_z);
	}

	public Vector3 GetWorldTileCenter (int x, int z)
	{
		return new Vector3 (((float)x + 0.5f) * cell_length_x, 0.5f, ((float)z + 0.5f) * cell_width_z);
	}

	public Vector3 GetWorldTileCenter (int x, int z, float object_y)
	{
		return new Vector3 (((float)x + 0.5f) * cell_length_x, object_y / 2, ((float)z + 0.5f) * cell_width_z);
	}

	public Vector3 GetWorldTileCenter ((int x, int z) xz_tuple)
	{
		return new Vector3 (((float)xz_tuple.x + 0.5f) * cell_length_x, 0.5f, ((float)xz_tuple.z + 0.5f) * cell_width_z);
	}

	public Vector3 GetWorldTileCenter ((int x, int z) xz_tuple, float object_y)
	{
		return new Vector3 (((float)xz_tuple.x + 0.5f) * cell_length_x, object_y / 2, ((float)xz_tuple.z + 0.5f) * cell_width_z);
	}

	#endregion

	#region SetValue

	public void SetValue (int x, int z, int parameter, int value)
	{
		grid_array[x, z, parameter] = value;
	}

	public void SetValue (int x, int z, grid_parameter parameter, int value)
	{
		grid_array[x, z, EnumTranslator(parameter)] = value;
	}

	public void SetValue (int x, int z, grid_parameter parameter, object_type value)
	{
		grid_array[x, z, EnumTranslator(parameter)] = EnumTranslator(value);
	}

	public void SetValue (int x, int z, grid_parameter parameter, enemy value)
	{
		grid_array[x, z, EnumTranslator(parameter)] = EnumTranslator(value);
	}

	public void SetValue ((int x, int z) position_tuple, int parameter, int value)
	{
		grid_array[position_tuple.x, position_tuple.z, parameter] = value;
	}

	public void SetValue ((int x, int z) position_tuple, grid_parameter parameter, int value)
	{
		grid_array[position_tuple.x, position_tuple.z, EnumTranslator(parameter)] = value;
	}

	public void SetValue ((int x, int z) position_tuple, grid_parameter parameter, object_type value)
	{
		grid_array[position_tuple.x, position_tuple.z, EnumTranslator(parameter)] = EnumTranslator(value);
	}

	public void SetValue ((int x, int z) position_tuple, grid_parameter parameter, enemy value)
	{
		grid_array[position_tuple.x, position_tuple.z, EnumTranslator(parameter)] = EnumTranslator(value);
	}
	#endregion

	public int GetValue (int x, int z, grid_parameter parameter)
	{
		return grid_array[x, z, EnumTranslator(parameter)];
	}

	public int GetValue ((int x, int z) position, grid_parameter parameter)
	{
		return grid_array[position.x, position.z, EnumTranslator(parameter)];
	}

	public (int x, int z) RandomTile (object_type type)
	{
		if (GetAnyEmptyTile() != (-1, -1))
		{
			List <(int x, int z)> tiles_tuple_list = new List <(int x, int z)> ();
			for (int x = 0; x < length_x; x++)
			{
				for (int z = 0; z < length_x; z++)
				{
					tiles_tuple_list.Add ((x, z));
				}
			}
			bool found = false;
			while (found == false)
			{
				int random_int = UnityEngine.Random.Range (0, tiles_tuple_list.Count);
				(int x, int z) random_tile = tiles_tuple_list [random_int];
				if (GetValue(random_tile.x, random_tile.z, grid_parameter.object_type) == EnumTranslator(type))
				{
					return (random_tile.x, random_tile.z);
				}
				else
				{
					tiles_tuple_list.RemoveAt (random_int);
				}
			}
		}
		return (-1, -1);
	}

	public (int x, int z) GetAnyEmptyTile ()
	{
		for (int x = 0; x < length_x; x++)
		{
			for (int z = 0; z < length_x; z++)
			{
				if (GetValue(x, z, grid_parameter.object_type) == EnumTranslator(object_type.empty))
				{
					return (x, z);
				}
			}
		}
		return (-1, -1);
	}

	public (int x, int z) GetClosestEmptyTile (Vector3 reference_position)
	{
		(int x, int z) ref_position = GetXZ (reference_position);
		int range = 1;
		while (range <= 5)
		{
			for (int x = ref_position.x - range; x <= ref_position.x + range; x++)
			{
				for (int z = ref_position.z - range; z <= ref_position.z + range; z++)
				{
					if (x >= 0 && x < length_x && z >= 0 && z < width_z)
					{
						if (GetValue(x, z, grid_parameter.object_type) == EnumTranslator(object_type.empty) && (x != 0 || z != 0))
						{
							return (x, z);
						}
					}
				}
			}
			range++;
		}
		return (-1, -1);
	}

	public (int x, int z) GetClosestEmptyTile ((int x, int z) reference_position)
	{
		int range = 1;
		while (range <= 5)
		{
			for (int x = reference_position.x - range; x <= reference_position.x + range; x++)
			{
				for (int z = reference_position.z - range; z <= reference_position.z + range; z++)
				{
					if (x >= 0 && x < length_x && z >= 0 && z < width_z)
					{
						if (GetValue(x, z, grid_parameter.object_type) == EnumTranslator(object_type.empty) && (x != 0 || z != 0))
						{
							return (x, z);
						}
					}
				}
			}
			range++;
		}
		return (-1, -1);
	}

	public bool CheckIfInsideGrid (int x, int z)
	{
		if (x >= 0 && x < length_x && z >= 0 && z < width_z)
		{
			return true;
		}
		return false;
	}

	public bool CheckIfNotDiagonal (int original_x, int original_z, int x, int z)
	{
		if ((x == original_x && z != original_z) || (x != original_x && z == original_z))
		{
			return true;
		}
		return false;
	}

	public bool CheckIfValidSpawnZone (int x, int z)
	{
		if (GetValue (x, z, grid_parameter.terrain) != EnumTranslator(terrain.water) &&
		GetValue (x, z, grid_parameter.object_type) == EnumTranslator(object_type.empty))
		{
			return true;
		}
		return false;
	}
	public bool CheckIfValidSpawnZone ((int x, int z) position)
	{
		if (GetValue (position.x, position.z, grid_parameter.terrain) != EnumTranslator(terrain.water) &&
		GetValue (position.x, position.z, grid_parameter.object_type) == EnumTranslator(object_type.empty))
		{
			return true;
		}
		return false;
	}

	public bool CheckIfValidSpawnZone (Vector3 vector_position)
	{
		(int x, int z) position = GetXZ (vector_position);
		if (GetValue (position.x, position.z, grid_parameter.terrain) != EnumTranslator (terrain.water) &&
		GetValue (position.x, position.z, grid_parameter.object_type) == EnumTranslator (object_type.empty))
		{
			return true;
		}
		return false;
	}
}
