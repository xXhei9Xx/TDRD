using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemyPicker
{
	#region variable declarations

	private DamageChart damage_chart;
	private GameHandler caller;
	private int amount_of_enemies_to_spawn;
	private int amount_of_physical_enemies_to_spawn;
	private int amount_of_fire_enemies_to_spawn;
	private int amount_of_frost_enemies_to_spawn;
	private int amount_of_electric_enemies_to_spawn;
	private int amount_of_poison_enemies_to_spawn;
	private int amount_of_magic_enemies_to_spawn;
	private int amount_of_tank_enemies_to_spawn;
	private int amount_of_support_enemies_to_spawn;
	private int amount_of_grave_keeper_enemies_to_spawn;
	private int amount_of_evolver_enemies_to_spawn;
	private int amount_of_jumper_enemies_to_spawn;
	private int amount_of_disruptor_enemies_to_spawn;
	private int amount_of_picked_physical_enemies;
	private int amount_of_picked_fire_enemies;
	private int amount_of_picked_frost_enemies;
	private int amount_of_picked_electric_enemies;
	private int amount_of_picked_poison_enemies;
	private int amount_of_picked_magic_enemies;
	private int amount_of_picked_tank_enemies;
	private int amount_of_picked_support_enemies;
	private int amount_of_picked_grave_keeper_enemies;
	private int amount_of_picked_evolver_enemies;
	private int amount_of_picked_jumper_enemies;
	private int amount_of_picked_disruptor_enemies;

	public void SetDamageChart (DamageChart damage_chart)
	{
		this.damage_chart = damage_chart;
	}

	#endregion

	public EnemyPicker (GameHandler caller)
	{
		this.caller = caller;
	}

	public void AddNewEnemiesToSpawnList ()
	{
		if (caller.testing_options.manual_spawn_list == false)
		{
			List<Enemy.enemy_id> enemies_to_spawn_list = new List<Enemy.enemy_id>();
			List<Enemy.enemy_id> pickable_enemies_list = new List<Enemy.enemy_id>();
			#region adding enemy id to list
			if (amount_of_physical_enemies_to_spawn > 0)
			{
				if (amount_of_tank_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.shield_skeleton);
				}
				if (amount_of_support_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.goblin_construction_team);
				}
				if (amount_of_grave_keeper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.brood_mother);
				}
				if (amount_of_evolver_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.werewolf);
				}
				if (amount_of_jumper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.steam_walker);
				}
				if (amount_of_disruptor_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.giant);
				}
			}
			if (amount_of_fire_enemies_to_spawn > 0)
			{
				if (amount_of_tank_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.magma_elemental);
				}
				if (amount_of_support_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.alchemist);
				}
				if (amount_of_grave_keeper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.bio_arsonist);
				}
				if (amount_of_evolver_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.fire_elemental);
				}
				if (amount_of_jumper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.wyrmling);
				}
				if (amount_of_disruptor_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.corruptor);
				}
			}
			if (amount_of_frost_enemies_to_spawn > 0)
			{
				if (amount_of_tank_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.lich);
				}
				if (amount_of_support_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.lady_frost);
				}
				if (amount_of_grave_keeper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.ice_consumer);
				}
				if (amount_of_evolver_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.dead_grove);
				}
				if (amount_of_jumper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.snow_diver);
				}
				if (amount_of_disruptor_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.ice_thrall);
				}
			}
			if (amount_of_electric_enemies_to_spawn > 0)
			{
				if (amount_of_tank_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.absorber);
				}
				if (amount_of_support_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.static_charge);
				}
				if (amount_of_grave_keeper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.charge_collector);
				}
				if (amount_of_evolver_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.zap);
				}
				if (amount_of_jumper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.rocket_goblin);
				}
				if (amount_of_disruptor_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.overcharge);
				}
			}
			if (amount_of_poison_enemies_to_spawn > 0)
			{
				if (amount_of_tank_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.green_belcher);
				}
				if (amount_of_support_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.ooze);
				}
				if (amount_of_grave_keeper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.corpse_eater);
				}
				if (amount_of_evolver_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.mutant);
				}
				if (amount_of_jumper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.grappling_spider);
				}
				if (amount_of_disruptor_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.plague_bearer);
				}
			}
			if (amount_of_magic_enemies_to_spawn > 0)
			{
				if (amount_of_tank_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.shadow_fiend);
				}
				if (amount_of_support_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.shaman);
				}
				if (amount_of_grave_keeper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.necromancer);
				}
				if (amount_of_evolver_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.mana_addict);
				}
				if (amount_of_jumper_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.phase_shifter);
				}
				if (amount_of_disruptor_enemies_to_spawn > 0)
				{
					pickable_enemies_list.Add (Enemy.enemy_id.mind_twister);
				}
			}
			#endregion
			for (int i = 0; i < amount_of_enemies_to_spawn; i++)
			{
				Enemy.enemy_id picked_enemy = pickable_enemies_list [caller.RandomInt (0, pickable_enemies_list.Count - 1)];
				enemies_to_spawn_list.Add (picked_enemy);
				CheckAndRemoveEnemyTypeFromList (picked_enemy, pickable_enemies_list);
			}
			foreach (Enemy.enemy_id enemy_id in enemies_to_spawn_list)
			{
				caller.AddEnemyToSpawnList (enemy_id);
			}
		}
		else
		{
			foreach (Enemy.enemy_id enemy_id in caller.testing_options.manual_enemy_spawn_list)
			{
				caller.AddEnemyToSpawnList (enemy_id);
			}
		}
		damage_chart.DamageDoneReset ();
	}

	private void CheckAndRemoveEnemyTypeFromList (Enemy.enemy_id enemy_id, List<Enemy.enemy_id> pickable_enemies_list)
	{
		var enemy_type_physical_array = Enum.GetNames (typeof (Enemy.enemy_type_physical));
		var enemy_type_fire_array = Enum.GetNames (typeof (Enemy.enemy_type_fire));
		var enemy_type_frost_array = Enum.GetNames (typeof (Enemy.enemy_type_frost));
		var enemy_type_electric_array = Enum.GetNames (typeof (Enemy.enemy_type_electric));
		var enemy_type_poison_array = Enum.GetNames (typeof (Enemy.enemy_type_poison));
		var enemy_type_magic_array = Enum.GetNames (typeof (Enemy.enemy_type_magic));
		if (enemy_type_physical_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_physical_enemies ++;
			if(amount_of_picked_physical_enemies >= amount_of_physical_enemies_to_spawn)
				{
				foreach (string physical_type in enemy_type_physical_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), physical_type)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), physical_type));
					}
				}
			}
		}
		if (enemy_type_fire_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_fire_enemies ++;
			if(amount_of_picked_fire_enemies >= amount_of_fire_enemies_to_spawn)
				{
				foreach (string fire_type in enemy_type_fire_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), fire_type)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), fire_type));
					}
				}
			}
		}
		if (enemy_type_frost_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_frost_enemies ++;
			if(amount_of_picked_frost_enemies >= amount_of_frost_enemies_to_spawn)
				{
				foreach (string frost_type in enemy_type_frost_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), frost_type)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), frost_type));
					}
				}
			}
		}
		if (enemy_type_electric_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_electric_enemies ++;
			if(amount_of_picked_electric_enemies >= amount_of_electric_enemies_to_spawn)
				{
				foreach (string electric_type in enemy_type_electric_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), electric_type)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), electric_type));
					}
				}
			}
		}
		if (enemy_type_poison_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_poison_enemies ++;
			if(amount_of_picked_poison_enemies >= amount_of_poison_enemies_to_spawn)
				{
				foreach (string poison_type in enemy_type_poison_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), poison_type)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), poison_type));
					}
				}
			}
		}
		if (enemy_type_magic_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_magic_enemies ++;
			if(amount_of_picked_magic_enemies >= amount_of_magic_enemies_to_spawn)
				{
				foreach (string magic_type in enemy_type_magic_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), magic_type)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), magic_type));
					}
				}
			}
		}
		var enemy_class_tank_array = Enum.GetNames (typeof (Enemy.enemy_class_tank));
		var enemy_class_support_array = Enum.GetNames (typeof (Enemy.enemy_class_support));
		var enemy_class_grave_keeper_array = Enum.GetNames (typeof (Enemy.enemy_class_grave_keeper));
		var enemy_class_evolver_array = Enum.GetNames (typeof (Enemy.enemy_class_evolver));
		var enemy_class_jumper_array = Enum.GetNames (typeof (Enemy.enemy_class_jumper));
		var enemy_class_disruptor_array = Enum.GetNames (typeof (Enemy.enemy_class_disruptor));
		if (enemy_class_tank_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_tank_enemies ++;
			if(amount_of_picked_tank_enemies >= amount_of_tank_enemies_to_spawn)
				{
				foreach (string tank_class in enemy_class_tank_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), tank_class)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), tank_class));
					}
				}
			}
		}
		if (enemy_class_support_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_support_enemies ++;
			if(amount_of_picked_support_enemies >= amount_of_support_enemies_to_spawn)
				{
				foreach (string support_class in enemy_class_support_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), support_class)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), support_class));
					}
				}
			}
		}
		if (enemy_class_grave_keeper_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_grave_keeper_enemies ++;
			if(amount_of_picked_grave_keeper_enemies >= amount_of_grave_keeper_enemies_to_spawn)
				{
				foreach (string grave_keeper_class in enemy_class_evolver_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), grave_keeper_class)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), grave_keeper_class));
					}
				}
			}
		}
		if (enemy_class_evolver_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_evolver_enemies ++;
			if(amount_of_picked_evolver_enemies >= amount_of_evolver_enemies_to_spawn)
				{
				foreach (string evolver_class in enemy_class_evolver_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), evolver_class)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), evolver_class));
					}
				}
			}
		}
		if (enemy_class_jumper_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_jumper_enemies ++;
			if(amount_of_picked_jumper_enemies >= amount_of_jumper_enemies_to_spawn)
				{
				foreach (string jumper_class in enemy_class_jumper_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), jumper_class)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), jumper_class));
					}
				}
			}
		}
		if (enemy_class_disruptor_array.Contains (enemy_id.ToString ()))
		{
			amount_of_picked_disruptor_enemies ++;
			if(amount_of_picked_disruptor_enemies >= amount_of_disruptor_enemies_to_spawn)
				{
				foreach (string disruptor_class in enemy_class_disruptor_array)
				{
					if (pickable_enemies_list.Contains ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), disruptor_class)))
					{
						pickable_enemies_list.Remove ((Enemy.enemy_id) Enum.Parse (typeof(Enemy.enemy_id), disruptor_class));
					}
				}
			}
		}
	}

    public class DamageChart : MonoBehaviour
	{
		#region variable declarations

		private int total_damage_done = 0;
		private int physical_damage_done = 0;
		private int fire_damage_done = 0;
		private int frost_damage_done = 0;
		private int electric_damage_done = 0;
		private int poison_damage_done = 0;
		private int magic_damage_done = 0;
		private Transform physical_damage_bar_object;
		private Transform fire_damage_bar_object;
		private Transform frost_damage_bar_object;
		private Transform electric_damage_bar_object;
		private Transform poison_damage_bar_object;
		private Transform magic_damage_bar_object;
		private TextMeshProUGUI physical_damage_text;
		private TextMeshProUGUI fire_damage_text;
		private TextMeshProUGUI frost_damage_text;
		private TextMeshProUGUI electric_damage_text;
		private TextMeshProUGUI poison_damage_text;
		private TextMeshProUGUI magic_damage_text;

		public void SetDamageChart (EnemyPicker enemy_picker)
		{
			enemy_picker.SetDamageChart (this);
		}

		#endregion

		#region damage addition and reset

		public void DamageAddition (int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage)
		{
			total_damage_done += physical_damage + fire_damage + frost_damage + electric_damage + poison_damage + magic_damage;
			physical_damage_done += physical_damage;
			fire_damage_done += fire_damage;
			frost_damage_done += frost_damage;
			electric_damage_done += electric_damage;
			poison_damage_done += poison_damage;
			magic_damage_done += magic_damage;
			physical_damage_bar_object.localScale = new Vector3 (((float) physical_damage_done / total_damage_done), 1, 1);
			physical_damage_text.text = "Physical - " + ((int) Math.Round ((decimal) physical_damage_done * 100 / total_damage_done)) + "%";
			fire_damage_bar_object.localScale = new Vector3 (((float) fire_damage_done / total_damage_done), 1, 1);
			fire_damage_text.text = "Fire - " + ((int) Math.Round ((decimal) fire_damage_done * 100 / total_damage_done)) + "%";
			frost_damage_bar_object.localScale = new Vector3 (((float) frost_damage_done / total_damage_done), 1, 1);
			frost_damage_text.text = "Frost - " + ((int) Math.Round ((decimal) frost_damage_done * 100 / total_damage_done)) + "%";
			electric_damage_bar_object.localScale = new Vector3 (((float) electric_damage_done / total_damage_done), 1, 1);
			electric_damage_text.text = "Electric - " + ((int) Math.Round ((decimal) electric_damage_done * 100 / total_damage_done)) + "%";
			poison_damage_bar_object.localScale = new Vector3 (((float) poison_damage_done / total_damage_done), 1, 1);
			poison_damage_text.text = "Poison - " + ((int) Math.Round ((decimal) poison_damage_done * 100 / total_damage_done)) + "%";
			magic_damage_bar_object.localScale = new Vector3 (((float) magic_damage_done / total_damage_done), 1, 1);
			magic_damage_text.text = "Magic - " + ((int) Math.Round ((decimal) magic_damage_done * 100 / total_damage_done)) + "%";

		}

		public void DamageDoneReset ()
		{
			
			total_damage_done = 0;
			physical_damage_done = 0;
			fire_damage_done = 0;
			frost_damage_done = 0;
			electric_damage_done = 0;
			poison_damage_done = 0;
			magic_damage_done = 0;
			physical_damage_bar_object.localScale = new Vector3 (0, 1, 1);
			fire_damage_bar_object.localScale = new Vector3 (0, 1, 1);
			frost_damage_bar_object.localScale = new Vector3 (0, 1, 1);
			electric_damage_bar_object.localScale = new Vector3 (0, 1, 1);
			poison_damage_bar_object.localScale = new Vector3 (0, 1, 1);
			magic_damage_bar_object.localScale = new Vector3 (0, 1, 1);
			physical_damage_text.text = "Physical - 0%";
			fire_damage_text.text = "Fire - 0%";
			frost_damage_text.text = "Frost - 0%";
			electric_damage_text.text = "Electric - 0%";
			poison_damage_text.text = "Poison - 0%";
			magic_damage_text.text = "Magic - 0%";
		}

		#endregion

		private void Start()
		{
			physical_damage_bar_object = GameObject.Find ("Physical Damage Bar").transform;
			fire_damage_bar_object = GameObject.Find ("Fire Damage Bar").transform;
			frost_damage_bar_object = GameObject.Find("Frost Damage Bar").transform;
			electric_damage_bar_object = GameObject.Find("Electric Damage Bar").transform;
			poison_damage_bar_object = GameObject.Find("Poison Damage Bar").transform;
			magic_damage_bar_object = GameObject.Find("Magic Damage Bar").transform;
			physical_damage_text = GameObject.Find ("Physical Damage Text").GetComponent<TextMeshProUGUI>();
			fire_damage_text = GameObject.Find ("Fire Damage Text").GetComponent<TextMeshProUGUI>();
			frost_damage_text = GameObject.Find("Frost Damage Text").GetComponent<TextMeshProUGUI>();
			electric_damage_text = GameObject.Find("Electric Damage Text").GetComponent<TextMeshProUGUI>();
			poison_damage_text = GameObject.Find("Poison Damage Text").GetComponent<TextMeshProUGUI>();
			magic_damage_text = GameObject.Find("Magic Damage Text").GetComponent<TextMeshProUGUI>();
		}

		public (int total, int physical, int fire, int frost, int electric, int poison, int magic) GetDamageDone()
		{
			return (total_damage_done, physical_damage_done, fire_damage_done, frost_damage_done, electric_damage_done, poison_damage_done, magic_damage_done);
		}
	}
}
