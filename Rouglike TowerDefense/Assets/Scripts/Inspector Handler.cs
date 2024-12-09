using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class InspectorHandler : MonoBehaviour
{
	#region variable declarations

	#region enum declarations

	public enum inspector_window_tab
	{
		general,
		advanced,
		both
	}

	#endregion
	#region tower stats

	private GameObject stat_block_physical_damage;
	private GameObject stat_block_fire_damage;
	private GameObject stat_block_frost_damage;
	private GameObject stat_block_electric_damage;
	private GameObject stat_block_poison_damage;
	private GameObject stat_block_magic_damage;
	private GameObject stat_block_attack_speed;
	private GameObject stat_block_projectile_speed;
	private GameObject stat_block_range;

	#endregion
	#region enemy stats

	private GameObject stat_block_physical_resistance;
	private GameObject stat_block_fire_resistance;
	private GameObject stat_block_frost_resistance;
	private GameObject stat_block_electric_resistance;
	private GameObject stat_block_poison_resistance;
	private GameObject stat_block_magic_resistance;
	private GameObject stat_block_cooldown;
	private GameObject stat_block_movement_speed;
	private GameObject stat_block_max_mana;
	private GameObject stat_block_mana_cost;

	#endregion

	private TextMeshProUGUI name_text;
	private TextMeshProUGUI description_text;

	private GameHandler caller;

	int counter = 0;
	int stat_block_height_shift = 80;

	Vector3 first_stat_position = new Vector3 (-345, 310, 0);

	#endregion

	public void Start()
	{
		SetInspectorWindowVisibility (true);
		#region refenerence: caller

		caller = GameObject.Find ("GameHandler").GetComponent<GameHandler>();

		#endregion
		#region tower stats inicialization

		stat_block_physical_damage = GameObject.Find ("Stat Block Physical Damage");
		stat_block_fire_damage = GameObject.Find ("Stat Block Fire Damage");
		stat_block_frost_damage = GameObject.Find ("Stat Block Frost Damage");
		stat_block_electric_damage = GameObject.Find ("Stat Block Electric Damage");
		stat_block_poison_damage = GameObject.Find ("Stat Block Poison Damage");
		stat_block_magic_damage = GameObject.Find ("Stat Block Magic Damage");
		stat_block_attack_speed = GameObject.Find ("Stat Block Attack Speed");
		stat_block_projectile_speed = GameObject.Find ("Stat Block Projectile Speed");
		stat_block_range = GameObject.Find ("Stat Block Range");

		#endregion
		#region enemy stats inicialization

		stat_block_physical_resistance = GameObject.Find ("Stat Block Physical Resistance");
		stat_block_fire_resistance = GameObject.Find ("Stat Block Fire Resistance");
		stat_block_frost_resistance = GameObject.Find ("Stat Block Frost Resistance");
		stat_block_electric_resistance = GameObject.Find ("Stat Block Electric Resistance");
		stat_block_poison_resistance = GameObject.Find ("Stat Block Poison Resistance");
		stat_block_magic_resistance = GameObject.Find ("Stat Block Magic Resistance");
		stat_block_cooldown = GameObject.Find ("Stat Block Cooldown");
		stat_block_movement_speed = GameObject.Find ("Stat Block Movement Speed");
		stat_block_max_mana = GameObject.Find ("Stat Block Max Mana");
		stat_block_mana_cost = GameObject.Find ("Stat Block Mana Cost");

		#endregion
		#region name, description

		name_text = GameObject.Find ("Window Name Text").GetComponent<TextMeshProUGUI> ();

		#endregion
		
		#region buttons

		GameObject.Find ("Close Window Button").GetComponent<Button_UI>().ClickFunc = () => {
		caller.SetInspectorWindow (false);
		SetInspectorWindowVisibility (false);
		};
		GameObject.Find ("General Window Button").GetComponent<Button_UI>().ClickFunc = () => {
		SetInspectorWindowVisibility (true, inspector_window_tab.general);
		};
		GameObject.Find ("Advanced Window Button").GetComponent<Button_UI>().ClickFunc = () => {
		SetInspectorWindowVisibility (true, inspector_window_tab.advanced);
		};

		#endregion
		SetInspectorWindowVisibility (false);
	}

	public void SetInspectorWindowVisibility (bool state, inspector_window_tab open_tab = inspector_window_tab.both)
	{
		int amount_of_children = transform.childCount;
		for (int i = 0; i < amount_of_children; i++)
		{
			if (open_tab == inspector_window_tab.both ||
			((open_tab == inspector_window_tab.general && transform.GetChild (i).gameObject.name != "Advanced Window") || 
			(open_tab == inspector_window_tab.advanced && transform.GetChild (i).gameObject.name != "General Window")))
			{
				transform.GetChild (i).gameObject.SetActive (state);
			}
			if ((open_tab == inspector_window_tab.general && transform.GetChild (i).gameObject.name == "Advanced Window") || 
			(open_tab == inspector_window_tab.advanced && transform.GetChild (i).gameObject.name == "General Window"))
			{
				transform.GetChild (i).gameObject.SetActive (!state);
			}
		}
	}

	public void ConstructTowerInspector (Tower.tower_id id,
	(int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage, int range, float cooldown) base_tower_stats_tuple,
	(int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage, int range, float cooldown) base_tower_base_stats_tuple)
	{
		SetInspectorWindowVisibility (true, inspector_window_tab.general);
		name_text.text = id.ToString ();
		StatLineConstructor (stat_block_physical_damage, base_tower_base_stats_tuple.physical_damage, base_tower_stats_tuple.physical_damage, true);
		StatLineConstructor (stat_block_fire_damage, base_tower_base_stats_tuple.fire_damage, base_tower_stats_tuple.fire_damage);
		StatLineConstructor (stat_block_frost_damage, base_tower_base_stats_tuple.frost_damage, base_tower_stats_tuple.frost_damage);
		StatLineConstructor (stat_block_electric_damage, base_tower_base_stats_tuple.electric_damage, base_tower_stats_tuple.electric_damage);
		StatLineConstructor (stat_block_poison_damage, base_tower_base_stats_tuple.poison_damage, base_tower_stats_tuple.poison_damage);
		StatLineConstructor (stat_block_magic_damage, base_tower_base_stats_tuple.magic_damage, base_tower_stats_tuple.magic_damage);
		StatLineConstructor (stat_block_range, base_tower_base_stats_tuple.range, base_tower_stats_tuple.range);
		StatLineConstructor (stat_block_cooldown, base_tower_base_stats_tuple.cooldown, base_tower_stats_tuple.cooldown);
		switch (id)
		{
			case Tower.tower_id.test:

			break;

			case Tower.tower_id.mana:

			break;

			case Tower.tower_id.blizzard:

			break;

			case Tower.tower_id.oil:

			break;

			case Tower.tower_id.lightning:

			break;

			case Tower.tower_id.flamethrower:

			break;
		}
	}

	public void ConstructEnemyInspector (Enemy.enemy_id id)
	{
	}

	private void StatLineConstructor (GameObject stat_block_object, int base_stat, int modified_stat, bool reset_counter = false)
	{
		if (reset_counter == true)
		{
			counter = 0;
		}
		stat_block_object.transform.localPosition = first_stat_position - new Vector3 (0, (stat_block_height_shift * counter));
		stat_block_object.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_stat.ToString();
		stat_block_object.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = modified_stat.ToString();
		counter++;
	}

	private void StatLineConstructor (GameObject stat_block_object, float base_stat, float modified_stat, bool reset_counter = false)
	{
		if (reset_counter == true)
		{
			counter = 0;
		}
		stat_block_object.transform.localPosition = first_stat_position - new Vector3 (0, (stat_block_height_shift * counter));
		stat_block_object.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_stat.ToString();
		stat_block_object.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = modified_stat.ToString();
		counter++;
	}
}
