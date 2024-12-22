using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using static Enemy;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using static GameGrid;
using Unity.VisualScripting;
using static PathFinding;

public class Enemy
{
	#region enum declarations

	#region enemy id, minion id

	public enum enemy_id
	{
		none,
		shield_skeleton,
		goblin_construction_team,
		brood_mother,
		werewolf,
		steam_walker,
		giant,
		magma_elemental,
		alchemist,
		bio_arsonist,
		fire_elemental,
		wyrmling,
		corruptor,
		lich,
		lady_frost,
		ice_consumer,
		dead_grove,
		snow_diver,
		ice_thrall,
		absorber,
		static_charge,
		charge_collector,
		zap,
		rocket_goblin,
		overcharge,
		green_belcher,
		ooze,
		corpse_eater,
		mutant,
		grappling_spider,
		plague_bearer,
		shadow_fiend,
		shaman,
		necromancer,
		mana_addict,
		phase_shifter,
		mind_twister
	}

	public static GameObject GetEnemyTemplate (enemy_id type)
	{
		switch (type)
		{
			case enemy_id.shield_skeleton: return GameObject.Find ("Shield Skeleton Template");

			case enemy_id.goblin_construction_team: return GameObject.Find ("Goblin Construction Team Template");

			case enemy_id.brood_mother: return GameObject.Find ("Brood Mother Template");

			case enemy_id.werewolf: return GameObject.Find ("Werewolf Template");

			case enemy_id.steam_walker: return GameObject.Find ("Steam Walker Template");

			case enemy_id.giant: return GameObject.Find ("Giant Template");

			case enemy_id.magma_elemental: return GameObject.Find ("Magma Elemental Template");

			case enemy_id.alchemist: return GameObject.Find ("Alchemist Template");

			case enemy_id.bio_arsonist: return GameObject.Find ("Bio Arsonist Template");

			case enemy_id.fire_elemental: return GameObject.Find ("Fire Elemental Template");

			case enemy_id.wyrmling: return GameObject.Find ("Wyrmling Template");

			case enemy_id.corruptor: return GameObject.Find ("Corruptor Template");

			case enemy_id.lich: return GameObject.Find ("Lich Template");

			case enemy_id.lady_frost: return GameObject.Find ("Lady Frost Template");

			case enemy_id.ice_consumer: return GameObject.Find ("ice Consumer Template");

			case enemy_id.dead_grove: return GameObject.Find ("dead Grove Template");

			case enemy_id.snow_diver: return GameObject.Find ("Snow Diver Template");

			case enemy_id.ice_thrall: return GameObject.Find ("Ice Thrall Template");

			case enemy_id.absorber: return GameObject.Find ("Absorber Template");

			case enemy_id.static_charge: return GameObject.Find ("Static Charge Template");

			case enemy_id.charge_collector: return GameObject.Find ("Charge Collector Template");

			case enemy_id.zap: return GameObject.Find ("Zap Template");

			case enemy_id.rocket_goblin: return GameObject.Find ("Rocket Goblin Template");

			case enemy_id.overcharge: return GameObject.Find ("Overcharge Template");

			case enemy_id.green_belcher: return GameObject.Find ("Green Belcher Template");

			case enemy_id.ooze: return GameObject.Find ("Ooze Template");

			case enemy_id.corpse_eater: return GameObject.Find ("Corpse Eater Template");

			case enemy_id.mutant: return GameObject.Find ("Mutant Template");

			case enemy_id.grappling_spider: return GameObject.Find ("Grappling Spider Template");

			case enemy_id.plague_bearer: return GameObject.Find ("Plague Bearer Template");

			case enemy_id.shadow_fiend: return GameObject.Find ("Shadow Fiend Template");

			case enemy_id.shaman: return GameObject.Find ("Shaman Template");

			case enemy_id.necromancer: return GameObject.Find ("Necromancer Template");

			case enemy_id.mana_addict: return GameObject.Find ("Mana AddictTemplate");

			case enemy_id.phase_shifter: return GameObject.Find ("Phase Shifter Template");

			case enemy_id.mind_twister: return GameObject.Find ("Mind Twister Template");

			default: return null;
		}
	}

	public enum minion_id
	{
		spider,
		ember,
		treant,
		whisp,
		zombie,
		skeleton
	}

	public static GameObject GetEnemyTemplate (minion_id type)
	{
		switch (type)
		{
			case minion_id.spider: return GameObject.Find ("Spider Template");

			case minion_id.ember: return GameObject.Find ("Ember Template");

			case minion_id.treant: return GameObject.Find ("Treant Template");

			case minion_id.whisp: return GameObject.Find ("Whisp Template");

			case minion_id.zombie: return GameObject.Find ("Zombie Template");

			case minion_id.skeleton: return GameObject.Find ("Skeleton Template");

			default: return null;
		}
	}

	#endregion
	#region enemy types
	public enum enemy_type
	{
		physical,
		fire,
		frost,
		electric,
		poison,
		magic
	}

	public enum enemy_type_physical
	{
		shield_skeleton,
		goblin_construction_team,
		brood_mother,
		werewolf,
		steam_walker,
		giant
	}

	public enum enemy_type_fire
	{
		magma_elemental,
		alchemist,
		bio_arsonist,
		fire_elemental,
		wyrmling,
		corruptor
	}

	public enum enemy_type_frost
	{
		lich,
		lady_frost,
		ice_consumer,
		dead_grove,
		snow_diver,
		ice_thrall
	}

	public enum enemy_type_electric
	{
		absorber,
		static_charge,
		charge_collector,
		zap,
		rocket_goblin,
		overcharge
	}

	public enum enemy_type_poison
	{
		green_belcher,
		ooze,
		corpse_eater,
		mutant,
		grappling_spider,
		plague_bearer
	}

	public enum enemy_type_magic
	{
		shadow_fiend,
		shaman,
		necromancer,
		mana_addict,
		phase_shifter,
		mind_twister
	}
	#endregion
	#region enemy calsses
	public enum enemy_class
	{
		tank,
		support,
		grave_keeper,
		evolver,
		jumper,
		disruptor
	}

	public enum enemy_class_tank
	{
		shield_skeleton,
		magma_elemental,
		lich,
		absorber,
		green_belcher,
		shadow_fiend
	}

	public enum enemy_class_support
	{
		goblin_construction_team,
		alchemist,
		lady_frost,
		static_charge,
		ooze,
		shaman
	}

	public enum enemy_class_grave_keeper
	{
		brood_mother,
		bio_arsonist,
		ice_consumer,
		charge_collector,
		corpse_eater,
		necromancer
	}

	public enum enemy_class_evolver
	{
		werewolf,
		fire_elemental,
		dead_grove,
		zap,
		mutant,
		mana_addict
	}

	public enum enemy_class_jumper
	{
		steam_walker,
		wyrmling,
		snow_diver,
		rocket_goblin,
		grappling_spider,
		phase_shifter
	}

	public enum enemy_class_disruptor
	{
		giant,
		corruptor,
		ice_thrall,
		over_charge,
		plague_bearer,
		mind_twister
	}
	#endregion

	#endregion

	//spawner constructor
	public Enemy (Vector3 position, GameHandler caller)
	{
		GameObject spawner = new GameObject("Spawner " + caller.GetSpawnerCounter ());
		spawner.transform.parent = GameObject.Find ("Spawner Initialized").transform;
		caller.AddSpawnerToSpawnerList (spawner);
		var xz = caller.GetGameGrid().GetXZ(position);
		caller.GetGameGrid().SetValue (xz.x, xz.z, grid_parameter.object_type, object_type.spawner);
		spawner.AddComponent<Spawner>().SetVariables (position, caller);
	}

	//enemy constructor
	public Enemy (enemy_id type, Vector3 position, GameHandler caller, GameObject enemy)
	{
		enemy.name = (type.ToString());
		enemy.transform.parent = GameObject.Find ("Enemy Initialized").transform;
		var xz = caller.GetGameGrid().GetXZ(position);
		caller.GetGameGrid().SetValue (xz.x, xz.z, GameGrid.grid_parameter.enemy, GameGrid.enemy.occupied);
		enemy.AddComponent<BaseEnemy>().SetVariables (enemy, caller, position, type);
		switch (type)
		{
			case enemy_id.shield_skeleton:
			enemy.AddComponent<ShieldSkeleton>().SetVariables (caller);
			break;

			case enemy_id.goblin_construction_team:
			enemy.AddComponent <GoblinConstructionTeam>().SetVariables (caller);
			break;

			case enemy_id.brood_mother:
			enemy.AddComponent <BroodMother>().SetVariables (caller);
			break;

			case enemy_id.werewolf:
			enemy.AddComponent <Werewolf>().SetVariables (caller);
			break;

			case enemy_id.steam_walker:
			enemy.AddComponent <SteamWalker>().SetVariables (caller);
			break;

			case enemy_id.giant:
			enemy.AddComponent <Giant>().SetVariables (caller);
			break;

			case enemy_id.magma_elemental:
			enemy.AddComponent <MagmaElemental>().SetVariables (caller);
			break;

			case enemy_id.alchemist:
			enemy.AddComponent<Alchemist>().SetVariables(caller);
			break;

			case enemy_id.bio_arsonist:
			enemy.AddComponent<BioArsonist>().SetVariables(caller);
			break;

			case enemy_id.fire_elemental:
			enemy.AddComponent<FireElemental>().SetVariables(caller);
			break;

			case enemy_id.wyrmling:
			enemy.AddComponent<Wyrmling>().SetVariables(caller);
			break;

			case enemy_id.corruptor:
			enemy.AddComponent<Corruptor>().SetVariables(caller);
			break;

			case enemy_id.lich:
			enemy.AddComponent<Lich>().SetVariables(caller);
			break;

			case enemy_id.lady_frost:
			enemy.AddComponent<LadyFrost>().SetVariables(caller);
			break;

			case enemy_id.ice_consumer:
			enemy.AddComponent<IceConsumer>().SetVariables(caller);
			break;

			case enemy_id.dead_grove:
			enemy.AddComponent<DeadGrove>().SetVariables(caller);
			break;

			case enemy_id.snow_diver:
			enemy.AddComponent<SnowDiver>().SetVariables(caller);
			break;

			case enemy_id.ice_thrall:
			enemy.AddComponent<IceThrall>().SetVariables(caller);
			break;

			case enemy_id.absorber:
			enemy.AddComponent<Absorber>().SetVariables(caller);
			break;

			case enemy_id.static_charge:
			enemy.AddComponent<StaticCharge>().SetVariables(caller);
			break;

			case enemy_id.charge_collector:
			enemy.AddComponent<ChargeCollector>().SetVariables(caller);
			break;

			case enemy_id.zap:
			enemy.AddComponent<Zap>().SetVariables(caller);
			break;

			case enemy_id.rocket_goblin:
			enemy.AddComponent<RocketGoblin>().SetVariables(caller);
			break;

			case enemy_id.overcharge:
			enemy.AddComponent<OverCharge>().SetVariables(caller);
			break;

			case enemy_id.green_belcher:
			enemy.AddComponent<GreenBelcher>().SetVariables (caller);
			break;

			case enemy_id.ooze:
			enemy.AddComponent<Ooze>().SetVariables(caller);
			break;

			case enemy_id.corpse_eater:
			enemy.AddComponent<CorpseEater>().SetVariables(caller);
			break;

			case enemy_id.mutant:
			enemy.AddComponent<Mutant>().SetVariables(caller);
			break;

			case enemy_id.grappling_spider:
			enemy.AddComponent<GrapplingSpider>().SetVariables(caller);
			break;

			case enemy_id.plague_bearer:
			enemy.AddComponent <PlagueBearer>();
			break;

			case enemy_id.shadow_fiend:
			enemy.AddComponent<Absorber>().SetVariables(caller);
			break;

			case enemy_id.shaman:
			enemy.AddComponent<Shaman>().SetVariables(caller);
			break;

			case enemy_id.necromancer:
			enemy.AddComponent<Necromancer>().SetVariables(caller);
			break;

			case enemy_id.mana_addict:
			enemy.AddComponent<ManaAddict>().SetVariables(caller);
			break;

			case enemy_id.phase_shifter:
			enemy.AddComponent<PhaseShifter>().SetVariables(caller);
			break;

			case enemy_id.mind_twister:
			enemy.AddComponent<MindTwister>().SetVariables(caller);
			break;
		}
	}
	//minion constructor
	public Enemy (minion_id type, Vector3 position, GameHandler caller, GameObject enemy)
	{
		enemy.name = (type.ToString());
		enemy.transform.parent = GameObject.Find ("Enemy Initialized").transform;
		var xz = caller.GetGameGrid().GetXZ(position);
		caller.GetGameGrid().SetValue (xz.x, xz.z, GameGrid.grid_parameter.enemy, GameGrid.enemy.occupied);
		enemy.AddComponent<BaseEnemy>().SetVariables (enemy, caller, position, type);
		switch (type)
		{
			case minion_id.spider:
			
			break;

			case minion_id.ember:

			break;

			case minion_id.treant:
			
			break;

			case minion_id.whisp:

			break;

			case minion_id.zombie:
			
			break;

			case minion_id.skeleton:

			break;
		}
	}

