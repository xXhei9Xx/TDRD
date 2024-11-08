using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static GameGrid;

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
				grid_terrain_array_string += caller.GetGameGrid().GetValue (x, z, GameGrid.grid_parameter.terrain) + ":";
			}
		}
		GridSave grid_save = new GridSave ();
		grid_save.SetTerrainArray (grid_terrain_array_string);
		string save_json = JsonUtility.ToJson(grid_save);
		File.WriteAllText(Application.dataPath + "/Map Saves/" + save_name + ".json", save_json);
	}

    public void SaveTerrainLoad (GameHandler caller, string save_name)
	{
		string save_json = File.ReadAllText(Application.dataPath + "/Map Saves/" + save_name + ".json");
		GridSave grid_load = JsonUtility.FromJson<GridSave>(save_json);
		string [] temp = grid_load.GetGridTerrainArrayString().Split(":");
		int [,] loaded_grid_array = new int [(temp.Length - 2), 1];
		for (int i = 0; i < (temp.Length - 2); i++)
		{
			string [] temp_2 = temp[i].Split(",");
			caller.GetGameGrid().SetValue (int.Parse(temp_2[0]), int.Parse(temp_2[1]), 
			caller.GetGameGrid().EnumTranslator(grid_parameter.terrain), int.Parse(temp_2[2]));
		}
	}

	public class GridSave
	{
		public string grid_terrain_array_string;

		public void SetTerrainArray (string grid_terrain_array_string)
		{
			this.grid_terrain_array_string = grid_terrain_array_string;
		}

		public string GetGridTerrainArrayString ()
		{
			return grid_terrain_array_string;
		}
	}
}
