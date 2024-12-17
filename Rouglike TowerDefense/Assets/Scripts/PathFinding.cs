using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using static GameGrid;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using System.Data.Common;
using static Core;

public class PathFinding
{
	#region variable declarations

	private List<(int x, int z)> tiles_to_check_list;

	public enum pathfinding_tile_parameter
	{
		empty,
		any_object,
		any_terrain,
		any_tile
	}

	#endregion
	
	public bool CheckPath (int x, int z, GameGrid grid, pathfinding_tile_parameter parameter, out int path_length)
	{
		int counter = 1;
		int next_x = 0;
		int next_z = 0;
		int current_x = 0;
		int current_z = 0;
		switch (parameter)
		{
			case pathfinding_tile_parameter.empty:
			next_x = grid.GetValue (x, z, grid_parameter.prev_x_empty);
			next_z = grid.GetValue (x, z, grid_parameter.prev_z_empty);
			break;

			case pathfinding_tile_parameter.any_object:
			next_x = grid.GetValue (x, z, grid_parameter.prev_x_any_object);
			next_z = grid.GetValue (x, z, grid_parameter.prev_z_any_object);
			break;

			case pathfinding_tile_parameter.any_terrain:
			next_x = grid.GetValue (x, z, grid_parameter.prev_x_any_terrain);
			next_z = grid.GetValue (x, z, grid_parameter.prev_z_any_terrain);
			break;

			case pathfinding_tile_parameter.any_tile:
			next_x = grid.GetValue (x, z, grid_parameter.prev_x_any_tile);
			next_z = grid.GetValue (x, z, grid_parameter.prev_z_any_tile);
			break;
		}
		while (counter < (grid.length_x * grid.width_z) + 1)
		{
			if (grid.GetValue (current_x, current_z, grid_parameter.object_type) == grid.EnumTranslator (object_type.core))
			{
				path_length = counter;
				return true;
			}
			switch (parameter)
			{
				case pathfinding_tile_parameter.empty:
				next_x = grid.GetValue (current_x, current_z, grid_parameter.prev_x_empty);
				next_z = grid.GetValue (current_x, current_z, grid_parameter.prev_z_empty);
				break;

				case pathfinding_tile_parameter.any_object:
				next_x = grid.GetValue (current_x, current_z, grid_parameter.prev_x_any_object);
				next_z = grid.GetValue (current_x, current_z, grid_parameter.prev_z_any_object);
				break;

				case pathfinding_tile_parameter.any_terrain:
				next_x = grid.GetValue (current_x, current_z, grid_parameter.prev_x_any_terrain);
				next_z = grid.GetValue (current_x, current_z, grid_parameter.prev_z_any_terrain);
				break;

				case pathfinding_tile_parameter.any_tile:
				next_x = grid.GetValue (current_x, next_z, grid_parameter.prev_x_any_tile);
				next_z = grid.GetValue (current_x, next_z, grid_parameter.prev_z_any_tile);
				break;
			}
			current_x = next_x;
			current_z = next_z;
			counter++;
			if (next_x == -1)
			{
				break;
			}
		}
		path_length = 0;
		return false;
	}

