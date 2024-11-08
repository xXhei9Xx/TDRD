using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
		
		};
		GameObject.Find ("Advanced Window Button").GetComponent<Button_UI>().ClickFunc = () => {
		
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
		}
	}

	public void ConstructTowerInspector (Tower.tower_id id,
	(int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage, int range, float cooldown) base_tower_stats_tuple,
	(int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage, int range, float cooldown) base_tower_base_stats_tuple)
	{
		SetInspectorWindowVisibility (true, inspector_window_tab.general);
		name_text.text = id.ToString ();
		int counter = 0;
		stat_block_physical_damage.transform.localPosition = new Vector3 (-330, 380 - (30 * counter));
		stat_block_physical_damage.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_tower_base_stats_tuple.physical_damage.ToString();
		stat_block_physical_damage.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = base_tower_stats_tuple.physical_damage.ToString();
		counter++;
		stat_block_fire_damage.transform.localPosition = new Vector3 (-330, 380 - (30 * counter));
		stat_block_fire_damage.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_tower_base_stats_tuple.fire_damage.ToString();
		stat_block_fire_damage.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = base_tower_stats_tuple.fire_damage.ToString();
		counter++;
		stat_block_frost_damage.transform.localPosition = new Vector3 (-330, 380 - (30 * counter));
		stat_block_frost_damage.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_tower_base_stats_tuple.frost_damage.ToString();
		stat_block_frost_damage.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = base_tower_stats_tuple.frost_damage.ToString();
		counter++;
		stat_block_electric_damage.transform.localPosition = new Vector3 (-330, 380 - (30 * counter));
		stat_block_electric_damage.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_tower_base_stats_tuple.electric_damage.ToString();
		stat_block_electric_damage.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = base_tower_stats_tuple.electric_damage.ToString();
		counter++;
		stat_block_poison_damage.transform.localPosition = new Vector3 (-330, 380 - (30 * counter));
		stat_block_poison_damage.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_tower_base_stats_tuple.poison_damage.ToString();
		stat_block_poison_damage.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = base_tower_stats_tuple.poison_damage.ToString();
		counter++;
		stat_block_magic_damage.transform.localPosition = new Vector3 (-330, 380 - (30 * counter));
		stat_block_magic_damage.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_tower_base_stats_tuple.magic_damage.ToString();
		stat_block_magic_damage.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = base_tower_stats_tuple.magic_damage.ToString();
		counter++;
		stat_block_range.transform.localPosition = new Vector3 (-330, 380 - (30 * counter));
		stat_block_range.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_tower_base_stats_tuple.range.ToString();
		stat_block_range.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = base_tower_stats_tuple.range.ToString();
		counter++;
		stat_block_cooldown.transform.localPosition = new Vector3 (-330, 380 - (30 * counter));
		stat_block_cooldown.transform.GetChild (1).GetComponent<TextMeshProUGUI> ().text = base_tower_base_stats_tuple.cooldown.ToString();
		stat_block_cooldown.transform.GetChild (2).GetComponent<TextMeshProUGUI> ().text = base_tower_stats_tuple.cooldown.ToString();
		counter++;
	}

	public void ConstructEnemyInspector (Enemy.enemy_id id)
	{

	}
}