	public class Spawner : MonoBehaviour
	{
		#region variable declarations

		[SerializeField] private bool draw_path = false;
		private float cooldown;
		private Vector3 position;
		private GameHandler caller;
		private	GameObject path_timer;
		private GameObject spawner_template, this_spawner, enemy_object;
		public int life_time = 0;

		public void SetVariables (Vector3 position, GameHandler caller)
		{
			cooldown = caller.testing_options.spawner_cooldown;
			this.caller = caller;
			this.position = position;
		}

		#endregion
		
		private void Start()
		{
			spawner_template = GameObject.Find("Spawner");
			this_spawner = Instantiate(spawner_template);
			this_spawner.name = "Spawner Object";
			this_spawner.transform.position = position;
			this_spawner.transform.parent = transform;
			DrawPathLines();
		}

		public void SpawnEnemy (Spawner parent)
		{
			bool repeat = true;
			if ((caller.GetGameGrid().GetValue (caller.GetGameGrid().GetXZ (position).x, caller.GetGameGrid().GetXZ (position).z,
			grid_parameter.enemy) == caller.GetGameGrid().EnumTranslator (enemy.empty)))
			{
				enemy_id type = caller.GetEnemyTypeToSpawn();
				if (type != enemy_id.none)
				{
					enemy_object = Instantiate (GetEnemyTemplate (type));
					caller.AddEnemyToEnemyList (enemy_object);
					Enemy enemy = new Enemy (type, position, caller, enemy_object);
				}
				else
				{
					repeat = false;
				}
			}
			if (repeat == true)
			{
				SpawnEnemyTimer (parent);
			}
		}

		//timer allowing spawner to spawn next enemy
		public void SpawnEnemyTimer (Spawner parent)
		{
			GameHandler.Timer.Create (() => {
			if (caller.CheckIfWaveActive () == true)
			{
				parent.SpawnEnemy (parent);
			}
			}, cooldown, "spawning cooldown", gameObject);
		}

		//continusly draws the path from the spawner to the core
		public void DrawPathLines ()
		{
			path_timer = GameHandler.Timer.Create(() =>
			{
				(int x, int z) current_position = caller.GetGameGrid().GetXZ (position);
				(int x, int z) next_position = (caller.GetGameGrid().GetValue (current_position, grid_parameter.prev_x_empty),
				caller.GetGameGrid().GetValue (current_position, grid_parameter.prev_z_empty));
				if (caller.GetPathfinding().CheckPath (current_position, caller.GetGameGrid(), pathfinding_tile_parameter.empty, out int path_length))
				{
					while (path_length != 0)
					{
						Debug.DrawLine(caller.GetGameGrid().GetWorldTileCenter (current_position, 0.1f),
						caller.GetGameGrid().GetWorldTileCenter (next_position, 0.1f), Color.red, 0.3f);
						if (caller.GetGameGrid().GetValue (next_position, grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.core))
						{
							break;
						}
						current_position = next_position;
						next_position = (caller.GetGameGrid().GetValue (next_position, grid_parameter.prev_x_empty),
						caller.GetGameGrid().GetValue (next_position, grid_parameter.prev_z_empty));
					}
				}
				DrawPathLines();
			}, 0.25f, "draw path", gameObject);
		}
	}

	public class BaseEnemy : MonoBehaviour
	{
		#region variable declarations

		private GameHandler caller;
		private GameObject this_test_enemy;
		private GameObject health_bar;
		private GameObject mana_bar;
		private GameObject casting_bar;
		private GameObject enemy_bar_canvas;
		private Action DeathAction = null;
		private Action DestinationReachedAction = null;
		private Action OnHitAction = null;
		private Action<(int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage)> BlockingAction = null;
		private List<Collider> this_enemy_collider_list = new List<Collider>();
		private (int x, int z) current_position, next_tile;
		[SerializeField] private bool moving = false, unique_action = false, cooldown = false, minion, mana_regen = true, blocking = false, blocked = false, calculate_distance_traveled = false, in_air = false;
		private (int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage) blocked_damage_tuple = (0, 0, 0, 0, 0, 0);
		[SerializeField] private float mana_timer = 1, distance_traveled = 0;
		private Vector3 previous_position;
		[SerializeField] private grid_direction direction;
		[SerializeField] private pathfinding_tile_parameter pathfinding_parameter;

		#region stats

		[SerializeField] private int health, max_health;
		[SerializeField] private int mana, max_mana;
		[SerializeField] private float healing_multiplier;
		[SerializeField] private float movement_speed;
		[SerializeField] private float physical_resistance;
		[SerializeField] private float fire_resistance;
		[SerializeField] private float frost_resistance;
		[SerializeField] private float electric_resistance;
		[SerializeField] private float poison_resistance;
		[SerializeField] private float magic_resistance;

		#endregion
		#region status effects

		private bool oil_status = false;

		#endregion