	public bool CheckPath ((int x, int z) starting_position, GameGrid grid, pathfinding_tile_parameter parameter, out int path_length)
	{
		int counter = 1;
		int next_x = 0;
		int next_z = 0;
		int current_x = 0;
		int current_z = 0;
		switch (parameter)
		{
			case pathfinding_tile_parameter.empty:
			next_x = grid.GetValue (starting_position.x, starting_position.z, grid_parameter.prev_x_empty);
			next_z = grid.GetValue (starting_position.x, starting_position.z, grid_parameter.prev_z_empty);
			break;

			case pathfinding_tile_parameter.any_object:
			next_x = grid.GetValue (starting_position.x, starting_position.z, grid_parameter.prev_x_any_object);
			next_z = grid.GetValue (starting_position.x, starting_position.z, grid_parameter.prev_z_any_object);
			break;

			case pathfinding_tile_parameter.any_terrain:
			next_x = grid.GetValue (starting_position.x, starting_position.z, grid_parameter.prev_x_any_terrain);
			next_z = grid.GetValue (starting_position.x, starting_position.z, grid_parameter.prev_z_any_terrain);
			break;

			case pathfinding_tile_parameter.any_tile:
			next_x = grid.GetValue (starting_position.x, starting_position.z, grid_parameter.prev_x_any_tile);
			next_z = grid.GetValue (starting_position.x, starting_position.z, grid_parameter.prev_z_any_tile);
			break;
		}
		while (counter < (grid.length_x * grid.width_z) + 1)
		{
			if (grid.GetValue (current_x, current_z, grid_parameter.object_type) == grid.EnumTranslator (object_type.core))
			{
				path_length = counter;
				return true;
			}
			switch (parameter)
			{
				case pathfinding_tile_parameter.empty:
				next_x = grid.GetValue (current_x, current_z, grid_parameter.prev_x_empty);
				next_z = grid.GetValue (current_x, current_z, grid_parameter.prev_z_empty);
				break;

				case pathfinding_tile_parameter.any_object:
				next_x = grid.GetValue (current_x, current_z, grid_parameter.prev_x_any_object);
				next_z = grid.GetValue (current_x, current_z, grid_parameter.prev_z_any_object);
				break;

				case pathfinding_tile_parameter.any_terrain:
				next_x = grid.GetValue (current_x, current_z, grid_parameter.prev_x_any_terrain);
				next_z = grid.GetValue (current_x, current_z, grid_parameter.prev_z_any_terrain);
				break;

				case pathfinding_tile_parameter.any_tile:
				next_x = grid.GetValue (current_x, next_z, grid_parameter.prev_x_any_tile);
				next_z = grid.GetValue (current_x, next_z, grid_parameter.prev_z_any_tile);
				break;
			}
			current_x = next_x;
			current_z = next_z;
			counter++;
			if (next_x == -1)
			{
				break;
			}
		}
		path_length = 0;
		return false;
	}

	public void FindPath (GameGrid grid, List<CoreObject> cores_list)
	{
		for (int x = 0; x < grid.length_x; x++)
		{
			for (int z = 0; z < grid.width_z; z++)
			{
				SetSimulated (x, z, grid, false);
				ResetPreviousXZ (x, z, grid);
			}
		}
		tiles_to_check_list = new List<(int x, int z)> {};
		var pathfinding_tile_parameter_values = Enum.GetValues (typeof (pathfinding_tile_parameter));
		foreach (int parameter in pathfinding_tile_parameter_values)
		{
			foreach (CoreObject core in cores_list)
			{
				tiles_to_check_list.Add (grid.GetXZ (core.position));
				SetSimulated (grid.GetXZ (core.position), grid, true);
			}
			while (tiles_to_check_list.Count != 0)
			{
				for (int x = tiles_to_check_list [0].x - 1 ; x <= tiles_to_check_list [0].x + 1; x++)
				{
					for (int z = tiles_to_check_list [0].z - 1 ; z <= tiles_to_check_list [0].z + 1; z++)
					{
						if (x >= 0 && x < grid.length_x && z >= 0 && z < grid.width_z && grid.GetValue (x, z, grid_parameter.simulated) == 0 &&
						((x != tiles_to_check_list [0].x && z == tiles_to_check_list [0].z) || (x == tiles_to_check_list [0].x && z != tiles_to_check_list [0].z)))
						{	
							if (CheckIfTileIsTraversable (grid, x, z, (pathfinding_tile_parameter)parameter) == true)
							{
								SetPreviousXZ (x, z, tiles_to_check_list [0].x, tiles_to_check_list [0].z, grid, (pathfinding_tile_parameter)parameter);
								tiles_to_check_list.Add ((x, z));
								SetSimulated (x, z, grid, true);
							}
						}
					}
				}
				for (int x = tiles_to_check_list [0].x - 1 ; x <= tiles_to_check_list [0].x + 1; x = x +2)
				{
					for (int z = tiles_to_check_list [0].z - 1 ; z <= tiles_to_check_list [0].z + 1; z = z + 2)
					{
						if (x >= 0 && x < grid.length_x && z >= 0 && z < grid.width_z && grid.GetValue (x, z, grid_parameter.simulated) == 0 &&
						CheckIfTileIsTraversable (grid, tiles_to_check_list [0].x, z, (pathfinding_tile_parameter)parameter) == true && CheckIfTileIsTraversable (grid, x, tiles_to_check_list [0].z, (pathfinding_tile_parameter)parameter) == true)
						{	
							if (CheckIfTileIsTraversable (grid, x, z, (pathfinding_tile_parameter)parameter) == true)
							{
								SetPreviousXZ (x, z, tiles_to_check_list [0].x, tiles_to_check_list [0].z, grid, (pathfinding_tile_parameter)parameter);
								tiles_to_check_list.Add ((x, z));
								SetSimulated (x, z, grid, true);
							}
						}
					}
				}
				tiles_to_check_list.RemoveAt(0);
			}
			for (int x = 0; x < grid.length_x; x++)
			{
				for (int z = 0; z < grid.width_z; z++)
				{
					SetSimulated (x, z, grid, false);
				}
			}
		}
		grid.GridTextUpdate ();
	}

