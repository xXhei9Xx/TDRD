using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

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
		prev_x,
		prev_z,
		simulated,
		terrain,
		spawn_zone,
		mana,
		tile_effect,
		amount_of_dead
	}

	public int EnumTranslator (grid_parameter parameter)
	{
		switch (parameter)
		{
			default: return 0;

			case grid_parameter.object_type: return 0;

			case grid_parameter.object_id: return 1;

			case grid_parameter.enemy: return 2;

			case grid_parameter.prev_x: return 3;

			case grid_parameter.prev_z: return 4;

			case grid_parameter.simulated: return 5;

			case grid_parameter.terrain: return 6;

			case grid_parameter.spawn_zone: return 7;

			case grid_parameter.mana: return 8;
		}
	}
	#endregion
	#region object type
	public enum object_type
	{
		empty,
		spawner,
		tower,
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

			case object_type.core: return 3;

			case object_type.tree: return 4;

			case object_type.rock: return 5;
		}
	}
	#endregion
	#region terrain
	public enum terrain
	{
		grass,
		sand,
		rock,
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
	#endregion
	#region grid direction
	public enum grid_direction
	{
		up,
		down,
		left,
		right
	}

	public grid_direction GetMovementDirection (Vector3 starting_position, (int x, int z) destination)
	{
		(int x, int z) direction = (destination.x - (int) Math.Floor (starting_position.x), destination.z - (int) Math.Floor (starting_position.z));
		if (direction.x != 0)
		{
			if (direction.x == -1)
			{
				return grid_direction.right;
			}
			else
			{
				return grid_direction.left;
			}
		}
		else
		{
			if (direction.z == -1)
			{
				return grid_direction.up;
			}
			else
			{
				return grid_direction.down;
			}
		}
	}

	public grid_direction GetMovementDirection ((int x, int z) starting_position, (int x, int z) destination)
	{
		(int x, int z) direction = (destination.x - starting_position.x, destination.z - starting_position.z);
		if (direction.x != 0)
		{
			if (direction.x == -1)
			{
				return grid_direction.right;
			}
			else
			{
				return grid_direction.left;
			}
		}
		else
		{
			if (direction.z == -1)
			{
				return grid_direction.up;
			}
			else
			{
				return grid_direction.down;
			}
		}
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
				array_of_characters_on_tile_lists [i, j] = new List<GameObject> ();
				text_objects_array[i, j] = new GameObject("text " + i + "," + j);
				text_objects_array[i, j].transform.SetParent(GameObject.Find("Text Collection").transform);
				text_objects_array [i, j].AddComponent<TextMeshProUGUI>().fontSize = 0.1f;
				text_objects_array[i, j].transform.position = GetWorldTileCenter(i, j, 0);
				text_objects_array[i, j].transform.Rotate (90, 0, 0);
				text_objects_array [i, j].GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.CenterGeoAligned;
				text_objects_array [i, j].GetComponent<RectTransform>().sizeDelta = new Vector2 (1, 1);
				for(int k = 0; k < Enum.GetNames(typeof(grid_parameter)).Length; k++)
				{
					grid_array [i,j,k] = 0;
					text_objects_array [i, j].GetComponent<TextMeshProUGUI>().text += grid_array [i,j,k] + " ";
					if (k > 0 && k % 6 == 0)
					{
						text_objects_array [i, j].GetComponent<TextMeshProUGUI>().text += System.Environment.NewLine;
					}
				}
			}
		}

		Debug.DrawLine (GetWorldPosition (length_x, 0, 0) , GetWorldPosition (length_x, width_z, 0), Color.white, 10000);
		Debug.DrawLine (GetWorldPosition (0, width_z, 0) , GetWorldPosition (length_x, width_z, 0), Color.white, 10000);
		GridTextToggle (display_text);
	}

	#region GridTextToggle, GridTextUpdate, GridTextTileUpdate

	public void GridTextToggle (bool state)
	{
		for (int i = 0; i < length_x; i++)
		{
			for (int j = 0; j < width_z; j++)
			{
				text_objects_array [i, j].GetComponent<TextMeshProUGUI>().enabled = state;
			}
		}
	}

	public void GridTextUpdate ()
	{
		for (int i = 0; i < length_x; i++)
		{
			for (int j = 0; j < width_z; j++)
			{
				GridTextTileUpdate (i, j);
			}
		}
	}

	public void GridTextTileUpdate (int x, int z)
	{
		text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text = "";
		for(int k = 0; k < Enum.GetNames(typeof(grid_parameter)).Length; k++)
		{
			text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += grid_array [x,z,k] + ",";
			if (k > 0 && k % 6 == 0)
			{
				text_objects_array [x, z].GetComponent<TextMeshProUGUI>().text += System.Environment.NewLine;
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
		return new Vector3 (x * cell_length_x, 0, z * cell_width_z);
	}

	public Vector3 GetWorldPosition (int x, int z, float object_y)
	{
		return new Vector3 (x * cell_length_x, object_y / 2, z * cell_width_z);
	}

	public Vector3 GetWorldTileCenter (int x, int z)
	{
		return new Vector3 (((float)x + 0.5f) * cell_length_x, 0, ((float)z + 0.5f) * cell_width_z);
	}

	public Vector3 GetWorldTileCenter (int x, int z, float object_y)
	{
		return new Vector3 (((float)x + 0.5f) * cell_length_x, object_y / 2, ((float)z + 0.5f) * cell_width_z);
	}

	public Vector3 GetWorldTileCenter ((int x, int z) xz_tuple)
	{
		return new Vector3 (((float)xz_tuple.x + 0.5f) * cell_length_x, 0, ((float)xz_tuple.z + 0.5f) * cell_width_z);
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
		GridTextTileUpdate (x, z);
	}

	public void SetValue (int x, int z, grid_parameter parameter, int value)
	{
		grid_array[x, z, EnumTranslator(parameter)] = value;
		GridTextTileUpdate (x, z);
	}

	public void SetValue (int x, int z, grid_parameter parameter, object_type value)
	{
		grid_array[x, z, EnumTranslator(parameter)] = EnumTranslator(value);
		GridTextTileUpdate (x, z);
	}

	public void SetValue (int x, int z, grid_parameter parameter, enemy value)
	{
		grid_array[x, z, EnumTranslator(parameter)] = EnumTranslator(value);
		GridTextTileUpdate (x, z);
	}

	public void SetValue ((int x, int z) position_tuple, int parameter, int value)
	{
		grid_array[position_tuple.x, position_tuple.z, parameter] = value;
		GridTextTileUpdate (position_tuple.x, position_tuple.z);
	}

	public void SetValue ((int x, int z) position_tuple, grid_parameter parameter, int value)
	{
		grid_array[position_tuple.x, position_tuple.z, EnumTranslator(parameter)] = value;
		GridTextTileUpdate (position_tuple.x, position_tuple.z);
	}

	public void SetValue ((int x, int z) position_tuple, grid_parameter parameter, object_type value)
	{
		grid_array[position_tuple.x, position_tuple.z, EnumTranslator(parameter)] = EnumTranslator(value);
		GridTextTileUpdate (position_tuple.x, position_tuple.z);
	}

	public void SetValue ((int x, int z) position_tuple, grid_parameter parameter, enemy value)
	{
		grid_array[position_tuple.x, position_tuple.z, EnumTranslator(parameter)] = EnumTranslator(value);
		GridTextTileUpdate (position_tuple.x, position_tuple.z);
	}
	#endregion

	public int GetValue (int x, int z, grid_parameter parameter)
	{
		return grid_array[x, z, EnumTranslator(parameter)];
	}

	public int RandomInt (int lower_range, int upper_range)
	{
		return (int) Math.Floor(UnityEngine.Random.Range((float) lower_range, (float) upper_range + 1));
	}

	public (int x, int z) RandomTile (object_type type)
	{
		int x, z;
		if (GetAnyEmptyTile() != (-1, -1))
		{
			bool found = false;
			while (found == false)
			{
				x = RandomInt (0, length_x - 1); z = RandomInt (0, width_z - 1);
				if (GetValue(x, z, grid_parameter.object_type) == EnumTranslator(type))
				{
					return (x, z);
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
}