		public void SetVariables (GameObject enemy, GameHandler caller, Vector3 position, enemy_id id)
		{
			minion = false;
			this_test_enemy = enemy;
			this.caller = caller;
			current_position = caller.GetGameGrid().GetXZ(position);
			transform.position = caller.GetGameGrid().GetWorldTileCenter (current_position, gameObject.transform.localScale.y);
			
			switch (id)
			{
				case enemy_id.shield_skeleton:
				movement_speed = caller.enemy_options.physical.shield_skeleton.movement_speed_per_second;
				max_health = caller.enemy_options.physical.shield_skeleton.health;
				max_mana = caller.enemy_options.physical.shield_skeleton.max_mana;
				physical_resistance = caller.enemy_options.physical.shield_skeleton.physical_resistance;
				fire_resistance = caller.enemy_options.physical.shield_skeleton.fire_resistance;
				frost_resistance = caller.enemy_options.physical.shield_skeleton.frost_resistance;
				electric_resistance = caller.enemy_options.physical.shield_skeleton.electric_resistance;
				poison_resistance = caller.enemy_options.physical.shield_skeleton.poison_resistance;
				magic_resistance = caller.enemy_options.physical.shield_skeleton.magic_resistance;
				break;

				case enemy_id.goblin_construction_team:
				movement_speed = caller.enemy_options.physical.goblin_construction_team.movement_speed_per_second;
				max_health = caller.enemy_options.physical.goblin_construction_team.health;
				max_mana = caller.enemy_options.physical.goblin_construction_team.max_mana;
				physical_resistance = caller.enemy_options.physical.goblin_construction_team.physical_resistance;
				fire_resistance = caller.enemy_options.physical.goblin_construction_team.fire_resistance;
				frost_resistance = caller.enemy_options.physical.goblin_construction_team.frost_resistance;
				electric_resistance = caller.enemy_options.physical.goblin_construction_team.electric_resistance;
				poison_resistance = caller.enemy_options.physical.goblin_construction_team.poison_resistance;
				magic_resistance = caller.enemy_options.physical.goblin_construction_team.magic_resistance;
				break;

				case enemy_id.brood_mother:
				movement_speed = caller.enemy_options.physical.brood_mother.movement_speed_per_second;
				max_health = caller.enemy_options.physical.brood_mother.health;
				max_mana = caller.enemy_options.physical.brood_mother.max_mana;
				physical_resistance = caller.enemy_options.physical.brood_mother.physical_resistance;
				fire_resistance = caller.enemy_options.physical.brood_mother.fire_resistance;
				frost_resistance = caller.enemy_options.physical.brood_mother.frost_resistance;
				electric_resistance = caller.enemy_options.physical.brood_mother.electric_resistance;
				poison_resistance = caller.enemy_options.physical.brood_mother.poison_resistance;
				magic_resistance = caller.enemy_options.physical.brood_mother.magic_resistance;
				break;

				case enemy_id.werewolf:
				movement_speed = caller.enemy_options.physical.werewolf.movement_speed_per_second;
				max_health = caller.enemy_options.physical.werewolf.health;
				max_mana = caller.enemy_options.physical.werewolf.max_mana;
				physical_resistance = caller.enemy_options.physical.werewolf.physical_resistance;
				fire_resistance = caller.enemy_options.physical.werewolf.fire_resistance;
				frost_resistance = caller.enemy_options.physical.werewolf.frost_resistance;
				electric_resistance = caller.enemy_options.physical.werewolf.electric_resistance;
				poison_resistance = caller.enemy_options.physical.werewolf.poison_resistance;
				magic_resistance = caller.enemy_options.physical.werewolf.magic_resistance;
				break;

				case enemy_id.steam_walker:
				movement_speed = caller.enemy_options.physical.steam_walker.movement_speed_per_second;
				max_health = caller.enemy_options.physical.steam_walker.health;
				max_mana = caller.enemy_options.physical.steam_walker.max_mana;
				physical_resistance = caller.enemy_options.physical.steam_walker.physical_resistance;
				fire_resistance = caller.enemy_options.physical.steam_walker.fire_resistance;
				frost_resistance = caller.enemy_options.physical.steam_walker.frost_resistance;
				electric_resistance = caller.enemy_options.physical.steam_walker.electric_resistance;
				poison_resistance = caller.enemy_options.physical.steam_walker.poison_resistance;
				magic_resistance = caller.enemy_options.physical.steam_walker.magic_resistance;
				break;

				case enemy_id.giant:
				movement_speed = caller.enemy_options.physical.giant.movement_speed_per_second;
				max_health = caller.enemy_options.physical.giant.health;
				max_mana = caller.enemy_options.physical.giant.max_mana;
				physical_resistance = caller.enemy_options.physical.giant.physical_resistance;
				fire_resistance = caller.enemy_options.physical.giant.fire_resistance;
				frost_resistance = caller.enemy_options.physical.giant.frost_resistance;
				electric_resistance = caller.enemy_options.physical.giant.electric_resistance;
				poison_resistance = caller.enemy_options.physical.giant.poison_resistance;
				magic_resistance = caller.enemy_options.physical.giant.magic_resistance;
				break;

				case enemy_id.magma_elemental:
				movement_speed = caller.enemy_options.fire.magma_elemental.movement_speed_per_second;
				max_health = caller.enemy_options.fire.magma_elemental.health;
				max_mana = caller.enemy_options.fire.magma_elemental.max_mana;
				physical_resistance = caller.enemy_options.fire.magma_elemental.physical_resistance;
				fire_resistance = caller.enemy_options.fire.magma_elemental.fire_resistance;
				frost_resistance = caller.enemy_options.fire.magma_elemental.frost_resistance;
				electric_resistance = caller.enemy_options.fire.magma_elemental.electric_resistance;
				poison_resistance = caller.enemy_options.fire.magma_elemental.poison_resistance;
				magic_resistance = caller.enemy_options.fire.magma_elemental.magic_resistance;
				break;

				case enemy_id.alchemist:
				movement_speed = caller.enemy_options.fire.alchemist.movement_speed_per_second;
				max_health = caller.enemy_options.fire.alchemist.health;
				max_mana = caller.enemy_options.fire.alchemist.max_mana;
				physical_resistance = caller.enemy_options.fire.alchemist.physical_resistance;
				fire_resistance = caller.enemy_options.fire.alchemist.fire_resistance;
				frost_resistance = caller.enemy_options.fire.alchemist.frost_resistance;
				electric_resistance = caller.enemy_options.fire.alchemist.electric_resistance;
				poison_resistance = caller.enemy_options.fire.alchemist.poison_resistance;
				magic_resistance = caller.enemy_options.fire.alchemist.magic_resistance;
				break;

				case enemy_id.bio_arsonist:
				movement_speed = caller.enemy_options.fire.bio_arsonist.movement_speed_per_second;
				max_health = caller.enemy_options.fire.bio_arsonist.health;
				max_mana = caller.enemy_options.fire.bio_arsonist.max_mana;
				physical_resistance = caller.enemy_options.fire.bio_arsonist.physical_resistance;
				fire_resistance = caller.enemy_options.fire.bio_arsonist.fire_resistance;
				frost_resistance = caller.enemy_options.fire.bio_arsonist.frost_resistance;
				electric_resistance = caller.enemy_options.fire.bio_arsonist.electric_resistance;
				poison_resistance = caller.enemy_options.fire.bio_arsonist.poison_resistance;
				magic_resistance = caller.enemy_options.fire.bio_arsonist.magic_resistance;
				break;

				case enemy_id.fire_elemental:
				movement_speed = caller.enemy_options.fire.fire_elemental.movement_speed_per_second;
				max_health = caller.enemy_options.fire.fire_elemental.health;
				max_mana = caller.enemy_options.fire.fire_elemental.max_mana;
				physical_resistance = caller.enemy_options.fire.fire_elemental.physical_resistance;
				fire_resistance = caller.enemy_options.fire.fire_elemental.fire_resistance;
				frost_resistance = caller.enemy_options.fire.fire_elemental.frost_resistance;
				electric_resistance = caller.enemy_options.fire.fire_elemental.electric_resistance;
				poison_resistance = caller.enemy_options.fire.fire_elemental.poison_resistance;
				magic_resistance = caller.enemy_options.fire.fire_elemental.magic_resistance;
				break;

				case enemy_id.wyrmling:
				movement_speed = caller.enemy_options.fire.wyrmling.movement_speed_per_second;
				max_health = caller.enemy_options.fire.wyrmling.health;
				max_mana = caller.enemy_options.fire.wyrmling.max_mana;
				physical_resistance = caller.enemy_options.fire.wyrmling.physical_resistance;
				fire_resistance = caller.enemy_options.fire.wyrmling.fire_resistance;
				frost_resistance = caller.enemy_options.fire.wyrmling.frost_resistance;
				electric_resistance = caller.enemy_options.fire.wyrmling.electric_resistance;
				poison_resistance = caller.enemy_options.fire.wyrmling.poison_resistance;
				magic_resistance = caller.enemy_options.fire.wyrmling.magic_resistance;
				break;

				case enemy_id.corruptor:
				movement_speed = caller.enemy_options.fire.corruptor.movement_speed_per_second;
				max_health = caller.enemy_options.fire.corruptor.health;
				max_mana = caller.enemy_options.fire.corruptor.max_mana;
				physical_resistance = caller.enemy_options.fire.corruptor.physical_resistance;
				fire_resistance = caller.enemy_options.fire.corruptor.fire_resistance;
				frost_resistance = caller.enemy_options.fire.corruptor.frost_resistance;
				electric_resistance = caller.enemy_options.fire.corruptor.electric_resistance;
				poison_resistance = caller.enemy_options.fire.corruptor.poison_resistance;
				magic_resistance = caller.enemy_options.fire.corruptor.magic_resistance;
				break;

				case enemy_id.lich:
				movement_speed = caller.enemy_options.frost.lich.movement_speed_per_second;
				max_health = caller.enemy_options.frost.lich.health;
				max_mana = caller.enemy_options.frost.lich.max_mana;
				physical_resistance = caller.enemy_options.frost.lich.physical_resistance;
				fire_resistance = caller.enemy_options.frost.lich.fire_resistance;
				frost_resistance = caller.enemy_options.frost.lich.frost_resistance;
				electric_resistance = caller.enemy_options.frost.lich.electric_resistance;
				poison_resistance = caller.enemy_options.frost.lich.poison_resistance;
				magic_resistance = caller.enemy_options.frost.lich.magic_resistance;
				break;

				case enemy_id.lady_frost:
				movement_speed = caller.enemy_options.frost.lady_frost.movement_speed_per_second;
				max_health = caller.enemy_options.frost.lady_frost.health;
				max_mana = caller.enemy_options.frost.lady_frost.max_mana;
				physical_resistance = caller.enemy_options.frost.lady_frost.physical_resistance;
				fire_resistance = caller.enemy_options.frost.lady_frost.fire_resistance;
				frost_resistance = caller.enemy_options.frost.lady_frost.frost_resistance;
				electric_resistance = caller.enemy_options.frost.lady_frost.electric_resistance;
				poison_resistance = caller.enemy_options.frost.lady_frost.poison_resistance;
				magic_resistance = caller.enemy_options.frost.lady_frost.magic_resistance;
				break;

				case enemy_id.ice_consumer:
				movement_speed = caller.enemy_options.frost.ice_consumer.movement_speed_per_second;
				max_health = caller.enemy_options.frost.ice_consumer.health;
				max_mana = caller.enemy_options.frost.ice_consumer.max_mana;
				physical_resistance = caller.enemy_options.frost.ice_consumer.physical_resistance;
				fire_resistance = caller.enemy_options.frost.ice_consumer.fire_resistance;
				frost_resistance = caller.enemy_options.frost.ice_consumer.frost_resistance;
				electric_resistance = caller.enemy_options.frost.ice_consumer.electric_resistance;
				poison_resistance = caller.enemy_options.frost.ice_consumer.poison_resistance;
				magic_resistance = caller.enemy_options.frost.ice_consumer.magic_resistance;
				break;

				case enemy_id.dead_grove:
				movement_speed = caller.enemy_options.frost.dead_grove.movement_speed_per_second;
				max_health = caller.enemy_options.frost.dead_grove.health;
				max_mana = caller.enemy_options.frost.dead_grove.max_mana;
				physical_resistance = caller.enemy_options.frost.dead_grove.physical_resistance;
				fire_resistance = caller.enemy_options.frost.dead_grove.fire_resistance;
				frost_resistance = caller.enemy_options.frost.dead_grove.frost_resistance;
				electric_resistance = caller.enemy_options.frost.dead_grove.electric_resistance;
				poison_resistance = caller.enemy_options.frost.dead_grove.poison_resistance;
				magic_resistance = caller.enemy_options.frost.dead_grove.magic_resistance;
				break;

				case enemy_id.snow_diver:
				movement_speed = caller.enemy_options.frost.snow_diver.movement_speed_per_second;
				max_health = caller.enemy_options.frost.snow_diver.health;
				max_mana = caller.enemy_options.frost.snow_diver.max_mana;
				physical_resistance = caller.enemy_options.frost.snow_diver.physical_resistance;
				fire_resistance = caller.enemy_options.frost.snow_diver.fire_resistance;
				frost_resistance = caller.enemy_options.frost.snow_diver.frost_resistance;
				electric_resistance = caller.enemy_options.frost.snow_diver.electric_resistance;
				poison_resistance = caller.enemy_options.frost.snow_diver.poison_resistance;
				magic_resistance = caller.enemy_options.frost.snow_diver.magic_resistance;
				break;

				case enemy_id.ice_thrall:
				movement_speed = caller.enemy_options.frost.ice_thrall.movement_speed_per_second;
				max_health = caller.enemy_options.frost.ice_thrall.health;
				max_mana = caller.enemy_options.frost.ice_thrall.max_mana;
				physical_resistance = caller.enemy_options.frost.ice_thrall.physical_resistance;
				fire_resistance = caller.enemy_options.frost.ice_thrall.fire_resistance;
				frost_resistance = caller.enemy_options.frost.ice_thrall.frost_resistance;
				electric_resistance = caller.enemy_options.frost.ice_thrall.electric_resistance;
				poison_resistance = caller.enemy_options.frost.ice_thrall.poison_resistance;
				magic_resistance = caller.enemy_options.frost.ice_thrall.magic_resistance;
				break;

				case enemy_id.absorber:
				movement_speed = caller.enemy_options.electric.absorber.movement_speed_per_second;
				max_health = caller.enemy_options.electric.absorber.health;
				max_mana = caller.enemy_options.electric.absorber.max_mana;
				physical_resistance = caller.enemy_options.electric.absorber.physical_resistance;
				fire_resistance = caller.enemy_options.electric.absorber.fire_resistance;
				frost_resistance = caller.enemy_options.electric.absorber.frost_resistance;
				electric_resistance = caller.enemy_options.electric.absorber.electric_resistance;
				poison_resistance = caller.enemy_options.electric.absorber.poison_resistance;
				magic_resistance = caller.enemy_options.electric.absorber.magic_resistance;
				break;

				case enemy_id.static_charge:
				movement_speed = caller.enemy_options.electric.static_charge.movement_speed_per_second;
				max_health = caller.enemy_options.electric.static_charge.health;
				max_mana = caller.enemy_options.electric.static_charge.max_mana;
				physical_resistance = caller.enemy_options.electric.static_charge.physical_resistance;
				fire_resistance = caller.enemy_options.electric.static_charge.fire_resistance;
				frost_resistance = caller.enemy_options.electric.static_charge.frost_resistance;
				electric_resistance = caller.enemy_options.electric.static_charge.electric_resistance;
				poison_resistance = caller.enemy_options.electric.static_charge.poison_resistance;
				magic_resistance = caller.enemy_options.electric.static_charge.magic_resistance;
				break;

				case enemy_id.charge_collector:
				movement_speed = caller.enemy_options.electric.charge_collector.movement_speed_per_second;
				max_health = caller.enemy_options.electric.charge_collector.health;
				max_mana = caller.enemy_options.electric.charge_collector.max_mana;
				physical_resistance = caller.enemy_options.electric.charge_collector.physical_resistance;
				fire_resistance = caller.enemy_options.electric.charge_collector.fire_resistance;
				frost_resistance = caller.enemy_options.electric.charge_collector.frost_resistance;
				electric_resistance = caller.enemy_options.electric.charge_collector.electric_resistance;
				poison_resistance = caller.enemy_options.electric.charge_collector.poison_resistance;
				magic_resistance = caller.enemy_options.electric.charge_collector.magic_resistance;
				break;

				case enemy_id.zap:
				movement_speed = caller.enemy_options.electric.zap.movement_speed_per_second;
				max_health = caller.enemy_options.electric.zap.health;
				max_mana = caller.enemy_options.electric.zap.max_mana;
				physical_resistance = caller.enemy_options.electric.zap.physical_resistance;
				fire_resistance = caller.enemy_options.electric.zap.fire_resistance;
				frost_resistance = caller.enemy_options.electric.zap.frost_resistance;
				electric_resistance = caller.enemy_options.electric.zap.electric_resistance;
				poison_resistance = caller.enemy_options.electric.zap.poison_resistance;
				magic_resistance = caller.enemy_options.electric.zap.magic_resistance;
				break;

				case enemy_id.rocket_goblin:
				movement_speed = caller.enemy_options.electric.rocket_goblin.movement_speed_per_second;
				max_health = caller.enemy_options.electric.rocket_goblin.health;
				max_mana = caller.enemy_options.electric.rocket_goblin.max_mana;
				physical_resistance = caller.enemy_options.electric.rocket_goblin.physical_resistance;
				fire_resistance = caller.enemy_options.electric.rocket_goblin.fire_resistance;
				frost_resistance = caller.enemy_options.electric.rocket_goblin.frost_resistance;
				electric_resistance = caller.enemy_options.electric.rocket_goblin.electric_resistance;
				poison_resistance = caller.enemy_options.electric.rocket_goblin.poison_resistance;
				magic_resistance = caller.enemy_options.electric.rocket_goblin.magic_resistance;
				break;

				case enemy_id.overcharge:
				movement_speed = caller.enemy_options.electric.overcharge.movement_speed_per_second;
				max_health = caller.enemy_options.electric.overcharge.health;
				max_mana = caller.enemy_options.electric.overcharge.max_mana;
				physical_resistance = caller.enemy_options.electric.overcharge.physical_resistance;
				fire_resistance = caller.enemy_options.electric.overcharge.fire_resistance;
				frost_resistance = caller.enemy_options.electric.overcharge.frost_resistance;
				electric_resistance = caller.enemy_options.electric.overcharge.electric_resistance;
				poison_resistance = caller.enemy_options.electric.overcharge.poison_resistance;
				magic_resistance = caller.enemy_options.electric.overcharge.magic_resistance;
				break;

				case enemy_id.green_belcher:
				movement_speed = caller.enemy_options.poison.green_belcher.movement_speed_per_second;
				max_health = caller.enemy_options.poison.green_belcher.health;
				max_mana = caller.enemy_options.poison.green_belcher.max_mana;
				physical_resistance = caller.enemy_options.poison.green_belcher.physical_resistance;
				fire_resistance = caller.enemy_options.poison.green_belcher.fire_resistance;
				frost_resistance = caller.enemy_options.poison.green_belcher.frost_resistance;
				electric_resistance = caller.enemy_options.poison.green_belcher.electric_resistance;
				poison_resistance = caller.enemy_options.poison.green_belcher.poison_resistance;
				magic_resistance = caller.enemy_options.poison.green_belcher.magic_resistance;
				break;

				case enemy_id.ooze:
				movement_speed = caller.enemy_options.poison.ooze.movement_speed_per_second;
				max_health = caller.enemy_options.poison.ooze.health;
				max_mana = caller.enemy_options.poison.ooze.max_mana;
				physical_resistance = caller.enemy_options.poison.ooze.physical_resistance;
				fire_resistance = caller.enemy_options.poison.ooze.fire_resistance;
				frost_resistance = caller.enemy_options.poison.ooze.frost_resistance;
				electric_resistance = caller.enemy_options.poison.ooze.electric_resistance;
				poison_resistance = caller.enemy_options.poison.ooze.poison_resistance;
				magic_resistance = caller.enemy_options.poison.ooze.magic_resistance;
				break;

				case enemy_id.corpse_eater:
				movement_speed = caller.enemy_options.poison.corpse_eater.movement_speed_per_second;
				max_health = caller.enemy_options.poison.corpse_eater.health;
				max_mana = caller.enemy_options.poison.corpse_eater.max_mana;
				physical_resistance = caller.enemy_options.poison.corpse_eater.physical_resistance;
				fire_resistance = caller.enemy_options.poison.corpse_eater.fire_resistance;
				frost_resistance = caller.enemy_options.poison.corpse_eater.frost_resistance;
				electric_resistance = caller.enemy_options.poison.corpse_eater.electric_resistance;
				poison_resistance = caller.enemy_options.poison.corpse_eater.poison_resistance;
				magic_resistance = caller.enemy_options.poison.corpse_eater.magic_resistance;
				break;

				case enemy_id.mutant:
				movement_speed = caller.enemy_options.poison.mutant.movement_speed_per_second;
				max_health = caller.enemy_options.poison.mutant.health;
				max_mana = caller.enemy_options.poison.mutant.max_mana;
				physical_resistance = caller.enemy_options.poison.mutant.physical_resistance;
				fire_resistance = caller.enemy_options.poison.mutant.fire_resistance;
				frost_resistance = caller.enemy_options.poison.mutant.frost_resistance;
				electric_resistance = caller.enemy_options.poison.mutant.electric_resistance;
				poison_resistance = caller.enemy_options.poison.mutant.poison_resistance;
				magic_resistance = caller.enemy_options.poison.mutant.magic_resistance;
				break;

				case enemy_id.grappling_spider:
				movement_speed = caller.enemy_options.poison.grappling_spider.movement_speed_per_second;
				max_health = caller.enemy_options.poison.grappling_spider.health;
				max_mana = caller.enemy_options.poison.grappling_spider.max_mana;
				physical_resistance = caller.enemy_options.poison.grappling_spider.physical_resistance;
				fire_resistance = caller.enemy_options.poison.grappling_spider.fire_resistance;
				frost_resistance = caller.enemy_options.poison.grappling_spider.frost_resistance;
				electric_resistance = caller.enemy_options.poison.grappling_spider.electric_resistance;
				poison_resistance = caller.enemy_options.poison.grappling_spider.poison_resistance;
				magic_resistance = caller.enemy_options.poison.grappling_spider.magic_resistance;
				break;

				case enemy_id.plague_bearer:
				movement_speed = caller.enemy_options.poison.plague_bearer.movement_speed_per_second;
				max_health = caller.enemy_options.poison.plague_bearer.health;
				max_mana = caller.enemy_options.poison.plague_bearer.max_mana;
				physical_resistance = caller.enemy_options.poison.plague_bearer.physical_resistance;
				fire_resistance = caller.enemy_options.poison.plague_bearer.fire_resistance;
				frost_resistance = caller.enemy_options.poison.plague_bearer.frost_resistance;
				electric_resistance = caller.enemy_options.poison.plague_bearer.electric_resistance;
				poison_resistance = caller.enemy_options.poison.plague_bearer.poison_resistance;
				magic_resistance = caller.enemy_options.poison.plague_bearer.magic_resistance;
				break;

				case enemy_id.shadow_fiend:
				movement_speed = caller.enemy_options.magic.shadow_fiend.movement_speed_per_second;
				max_health = caller.enemy_options.magic.shadow_fiend.health;
				max_mana = caller.enemy_options.magic.shadow_fiend.max_mana;
				physical_resistance = caller.enemy_options.magic.shadow_fiend.physical_resistance;
				fire_resistance = caller.enemy_options.magic.shadow_fiend.fire_resistance;
				frost_resistance = caller.enemy_options.magic.shadow_fiend.frost_resistance;
				electric_resistance = caller.enemy_options.magic.shadow_fiend.electric_resistance;
				poison_resistance = caller.enemy_options.magic.shadow_fiend.poison_resistance;
				magic_resistance = caller.enemy_options.magic.shadow_fiend.magic_resistance;
				break;

				case enemy_id.shaman:
				movement_speed = caller.enemy_options.magic.shaman.movement_speed_per_second;
				max_health = caller.enemy_options.magic.shaman.health;
				max_mana = caller.enemy_options.magic.shaman.max_mana;
				physical_resistance = caller.enemy_options.magic.shaman.physical_resistance;
				fire_resistance = caller.enemy_options.magic.shaman.fire_resistance;
				frost_resistance = caller.enemy_options.magic.shaman.frost_resistance;
				electric_resistance = caller.enemy_options.magic.shaman.electric_resistance;
				poison_resistance = caller.enemy_options.magic.shaman.poison_resistance;
				magic_resistance = caller.enemy_options.magic.shaman.magic_resistance;
				break;

				case enemy_id.necromancer:
				movement_speed = caller.enemy_options.magic.necromancer.movement_speed_per_second;
				max_health = caller.enemy_options.magic.necromancer.health;
				max_mana = caller.enemy_options.magic.necromancer.max_mana;
				physical_resistance = caller.enemy_options.magic.necromancer.physical_resistance;
				fire_resistance = caller.enemy_options.magic.necromancer.fire_resistance;
				frost_resistance = caller.enemy_options.magic.necromancer.frost_resistance;
				electric_resistance = caller.enemy_options.magic.necromancer.electric_resistance;
				poison_resistance = caller.enemy_options.magic.necromancer.poison_resistance;
				magic_resistance = caller.enemy_options.magic.necromancer.magic_resistance;
				break;

				case enemy_id.mana_addict:
				movement_speed = caller.enemy_options.magic.mana_addict.movement_speed_per_second;
				max_health = caller.enemy_options.magic.mana_addict.health;
				max_mana = caller.enemy_options.magic.mana_addict.max_mana;
				physical_resistance = caller.enemy_options.magic.mana_addict.physical_resistance;
				fire_resistance = caller.enemy_options.magic.mana_addict.fire_resistance;
				frost_resistance = caller.enemy_options.magic.mana_addict.frost_resistance;
				electric_resistance = caller.enemy_options.magic.mana_addict.electric_resistance;
				poison_resistance = caller.enemy_options.magic.mana_addict.poison_resistance;
				magic_resistance = caller.enemy_options.magic.mana_addict.magic_resistance;
				break;

				case enemy_id.phase_shifter:
				movement_speed = caller.enemy_options.magic.phase_shifter.movement_speed_per_second;
				max_health = caller.enemy_options.magic.phase_shifter.health;
				max_mana = caller.enemy_options.magic.phase_shifter.max_mana;
				physical_resistance = caller.enemy_options.magic.shadow_fiend.physical_resistance;
				fire_resistance = caller.enemy_options.magic.phase_shifter.fire_resistance;
				frost_resistance = caller.enemy_options.magic.phase_shifter.frost_resistance;
				electric_resistance = caller.enemy_options.magic.phase_shifter.electric_resistance;
				poison_resistance = caller.enemy_options.magic.phase_shifter.poison_resistance;
				magic_resistance = caller.enemy_options.magic.phase_shifter.magic_resistance;
				break;

				case enemy_id.mind_twister:
				movement_speed = caller.enemy_options.magic.mind_twister.movement_speed_per_second;
				max_health = caller.enemy_options.magic.mind_twister.health;
				max_mana = caller.enemy_options.magic.mind_twister.max_mana;
				physical_resistance = caller.enemy_options.magic.mind_twister.physical_resistance;
				fire_resistance = caller.enemy_options.magic.mind_twister.fire_resistance;
				frost_resistance = caller.enemy_options.magic.mind_twister.frost_resistance;
				electric_resistance = caller.enemy_options.magic.mind_twister.electric_resistance;
				poison_resistance = caller.enemy_options.magic.mind_twister.poison_resistance;
				magic_resistance = caller.enemy_options.magic.mind_twister.magic_resistance;
				break;
			}

			if (max_health == 0)
			{
				max_health = 100;
			}
			if (movement_speed == 0)
			{ 
				movement_speed = 0.25f;
			}
			if (max_mana == 0)
			{
				max_mana = 100;
			}
			mana = max_mana;
			health = max_health;
		}