	private void SetPreviousXZ (int x, int z, int prev_x, int prev_z, GameGrid grid, pathfinding_tile_parameter parameter)
	{
		switch (parameter)
		{
			case pathfinding_tile_parameter.empty:
			grid.SetValue (x, z, grid_parameter.prev_x_empty, prev_x);
			grid.SetValue (x, z, grid_parameter.prev_z_empty, prev_z);
			break;

			case pathfinding_tile_parameter.any_object:
			grid.SetValue (x, z, grid_parameter.prev_x_any_object, prev_x);
			grid.SetValue (x, z, grid_parameter.prev_z_any_object, prev_z);
			break;

			case pathfinding_tile_parameter.any_terrain:
			grid.SetValue (x, z, grid_parameter.prev_x_any_terrain, prev_x);
			grid.SetValue (x, z, grid_parameter.prev_z_any_terrain, prev_z);
			break;

			case pathfinding_tile_parameter.any_tile:
			grid.SetValue (x, z, grid_parameter.prev_x_any_tile, prev_x);
			grid.SetValue (x, z, grid_parameter.prev_z_any_tile, prev_z);
			break;
		}
	}

	public void ResetPreviousXZ (int x, int z, GameGrid grid)
	{
		grid.SetValue (x, z, grid_parameter.prev_x_empty, -1);
		grid.SetValue (x, z, grid_parameter.prev_z_empty, -1);
		grid.SetValue (x, z, grid_parameter.prev_x_any_object, -1);
		grid.SetValue (x, z, grid_parameter.prev_z_any_object, -1);
		grid.SetValue (x, z, grid_parameter.prev_x_any_terrain, -1);
		grid.SetValue (x, z, grid_parameter.prev_z_any_terrain, -1);
		grid.SetValue (x, z, grid_parameter.prev_x_any_tile, -1);
		grid.SetValue (x, z, grid_parameter.prev_z_any_tile, -1);
	}

	private void SetSimulated (int x, int z, GameGrid grid, bool simulated)
	{
		if (simulated == true)
		{
			grid.SetValue (x, z, grid_parameter.simulated, 1);
		}
		else
		{
			grid.SetValue (x, z, grid_parameter.simulated, 0);
		}
	}

	private void SetSimulated ((int x, int z) position, GameGrid grid, bool simulated)
	{
		if (simulated == true)
		{
			grid.SetValue (position.x, position.z, grid_parameter.simulated, 1);
		}
		else
		{
			grid.SetValue (position.x, position.z, grid_parameter.simulated, 0);
		}
	}

	private bool CheckIfTileIsTraversable (GameGrid grid, int x, int z, pathfinding_tile_parameter parameter)
	{
		switch (parameter)
		{
			case pathfinding_tile_parameter.empty:
			if ((grid.GetValue (x, z, grid_parameter.object_type) == grid.EnumTranslator (object_type.empty) ||
				 grid.GetValue (x, z, grid_parameter.object_type) == grid.EnumTranslator (object_type.traversable_tower) ||
				 grid.GetValue (x, z, grid_parameter.object_type) == grid.EnumTranslator (object_type.spawner)) &&
				 grid.GetValue (x, z, grid_parameter.terrain) != grid.EnumTranslator (terrain.water))
			{
				return true;
			}
			else
			{
				return false;
			}

			case pathfinding_tile_parameter.any_object:
			if (grid.GetValue (x, z, grid_parameter.terrain) != grid.EnumTranslator (terrain.water))
			{
				return true;
			}
			else
			{
				return false;
			}

			case pathfinding_tile_parameter.any_terrain:
			if (grid.GetValue (x, z, grid_parameter.object_type) == grid.EnumTranslator (object_type.empty) ||
				grid.GetValue(x, z, grid_parameter.object_type) == grid.EnumTranslator (object_type.traversable_tower) ||
				 grid.GetValue (x, z, grid_parameter.object_type) == grid.EnumTranslator (object_type.spawner))
			{
				return true;
			}
			else
			{
				return false;
			}

			case pathfinding_tile_parameter.any_tile:
			return true;
		}
		return false;
	}
}
