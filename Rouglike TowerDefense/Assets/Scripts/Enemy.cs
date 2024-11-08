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

public class Enemy
{
	#region enum declarations

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

	public enum minion_id
	{
		spider,
		treant,
		zombie
	}

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

	public static GameObject GetEnemyTemplate (minion_id type)
	{
		switch (type)
		{
			case minion_id.spider: return GameObject.Find ("Spider Template");

			case minion_id.treant: return GameObject.Find ("Treant Template");

			case minion_id.zombie: return GameObject.Find ("Zombie Template");

			default: return null;
		}
	}
	#endregion

	//spawner constructor
	public Enemy (Vector3 position, GameHandler caller)
	{
		GameObject spawner = new GameObject("Spawner " + caller.GetSpawnerCounter ());
		spawner.transform.parent = GameObject.Find ("Spawner Initialized").transform;
		caller.AddSpawnerToSpawnerList (spawner);
		var xz = caller.GetGameGrid().GetXZ(position);
		caller.GetGameGrid().SetValue (xz.x, xz.z, GameGrid.grid_parameter.object_type, GameGrid.object_type.spawner);
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
			//enemy.AddComponent<ShieldSkeleton>();
			break;

			case enemy_id.goblin_construction_team:
			enemy.AddComponent <GoblinConstructionTeam>();
			break;

			case enemy_id.brood_mother:
			enemy.AddComponent <BroodMother>().SetVariables (caller);
			break;

			case enemy_id.werewolf:
			enemy.AddComponent <Werewolf>();
			break;

			case enemy_id.steam_walker:
			enemy.AddComponent <SteamWalker>();
			break;

			case enemy_id.giant:
			enemy.AddComponent <Giant>();
			break;

			case enemy_id.magma_elemental:
			enemy.AddComponent <MagmaElemental>();
			break;

			case enemy_id.alchemist:
			enemy.AddComponent <Alchemist>();
			break;

			case enemy_id.bio_arsonist:
			enemy.AddComponent <BioArsonist>();
			break;

			case enemy_id.fire_elemental:
			enemy.AddComponent <FireElemental>();
			break;

			case enemy_id.wyrmling:
			enemy.AddComponent <Wyrmling>();
			break;

			case enemy_id.corruptor:
			enemy.AddComponent <Corruptor>();
			break;

			case enemy_id.lich:
			enemy.AddComponent <Lich>();
			break;

			case enemy_id.lady_frost:
			enemy.AddComponent <LadyFrost>();
			break;

			case enemy_id.ice_consumer:
			enemy.AddComponent <IceConsumer>();
			break;

			case enemy_id.dead_grove:
			enemy.AddComponent <DeadGrove>();
			break;

			case enemy_id.snow_diver:
			enemy.AddComponent <SnowDiver>();
			break;

			case enemy_id.ice_thrall:
			enemy.AddComponent <IceThrall>();
			break;

			case enemy_id.absorber:
			enemy.AddComponent <Absorber>();
			break;

			case enemy_id.static_charge:
			enemy.AddComponent <StaticCharge>();
			break;

			case enemy_id.charge_collector:
			enemy.AddComponent <ChargeCollector>();
			break;

			case enemy_id.zap:
			enemy.AddComponent <Zap>();
			break;

			case enemy_id.rocket_goblin:
			enemy.AddComponent <RocketGoblin>();
			break;

			case enemy_id.overcharge:
			enemy.AddComponent <OverCharge>();
			break;

			case enemy_id.green_belcher:
			enemy.AddComponent<GreenBelcher>().SetVariables (caller);
			break;

			case enemy_id.ooze:
			enemy.AddComponent <Ooze>();
			break;

			case enemy_id.corpse_eater:
			enemy.AddComponent <CorpseEater>();
			break;

			case enemy_id.mutant:
			enemy.AddComponent <Mutant>();
			break;

			case enemy_id.grappling_spider:
			enemy.AddComponent <GrapplingSpider>();
			break;

			case enemy_id.plague_bearer:
			enemy.AddComponent <PlagueBearer>();
			break;

			case enemy_id.shadow_fiend:
			enemy.AddComponent <Absorber>();
			break;

			case enemy_id.shaman:
			enemy.AddComponent <Shaman>();
			break;

			case enemy_id.necromancer:
			enemy.AddComponent <Necromancer>();
			break;

			case enemy_id.mana_addict:
			enemy.AddComponent <ManaAddict>();
			break;

			case enemy_id.phase_shifter:
			enemy.AddComponent <PhaseShifter>();
			break;

			case enemy_id.mind_twister:
			enemy.AddComponent <MindTwister>();
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

			case minion_id.treant:
			
			break;

			case minion_id.zombie:
			
			break;
		}
	}

	public class Spawner : MonoBehaviour
	{
		#region variable declarations

		private bool draw_path = false;
		private float cooldown;
		private Vector3 position;
		private GameHandler caller;
		private GameObject spawner_template, this_spawner, enemy_object;
		private (int x, int z) [] path_tuple_array = new (int x, int z) [0];

		public void SetVariables (Vector3 position, GameHandler caller)
		{
			cooldown = caller.testing_options.spawner_cooldown;
			this.caller = caller;
			this.position = position;
			SetNewPathfinding ();
		}

		#endregion
		
		private void Start()
		{
			spawner_template = GameObject.Find("Spawner");
			this_spawner = Instantiate(spawner_template);
			this_spawner.name = "Spawner Object";
			this_spawner.transform.position = position;
			this_spawner.transform.parent = transform;
		}

		public void SpawnEnemy (Spawner parent)
		{
			bool repeat = true;
			if ((caller.GetGameGrid().GetValue(caller.GetGameGrid().GetXZ(position).x, caller.GetGameGrid().GetXZ(position).z,
			GameGrid.grid_parameter.enemy) == caller.GetGameGrid().EnumTranslator(GameGrid.enemy.empty)))
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
			Action action;
			GameHandler.Timer.Create (action = () => {
			if (caller.CheckIfWaveActive () == true)
			{
				parent.SpawnEnemy (parent);
			}
			}, cooldown, "spawning cooldown", gameObject);
		}

		//continusly draws the path from the spawner to the core
		public void DrawPathLines ()
		{
			Action action;
			GameHandler.Timer.Create (action = () => {
			for (int i = 0; i < path_tuple_array.Length - 1; i++)
			{
				Debug.DrawLine (caller.GetGameGrid().GetWorldTileCenter(path_tuple_array [i].x, path_tuple_array [i].z, 0),
				caller.GetGameGrid().GetWorldTileCenter(path_tuple_array [i + 1].x, path_tuple_array [i + 1].z, 0), Color.red, 0.25f);
			}
			DrawPathLines ();
			}, 0.25f, "draw path", gameObject);
		}

		//determines which core is the closest and establishes the shortest path to it
		public void SetNewPathfinding ()
		{
			caller.GetPathfinding().FindPath (position, caller.GetGameGrid(), out path_tuple_array, PathFinding.pathfinding_tile_parameter.empty);
			if (draw_path == false)
			{
				draw_path = true;
				DrawPathLines ();
			}
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
		private Action DeathAction = null;
		private (int x, int z) current_position, next_tile;
		private (int x, int z) [] path_tuple_array;
		private bool moving = false, unique_action = false, cooldown = false, minion;
		private int counter;
		private float mana_timer = 1;
		private grid_direction direction;

		#region stats

		private int health, max_health;
		private int mana, max_mana;
		private float healing_multiplier;
		private float movement_speed;
		private float physical_resistance;
		private float fire_resistance;
		private float frost_resistance;
		private float electric_resistance;
		private float poison_resistance;
		private float magic_resistance;

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

		public void SetNewPathfinding ()
		{
			caller.GetPathfinding().FindPath (transform.position, caller.GetGameGrid(), out path_tuple_array, PathFinding.pathfinding_tile_parameter.empty);
		}

		public void SetUniqueAction (bool state)
		{
			unique_action = state;
		}

		public void SetDeathAction (Action death_action)
		{
			DeathAction = death_action;
		}

		#endregion

		private void Start()
		{
			#region health & mana bars setup
			GameObject enemy_bar_canvas = Instantiate (GameObject.Find ("Enemy Bar Canvas") );
			enemy_bar_canvas.transform.SetParent (transform, true);
			enemy_bar_canvas.transform.localPosition = new Vector3 (0, 1, 0.25f);
			health_bar = this_test_enemy.transform.GetChild(2).GetChild(0).GetChild(2).gameObject;
			mana_bar = this_test_enemy.transform.GetChild(2).GetChild(1).GetChild(2).gameObject;
			casting_bar = this_test_enemy.transform.GetChild(2).GetChild(2).GetChild(2).gameObject;
			HealthBarToggle (false);
			ManaBarToggle (false);
			CastingBarToggle (false);
			#endregion
			SetNewPathfinding ();
			counter = path_tuple_array.Length - 2;
		}

		private void Update()
		{
			//mana regen
			if (mana < max_mana)
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
					next_tile = path_tuple_array [counter];
					direction = caller.GetGameGrid().GetMovementDirection (next_tile, current_position);
					moving = true;
					counter--;
				}
				//moving enemy object
				this_test_enemy.transform.position += GetDirectionVector (direction) * Time.deltaTime * movement_speed;
				//action triggered when enemy dies while walking
				if (health <= 0)
				{
					caller.GetGameGrid().SetValue (current_position.x, current_position.z, GameGrid.grid_parameter.enemy, GameGrid.enemy.empty);
					DeathActionAndSelfDestroy();
				}
				//action upon reaching the center of the next destination tile
				if (CheckIfDestinationReached (caller, this_test_enemy, next_tile, direction))
				{
					//changing tile parameter values
					caller.GetGameGrid().SetValue (current_position.x, current_position.z, GameGrid.grid_parameter.enemy, GameGrid.enemy.empty);
					current_position = next_tile;
					if (counter == -1)
					{
						//action triggered when enemy reaches core
						caller.testing_options.core_health--;
						Debug.Log ("core health = " + caller.testing_options.core_health);
						caller.RemoveEnemyFromEnemyList (gameObject);
						Destroy (gameObject);
					}
					moving = false;
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

		public int GetDistanceFromCore ()
		{
			return counter;
		}
		#endregion

		#region check cooldown and mana, set mana and cooldown, check if moving, check unique action, casting bar update, death action and self destroy

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

		public bool CheckIfMoving ()
		{
			return moving;
		}

		public bool CheckUniqueAction ()
		{
			return unique_action;
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

		#region stat modification & damage calulation

		public void MovementSpeedMultiplier (float speed_multiplier)
		{
			movement_speed *= speed_multiplier;
		}

		public void MaxHealthAddition (int max_health_increase)
		{
			max_health += max_health_increase;
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

		public int CalculateIncomingDamage ((int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage) damage_tuple)
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
			return total_damage;
		}

		public void HealthAddition (int health_increase)
		{
			health += health_increase;
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



		#endregion

		#region dead body - create, dead body class
		private void CreateDeadBody ()
		{
			GameObject dead_body = Instantiate (GameObject.Find ("Dead Template") );
			dead_body.transform.SetParent (GameObject.Find ("Dead Bodies").transform, false);
			dead_body.transform.position = transform.position;
			dead_body.transform.Rotate (0, caller.RandomInt (0, 360), 0);
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

			private void Update()
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
			if (collider.gameObject.tag == "test tower projectile")
			{
				HealthAddition (-CalculateIncomingDamage (collider.GetComponent<Tower.ShotProjectile>().damage_tuple));
			}
			if (collider.gameObject.tag == "oil")
			{
				MovementSpeedMultiplier (caller.tower_options.oil.slow_amount);
			}
		}

		private void OnTriggerExit (Collider collider)
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

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.shield_skeleton.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.shield_skeleton.mana_cost, caller.enemy_options.physical.shield_skeleton.cooldown);
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

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckUniqueAction() == true)
			{
				building_timer += Time.deltaTime;
				if (building_timer > 1)
				{
					(int x, int z) scaffolding_position = AddDirectionToPosition (jump_direction, GetComponent<BaseEnemy>().GetCurrentPosition());
					caller.GetGameGrid().SetValue (scaffolding_position, grid_parameter.object_type, object_type.empty);
					GetComponent<BaseEnemy>().SetUniqueAction (false);
				}
			}
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.goblin_construction_team.mana_cost) == true)
			{
				if (CheckStraightLineJumpViability (caller, GetComponent<BaseEnemy>().GetCurrentPosition (), 1, out jump_direction) == true)
				{
					GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.goblin_construction_team.mana_cost, caller.enemy_options.physical.goblin_construction_team.cooldown);
					GetComponent<BaseEnemy>().SetUniqueAction (true);
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

		private void Update()
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

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.werewolf.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.werewolf.mana_cost, caller.enemy_options.physical.werewolf.cooldown);
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

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.steam_walker.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.steam_walker.mana_cost, caller.enemy_options.physical.steam_walker.cooldown);
			}
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

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.physical.giant.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.physical.giant.mana_cost, caller.enemy_options.physical.giant.cooldown);
			}
		}
	}
	#endregion
	#region fire classes
	public class MagmaElemental : MonoBehaviour
	{
		#region variable declarations

		GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.fire.magma_elemental.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.fire.magma_elemental.mana_cost, caller.enemy_options.fire.magma_elemental.cooldown);
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

		private void Update()
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

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
		#endregion

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.fire.bio_arsonist.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.fire.bio_arsonist.mana_cost, caller.enemy_options.fire.bio_arsonist.cooldown);
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.electric.rocket_goblin.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.electric.rocket_goblin.mana_cost, caller.enemy_options.electric.rocket_goblin.cooldown);
			}
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
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

		private void Update()
		{
			if (GetComponent<BaseEnemy>().CheckCooldownAndMana (caller.enemy_options.magic.mind_twister.mana_cost) == true)
			{
				GetComponent<BaseEnemy>().SetManaAndCooldown (caller.enemy_options.magic.mind_twister.mana_cost, caller.enemy_options.magic.mind_twister.cooldown);
			}
		}
	}
	#endregion

	public static Vector3 GetDirectionVector (GameGrid.grid_direction direction)
	{
		switch (direction)
		{
			default: return new Vector3 (0, 0, 0);	

			case GameGrid.grid_direction.up: return new Vector3 (0, 0, 1);

			case GameGrid.grid_direction.down: return new Vector3 (0, 0, -1);

			case GameGrid.grid_direction.right: return new Vector3 (1, 0, 0);

			case GameGrid.grid_direction.left: return new Vector3 (-1, 0, 0);
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
		List<grid_direction> directions_to_check_list = new List<grid_direction> ();
		List<int> direction_path_length_list = new List<int> ();
		for (int x = position.x - range; x < position.x; x++)
		{
			if (caller.GetGameGrid().GetValue (x, position.z, grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator (object_type.empty) &&
			caller.GetGameGrid().GetValue (position.x - (range + 1), position.z, grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator  (object_type.empty) )
			{
				directions_to_check_list.Add (grid_direction.down);
			}
		}
		for (int x = position.x + range; x > position.x; x--)
		{
			if (caller.GetGameGrid().GetValue (x, position.z, grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator (object_type.empty) &&
			caller.GetGameGrid().GetValue (position.x + (range + 1), position.z, grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator  (object_type.empty) )
			{
				directions_to_check_list.Add (grid_direction.up);
			}
		}
		for (int z = position.z - range; z < position.z; z++)
		{
			if (caller.GetGameGrid().GetValue (position.x, z, grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator (object_type.empty) &&
			caller.GetGameGrid().GetValue (position.x, position.z - (range + 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator  (object_type.empty) )
			{
				directions_to_check_list.Add (grid_direction.left);
			}
		}
		for (int z = position.z + range; z > position.z; z++)
		{
			if (caller.GetGameGrid().GetValue (position.x, z, grid_parameter.object_type) != caller.GetGameGrid().EnumTranslator (object_type.empty) &&
			caller.GetGameGrid().GetValue (position.x, position.z + (range + 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator  (object_type.empty) )
			{
				directions_to_check_list.Add (grid_direction.right);
			}
		}
		if (directions_to_check_list.Count > 0)
		{
			foreach (grid_direction direction in directions_to_check_list)
			{
				(int x, int z) [] direction_path_array = new (int x, int z) [] {};
				if (caller.GetPathfinding().FindPath (position, caller.GetGameGrid(), out direction_path_array, PathFinding.pathfinding_tile_parameter.empty) == true)
				{
					direction_path_length_list.Add (direction_path_array.Length);
				}
				else
				{
					directions_to_check_list.Remove (direction);
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

	public static GameHandler GetGameHandler ()
	{
		return GameObject.Find ("GameHandler").GetComponent<GameHandler>();
	}
}