		public void SetVariables (GameObject enemy, GameHandler caller, Vector3 position, minion_id id)
		{
			minion = true;
			this_test_enemy = enemy;
			this.caller = caller;
			current_position = caller.GetGameGrid().GetXZ(position);
			transform.position = caller.GetGameGrid().GetWorldTileCenter (current_position, gameObject.transform.localScale.y);
			
			switch (id)
			{
				case minion_id.spider:
				movement_speed = caller.enemy_options.minion.spider.movement_speed_per_second;
				max_health = caller.enemy_options.minion.spider.health;
				break;

				case minion_id.treant:
				movement_speed = caller.enemy_options.minion.treant.movement_speed_per_second;
				max_health = caller.enemy_options.minion.treant.health;
				break;

				case minion_id.zombie:
				movement_speed = caller.enemy_options.minion.zombie.movement_speed_per_second;
				max_health = caller.enemy_options.minion.zombie.health;
				break;
			}

			if (max_health == 0)
			{
				max_health = 100;
			}
			if (movement_speed == 0)
			{ 
				movement_speed = 0.25f;
			}
			if (max_mana == 0)
			{
				max_mana = 100;
			}
			mana = max_mana;
			health = max_health;
		}

		public void SetDeathAction (Action death_action)
		{
			DeathAction = death_action;
		}

		#endregion

		private void Start()
		{
			#region health & mana bars setup
			enemy_bar_canvas = Instantiate (GameObject.Find ("Enemy Bar Canvas") );
			enemy_bar_canvas.transform.SetParent (transform, true);
			enemy_bar_canvas.transform.localPosition = new Vector3 (0, 1, 0.25f);
			enemy_bar_canvas.transform.SetSiblingIndex (2);
			health_bar = this_test_enemy.transform.GetChild(2).GetChild(0).GetChild(2).gameObject;
			mana_bar = this_test_enemy.transform.GetChild(2).GetChild(1).GetChild(2).gameObject;
			casting_bar = this_test_enemy.transform.GetChild(2).GetChild(2).GetChild(2).gameObject;
			HealthBarToggle (false);
			ManaBarToggle (false);
			CastingBarToggle (false);
			#endregion
			this_enemy_collider_list.Add (GetComponent<Collider> ());
			foreach (Collider collider in GetComponentsInChildren<Collider> ())
			{
				this_enemy_collider_list.Add (collider);
			}
			previous_position = transform.position;
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<Rigidbody> ().freezeRotation = true;

		}

		private void FixedUpdate()
		{
			//mana regen
			if (mana < max_mana && mana_regen == true)
			{
				mana_timer -= Time.deltaTime;
				if (mana_timer < 0 )
				{
					mana += caller.enemy_options.mana_reg_per_sec;
					mana_timer = 1;
					if (mana > max_mana)
					{
						mana = max_mana;
					}
				}
			}
			//walking
			if (unique_action == false)
			{
				if (moving == false)
				{
					//choosing the next destination tile
					caller.GetGameGrid().SetValue (current_position.x, current_position.z, GameGrid.grid_parameter.enemy, GameGrid.enemy.occupied);
					next_tile = (caller.GetGameGrid().GetValue (current_position, grid_parameter.prev_x_empty),
					caller.GetGameGrid().GetValue (current_position, grid_parameter.prev_z_empty));
					direction = caller.GetGameGrid().GetMovementDirection (current_position, next_tile);
					//rotating enemy to match movement direction
					switch (direction)
					{
						case grid_direction.up:
						transform.Rotate (- transform.rotation.eulerAngles.x, - transform.rotation.eulerAngles.y, - transform.rotation.eulerAngles.z);
						enemy_bar_canvas.transform.Rotate (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y);
						break;

						case grid_direction.down:
						transform.Rotate (- transform.rotation.eulerAngles.x, - transform.rotation.eulerAngles.y + 180, - transform.rotation.eulerAngles.z);
						enemy_bar_canvas.transform.Rotate (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y - 180);
						break;

						case grid_direction.left:
						transform.Rotate (- transform.rotation.eulerAngles.x, - transform.rotation.eulerAngles.y - 90, - transform.rotation.eulerAngles.z);
						enemy_bar_canvas.transform.Rotate (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y + 90);
						break;

						case grid_direction.right:
						transform.Rotate (- transform.rotation.eulerAngles.x, - transform.rotation.eulerAngles.y + 90, - transform.rotation.eulerAngles.z);
						enemy_bar_canvas.transform.Rotate (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y - 90);
						break;

						case grid_direction.top_left:
						transform.Rotate (- transform.rotation.eulerAngles.x, - transform.rotation.eulerAngles.y - 45, - transform.rotation.eulerAngles.z);
						enemy_bar_canvas.transform.Rotate (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y + 45);
						break;

						case grid_direction.top_right:
						transform.Rotate (- transform.rotation.eulerAngles.x, - transform.rotation.eulerAngles.y + 45, - transform.rotation.eulerAngles.z);
						enemy_bar_canvas.transform.Rotate (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y - 45);
						break;

						case grid_direction.bottom_left:
						transform.Rotate (- transform.rotation.eulerAngles.x, - transform.rotation.eulerAngles.y - 135, - transform.rotation.eulerAngles.z);
						enemy_bar_canvas.transform.Rotate (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y + 135);
						break;

						case grid_direction.bottom_right:
						transform.Rotate (- transform.rotation.eulerAngles.x, - transform.rotation.eulerAngles.y + 135, - transform.rotation.eulerAngles.z);
						enemy_bar_canvas.transform.Rotate (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y - 135);
						break;
					}
					moving = true;
				}
				//moving enemy object
				this_test_enemy.transform.position += GetDirectionVector (direction, movement_speed);
				//action triggered when enemy dies while walking
				if (health <= 0)
				{
					caller.GetGameGrid().SetValue (current_position.x, current_position.z, GameGrid.grid_parameter.enemy, GameGrid.enemy.empty);
					DeathActionAndSelfDestroy();
				}
				//calculating distance traveled
				if (calculate_distance_traveled == true)
				{
					distance_traveled += Vector3.Distance (previous_position, transform.position);
					previous_position = transform.position;
				}
				//action upon reaching the center of the next destination tile
				if (CheckIfDestinationReached (caller, this_test_enemy, next_tile, direction))
				{
					//changing tile parameter values
					caller.GetGameGrid().SetValue (current_position.x, current_position.z, GameGrid.grid_parameter.enemy, GameGrid.enemy.empty);
					current_position = next_tile;
					if (caller.GetGameGrid().GetValue (current_position, grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.core))
					{
						//action triggered when enemy reaches core
						caller.testing_options.core_health--;
						Debug.Log ("core health = " + caller.testing_options.core_health);
						caller.RemoveEnemyFromEnemyList (gameObject);
						Destroy (gameObject);
					}
					moving = false;
					//action made after reaching the center of the next tile in path
					if (DestinationReachedAction != null)
					{
						DestinationReachedAction ();
					}
				}
			}
			else
			{
				//action triggered when enemy dies while not walking
				if (health <= 0)
				{
					DeathActionAndSelfDestroy();
				}
			}
		}

