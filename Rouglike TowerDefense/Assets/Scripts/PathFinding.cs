using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PathFinding
{
	#region variable declarations

	private (int x, int z) ending_tuple;
	private bool found_path;
	private List<(int x, int z)> tiles_to_check_list;

	public enum pathfinding_tile_parameter
	{
		empty,
		any_terrain,
		any_tile
	}

	#endregion
	
	public bool FindPath (Vector3 position, GameGrid grid, out (int x, int z) [] path_tuple_array, pathfinding_tile_parameter parameter)
	{
		for (int x = 0 ; x < grid.length_x; x++)
		{
			for (int z = 0 ; z < grid.length_x; z++)
			{
				SetPreviousXZ (x, z, 0, 0, grid, false);
			}
		}
		found_path = false;
		tiles_to_check_list = new List<(int x, int z)> {grid.GetXZ(position)};
		grid.SetValue(tiles_to_check_list [0].x, tiles_to_check_list [0].z, GameGrid.grid_parameter.simulated, 1);
		while (found_path == false)
		{
			for (int x = tiles_to_check_list [0].x - 1 ; x <= tiles_to_check_list [0].x + 1; x++)
			{
				for (int z = tiles_to_check_list [0].z - 1 ; z <= tiles_to_check_list [0].z + 1; z++)
				{
					if (x >= 0 && x < grid.length_x && z >= 0 && z < grid.width_z && grid.GetValue(x, z, GameGrid.grid_parameter.simulated) == 0 &&
					( (x != tiles_to_check_list [0].x && z == tiles_to_check_list [0].z) || (x == tiles_to_check_list [0].x && z != tiles_to_check_list [0].z) ) )
					{	
						if (grid.GetValue(x, z, GameGrid.grid_parameter.object_type) == grid.EnumTranslator(GameGrid.object_type.core))
						{
							found_path = true;
							ending_tuple = (x, z);
						}
						else
						{
							switch (parameter)
							{
								case pathfinding_tile_parameter.empty:
								if (grid.GetValue (x, z, GameGrid.grid_parameter.object_type) == grid.EnumTranslator (GameGrid.object_type.empty) &&
									grid.GetValue (x, z, GameGrid.grid_parameter.terrain) != grid.EnumTranslator (GameGrid.terrain.water) &&
									grid.GetValue (x, z, GameGrid.grid_parameter.simulated) == 0)
								{
									tiles_to_check_list.Add ((x, z));
								}
								break;

								case pathfinding_tile_parameter.any_terrain:
								if (grid.GetValue (x, z, GameGrid.grid_parameter.object_type) != grid.EnumTranslator (GameGrid.object_type.tower) &&
								    grid.GetValue (x, z, GameGrid.grid_parameter.simulated) == 0)
								{
									tiles_to_check_list.Add ((x, z));
								}
								break;

								case pathfinding_tile_parameter.any_tile:
								if (grid.GetValue (x, z, GameGrid.grid_parameter.simulated) == 0)
								{
									tiles_to_check_list.Add ((x, z));
								}
								break;
							}
						}

						SetPreviousXZ (x, z, tiles_to_check_list [0].x, tiles_to_check_list [0].z, grid, true);
					}
				}
			}
			
			tiles_to_check_list.RemoveAt(0);
			if (tiles_to_check_list.Count == 0)
			{
				path_tuple_array = null;
				for (int x = 0 ; x < grid.length_x; x++)
				{
					for (int z = 0 ; z < grid.length_x; z++)
					{
							SetPreviousXZ (x, z, 0, 0, grid, false);
					}
				}
				return false;
			}
		}
		path_tuple_array = new (int x, int z) [1] {ending_tuple};
		int counter = 0;
		while (!(path_tuple_array [counter].x == grid.GetXZ(position).x && path_tuple_array [counter].z == grid.GetXZ(position).z))
		{
			path_tuple_array = path_tuple_array.Append((grid.GetValue(path_tuple_array [counter].x,
			path_tuple_array [counter].z, GameGrid.grid_parameter.prev_x),
			grid.GetValue(path_tuple_array [counter].x, path_tuple_array [counter].z, GameGrid.grid_parameter.prev_z))).ToArray();
			counter++;
		}
		return true;
	}

	public bool FindPath ((int x, int z) position, GameGrid grid, out (int x, int z) [] path_tuple_array, pathfinding_tile_parameter parameter)
	{
		for (int x = 0 ; x < grid.length_x; x++)
		{
			for (int z = 0 ; z < grid.length_x; z++)
			{
				SetPreviousXZ (x, z, 0, 0, grid, false);
			}
		}
		found_path = false;
		tiles_to_check_list = new List<(int x, int z)> {position};
		grid.SetValue(tiles_to_check_list [0].x, tiles_to_check_list [0].z, GameGrid.grid_parameter.simulated, 1);
		while (found_path == false)
		{
			for (int x = tiles_to_check_list [0].x - 1 ; x <= tiles_to_check_list [0].x + 1; x++)
			{
				for (int z = tiles_to_check_list [0].z - 1 ; z <= tiles_to_check_list [0].z + 1; z++)
				{
					if (x >= 0 && x < grid.length_x && z >= 0 && z < grid.width_z && grid.GetValue(x, z, GameGrid.grid_parameter.simulated) == 0 &&
					( (x != tiles_to_check_list [0].x && z == tiles_to_check_list [0].z) || (x == tiles_to_check_list [0].x && z != tiles_to_check_list [0].z) ) )
					{	
						if (grid.GetValue(x, z, GameGrid.grid_parameter.object_type) == grid.EnumTranslator(GameGrid.object_type.core))
						{
							found_path = true;
							ending_tuple = (x, z);
						}
						else
						{
							switch (parameter)
							{
								case pathfinding_tile_parameter.empty:
								if (grid.GetValue (x, z, GameGrid.grid_parameter.object_type) == grid.EnumTranslator (GameGrid.object_type.empty) &&
									grid.GetValue (x, z, GameGrid.grid_parameter.terrain) != grid.EnumTranslator (GameGrid.terrain.water) &&
									grid.GetValue (x, z, GameGrid.grid_parameter.simulated) == 0)
								{
									tiles_to_check_list.Add ((x, z));
								}
								break;

								case pathfinding_tile_parameter.any_terrain:
								if (grid.GetValue (x, z, GameGrid.grid_parameter.object_type) != grid.EnumTranslator (GameGrid.object_type.tower) &&
								    grid.GetValue (x, z, GameGrid.grid_parameter.simulated) == 0)
								{
									tiles_to_check_list.Add ((x, z));
								}
								break;

								case pathfinding_tile_parameter.any_tile:
								if (grid.GetValue (x, z, GameGrid.grid_parameter.simulated) == 0)
								{
									tiles_to_check_list.Add ((x, z));
								}
								break;
							}
						}

						SetPreviousXZ (x, z, tiles_to_check_list [0].x, tiles_to_check_list [0].z, grid, true);
					}
				}
			}
			
			tiles_to_check_list.RemoveAt(0);
			if (tiles_to_check_list.Count == 0)
			{
				path_tuple_array = null;
				for (int x = 0 ; x < grid.length_x; x++)
				{
					for (int z = 0 ; z < grid.length_x; z++)
					{
							SetPreviousXZ (x, z, 0, 0, grid, false);
					}
				}
				return false;
			}
		}
		path_tuple_array = new (int x, int z) [1] {ending_tuple};
		int counter = 0;
		while (!(path_tuple_array [counter].x == position.x && path_tuple_array [counter].z == position.z))
		{
			path_tuple_array = path_tuple_array.Append((grid.GetValue(path_tuple_array [counter].x,
			path_tuple_array [counter].z, GameGrid.grid_parameter.prev_x),
			grid.GetValue(path_tuple_array [counter].x, path_tuple_array [counter].z, GameGrid.grid_parameter.prev_z))).ToArray();
			counter++;
		}
		return true;
	}

	private void SetPreviousXZ (int x, int z, int prev_x, int prev_z, GameGrid grid, bool simulated)
	{
		grid.SetValue (x, z, GameGrid.grid_parameter.prev_x, prev_x);
		grid.SetValue (x, z, GameGrid.grid_parameter.prev_z, prev_z);
		if (simulated == true)
		{
			grid.SetValue (x, z, GameGrid.grid_parameter.simulated, 1);
		}
		else
		{
			grid.SetValue (x, z, GameGrid.grid_parameter.simulated, 0);
		}
	}
}
