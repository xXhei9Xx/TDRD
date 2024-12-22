using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static GameGrid;
using UnityEngine.UIElements;

public class SaveHandler
{
	public void SaveTerrainCreate (GameHandler caller, string save_name)
	{
		string grid_terrain_array_string = "";
		for (int x = 0; x < caller.GetGameGrid().length_x; x++)
		{
			for (int z = 0; z < caller.GetGameGrid().width_z; z++)
			{
				grid_terrain_array_string += x.ToString() + "," + z.ToString() + ",";
				grid_terrain_array_string += caller.GetGameGrid().GetValue (x, z, grid_parameter.terrain) + ":";
			}
		}
		string grid_spawn_zone_array_string = "";
		for (int x = 0; x < caller.GetGameGrid().length_x; x++)
		{
			for (int z = 0; z < caller.GetGameGrid().width_z; z++)
			{
				grid_spawn_zone_array_string += x.ToString() + "," + z.ToString() + ",";
				grid_spawn_zone_array_string += caller.GetGameGrid().GetValue (x, z, grid_parameter.spawn_zone) + ":";
			}
		}
		GridSave grid_save = new GridSave ();
		grid_save.SetGridVariables (caller.GetGameGrid().length_x, caller.GetGameGrid().width_z, grid_terrain_array_string, grid_spawn_zone_array_string);
		string save_json = JsonUtility.ToJson(grid_save);
		File.WriteAllText(Application.dataPath + "/Map Saves/" + save_name + ".json", save_json);
	}

	public void SaveTerrainCreate (MapCreator caller, string save_name)
	{
		string grid_terrain_array_string = "";
		for (int x = 0; x < caller.GetGameGrid().length_x; x++)
		{
			for (int z = 0; z < caller.GetGameGrid().width_z; z++)
			{
				grid_terrain_array_string += x.ToString() + "," + z.ToString() + ",";
				grid_terrain_array_string += caller.GetGameGrid().GetValue (x, z, grid_parameter.terrain) + ":";
			}
		}
		string grid_spawn_zone_array_string = "";
		for (int x = 0; x < caller.GetGameGrid().length_x; x++)
		{
			for (int z = 0; z < caller.GetGameGrid().width_z; z++)
			{
				grid_spawn_zone_array_string += x.ToString() + "," + z.ToString() + ",";
				grid_spawn_zone_array_string += caller.GetGameGrid().GetValue (x, z, grid_parameter.spawn_zone) + ":";
			}
		}
		GridSave grid_save = new GridSave ();
		grid_save.SetGridVariables (caller.GetGameGrid().length_x, caller.GetGameGrid().width_z, grid_terrain_array_string, grid_spawn_zone_array_string);
		string save_json = JsonUtility.ToJson(grid_save);
		File.WriteAllText(Application.dataPath + "/Map Saves/" + save_name + ".json", save_json);
	}

    public void SaveTerrainLoad (GameHandler caller, string save_name)
	{
		string save_json = File.ReadAllText(Application.dataPath + "/Map Saves/" + save_name + ".json");
		GridSave grid_load = JsonUtility.FromJson<GridSave>(save_json);
		string [] temp = grid_load.GetGridTerrainArrayString().Split(":");
		int [,] loaded_terrain_array = new int [(temp.Length - 1), 1];
		for (int i = 0; i < (temp.Length - 1); i++)
		{
			string [] temp_2 = temp[i].Split(",");
			caller.GetGameGrid().SetValue (int.Parse(temp_2[0]), int.Parse(temp_2[1]), 
			caller.GetGameGrid().EnumTranslator(grid_parameter.terrain), int.Parse(temp_2[2]));
		}
		temp = grid_load.GetGridSpawnZoneArrayString().Split(":");
		int [,] loaded_spawn_zone_array = new int [(temp.Length - 1), 1];
		for (int i = 0; i < (temp.Length - 1); i++)
		{
			string [] temp_2 = temp[i].Split(",");
			caller.GetGameGrid().SetValue (int.Parse(temp_2[0]), int.Parse(temp_2[1]), 
			caller.GetGameGrid().EnumTranslator(grid_parameter.spawn_zone), int.Parse(temp_2[2]));
		}
	}

	public void SaveTerrainLoad (MapCreator caller, string save_name)
	{
		string save_json = File.ReadAllText(Application.dataPath + "/Map Saves/" + save_name + ".json");
		GridSave grid_load = JsonUtility.FromJson<GridSave>(save_json);
		string [] temp = grid_load.GetGridTerrainArrayString().Split(":");
		int [,] loaded_terrain_array = new int [(temp.Length - 1), 1];
		for (int i = 0; i < (temp.Length - 1); i++)
		{
			string [] temp_2 = temp[i].Split(",");
			caller.GetGameGrid().SetValue (int.Parse(temp_2[0]), int.Parse(temp_2[1]), 
			caller.GetGameGrid().EnumTranslator(grid_parameter.terrain), int.Parse(temp_2[2]));
		}
		temp = grid_load.GetGridSpawnZoneArrayString().Split(":");
		int [,] loaded_spawn_zone_array = new int [(temp.Length - 1), 1];
		for (int i = 0; i < (temp.Length - 1); i++)
		{
			string [] temp_2 = temp[i].Split(",");
			caller.GetGameGrid().SetValue (int.Parse(temp_2[0]), int.Parse(temp_2[1]), 
			caller.GetGameGrid().EnumTranslator(grid_parameter.spawn_zone), int.Parse(temp_2[2]));
		}
	}

	public (int length_x, int width_z) GetGridDimensions (GameHandler caller, string save_name)
	{
		string save_json = File.ReadAllText(Application.dataPath + "/Map Saves/" + save_name + ".json");
		GridSave grid_load = JsonUtility.FromJson<GridSave>(save_json);
		return grid_load.GetGridDimensions ();
	}

	public (int length_x, int width_z) GetGridDimensions (MapCreator caller, string save_name)
	{
		string save_json = File.ReadAllText(Application.dataPath + "/Map Saves/" + save_name + ".json");
		GridSave grid_load = JsonUtility.FromJson<GridSave>(save_json);
		return grid_load.GetGridDimensions ();
	}

	public class GridSave
	{
		public int length_x, width_z;
		public string grid_terrain_array_string;
		public string grid_spawn_zone_array_string;

		public void SetGridVariables (int length_x, int width_z, string grid_terrain_array_string, string grid_spawn_zone_array_string)
		{
			this.length_x = length_x;
			this.width_z = width_z;
			this.grid_terrain_array_string = grid_terrain_array_string;
			this.grid_spawn_zone_array_string = grid_spawn_zone_array_string;
		}

		public string GetGridTerrainArrayString ()
		{
			return grid_terrain_array_string;
		}

		public string GetGridSpawnZoneArrayString ()
		{
			return grid_spawn_zone_array_string;
		}

		public (int length_x, int width_z) GetGridDimensions ()
		{
			return (length_x, width_z);
		}
	}
}