		#region get: current position, health, max health, distance from core, mana, max mana

		public (int x, int z) GetCurrentPosition ()
		{
			return current_position;
		}

		public int GetCurrentHealth ()
		{
			return health;
		}

		public int GetMaxHealth ()
		{
			return max_health;
		}

		public int GetCurrentMana ()
		{
			return mana;
		}

		public int GetMaxMana ()
		{
			return max_mana;
		}

	
		#endregion

		#region check cooldown and mana, set mana and cooldown, get mana, check if moving, check unique action, casting bar update, death action and self destroy

		public bool CheckCooldownAndMana (int mana_cost)
		{
			if (mana >= mana_cost && cooldown == false)
			{
				return true;
			}
			return false;
		}

		public void SetManaAndCooldown (int mana_cost, float cooldown_time)
		{
			ManaAddition (-mana_cost);
			if (cooldown_time > 0)
			{
				cooldown = true;
				GameHandler.Timer.Create (()=>{
				cooldown = false;},
				cooldown_time, "cooldown", gameObject);
			}
		}

		public int GetMana ()
		{
			return mana;
		}

		public bool CheckIfMoving ()
		{
			return moving;
		}

		public bool CheckUniqueAction ()
		{
			return unique_action;
		}

		public void SetUniqueAction (bool state)
		{
			unique_action = state;
		}

		public void CastingBarUpdate (float timer, float casting_time)
		{
			casting_bar.transform.localScale = new Vector3 (timer / casting_time, 1 , 1);
			if (timer > casting_time)
			{
				CastingBarToggle (false);
			}
			if (timer < casting_time)
			{
				CastingBarToggle (true);
			}
		}

		public void DeathActionAndSelfDestroy ()
		{
			if (DeathAction != null)
			{
				DeathAction();
			}
			caller.RemoveEnemyFromEnemyList (gameObject);
			if (minion == false)
			{
				CreateDeadBody ();
			}
			Destroy (gameObject);
		}

		#endregion

		#region damage calulation, get blocked damage, set blocking, reset blocked damage, set blocking action, set calculate distance traveled

		public void CalculateIncomingDamage ((int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage) damage_tuple)
		{
			if (blocking == false)
			{
				int physical_damage_after_resistance = (int) Math.Floor (((1 - physical_resistance) * damage_tuple.physical_damage));
				int fire_damage_after_resistance = (int) Math.Floor (((1 - fire_resistance)  * damage_tuple.fire_damage));
				int frost_damage_after_resistance = (int) Math.Floor (((1 - frost_resistance) * damage_tuple.frost_damage));
				int electric_damage_after_resistance = (int) Math.Floor (((1 - electric_resistance) * damage_tuple.electric_damage));
				int poison_damage_after_resistance = (int) Math.Floor (((1 - poison_resistance) * damage_tuple.poison_damage));
				int magic_damage_after_resistance = (int) Math.Floor (((1 - magic_resistance) * damage_tuple.magic_damage));
				int total_damage = physical_damage_after_resistance + fire_damage_after_resistance + frost_damage_after_resistance + electric_damage_after_resistance +
				poison_damage_after_resistance + magic_damage_after_resistance;
				caller.GetDamageChart().DamageAddition (physical_damage_after_resistance, fire_damage_after_resistance, frost_damage_after_resistance, electric_damage_after_resistance,
				poison_damage_after_resistance, magic_damage_after_resistance);
				HealthAddition (-total_damage);
				if (OnHitAction != null)
				{
					OnHitAction ();
				}
			}
			else
			{
				blocking = false;
				blocked = true;
				blocked_damage_tuple.physical_damage =+ damage_tuple.physical_damage;
				blocked_damage_tuple.fire_damage =+ damage_tuple.fire_damage;
				blocked_damage_tuple.frost_damage =+ damage_tuple.frost_damage;
				blocked_damage_tuple.electric_damage =+ damage_tuple.electric_damage;
				blocked_damage_tuple.poison_damage =+ damage_tuple.poison_damage;
				blocked_damage_tuple.magic_damage =+ damage_tuple.magic_damage;
				BlockingAction (blocked_damage_tuple);
			}
		}

		public bool GetBlockedDamageTuple (out (int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage) blocked_damage_tuple)
		{
			blocked_damage_tuple = this.blocked_damage_tuple;
			if (blocked == true)
			{
				return true;
			}
			return false;
		}

		public void SetBlocking (bool state)
		{
			blocking = state;
		}

		public void ResetBlockedDamageTuple ()
		{
			blocked_damage_tuple = (0, 0, 0, 0, 0, 0);
		}

		public void SetBlockingAction (Action<(int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage)> action)
		{
			BlockingAction = action;
		}

		public float GetDistanceTraveled ()
		{
			return distance_traveled;
		}

		public void ResetDistanceTraveled ()
		{
			distance_traveled = 0;
		}

		public void SetCalculateDistanceTraveled (bool state)
		{
			calculate_distance_traveled = state;
		}

		public void HealthBarUpdate ()
		{
			if (health > max_health)
			{
				health = max_health;
			}
			health_bar.transform.localScale = new Vector3 ((float) health / max_health, 1, 1);
			if (health / max_health == 1)
			{
				HealthBarToggle (false);
			}
			else
			{
				HealthBarToggle (true);
				if ((float) health / max_health >= 0.75f)
				{
					health_bar.GetComponent<Image>().color = new Color (0.4980392f, 1, 0);
				}
				else
				{
					if ((float) health / max_health >= 0.5f)
					{
						health_bar.GetComponent<Image>().color = new Color (1, 1, 0);
					}
					else
					{
						if ((float) health / max_health >= 0.25f)
						{
							health_bar.GetComponent<Image>().color = new Color (1, 0.6470588f, 0);
						}
						else
						{
							health_bar.GetComponent<Image>().color = new Color (1, 0.2705882f, 0);
						}
					}
				}
			}
		}

		#endregion

		#region set destination reached action, get next tile, set on hit action, get resistances, set in air, set pathfinding parameter, get movement speed, get movement direction

		public void SetDestinationReachedAction (Action action)
		{
			DestinationReachedAction = action;
		}

		public (int x, int z) GetNextTile ()
		{
			return next_tile;
		}

		public void SetOnHitAction (Action action)
		{
			OnHitAction = action;
		}

		public void SetManaRegen (bool state)
		{
			mana_regen = state;
		}

		public (float physical, float fire, float frost, float electric, float poison, float magic) GetResistances ()
		{
			return (physical_resistance, fire_resistance, frost_resistance, electric_resistance, poison_resistance, magic_resistance);
		}

		public void SetInAir (bool state)
		{
			in_air = state;
		}

		public void SetPathfindingParameter (pathfinding_tile_parameter parameter)
		{
			pathfinding_parameter = parameter;
		}

		public float GetMovementSpeed ()
		{
			return movement_speed;
		}

		public grid_direction GetMovementDirection ()
		{
			return direction;
		}

		#endregion

		#region stat modification

		public void MovementSpeedMultiplier (float speed_multiplier)
		{
			movement_speed *= speed_multiplier;
		}

		public void MaxHealthAddition (int max_health_increase)
		{
			max_health += max_health_increase;
			HealthBarUpdate();
		}

		public void PhysicalResistanceAddition (float resistance_increase)
		{
			physical_resistance += resistance_increase;
		}

		public void FireResistanceAddition (float resistance_increase)
		{
			fire_resistance += resistance_increase;
		}

		public void FrostResistanceAddition (float resistance_increase)
		{
			frost_resistance += resistance_increase;
		}

		public void ElectricResistanceAddition (float resistance_increase)
		{
			electric_resistance += resistance_increase;
		}

		public void PoisonResistanceAddition (float resistance_increase)
		{
			poison_resistance += resistance_increase;
		}

		public void MagicResistanceAddition (float resistance_increase)
		{
			magic_resistance += resistance_increase;
		}

		public void HealingMultiplier (float healing_multiplier)
		{
			this.healing_multiplier += healing_multiplier;
		}

		public void HealthAddition (int health_increase)
		{
			health += health_increase;
			HealthBarUpdate();
		}

		public void ManaAddition (int mana_increase)
		{
			mana += mana_increase;
			if (mana > max_mana)
			{
				mana = max_mana;
			}
			if (mana < 0)
			{
				mana = 0;
			}
			mana_bar.transform.localScale = new Vector3 ((float) mana / max_mana, 1, 1);
			if (mana / max_mana == 1)
			{
				ManaBarToggle (false);
			}
			else
			{
				ManaBarToggle (true);
			}
		}

		public void MaxManaAddition (int mana_increase)
		{
			max_mana += mana_increase;
		}
		#endregion

		#region status effects

		public bool GetOilStatus ()
		{
			return oil_status;
		}

		public void SetOilStatus (bool state)
		{
			oil_status = state;
		}

		#endregion

		#region dead body - create, dead body class
		private void CreateDeadBody ()
		{
			GameObject dead_body = Instantiate (GameObject.Find ("Dead Template") );
			dead_body.transform.SetParent (GameObject.Find ("Dead Bodies").transform, false);
			dead_body.transform.position = transform.position - new Vector3 (0, (transform.localScale.y / 2) + 0.2f, 0);
			dead_body.transform.Rotate (0, UnityEngine.Random.Range (0, 361), 0);
			dead_body.AddComponent<DeadBody>().SetVariables (caller);
		}

		public class DeadBody : MonoBehaviour
		{
			#region variable declarations

			GameHandler caller;
			public bool used = false;

			public void SetVariables (GameHandler caller)
			{
				this.caller = caller;
			}

			#endregion

			public void SetUsed (bool state)
			{
				used = state;
			}

			public bool GetUsed ()
			{
				return used;
			}

			private void FixedUpdate()
			{
				if (caller.CheckIfWaveActive () == false)
				{
					Destroy (gameObject);
				}
			}
		}

		#endregion

		#region bar toggle - health, mana, casting

		public void HealthBarToggle (bool state)
		{
			this_test_enemy.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().enabled = state;
			this_test_enemy.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Image>().enabled = state;
			this_test_enemy.transform.GetChild(2).GetChild(0).GetChild(2).GetComponent<Image>().enabled = state;
		}

		public void ManaBarToggle (bool state)
		{
			this_test_enemy.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Image>().enabled = state;
			this_test_enemy.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Image>().enabled = state;
			this_test_enemy.transform.GetChild(2).GetChild(1).GetChild(2).GetComponent<Image>().enabled = state;
		}

		public void CastingBarToggle (bool state)
		{
			this_test_enemy.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>().enabled = state;
			this_test_enemy.transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<Image>().enabled = state;
			this_test_enemy.transform.GetChild(2).GetChild(2).GetChild(2).GetComponent<Image>().enabled = state;
		}

		#endregion

		private void OnTriggerEnter (Collider collider)
		{
			TriggerEnterEvent (collider);
		}

		public void TriggerEnterEvent (Collider collider)
		{
			if (collider.gameObject.tag == "test tower projectile")
			{
				CalculateIncomingDamage (collider.GetComponent<Tower.ShotProjectile>().damage_tuple);
			}
			if (collider.gameObject.tag == "oil")
			{
				MovementSpeedMultiplier (caller.tower_options.oil.slow_amount);
			}
			if (collider.gameObject.tag == "plane" && in_air == true)
			{
				transform.position = caller.GetGameGrid().GetWorldTileCenter(caller.GetGameGrid().GetXZ(transform.position), transform.localScale.y + 0.2f);
				current_position = caller.GetGameGrid().GetXZ(transform.position);
				unique_action = false;
				in_air = false;
			}
		}

		private void OnTriggerExit (Collider collider)
		{
			TriggerExitEvent (collider);
		}

		public void TriggerExitEvent (Collider collider)
		{
			if (collider.gameObject.tag == "oil")
			{
				GameHandler.Timer.Create (() => {MovementSpeedMultiplier (1 / caller.tower_options.oil.slow_amount);},
				caller.tower_options.oil.slow_duration, "oil slow duration", gameObject);
			}
		}
	}

	#region physical classes
	public class ShieldSkeleton : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Start()
		{
			transform.GetChild(3).AddComponent<Skeleton_Shield>();
			GetComponent<BaseEnemy>().SetBlockingAction (ProcessShieldBlocking);
		}

		private void ProcessShieldBlocking
		((int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage) blocked_damage_tuple)
		{
			int mana_cost = (int) (caller.enemy_options.physical.shield_skeleton.mana_cost_per_dmg * (blocked_damage_tuple.physical_damage + blocked_damage_tuple.fire_damage + 
			blocked_damage_tuple.frost_damage + blocked_damage_tuple.electric_damage + blocked_damage_tuple.poison_damage + blocked_damage_tuple.magic_damage));
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (mana_cost, caller.enemy_options.physical.shield_skeleton.cooldown);
				GetComponent<BaseEnemy>().ResetBlockedDamageTuple();
			}
			else
			{
				if (GetComponent<BaseEnemy>().CheckCooldownAndMana (0) == true)
				{
					if (GetComponent<BaseEnemy>().GetMana() != 0)
					{
						int damage_reduction = mana_cost / GetComponent<BaseEnemy>().GetMana();
						blocked_damage_tuple.physical_damage *= damage_reduction;
						blocked_damage_tuple.fire_damage *= damage_reduction;
						blocked_damage_tuple.frost_damage *= damage_reduction;
						blocked_damage_tuple.electric_damage *= damage_reduction;
						blocked_damage_tuple.poison_damage *= damage_reduction;
						blocked_damage_tuple.magic_damage *= damage_reduction;
						GetComponent<BaseEnemy>().SetManaAndCooldown (GetComponent<BaseEnemy>().GetMana(), caller.enemy_options.physical.shield_skeleton.cooldown);
						GetComponent<BaseEnemy>().CalculateIncomingDamage (blocked_damage_tuple);
						GetComponent<BaseEnemy>().ResetBlockedDamageTuple();
					}
				}
			}
		}

		public class Skeleton_Shield : MonoBehaviour
		{
			#region variable declarations

			#endregion

			private void OnTriggerEnter(Collider other)
			{
				if (GetComponentInParent<BaseEnemy>().GetMana() > 0)
				{
					GetComponentInParent<BaseEnemy>().SetBlocking (true);
				}
				GetComponentInParent<BaseEnemy>().TriggerEnterEvent (other);
			}

			private void OnTriggerExit(Collider other)
			{
				GetComponentInParent<BaseEnemy>().TriggerExitEvent (other);
			}
		}
	}

	public class GoblinConstructionTeam : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;
		private grid_direction jump_direction;
		private float building_timer = 0;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Start()
		{
			GetComponent<BaseEnemy>().SetDestinationReachedAction (()=>{
				if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.goblin_construction_team.mana_cost) == true)
				{
					if (CheckStraightLineJumpViability (caller, GetComponent<BaseEnemy>().GetCurrentPosition (), 2, out jump_direction) == true)
					{
						GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.goblin_construction_team.mana_cost, caller.enemy_options.physical.goblin_construction_team.cooldown);
						GetComponent<BaseEnemy>().SetUniqueAction (true);
					}
				}
			});
		}

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckUniqueAction() == true)
			{
				building_timer += Time.deltaTime;
				if (building_timer > 1)
				{
					(int x, int z) scaffolding_position = AddDirectionToPosition (jump_direction, GetComponent<BaseEnemy>().GetCurrentPosition());
					caller.GetGameGrid().SetValue (scaffolding_position, grid_parameter.object_type, object_type.traversable_tower);
					GetComponent<BaseEnemy>().SetUniqueAction (false);
					caller.SetNewPathfinding ();
				}
			}
		}
	}

	public class BroodMother : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;
		GameObject target_body = null;
		private float casting_timer = 0;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
			GetComponent <BaseEnemy>().SetDeathAction (()=>{
			if (target_body != null)
			{
				target_body.GetComponent<BaseEnemy.DeadBody>().SetUsed (false);
			}
			});
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent <BaseEnemy>().CheckUniqueAction() == true)
			{
				casting_timer += Time.deltaTime;
				GetComponent <BaseEnemy>().CastingBarUpdate (casting_timer, caller.enemy_options.physical.brood_mother.casting_time);
				if (casting_timer > caller.enemy_options.physical.brood_mother.casting_time)
				{
					casting_timer = 0;
					GetComponent <BaseEnemy>().SetUniqueAction (false);
					GameObject timer = GameHandler.Timer.Create (caller.enemy_options.physical.brood_mother.egg_hatch_time,"spider spawn", target_body);
					timer.GetComponent<GameHandler.Timer.TimerComponent>().SetAction (()=>{
					minion_id type = minion_id.spider;
					GameObject enemy_object = Instantiate (GetEnemyTemplate (type));
					GameHandler caller = GetGameHandler();
					caller.AddEnemyToEnemyList (enemy_object);
					Enemy enemy = new (type, timer.GetComponentInParent<BaseEnemy.DeadBody>().transform.position, caller, enemy_object);
					Destroy (timer.GetComponentInParent<BaseEnemy.DeadBody>().gameObject);});
					target_body = null;
				}
			}
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.brood_mother.mana_cost) == true && GetComponent <BaseEnemy>().CheckUniqueAction() == false)
			{
				target_body = GetClosestDeadBody (transform.position, caller.enemy_options.physical.brood_mother.casting_range);
				if (target_body != null)
				{
					GetComponent <BaseEnemy>().SetUniqueAction (true);
					target_body.GetComponent<BaseEnemy.DeadBody>().SetUsed (true);
					GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.brood_mother.mana_cost, caller.enemy_options.physical.brood_mother.cooldown + caller.enemy_options.physical.brood_mother.casting_time);
				}
			}
		}
	}

	public class Werewolf : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Start()
		{
			GetComponent<BaseEnemy>().SetCalculateDistanceTraveled (true);
		}

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.werewolf.mana_cost) == true &&
			GetComponent<BaseEnemy>().GetDistanceTraveled() >= caller.enemy_options.physical.werewolf.evolution_distance)
			{
				GameObject closest_enemy = GetClosestEnemy (gameObject, caller.enemy_options.physical.werewolf.casting_range);
				if (closest_enemy != null)
				{
					closest_enemy.GetComponent<BaseEnemy>().HealthAddition (- caller.enemy_options.physical.werewolf.casting_damage);
					GetComponent<BaseEnemy>().MaxHealthAddition (caller.enemy_options.physical.werewolf.casting_max_health_increase);
					GetComponent<BaseEnemy>().HealthAddition (caller.enemy_options.physical.werewolf.casting_heal);
					GetComponent<BaseEnemy>().MovementSpeedMultiplier (caller.enemy_options.physical.werewolf.casting_movement_speed_multiplier);
					GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.werewolf.mana_cost, caller.enemy_options.physical.werewolf.cooldown);
					GetComponent<BaseEnemy>().ResetDistanceTraveled();
				}
			}
		}
	}

	public class SteamWalker : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Start()
		{
			GetComponent<BaseEnemy>().SetPathfindingParameter (pathfinding_tile_parameter.any_object);
			GetComponent<BaseEnemy>().SetDestinationReachedAction (()=>{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.steam_walker.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetPathfindingParameter (pathfinding_tile_parameter.any_object);
			}
			if (caller.GetGameGrid().GetValue (GetComponent<BaseEnemy>().GetNextTile(), grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator (object_type.empty))
			{
				if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.steam_walker.mana_cost) == true)
				{
					GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.steam_walker.mana_cost, caller.enemy_options.physical.steam_walker.cooldown);
					GetComponent<Rigidbody>().useGravity = false;
					transform.position = new Vector3 (transform.position.x, 1.5f, transform.position.z);
				}
				else
				{
					GetComponent<BaseEnemy>().SetPathfindingParameter (pathfinding_tile_parameter.empty);
				}
			}
			if (caller.GetGameGrid().GetValue (GetComponent<BaseEnemy>().GetCurrentPosition(), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
				caller.GetGameGrid().GetValue (GetComponent<BaseEnemy>().GetNextTile(), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty))
			{
				GetComponent<Rigidbody>().useGravity = true;
				transform.position = new Vector3 (transform.position.x, transform.localScale.y + 0.01f, transform.position.z);
			}
			});
		}
	}
	
	public class Giant : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Start()
		{
			GetComponent<BaseEnemy>().SetOnHitAction (()=>{
				GetComponent<BaseEnemy>().ManaAddition (caller.enemy_options.physical.giant.mana_gained_on_hit);
			});
			GetComponent<BaseEnemy>().SetManaRegen (false);
			//GetComponent<BaseEnemy>().ManaAddition (- GetComponent<BaseEnemy>().GetCurrentMana());
			GetComponent<BaseEnemy>().SetDestinationReachedAction (()=>{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.giant.mana_cost) == true)
			{
				Collider[] colliders_in_range = Physics.OverlapSphere (transform.position, caller.enemy_options.physical.giant.casting_range);
				List<GameObject> towers_in_range = new List<GameObject>();
				List<GameObject> interactable_towers = new List<GameObject>();
				List<float> tower_distance = new List<float>();
				GameObject target_tower = null;
				foreach (Collider collider in colliders_in_range)
				{
					if (collider.gameObject.tag == "tower")
					{
						towers_in_range.Add (collider.gameObject);
					}
				}
				foreach (GameObject tower in towers_in_range)
				{
					(int x, int z) giant_to_tower_shift = (caller.GetGameGrid().GetXZ (tower.transform.position).x - caller.GetGameGrid().GetXZ (transform.position).x,
					caller.GetGameGrid().GetXZ (tower.transform.position).z - caller.GetGameGrid().GetXZ (transform.position).z);
					if ((giant_to_tower_shift.x == 0 || giant_to_tower_shift.z == 0) && caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ 
					(tower.transform.position).x + giant_to_tower_shift.x, caller.GetGameGrid().GetXZ (tower.transform.position).z + giant_to_tower_shift.z),
					grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty))
					{
						interactable_towers.Add (tower);
					}
					if ((giant_to_tower_shift.x != 0 && giant_to_tower_shift.z != 0) && caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ
					(tower.transform.position).x + giant_to_tower_shift.x, caller.GetGameGrid().GetXZ (tower.transform.position).z + giant_to_tower_shift.z),
					grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
					caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (tower.transform.position).x, caller.GetGameGrid().GetXZ (tower.transform.position).z
					+ giant_to_tower_shift.z), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
					caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (tower.transform.position).x + giant_to_tower_shift.x,
					caller.GetGameGrid().GetXZ (tower.transform.position).z), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty))
					{
						interactable_towers.Add (tower);
					}
				}
				foreach (GameObject tower in interactable_towers)
				{
					tower_distance.Add (Vector3.Distance (transform.position, tower.transform.position));
				}
				if (tower_distance.Count > 0)
				{
					target_tower = towers_in_range [tower_distance.IndexOf(tower_distance.Min())];
				}
				if (target_tower != null)
				{
					(int x, int z) tower_shift = (caller.GetGameGrid().GetXZ (target_tower.transform.position).x - caller.GetGameGrid().GetXZ (transform.position).x,
					caller.GetGameGrid().GetXZ (target_tower.transform.position).z - caller.GetGameGrid().GetXZ (transform.position).z);
					grid_direction moving_direction = grid_direction.up;
					switch (tower_shift)
					{
						case (1, 0):
						moving_direction = grid_direction.right;
						break;

						case (-1, 0):
						moving_direction = grid_direction.left;
						break;

						case (0, 1):
						moving_direction = grid_direction.up;
						break;

						case (0, -1):
						moving_direction = grid_direction.down;
						break;

						case (1, 1):
						moving_direction = grid_direction.top_right;
						break;

						case (-1, 1):
						moving_direction = grid_direction.top_left;
						break;

						case (1, -1):
						moving_direction = grid_direction.bottom_right;
						break;

						case (-1, -1):
						moving_direction = grid_direction.bottom_left;
						break;
					}
					target_tower.GetComponentInParent<Tower.BaseTower>().SetMovingTower (gameObject, caller.enemy_options.physical.giant.tower_push_distance, moving_direction);
					GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.giant.mana_cost, caller.enemy_options.physical.giant.cooldown);
				}
			}
			});
		}
	}
	#endregion
	#region fire classes
	public class MagmaElemental : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;
		[SerializeField] float buff_end_timer = 0;
		float physical_resistance_increase = 0;
		float fire_resistance_increase = 0;
		float frost_resistance_increase = 0;
		float electric_resistance_increase = 0;
		float poison_resistance_increase = 0;
		float magic_resistance_increase = 0;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Start()
		{
			GetComponent<BaseEnemy>().SetOnHitAction (()=>{
				buff_end_timer = 0;
				if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.fire.magma_elemental.mana_cost) == true)
				{
					if (GetComponent<BaseEnemy>().GetResistances().physical < caller.enemy_options.max_resistances)
					{
						float resistance_overflow = (GetComponent<BaseEnemy>().GetResistances().physical + caller.enemy_options.fire.magma_elemental.resistance_increase) - caller.enemy_options.max_resistances;
						if (resistance_overflow < 0)
						{
							resistance_overflow = 0;
						}
						GetComponent<BaseEnemy>().PhysicalResistanceAddition (caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow);
						physical_resistance_increase += caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow;
					}
					if (GetComponent<BaseEnemy>().GetResistances().fire < caller.enemy_options.max_resistances)
					{
						float resistance_overflow = (GetComponent<BaseEnemy>().GetResistances().fire + caller.enemy_options.fire.magma_elemental.resistance_increase) - caller.enemy_options.max_resistances;
						if (resistance_overflow < 0)
						{
							resistance_overflow = 0;
						}
						GetComponent<BaseEnemy>().FireResistanceAddition (caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow);
						fire_resistance_increase += caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow;
					}
					if (GetComponent<BaseEnemy>().GetResistances().frost < caller.enemy_options.max_resistances)
					{
						float resistance_overflow = (GetComponent<BaseEnemy>().GetResistances().frost + caller.enemy_options.fire.magma_elemental.resistance_increase) - caller.enemy_options.max_resistances;
						if (resistance_overflow < 0)
						{
							resistance_overflow = 0;
						}
						GetComponent<BaseEnemy>().FrostResistanceAddition (caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow);
						frost_resistance_increase += caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow;
					}
					if (GetComponent<BaseEnemy>().GetResistances().electric < caller.enemy_options.max_resistances)
					{
						float resistance_overflow = (GetComponent<BaseEnemy>().GetResistances().electric + caller.enemy_options.fire.magma_elemental.resistance_increase) - caller.enemy_options.max_resistances;
						if (resistance_overflow < 0)
						{
							resistance_overflow = 0;
						}
						GetComponent<BaseEnemy>().ElectricResistanceAddition (caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow);
						electric_resistance_increase += caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow;
					}
					if (GetComponent<BaseEnemy>().GetResistances().poison < caller.enemy_options.max_resistances)
					{
						float resistance_overflow = (GetComponent<BaseEnemy>().GetResistances().poison + caller.enemy_options.fire.magma_elemental.resistance_increase) - caller.enemy_options.max_resistances;
						if (resistance_overflow < 0)
						{
							resistance_overflow = 0;
						}
						GetComponent<BaseEnemy>().PoisonResistanceAddition (caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow);
						poison_resistance_increase += caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow;
					}
					if (GetComponent<BaseEnemy>().GetResistances().magic < caller.enemy_options.max_resistances)
					{
						float resistance_overflow = (GetComponent<BaseEnemy>().GetResistances().magic + caller.enemy_options.fire.magma_elemental.resistance_increase) - caller.enemy_options.max_resistances;
						if (resistance_overflow < 0)
						{
							resistance_overflow = 0;
						}
						GetComponent<BaseEnemy>().MagicResistanceAddition (caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow);
						magic_resistance_increase += caller.enemy_options.fire.magma_elemental.resistance_increase - resistance_overflow;
					}
					GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.fire.magma_elemental.mana_cost, caller.enemy_options.fire.magma_elemental.cooldown);
				}
			});
		}

		private void FixedUpdate()
		{
			buff_end_timer += Time.deltaTime;
			if (buff_end_timer >= caller.enemy_options.fire.magma_elemental.time_to_buff_reset)
			{
				buff_end_timer = 0;
				GetComponent<BaseEnemy>().PhysicalResistanceAddition (- physical_resistance_increase);
				GetComponent<BaseEnemy>().FireResistanceAddition (- fire_resistance_increase);
				GetComponent<BaseEnemy>().FrostResistanceAddition (- frost_resistance_increase);
				GetComponent<BaseEnemy>().ElectricResistanceAddition (- electric_resistance_increase);
				GetComponent<BaseEnemy>().PoisonResistanceAddition (- poison_resistance_increase);
				GetComponent<BaseEnemy>().MagicResistanceAddition (- magic_resistance_increase);
				physical_resistance_increase = 0;
				fire_resistance_increase = 0;
				frost_resistance_increase = 0;
				electric_resistance_increase = 0;
				poison_resistance_increase = 0;
				magic_resistance_increase = 0;
			}
		}
	}

	public class Alchemist : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.fire.alchemist.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.fire.alchemist.mana_cost, caller.enemy_options.fire.alchemist.cooldown);
			}
		}
	}

	public class BioArsonist : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;
		GameObject target_body = null;
		float casting_timer = 0;
		int dead_bodies_loaded = 0;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent <BaseEnemy>().CheckUniqueAction() == true)
			{
				casting_timer += Time.deltaTime;
				GetComponent <BaseEnemy>().CastingBarUpdate (casting_timer, caller.enemy_options.fire.bio_arsonist.casting_time);
				if (casting_timer > caller.enemy_options.fire.bio_arsonist.casting_time)
				{
					casting_timer = 0;
					GetComponent <BaseEnemy>().SetUniqueAction (false); 
					Destroy (target_body);
					target_body = null;
					dead_bodies_loaded++;
				}
			}
			if (GetComponent <BaseEnemy>().CheckUniqueAction() == false)
			{
				target_body = GetClosestDeadBody (transform.position, caller.enemy_options.physical.brood_mother.casting_range);
				if (target_body != null)
				{
					GetComponent <BaseEnemy>().SetUniqueAction (true);
					target_body.GetComponent<BaseEnemy.DeadBody>().SetUsed (true);
				}
				else
				{
					if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.fire.bio_arsonist.mana_cost) == true && dead_bodies_loaded > 0)
					{
						Debug.Log ("looking for towers");
						GameObject target_tower = GetClosestTower (gameObject, caller.enemy_options.fire.bio_arsonist.casting_range);
						if (target_tower != null)
						{
							GameObject enemy_object = Instantiate (GetEnemyTemplate (minion_id.ember));
							target_tower.GetComponentInParent<Tower.BaseTower>().HealthAddition (- caller.enemy_options.fire.bio_arsonist.tower_damage);
							(int x, int z) target_tile = caller.GetGameGrid().GetClosestEmptyTile (target_tower.transform.position);
							Enemy enemy = new Enemy (minion_id.ember, caller.GetGameGrid().GetWorldTileCenter (target_tile), caller, enemy_object);
							dead_bodies_loaded--;
							GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.fire.bio_arsonist.mana_cost, caller.enemy_options.fire.bio_arsonist.cooldown);
						}
					}
				}
			}
		}
	}

	public class FireElemental : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.fire.fire_elemental.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.fire.fire_elemental.mana_cost, caller.enemy_options.fire.fire_elemental.cooldown);
			}
		}
	}

	public class Wyrmling : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.fire.wyrmling.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.fire.wyrmling.mana_cost, caller.enemy_options.fire.wyrmling.cooldown);
			}
		}
	}

	public class Corruptor : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.fire.corruptor.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.fire.corruptor.mana_cost, caller.enemy_options.fire.corruptor.cooldown);
			}
		}
	}
	#endregion
	#region frost classes
	public class Lich : MonoBehaviour
	{
		#region variable declarations

		private List<GameObject> list_of_enemies_in_life_steal_range;
		private GameHandler caller;
		private bool in_life_steal_threshold = false;
		
		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}

		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().GetCurrentHealth() <= (caller.enemy_options.frost.lich.life_steal_threshold * 
			GetComponent<BaseEnemy>().GetMaxHealth() ) && in_life_steal_threshold == false)
			{
				in_life_steal_threshold = true;
				list_of_enemies_in_life_steal_range = GetListOfEnemiesInRange ();
			}
			if (in_life_steal_threshold == true && list_of_enemies_in_life_steal_range == null)
			{
				list_of_enemies_in_life_steal_range = GetListOfEnemiesInRange ();
			}
			if (in_life_steal_threshold == true && list_of_enemies_in_life_steal_range != null)
			{
				for (int i = 0; i < list_of_enemies_in_life_steal_range.Count; i++)
				{
					list_of_enemies_in_life_steal_range [i].GetComponent<BaseEnemy>().HealthAddition ((int) (Time.deltaTime * caller.enemy_options.frost.lich.life_steal_rate_per_second));
					GetComponent<BaseEnemy>().HealthAddition ((int) (Time.deltaTime * caller.enemy_options.frost.lich.life_steal_rate_per_second));
					Debug.DrawLine (transform.position, list_of_enemies_in_life_steal_range [i].transform.position, Color.green, Time.fixedDeltaTime);
				}
			}
			if (GetComponent<BaseEnemy>().GetCurrentHealth() > (caller.enemy_options.frost.lich.life_steal_threshold * 
			GetComponent<BaseEnemy>().GetMaxHealth() ) && in_life_steal_threshold == true)
			{
				in_life_steal_threshold = false;
			}
		}

		private List<GameObject> GetListOfEnemiesInRange ()
		{
			Collider[] colliders_in_range = Physics.OverlapSphere(transform.position, caller.enemy_options.frost.lich.life_steal_range);
			List<GameObject> enemies_in_range = new List<GameObject>();
			foreach (Collider collider in colliders_in_range)
			{
				if (collider.gameObject.tag == "enemy" && collider.gameObject != gameObject)
				{
					enemies_in_range.Add (collider.gameObject);
				}
			}
			return enemies_in_range;
		}
	}

	public class LadyFrost : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.frost.lady_frost.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.frost.lady_frost.mana_cost, caller.enemy_options.frost.lady_frost.cooldown);
			}
		}
	}

	public class IceConsumer : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.frost.ice_consumer.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.frost.ice_consumer.mana_cost, caller.enemy_options.frost.ice_consumer.cooldown);
			}
		}
	}

	public class DeadGrove : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.frost.dead_grove.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.frost.dead_grove.mana_cost, caller.enemy_options.frost.dead_grove.cooldown);
			}
		}
	}

	public class SnowDiver : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.frost.snow_diver.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.frost.snow_diver.mana_cost, caller.enemy_options.frost.snow_diver.cooldown);
			}
		}
	}

	public class IceThrall : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.frost.ice_thrall.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.frost.ice_thrall.mana_cost, caller.enemy_options.frost.ice_thrall.cooldown);
			}
		}
	}
	#endregion
	#region electric classes
	public class Absorber :MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.electric.absorber.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.electric.absorber.mana_cost, caller.enemy_options.electric.absorber.cooldown);
			}
		}
	}

	public class StaticCharge :MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.electric.static_charge.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.electric.static_charge.mana_cost, caller.enemy_options.electric.static_charge.cooldown);
			}
		}
	}

	public class ChargeCollector :MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.electric.charge_collector.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.electric.charge_collector.mana_cost, caller.enemy_options.electric.charge_collector.cooldown);
			}
		}
	}

	public class Zap :MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.electric.zap.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.electric.zap.mana_cost, caller.enemy_options.electric.zap.cooldown);
			}
		}
	}

	public class RocketGoblin :MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Start()
		{
			GetComponent<BaseEnemy>().SetDestinationReachedAction (()=>{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.electric.rocket_goblin.mana_cost) == true)
			{
				if (CheckStraightLineJumpViability (caller, GetComponent<BaseEnemy>().GetCurrentPosition (), caller.enemy_options.electric.rocket_goblin.casting_range,
				out grid_direction jump_direction))
				{
					StraightLineJump (1.5f, caller.enemy_options.electric.rocket_goblin.casting_range, gameObject, jump_direction);
					GetComponent<BaseEnemy>().SetUniqueAction (true);
					GetComponent<BaseEnemy>().SetInAir (true);
					GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.electric.rocket_goblin.mana_cost, caller.enemy_options.electric.rocket_goblin.cooldown);
				}
			}
			});
		}
	}

	public class OverCharge :MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.electric.overcharge.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.electric.overcharge.mana_cost, caller.enemy_options.electric.overcharge.cooldown);
			}
		}
	}
	#endregion
	#region poison classes
	public class GreenBelcher : MonoBehaviour
	{
		#region variable declarations

		private GameHandler caller;
		private int last_health_state;
		private GameObject timer;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}

		#endregion

		private void Start()
		{
			last_health_state = GetComponent<BaseEnemy>().GetCurrentHealth();
		}

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().GetCurrentHealth() != last_health_state)
			{
				if (GetComponent<BaseEnemy>().GetCurrentHealth() < last_health_state)
				{
					if (timer != null)
					{
						Destroy (timer);
					}
					int health_difference = last_health_state - GetComponent<BaseEnemy>().GetCurrentHealth();
					Action action = () => {GetComponent<BaseEnemy>().HealthAddition ((int) (health_difference * caller.enemy_options.poison.green_belcher.health_regain));};
					timer = GameHandler.Timer.Create (action, caller.enemy_options.poison.green_belcher.health_regain_time, "regain health", gameObject);
				}
				last_health_state = GetComponent<BaseEnemy>().GetCurrentHealth();
			}
		}
	}

	public class Ooze : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.poison.ooze.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.poison.ooze.mana_cost, caller.enemy_options.poison.ooze.cooldown);
			}
		}
	}

	public class CorpseEater : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.poison.corpse_eater.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.poison.corpse_eater.mana_cost, caller.enemy_options.poison.corpse_eater.cooldown);
			}
		}
	}

	public class Mutant : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.poison.mutant.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.poison.mutant.mana_cost, caller.enemy_options.poison.mutant.cooldown);
			}
		}
	}

	public class GrapplingSpider : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.poison.grappling_spider.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.poison.grappling_spider.mana_cost, caller.enemy_options.poison.grappling_spider.cooldown);
			}
		}
	}

	public class PlagueBearer :MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		#endregion

		private void Start()
		{
			
		}

		private void FixedUpdate()
		{
			
		}

		private List<GameObject> GetListOfEnemiesInRange ()
		{
			Collider[] colliders_in_range = Physics.OverlapSphere(transform.position, caller.enemy_options.frost.lich.life_steal_range);
			List<GameObject> enemies_in_range = new List<GameObject>();
			foreach (Collider collider in colliders_in_range)
			{
				if (collider.gameObject.tag == "enemy" && collider.gameObject != gameObject)
				{
					enemies_in_range.Add (collider.gameObject);
				}
			}
			return enemies_in_range;
		}
	}
	#endregion
	#region magic classes
	public class ShadowFiend : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.magic.shadow_fiend.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.magic.shadow_fiend.mana_cost, caller.enemy_options.magic.shadow_fiend.cooldown);
			}
		}
	}

	public class Shaman : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.magic.shaman.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.magic.shaman.mana_cost, caller.enemy_options.magic.shaman.cooldown);
			}
		}
	}

	public class Necromancer : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.magic.necromancer.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.magic.necromancer.mana_cost, caller.enemy_options.magic.necromancer.cooldown);
			}
		}
	}

	public class ManaAddict : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.magic.mana_addict.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.magic.mana_addict.mana_cost, caller.enemy_options.magic.mana_addict.cooldown);
			}
		}
	}

	public class PhaseShifter : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.magic.phase_shifter.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.magic.phase_shifter.mana_cost, caller.enemy_options.magic.phase_shifter.cooldown);
			}
		}
	}

	public class MindTwister : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void FixedUpdate()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.magic.mind_twister.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.magic.mind_twister.mana_cost, caller.enemy_options.magic.mind_twister.cooldown);
			}
		}
	}
	#endregion

	public static Vector3 GetDirectionVector (grid_direction direction, float movement_speed)
	{
		switch (direction)
		{
			default: return new Vector3 (0, 0, 0);	

			case grid_direction.up:
			return new Vector3 (0, 0, Time.deltaTime * movement_speed);

			case grid_direction.down:
			return new Vector3 (0, 0, Time.deltaTime * - movement_speed);

			case grid_direction.right:
			return new Vector3 (Time.deltaTime * movement_speed, 0, 0);

			case grid_direction.left:
			return new Vector3 (Time.deltaTime * - movement_speed, 0, 0);

			case grid_direction.top_left:
			return new Vector3 (- Time.deltaTime * movement_speed / (float) Math.Pow (2, 0.5f), 0, Time.deltaTime * movement_speed / (float) Math.Pow (2, 0.5f));

			case grid_direction.top_right:
			return new Vector3 (Time.deltaTime * movement_speed / (float) Math.Pow (2, 0.5f), 0, Time.deltaTime * movement_speed / (float) Math.Pow (2, 0.5f));

			case grid_direction.bottom_left:
			return new Vector3 (- Time.deltaTime * movement_speed / (float) Math.Pow (2, 0.5f), 0, - Time.deltaTime * movement_speed / (float) Math.Pow (2, 0.5f));

			case grid_direction.bottom_right:
			return new Vector3 (Time.deltaTime * movement_speed / (float) Math.Pow (2, 0.5f), 0, - Time.deltaTime * movement_speed / (float) Math.Pow (2, 0.5f));
		}
	}

	public static (int x, int z) AddDirectionToPosition (GameGrid.grid_direction direction, (int x, int z) position)
	{
		switch (direction)
		{
			default: return (position.x, position.z);	

			case GameGrid.grid_direction.up: return (position.x, position.z + 1);

			case GameGrid.grid_direction.down: return (position.x, position.z - 1);

			case GameGrid.grid_direction.right: return (position.x + 1, position.z);

			case GameGrid.grid_direction.left: return (position.x - 1, position.z);
		}
	}

	public static bool CheckIfDestinationReached (GameHandler caller, GameObject enemy_object, (int x, int z) destination_tile, GameGrid.grid_direction direction)
	{
		switch (direction)
		{
			default: return false;	

			case GameGrid.grid_direction.up: 
			if (enemy_object.transform.position.z >= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).z)
			{
				return true;
			}
			else
			{
				return false;
			}

			case GameGrid.grid_direction.down: 
			if (enemy_object.transform.position.z <= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).z)
			{
				return true;
			}
			else
			{
				return false;
			}

			case GameGrid.grid_direction.right: 
			if (enemy_object.transform.position.x >= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).x)
			{
				return true;
			}
			else
			{
				return false;
			}

			case GameGrid.grid_direction.left: 
			if (enemy_object.transform.position.x <= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).x)
			{
				return true;
			}
			else
			{
				return false;
			}
			case GameGrid.grid_direction.top_left: 
			if (enemy_object.transform.position.z >= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).z &&
			enemy_object.transform.position.x <= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).x)
			{
				return true;
			}
			else
			{
				return false;
			}

			case GameGrid.grid_direction.top_right: 
			if (enemy_object.transform.position.z >= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).z &&
			enemy_object.transform.position.x >= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).x)
			{
				return true;
			}
			else
			{
				return false;
			}

			case GameGrid.grid_direction.bottom_left: 
			if (enemy_object.transform.position.z <= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).z &&
			enemy_object.transform.position.x <= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).x)
			{
				return true;
			}
			else
			{
				return false;
			}

			case GameGrid.grid_direction.bottom_right: 
			if (enemy_object.transform.position.z <= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).z &&
			enemy_object.transform.position.x >= caller.GetGameGrid().GetWorldTileCenter (destination_tile.x, destination_tile.z, enemy_object.transform.localScale.y).x)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public static GameObject GetClosestDeadBody (Vector3 enemy_position, float range)
	{
		Collider[] colliders_in_range = Physics.OverlapSphere(enemy_position, range);
		List<GameObject> bodies_in_range = new List<GameObject>();
		List<float> bodies_distance = new List<float>();
		foreach (Collider collider in colliders_in_range)
		{
			if (collider.gameObject.tag == "dead")
			{
				if (collider.GetComponent<BaseEnemy.DeadBody>().GetUsed() == false)
				{
					bodies_in_range.Add (collider.gameObject);
				}
			}
		}
		if (bodies_in_range.Count != 0)
		{
			foreach (GameObject body in bodies_in_range)
			{
				bodies_distance.Add (Vector3.Distance (enemy_position, body.transform.position));
			}
			return bodies_in_range [bodies_distance.IndexOf (bodies_distance.Min () )];
		}
		else
		{
			return null;
		}
	}

	public static bool CheckStraightLineJumpViability (GameHandler caller, (int x, int z) position, int range, out grid_direction jump_direction)
	{
		caller.GetPathfinding().CheckPath (position, caller.GetGameGrid(), pathfinding_tile_parameter.empty, out int original_path_length);
		List<grid_direction> directions_to_check_list = new List<grid_direction> ();
		List<int> direction_path_length_list = new List<int> ();
		if (position.x + range < caller.GetGameGrid().length_x)
		{
			if (caller.GetGameGrid().GetValue (position.x + range, position.z, grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
			caller.GetGameGrid().GetValue (position.x + 1, position.z, grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator  (object_type.empty) )
			{
				directions_to_check_list.Add (grid_direction.right);
			}
		}
		if (position.x - range >= 0)
		{
			if (caller.GetGameGrid().GetValue (position.x - range, position.z, grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
			caller.GetGameGrid().GetValue (position.x - 1, position.z, grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator  (object_type.empty) )
			{
				directions_to_check_list.Add (grid_direction.left);
			}
		}
		if (position.z + range < caller.GetGameGrid().width_z)
		{
			if (caller.GetGameGrid().GetValue (position.x, position.z + range, grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
			caller.GetGameGrid().GetValue (position.x, position.z + 1, grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator  (object_type.empty) )
			{
				directions_to_check_list.Add (grid_direction.up);
			}
		}
		if (position.z - range >= 0)
		{
			if (caller.GetGameGrid().GetValue (position.x, position.z - range, grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
			caller.GetGameGrid().GetValue (position.x, position.z - 1, grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator  (object_type.empty) )
			{
				directions_to_check_list.Add (grid_direction.down);
			}
		}
		if (directions_to_check_list.Count > 0)
		{
			foreach (grid_direction direction in directions_to_check_list)
			{
				(int x, int z) position_temp = (0, 0);
				switch (direction)
				{
					case grid_direction.up:
					position_temp = (position.x, position.z + range);
					break;

					case grid_direction.down:
					position_temp = (position.x, position.z - range);
					break;

					case grid_direction.left:
					position_temp = (position.x - range, position.z);
					break;

					case grid_direction.right:
					position_temp = (position.x + range, position.z);
					break;
				}
				if (caller.GetPathfinding().CheckPath (position_temp, caller.GetGameGrid(), pathfinding_tile_parameter.empty, out int new_path_length) == true)
				{
					if (new_path_length < original_path_length)
					{
						direction_path_length_list.Add (new_path_length);
					}
				}
			}
			if (direction_path_length_list.Count > 0)
			{
				jump_direction = directions_to_check_list [direction_path_length_list.IndexOf (direction_path_length_list.Min () ) ];
				return true;
			}
		}
		jump_direction = grid_direction.down;
		return false;
	}

	public static void StraightLineJump (float peak_jump_height, int jump_distance, GameObject enemy, grid_direction jump_direction)
	{
		Rigidbody enemy_rigid_body = enemy.GetComponent<Rigidbody> ();
		float sin_alpha = 4 * peak_jump_height / (float) Math.Pow ((Math.Pow (peak_jump_height, 2) + Math.Pow (jump_distance, 2)), 0.5f);
		float cos_alpha = jump_distance / (float) Math.Pow ((Math.Pow (peak_jump_height, 2) + Math.Pow (jump_distance, 2)), 0.5f);
		float starting_speed = (float) Math.Pow ((2 * peak_jump_height * 9.81f / Math.Pow (sin_alpha, 2)), 0.5f);
		enemy_rigid_body.AddRelativeForce (Vector3.up * sin_alpha * starting_speed, ForceMode.Impulse);
		switch (jump_direction)
		{  
			case grid_direction.down:
			enemy_rigid_body.AddRelativeForce (Vector3.back * cos_alpha * starting_speed, ForceMode.Impulse);
			break;

			case grid_direction.up:
			enemy_rigid_body.AddRelativeForce (Vector3.forward * cos_alpha * starting_speed, ForceMode.Impulse);
			break;

			case grid_direction.left:
			enemy_rigid_body.AddRelativeForce (Vector3.left * cos_alpha * starting_speed, ForceMode.Impulse);
			break;

			case grid_direction.right:
			enemy_rigid_body.AddRelativeForce (Vector3.right * cos_alpha * starting_speed, ForceMode.Impulse);
			break;
		}
		enemy_rigid_body.freezeRotation = true;
	}

	private static GameObject GetClosestEnemy (GameObject this_enemy, float range)
	{
		Collider[] colliders_in_range = Physics.OverlapSphere(this_enemy.transform.position, range);
		List<GameObject> enemies_in_range = new List<GameObject>();
		List<float> enemies_distance = new List<float>();
		GameObject closest_enemy = null;
		foreach (Collider collider in colliders_in_range)
		{
			if (collider.gameObject.tag == "enemy" && collider.gameObject != this_enemy)
			{
				enemies_in_range.Add (collider.gameObject);
			}
		}
		foreach (GameObject enemy in enemies_in_range)
		{
			enemies_distance.Add (Vector3.Distance (this_enemy.transform.position, enemy.transform.position));
		}
		if (enemies_distance.Count > 0)
		{
			closest_enemy = enemies_in_range [enemies_distance.IndexOf(enemies_distance.Min())];
		}
		return closest_enemy;
	}

	private static GameObject GetClosestTower (GameObject this_enemy, float range)
	{
		Collider[] colliders_in_range = Physics.OverlapSphere(this_enemy.transform.position, range);
		List<GameObject> towers_in_range = new List<GameObject>();
		List<float> towers_distance = new List<float>();
		GameObject closest_tower = null;
		foreach (Collider collider in colliders_in_range)
		{
			if (collider.gameObject.tag == "tower")
			{
				towers_in_range.Add (collider.gameObject);
			}
		}
		foreach (GameObject tower in towers_in_range)
		{
			towers_distance.Add (Vector3.Distance (this_enemy.transform.position, tower.transform.position));
		}
		if (towers_distance.Count > 0)
		{
			closest_tower = towers_in_range [towers_distance.IndexOf(towers_distance.Min())];
		}
		return closest_tower;
	}

	public static GameHandler GetGameHandler ()
	{
		return GameObject.Find ("GameHandler").GetComponent<GameHandler>();
	}
}
