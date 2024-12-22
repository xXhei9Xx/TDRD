using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Enemy;
using Unity.VisualScripting;
using static EnemyPicker;
using CodeMonkey.Utils;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using static GameGrid;
using System.IO;
using static Core;

public class GameHandler : MonoBehaviour
{
	#region variable declarations

	[SerializeField] public TestingOptions testing_options;
	[SerializeField] public GameplayOptions gameplay_options;
	[SerializeField] public DeckOptions deck_options;
	[SerializeField] public TowerOptions tower_options;
	[SerializeField] public EnemyOptions enemy_options;
	new GameObject camera;
	private (float x, float y, float z) board_center_position_tuple;
	private int wave_number = 0, tower_counter = 0, enemy_counter = 0, spawner_counter = 0, core_counter = 0, mana_amount = 0, turns_since_last_spawner = 0;
	private List<(int x, int z, grid_direction direction)> mana_outline_tuple_list = new List<(int x, int z, grid_direction direction)> ();
	private GameObject wave_button;
	private GameObject time_buttons, time_button_pause, time_button_0_25, time_button_0_5, time_button_1, time_button_2, time_button_4;
	private GameObject add_card_button;
	private GameObject upgrade_card_button;
	private GameObject object_under_cursor = null;
	private TextMeshProUGUI mana_display_text;
	private TextMeshProUGUI wave_display_text;
	private CardHandler card_handler;
	private GameGrid grid;
	private SaveHandler save_handler;
	private EnemyPicker enemy_picker;
	private DamageChart damage_chart;
	private bool wave_active = false, inspector_window_open = false, first_path = false;
	private List<enemy_id> enemy_to_spawn_list = new List<enemy_id> {};
	private List<CoreObject> cores_list = new List<CoreObject> ();
	private List<GameObject> enemy_list = new List<GameObject> ();
	private List<GameObject> spawner_list = new List<GameObject> ();
	private PathFinding pathfinding;
	private Color time_button_color_enter, time_button_color_exit;
	private camera_directions camera_direction = camera_directions.up;
	private InspectorHandler inspector_handler;
	private float pathfinding_timer = 0;
	private List<(int x, int z)> spawner_tile_list = new List<(int x, int z)> {}, core_tile_list = new List<(int x, int z)> {};

	#region testing options
	[Serializable] public class TestingOptions
	{
		[SerializeField] public bool display_grid_text = true;
		[SerializeField] public bool manual_spawn_list = false;
		[SerializeField] public bool random_core_positions = true;
		[SerializeField] public bool random_spawner_positions = true;
		[SerializeField] [Range (1,4)] public int amount_of_turns_to_new_spawner;
		[SerializeField] [Range (1,10)] public int spawner_lifetime_amount_of_turns;
		[SerializeField] public List<enemy_id> manual_enemy_spawn_list;
		[SerializeField] public int first_wave_amount_of_enemies = 5;
		[SerializeField] public int enemies_per_wave_increment = 5;
		[SerializeField] public int amount_of_cores = 1;
		[SerializeField] public int amount_of_spawners = 1;
		[SerializeField] public int core_health = 10;
		[SerializeField] public float  spawner_cooldown = 2;
	}
	#endregion
	#region gameplay options
	[Serializable] public class GameplayOptions
	{
		[SerializeField] public MapOptions map;
		[SerializeField] public UIOptions ui;
		[SerializeField] public Controls controls;
	}

	[Serializable] public class MapOptions
	{
		[SerializeField] public bool load_save = false;
		[SerializeField] public bool random_save = false;
		[SerializeField] public string save_name;
		[SerializeField] [Range (10, 30)] public int length_x = 10;
		[SerializeField] [Range (10, 30)] public int width_z = 10;
		[SerializeField] [Range (1, 3)] public float cell_length_x = 1;
		[SerializeField] [Range (1, 3)] public float cell_width_z = 1;
	}

	[Serializable] public class UIOptions
	{
		[SerializeField] public Material outline_off_pointer_material;
		[SerializeField] public Material outline_on_pointer_material;
		[SerializeField] [Range (0.1f, 2)] public float card_movement_time = 0.5f;
		[SerializeField] [Range (0.5f, 4)] public float camera_movement_speed;
		[SerializeField] [Range (0.5f, 4)] public float tower_pushing_speed;
		[SerializeField] [Range (4f, 6f)] public float space_between_cards;
	}

	[Serializable] public class Controls
	{
		public int MouseButtonTranslator (MouseButton button)
		{
			switch (button)
			{
				case MouseButton.Left: return 0;

				case MouseButton.Right: return 1;

				case MouseButton.Middle: return 2;

				default: return 0;
			}
		}

		[SerializeField] public MouseButton drag_card;
		[SerializeField] public MouseButton cancel;
		[SerializeField] public KeyCode move_left;
		[SerializeField] public KeyCode move_right;
		[SerializeField] public KeyCode move_up;
		[SerializeField] public KeyCode move_down;
		[SerializeField] public KeyCode rotate_left;
		[SerializeField] public KeyCode rotate_right;
	}

	#endregion
	#region deck options
	[Serializable] public class DeckOptions
	{
		[SerializeField] public List<CardHandler.card_id> cards_in_deck;
		[SerializeField] [Range (1, 5)] public int starting_card_amount_in_hand = 5;
		[SerializeField] [Range (5, 10)] public int max_hand_size = 8;
		[SerializeField] [Range (2, 5)]public int amount_of_new_cards_to_choose = 3;
		[SerializeField] [Range (2, 5)]public int amount_of_upgrades_to_choose;
		[SerializeField] public TowerUpgradesOptions tower_upgrades;
		[SerializeField] public AbilityUpgradesOptions ability_upgrades;
	}

	#region tower upgrades
	[Serializable] public class TowerUpgradesOptions
	{
		[SerializeField] public WallUpgradeOptions wall;
		[SerializeField] public ManaGeneratorOptions mana;
		[SerializeField] public TestTowerUpgradeOptions test;
		[SerializeField] public TowerBlizzardUpgradeOptions blizzard;
		[SerializeField] public TowerTeleportUpgradeOptions teleport;
		[SerializeField] public TowerWaterHoseUpgradeOptions water_hose;
		[SerializeField] public TowerLightningUpgradeOptions lightning;
		[SerializeField] public TowerOilUpgradeOptions oil;
		[SerializeField] public TowerFlameThrowerUpgradeOptions flamethrower;
	}

	[Serializable] public class WallUpgradeOptions
	{
		
	}

	[Serializable] public class ManaGeneratorUpgradeOptions
	{
		
	}

	[Serializable] public class TestTowerUpgradeOptions
	{
		[SerializeField] [Range (1, 5)] public int physical_damage;
		[SerializeField] [Range (1, 5)] public int fire_damage;
		[SerializeField] [Range (1, 5)] public int frost_damage;
		[SerializeField] [Range (1, 5)] public int electric_damage;
		[SerializeField] [Range (1, 5)] public int poison_damage;
		[SerializeField] [Range (1, 5)] public int magic_damage;
		[SerializeField] [Range (0.5f, 1f)] public float cooldown;
		[SerializeField] [Range (1, 5)] public int mana_requirement;
		[SerializeField] [Range (1f, 4f)] public float health;
	}

	[Serializable] public class TowerBlizzardUpgradeOptions
	{
		
	}

	[Serializable] public class TowerTeleportUpgradeOptions
	{
		
	}

	[Serializable] public class TowerWaterHoseUpgradeOptions
	{
		
	}

	[Serializable] public class TowerLightningUpgradeOptions
	{
		
	}

	[Serializable] public class TowerOilUpgradeOptions
	{
		
	}

	[Serializable] public class TowerFlameThrowerUpgradeOptions
	{
		
	}
	#endregion
	#region ability upgrades
	[Serializable] public class AbilityUpgradesOptions
	{
		[SerializeField] public AbilityRepairOptions repair;
	}

	[Serializable] public class AbilityRepairOptions
	{
		//[SerializeField] public
	}
	#endregion
	#endregion
	#region tower options
	[Serializable] public class TowerOptions
	{
		[SerializeField] public WallOptions wall;
		[SerializeField] public ManaGeneratorOptions mana_generator;
		[SerializeField] public TestTowerOptions test;
		[SerializeField] public TowerBlizzardOptions blizzard;
		[SerializeField] public TowerTeleportOptions teleport;
		[SerializeField] public TowerWaterHoseOptions water_hose;
		[SerializeField] public TowerLightningOptions lightning;
		[SerializeField] public TowerOilOptions oil;
		[SerializeField] public TowerFlameThrowerOptions flamethrower;
	}

	[Serializable] public class WallOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (0, 100)] public int mana_requirement;
	}

	[Serializable] public class ManaGeneratorOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (1, 10)] public int range;
		[SerializeField] [Range (1, 100)] public int mana_generated = 10;
	}

	[Serializable] public class TestTowerOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (0, 100)] public int mana_requirement;
		[SerializeField] [Range (0, 100)] public int physical_damage;
		[SerializeField] [Range (0, 100)] public int fire_damage;
		[SerializeField] [Range (0, 100)] public int frost_damage;
		[SerializeField] [Range (0, 100)] public int electric_damage;
		[SerializeField] [Range (0, 100)] public int poison_damage;
		[SerializeField] [Range (0, 100)] public int magic_damage;
		[SerializeField] [Range (1, 10)] public int range;
		[SerializeField] [Range (0, 5)] public float cooldown_time;
		[SerializeField] [Range (0.25f, 5)] public float projectile_speed = 2;
	}

	[Serializable] public class TowerBlizzardOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (0, 100)] public int mana_requirement;
		[SerializeField] [Range (0, 100)] public int physical_damage;
		[SerializeField] [Range (0, 100)] public int fire_damage;
		[SerializeField] [Range (0, 100)] public int frost_damage;
		[SerializeField] [Range (0, 100)] public int electric_damage;
		[SerializeField] [Range (0, 100)] public int poison_damage;
		[SerializeField] [Range (0, 100)] public int magic_damage;
		[SerializeField] [Range (1, 10)] public int range;
		[SerializeField] [Range (0, 5)] public float cooldown_time;
		[SerializeField] public float slow_amount;
		[SerializeField] public float slow_duration;
		[SerializeField] public float cooldown;
	}

	[Serializable] public class TowerTeleportOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (0, 100)] public int mana_requirement;
		[SerializeField] [Range (1, 10)] public int range;
	}

	[Serializable] public class TowerWaterHoseOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (0, 100)] public int mana_requirement;
		[SerializeField] [Range (0, 100)] public int physical_damage;
		[SerializeField] [Range (0, 100)] public int fire_damage;
		[SerializeField] [Range (0, 100)] public int frost_damage;
		[SerializeField] [Range (0, 100)] public int electric_damage;
		[SerializeField] [Range (0, 100)] public int poison_damage;
		[SerializeField] [Range (0, 100)] public int magic_damage;
		[SerializeField] [Range (1, 10)] public int range;
		[SerializeField] [Range (0, 5)] public float cooldown_time;
		[SerializeField] public float push_distance;
		[SerializeField] public float cooldown;
		[SerializeField] public int wall_collision_damage;
	}

	[Serializable] public class TowerLightningOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (0, 100)] public int mana_requirement;
		[SerializeField] [Range (0, 100)] public int physical_damage;
		[SerializeField] [Range (0, 100)] public int fire_damage;
		[SerializeField] [Range (0, 100)] public int frost_damage;
		[SerializeField] [Range (0, 100)] public int electric_damage;
		[SerializeField] [Range (0, 100)] public int poison_damage;
		[SerializeField] [Range (0, 100)] public int magic_damage;
		[SerializeField] [Range (1, 10)] public int range;
		[SerializeField] [Range (0, 5)] public float cooldown_time;
		[SerializeField] public int amount_of_bounces;
		[SerializeField] public float bounce_range;
		[SerializeField] public  int damage = 10;
		[SerializeField] public float cooldown;
	}

	[Serializable] public class TowerOilOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (0, 100)] public int mana_requirement;
		[SerializeField] [Range (0, 100)] public int physical_damage;
		[SerializeField] [Range (0, 100)] public int fire_damage;
		[SerializeField] [Range (0, 100)] public int frost_damage;
		[SerializeField] [Range (0, 100)] public int electric_damage;
		[SerializeField] [Range (0, 100)] public int poison_damage;
		[SerializeField] [Range (0, 100)] public int magic_damage;
		[SerializeField] [Range (1, 10)] public int range;
		[SerializeField] [Range (0, 5)] public float cooldown_time;
		[SerializeField] public float slow_amount;
		[SerializeField] public float slow_duration;
		[SerializeField] public float oil_on_tile_duration;
		[SerializeField] public float cooldown;
	}

	[Serializable] public class TowerFlameThrowerOptions
	{
		[SerializeField] [Range (1, 100)] public int health;
		[SerializeField] [Range (0, 100)] public int mana_requirement;
		[SerializeField] [Range (0, 100)] public int physical_damage;
		[SerializeField] [Range (0, 100)] public int fire_damage;
		[SerializeField] [Range (0, 100)] public int frost_damage;
		[SerializeField] [Range (0, 100)] public int electric_damage;
		[SerializeField] [Range (0, 100)] public int poison_damage;
		[SerializeField] [Range (0, 100)] public int magic_damage;
		[SerializeField] [Range (1, 10)] public int range;
		[SerializeField] [Range (0, 5)] public float cooldown_time;
		[SerializeField] public float overheat_threshold;
		[SerializeField] public float overheat_duration;
		[SerializeField] public float cooldown;
	}
	#endregion
	#region enemy options
	[Serializable] public class EnemyOptions
	{
		[SerializeField] public int mana_reg_per_sec;
		[SerializeField] [Range (0.5f, 0.9f)] public float max_resistances;
		[SerializeField] public Physical physical;
		[SerializeField] public Fire fire;
		[SerializeField] public Frost frost;
		[SerializeField] public Electric electric;
		[SerializeField] public Poison poison;
		[SerializeField] public Magic magic;
		[SerializeField] public Minion minion;
	}

	#region physical
	[Serializable] public class Physical
	{
		[SerializeField] public ShieldSkeletonOptions shield_skeleton;
		[SerializeField] public GoblinConstructionTeamOptions goblin_construction_team;
		[SerializeField] public BroodMotherOptions brood_mother;
		[SerializeField] public WerewolfOptions werewolf;
		[SerializeField] public SteamWalkerOptions steam_walker;
		[SerializeField] public GiantOptions giant;
	}

	[Serializable] public class ShieldSkeletonOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (0.1f, 10)] public float mana_cost_per_dmg;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class GoblinConstructionTeamOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class BroodMotherOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance; 
		[SerializeField] [Range (0.25f, 2)] public float egg_hatch_time;
	}

	[Serializable] public class WerewolfOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
		[SerializeField] [Range (1, 100)] public int evolution_distance;
		[SerializeField] [Range (1, 100)] public int casting_damage;
		[SerializeField] [Range (1, 100)] public int casting_heal;
		[SerializeField] [Range (1, 100)] public int casting_max_health_increase;
		[SerializeField] [Range (1.05f, 1.5f)] public float casting_movement_speed_multiplier;
	}

	[Serializable] public class SteamWalkerOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class GiantOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
		[SerializeField] [Range (1, 10)] public int mana_gained_on_hit;
		[SerializeField] [Range (1, 3)] public int tower_push_distance;
	}

	#endregion
	#region fire
	[Serializable] public class Fire
	{
		[SerializeField] public MagmaElementalOptions magma_elemental;
		[SerializeField] public AlchemistOptions alchemist;
		[SerializeField] public BioArsonistOptions bio_arsonist;
		[SerializeField] public FireElementalOptions fire_elemental;
		[SerializeField] public WyrmlingOptions wyrmling;
		[SerializeField] public CorruptorOptions corruptor;
	}

	[Serializable] public class MagmaElementalOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
		[SerializeField] [Range (0.01f, 0.1f)] public float resistance_increase;
		[SerializeField] [Range (1f, 10f)] public float time_to_buff_reset;
	}

	[Serializable] public class AlchemistOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class BioArsonistOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
		[SerializeField] [Range (1, 100)] public int tower_damage;
	}

	[Serializable] public class FireElementalOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class WyrmlingOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class CorruptorOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}
	#endregion
	#region frost
	[Serializable] public class Frost
	{
		[SerializeField] public LichOptions lich;
		[SerializeField] public LadyFrostOptions lady_frost;
		[SerializeField] public IceConsumerOptions ice_consumer;
		[SerializeField] public DeadGroveOptions dead_grove;
		[SerializeField] public SnowDiverOptions snow_diver;
		[SerializeField] public IceThrallOptions ice_thrall;
	}
	
	[Serializable] public class LichOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
		[SerializeField] public float life_steal_threshold;
		[SerializeField] public float life_steal_rate_per_second;
		[SerializeField] public float life_steal_range;
	}

	[Serializable] public class LadyFrostOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class IceConsumerOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class DeadGroveOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class SnowDiverOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class IceThrallOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}
	#endregion
	#region electric
	[Serializable] public class Electric
	{
		[SerializeField] public AbsorberOptions absorber;
		[SerializeField] public StaticChargeOptions static_charge;
		[SerializeField] public ChargeCollectorOptions charge_collector;
		[SerializeField] public ZapOptions zap;
		[SerializeField] public RocketGoblinOptions rocket_goblin;
		[SerializeField] public OverChargeOptions overcharge;
	}

	[Serializable] public class AbsorberOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class StaticChargeOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class ChargeCollectorOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class ZapOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class RocketGoblinOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (1, 5)] public int casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class OverChargeOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}
	#endregion
	#region poison
	[Serializable] public class Poison
	{
		[SerializeField] public GreenBelcherOptions green_belcher;
		[SerializeField] public OozeOptions ooze;
		[SerializeField] public CorpseEaterOptions corpse_eater;
		[SerializeField] public MutantOptions mutant;
		[SerializeField] public GrapplingSpiderOptions grappling_spider;
		[SerializeField] public PlagueBearerOptions plague_bearer;
	}

	[Serializable] public class GreenBelcherOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
		[SerializeField] public float health_regain;
		[SerializeField] public float health_regain_time;
	}

	[Serializable] public class OozeOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class CorpseEaterOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class MutantOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class GrapplingSpiderOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class PlagueBearerOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}
	#endregion
	#region magic
	[Serializable] public class Magic
	{
		[SerializeField] public ShadowFiendOptions shadow_fiend;
		[SerializeField] public ShamanOptions shaman;
		[SerializeField] public NecromancerOptions necromancer;
		[SerializeField] public ManaAddictOptions mana_addict;
		[SerializeField] public PhaseShifterOptions phase_shifter;
		[SerializeField] public MindTwisterOptions mind_twister;
	}

	[Serializable] public class ShadowFiendOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class ShamanOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class NecromancerOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class ManaAddictOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class PhaseShifterOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class MindTwisterOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (1, 100)] public int max_mana;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0.25f, 5)] public float casting_range;
		[SerializeField] [Range (0.1f, 5)] public float casting_time;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}
	#endregion
	#region minion
	[Serializable] public class Minion
	{
		[SerializeField] public SpiderOptions spider;
		[SerializeField] public TreantOptions treant;
		[SerializeField] public ZombieOptions zombie;
	}

	[Serializable] public class SpiderOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range(0f, 5f)] public float cooldown;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class TreantOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range (0f, 5f)] public float cooldown;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}

	[Serializable] public class ZombieOptions
	{
		[SerializeField] [Range (0.25f, 2)] public float movement_speed_per_second;
		[SerializeField] [Range (100, 1000)] public int health;
		[SerializeField] [Range (1, 100)] public int mana_cost;
		[SerializeField] [Range(0f, 5f)] public float cooldown;
		[SerializeField] [Range (0f, 0.75f)] public float physical_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float fire_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float frost_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float electric_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float poison_resistance;
		[SerializeField] [Range (0f, 0.75f)] public float magic_resistance;
	}
	#endregion
	
	#endregion

	private enum camera_directions
	{
		up,
		down,
		left,
		right
	}

	#endregion

	void Start()
    {
		#region initializing enemy picker, grid, pathfinding, save handler

		enemy_picker = new EnemyPicker (this);
		GameObject.Find ("Damage Chart").AddComponent<DamageChart>().SetDamageChart (enemy_picker);
		if (gameplay_options.map.load_save == true)
		{
			save_handler = new SaveHandler();
			var info = new DirectoryInfo (Application.dataPath + "/Map Saves/");
				var fileInfo = info.GetFiles();
			if (gameplay_options.map.random_save == true && fileInfo.Count() > 0)
			{
				gameplay_options.map.save_name = fileInfo [UnityEngine.Random.Range (0, fileInfo.Count() / 2)].Name.Replace (".json", "");
			}
			grid = new GameGrid (save_handler.GetGridDimensions (this,gameplay_options.map.save_name), gameplay_options.map.cell_length_x, gameplay_options.map.cell_width_z, testing_options.display_grid_text);
			save_handler.SaveTerrainLoad (this, gameplay_options.map.save_name);
			MapConstructor map_constructor = new MapConstructor (this);
		}
		else
		{
			grid = new GameGrid (gameplay_options.map.length_x, gameplay_options.map.width_z, gameplay_options.map.cell_length_x, gameplay_options.map.cell_width_z, testing_options.display_grid_text);
		}
		pathfinding = new PathFinding ();

		#endregion
		#region references

		damage_chart = GameObject.Find ("Damage Chart").GetComponent<DamageChart>();
		wave_button = GameObject.Find ("Wave Button");
		time_buttons = GameObject.Find ("Time Buttons");
		time_button_pause = GameObject.Find ("Time Button Pause");
		time_button_0_25 = GameObject.Find ("Time Button 0.25");
		time_button_0_5 = GameObject.Find ("Time Button 0.5");
		time_button_1 = GameObject.Find ("Time Button 1");
		time_button_2 = GameObject.Find ("Time Button 2");
		time_button_4 = GameObject.Find ("Time Button 4");
		add_card_button = GameObject.Find ("Add Card Button");
		upgrade_card_button = GameObject.Find ("Upgrade Card Button");
		mana_display_text = GameObject.Find ("Mana Text").GetComponent<TextMeshProUGUI>();
		wave_display_text = GameObject.Find ("Wave Text").GetComponent<TextMeshProUGUI>();
		card_handler = GameObject.Find ("Card Handler").GetComponent<CardHandler>();
		inspector_handler = GameObject.Find ("Inspector Handler").GetComponent<InspectorHandler>();
		camera = GameObject.Find("Main Camera");

		#endregion
		#region button functions, time button color

		wave_button.GetComponent<Button_UI> ().ClickFunc = () => {
		if (card_handler.drawing == false)
		{
		wave_active = true; wave_button.SetActive(false);
		UpdateWaveNumber ();
		}};
		time_button_pause.GetComponent<Button_UI> ().ClickFunc = () => {if (CheckIfWaveActive () == true)  {Time.timeScale = 0;} SetActiveTimeButtonColor (time_button_pause);};
		time_button_0_25.GetComponent<Button_UI> ().ClickFunc = () => {if (CheckIfWaveActive () == true)  {Time.timeScale = 0.25f;} SetActiveTimeButtonColor (time_button_0_25);};
		time_button_0_5.GetComponent<Button_UI> ().ClickFunc = () => {if (CheckIfWaveActive() == true) {Time.timeScale = 0.5f;} SetActiveTimeButtonColor (time_button_0_5);};
		time_button_1.GetComponent<Button_UI> ().ClickFunc = () => {if (CheckIfWaveActive() == true) {Time.timeScale = 1;} SetActiveTimeButtonColor (time_button_1);};
		time_button_2.GetComponent<Button_UI> ().ClickFunc = () => {if (CheckIfWaveActive() == true) {Time.timeScale = 2;} SetActiveTimeButtonColor (time_button_2);};
		time_button_4.GetComponent<Button_UI> ().ClickFunc = () => {if (CheckIfWaveActive() == true) {Time.timeScale = 4;} SetActiveTimeButtonColor (time_button_4);};
		time_buttons.SetActive (false);
		add_card_button.GetComponent<Button_UI> ().ClickFunc = () => {};
		upgrade_card_button.GetComponent<Button_UI> ().ClickFunc = () => {};
		add_card_button.SetActive (false);
		upgrade_card_button.SetActive (false);
		time_button_color_enter = new Color (0.6078432f, 0.6078432f, 0.6078432f);
		time_button_color_exit = new Color (0.4156863f, 0.4156863f, 0.4156863f);

		#endregion
		#region spawning spawners and cores

		for (int x = 0; x < grid.length_x; x++)
		{
			for (int z = 0; z < grid.width_z; z++)
			{
				if (grid.GetValue (x, z, grid_parameter.spawn_zone) == grid.EnumTranslator (spawn_zone.spawner))
				{
					spawner_tile_list.Add ((x, z));
				}
				else
				{
					if (grid.GetValue (x, z, grid_parameter.spawn_zone) == grid.EnumTranslator (spawn_zone.core))
					{
						core_tile_list.Add ((x, z));
					}
				}
			}
		}
		//creating core in preset position
		if (testing_options.random_spawner_positions == false)
		{
			new Core (grid.GetWorldTileCenter (4, 9, 0.5f), this);
		}
		else
		{
			//creating cores in random positions
			for (int i = 0; i < testing_options.amount_of_cores; i++)
			{
				int index = UnityEngine.Random.Range (0, core_tile_list.Count);
				new Core (grid.GetWorldTileCenter (core_tile_list [index]), this);
				core_tile_list.RemoveAt (index);
			}
		}
		//creating spawner in preset position
		if (testing_options.random_spawner_positions == false)
		{
			new Enemy (grid.GetWorldTileCenter (4, 0, 0.5f), this);
		}
		else
		{
			//creating spawners in random positions
			for (int i = 0; i < testing_options.amount_of_spawners; i++)
			{
				int index = UnityEngine.Random.Range (0, spawner_tile_list.Count);
				new Enemy (grid.GetWorldTileCenter (spawner_tile_list [index]), this);
				spawner_tile_list.RemoveAt (index);
			}
		}

		#endregion
		
		SetManaOutlineTimer ();
		#region board center tupple

		if (gameplay_options.map.length_x % 2 == 0)
		{
			board_center_position_tuple.x = (((float) gameplay_options.map.length_x / 2) * gameplay_options.map.cell_length_x);
		}
		else
		{
			board_center_position_tuple.x = (((float) gameplay_options.map.length_x / 2 + 0.5f) * gameplay_options.map.cell_length_x);
		}
		if (gameplay_options.map.length_x >= gameplay_options.map.width_z)
		{
			board_center_position_tuple.y = gameplay_options.map.length_x;
		}
		else
		{
			board_center_position_tuple.y = gameplay_options.map.width_z;
		}
		if (gameplay_options.map.width_z % 2 == 0)
		{
			board_center_position_tuple.z = (((float) gameplay_options.map.width_z / 2) * gameplay_options.map.cell_width_z);
		}
		else
		{
			board_center_position_tuple.z = (((float) gameplay_options.map.width_z / 2 + 0.5f) * gameplay_options.map.cell_width_z);
		}

		#endregion
		camera.transform.position = new Vector3 (board_center_position_tuple.x, board_center_position_tuple.y, board_center_position_tuple.z);
		camera.AddComponent<CameraMovement>().SetVariables (this);
		pathfinding.FindPath (grid, cores_list);
    }

	private void FixedUpdate()
    {
		#region pathfinding
		pathfinding_timer += Time.deltaTime;
		if (pathfinding_timer > 1f && first_path == false)
		{
			
			first_path = true;
		}

		#endregion
		#region changing outline color

		if (inspector_window_open == false)
		{
			Ray mouse_world_ray = camera.GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (mouse_world_ray, out RaycastHit rayCastHit);
			if (object_under_cursor != null)
			{
				if (object_under_cursor != rayCastHit.collider.gameObject && rayCastHit.collider.gameObject.TryGetComponent (out MeshRenderer mesh_renderer) == true)
				{
					SetOutlineMaterial (object_under_cursor, gameplay_options.ui.outline_off_pointer_material);
				}
			}
			if ((rayCastHit.collider.tag == "tower" || rayCastHit.collider.tag == "enemy") && object_under_cursor != rayCastHit.collider.gameObject)
			{
				SetOutlineMaterial (rayCastHit.collider.gameObject, gameplay_options.ui.outline_on_pointer_material);
			}
			if (rayCastHit.collider.tag == "tower" && Input.GetMouseButtonDown (gameplay_options.controls.MouseButtonTranslator (gameplay_options.controls.drag_card)))
			{
				inspector_window_open = true;
				Tower.BaseTower base_tower = rayCastHit.collider.GetComponentInParent<Tower.BaseTower>();
				inspector_handler.ConstructTowerInspector (base_tower.tower_id, base_tower.GetTowerStats(), base_tower.GetTowerBaseStats());
			}
			object_under_cursor = rayCastHit.collider.gameObject;
		}

		#endregion
		#region start of the wave events
		if (wave_active == true && card_handler.GetComponent<CardHandler>().tower_discarding == true && card_handler.finished_discarding == true)
		{
			time_buttons.SetActive (true);
			SetActiveTimeButtonColor (time_button_1);
			enemy_picker.AddNewEnemiesToSpawnList ();
			foreach (GameObject spawner in spawner_list)
			{
				spawner.GetComponent<Spawner>().SpawnEnemyTimer (spawner.GetComponent<Spawner>());
				spawner.GetComponent<Spawner>().life_time++;
			}
			card_handler.GetComponent<CardHandler>().tower_discarding = false;
		}
		#endregion
		#region end of the wave events
		if (wave_active == true && enemy_to_spawn_list.Count == 0 && enemy_list.Count == 0 && card_handler.ability_discarding == false && card_handler.finished_discarding == true)
		{
			time_buttons.SetActive (false);
			Time.timeScale = 1;
			wave_active = false;
			card_handler.finished_discarding = false;
			card_handler.ability_discarding = true;
		}
		if (card_handler.ability_discarding == true && card_handler.finished_discarding == true)
		{
			card_handler.ability_discarding = false;
			card_handler.finished_discarding = false;
			card_handler.AddingNewCardToDeck ();
			//spawning new spawners
			for (int counter = 0; counter < spawner_list.Count;)
			{
				if (spawner_list [counter].GetComponent<Spawner>().life_time >= testing_options.spawner_lifetime_amount_of_turns)
				{
					spawner_tile_list.Add (grid.GetXZ (spawner_list [counter].transform.position));
					Destroy (spawner_list [counter]);
					spawner_list.Remove (spawner_list [counter]);
				}
				else
				{
					counter++;
				}
			}
			turns_since_last_spawner++;
			if (turns_since_last_spawner >= testing_options.amount_of_turns_to_new_spawner)
			{
				int index = UnityEngine.Random.Range (0, spawner_tile_list.Count);
				new Enemy (grid.GetWorldTileCenter (spawner_tile_list [index]), this);
				spawner_tile_list.RemoveAt (index);
				turns_since_last_spawner = 0;
			}
		}
		if (card_handler.picking == false && card_handler.after_picking == true)
		{
			card_handler.after_picking = false;
			enemy_counter = 0;
			wave_button.SetActive(true);
			card_handler.gameObject.SetActive(true);
			card_handler.drawing = true;
		}
		#endregion
    }

	#region references: game grid, pathfinding, damage chart

	public GameGrid GetGameGrid ()
	{
		return grid;
	}

	public PathFinding GetPathfinding ()
	{
		return pathfinding;
	}

	public DamageChart GetDamageChart ()
	{
		return damage_chart;
	}

	#endregion
	#region references: enemy counter, spawner counter, tower counter, core counter
	
	public int GetCoreCounter ()
	{
		core_counter ++;
		return core_counter;
	}

	public int GetTowerCounter ()
	{
		tower_counter ++;
		return tower_counter;
	}

	public int GetEnemyCounter ()
	{
		enemy_counter ++;
		return enemy_counter;
	}

	public int GetSpawnerCounter ()
	{
		spawner_counter ++;
		return spawner_counter;
	}

	#endregion
	#region mana: get / set outline, set outline timer, update, 

	public void GetManaOutline ()
	{
		mana_outline_tuple_list = new List<(int x, int z, GameGrid.grid_direction direction)> ();
		for (int x = 0; x < grid.length_x; x++)
		{
			for (int z = 0; z < grid.width_z; z++)
			{
				if (grid.GetValue (x, z, GameGrid.grid_parameter.mana) == grid.EnumTranslator (GameGrid.mana.connected))
				{
					for (int x_2 = x - 1; x_2 <= x + 1; x_2++)
					{
						for (int z_2 = z - 1; z_2 <= z + 1; z_2++)
						{
							if (grid.CheckIfInsideGrid (x_2, z_2) && grid.CheckIfNotDiagonal (x, z, x_2, z_2) &&
							    grid.GetValue (x_2, z_2, GameGrid.grid_parameter.mana) == grid.EnumTranslator (GameGrid.mana.unconnected))
							{
								GameGrid.grid_direction direction = grid.GetMovementDirection ((x_2, z_2), (x, z));
								mana_outline_tuple_list.Add ((x_2, z_2, direction));
							}
						}
					}
				}
			}
		}
	}

	public void DrawManaOutline ()
	{
		for (int i = 0; i < mana_outline_tuple_list.Count; i++)
		{
			switch (mana_outline_tuple_list [i].direction)
			{
				case GameGrid.grid_direction.down: 
				Debug.DrawLine (grid.GetWorldPosition (mana_outline_tuple_list [i].x, mana_outline_tuple_list [i].z + 1, 0),
				grid.GetWorldPosition (mana_outline_tuple_list [i].x + 1, mana_outline_tuple_list [i].z + 1, 0), Color.blue, 1);
				break;

				case GameGrid.grid_direction.up:
				Debug.DrawLine (grid.GetWorldPosition(mana_outline_tuple_list[i].x, mana_outline_tuple_list[i].z, 0),
				grid.GetWorldPosition(mana_outline_tuple_list[i].x + 1, mana_outline_tuple_list[i].z, 0), Color.blue, 1);
				break;

				case GameGrid.grid_direction.right:
				Debug.DrawLine (grid.GetWorldPosition(mana_outline_tuple_list[i].x, mana_outline_tuple_list[i].z, 0),
				grid.GetWorldPosition(mana_outline_tuple_list[i].x, mana_outline_tuple_list[i].z + 1, 0), Color.blue, 1);
				break;

				case GameGrid.grid_direction.left:
				Debug.DrawLine (grid.GetWorldPosition(mana_outline_tuple_list[i].x + 1, mana_outline_tuple_list[i].z, 0),
				grid.GetWorldPosition(mana_outline_tuple_list[i].x + 1, mana_outline_tuple_list[i].z + 1, 0), Color.blue, 1);
				break;
			}
		}
	}

	public void SetManaOutlineTimer ()
	{
		Action action;
		Timer.Create (action = () => {
		DrawManaOutline ();
		SetManaOutlineTimer ();
		}, 1, "Mana Outline", gameObject);
	}

	public void UpdateMana ()
	{
		mana_amount = 0;
		Tower.BaseTower [] towers_array = GameObject.Find ("Tower Initialized").GetComponentsInChildren<Tower.BaseTower>();
		foreach (Tower.BaseTower tower in towers_array)
		{
			mana_amount -= tower.GetManaRequirement();
		}
		mana_display_text.text = "Mana:" + Environment.NewLine + mana_amount.ToString();
	}

	#endregion
	#region enemy: get type to spawn, check for, add to enemy list, remove from enemy list, add to spawn list, reset pathfinding of all enemies

	public enemy_id GetEnemyTypeToSpawn ()
	{
		if (enemy_to_spawn_list.Count > 0)
		{
			int enemy_index = UnityEngine.Random.Range (0, enemy_to_spawn_list.Count);
			enemy_id type = enemy_to_spawn_list [enemy_index];
			enemy_to_spawn_list.RemoveAt (enemy_index);
			return type;
		}
		else
		{
			return enemy_id.none;
		}
	}

	public bool CheckForEnemies ()
	{
		if (enemy_list.Count > 0)
		{
			return true;
		}
		return false;
	}
	public void AddEnemyToEnemyList (GameObject enemy)
	{
		enemy_list.Add (enemy);
	}

	public void RemoveEnemyFromEnemyList (GameObject enemy)
	{
		enemy_list.Remove (enemy);
	}

	public void AddEnemyToSpawnList (enemy_id id)
	{
		enemy_to_spawn_list.Add (id);
	}

	#endregion
	#region Spawner: pathfinding, add to spawner list

	public void AddSpawnerToSpawnerList (GameObject spawner)
	{
		spawner_list.Add (spawner);
	}

	public void SetNewPathfinding ()
	{
		pathfinding.FindPath (grid, cores_list);
	}

	#endregion
	#region wave: check if active, update number

	public bool CheckIfWaveActive ()
	{
		return wave_active;
	}

	private void UpdateWaveNumber ()
	{
		wave_number++;
		wave_display_text.text = "Wave " + wave_number.ToString();
	}

	#endregion
	
	public void ChooseAddOrUpgradeCard ()
	{
		
	}

	private void SetActiveTimeButtonColor (GameObject button_object)
	{
		time_button_pause.transform.GetChild (0).GetComponent<Image>().color = time_button_color_exit;
		time_button_0_25.transform.GetChild (0).GetComponent<Image>().color = time_button_color_exit;
		time_button_0_5.transform.GetChild (0).GetComponent<Image>().color = time_button_color_exit;
		time_button_1.transform.GetChild (0).GetComponent<Image>().color = time_button_color_exit;
		time_button_2.transform.GetChild (0).GetComponent<Image>().color = time_button_color_exit;
		time_button_4.transform.GetChild (0).GetComponent<Image>().color = time_button_color_exit;
		time_button_pause.GetComponent<Button_UI>().enabled = true;
		time_button_0_25.GetComponent<Button_UI>().enabled = true;
		time_button_0_5.GetComponent<Button_UI>().enabled = true;
		time_button_1.GetComponent<Button_UI>().enabled = true;
		time_button_2.GetComponent<Button_UI>().enabled = true;
		time_button_4.GetComponent<Button_UI>().enabled = true;

		button_object.transform.GetChild (0).GetComponent<Image>().color = time_button_color_enter;
		button_object.GetComponent<Button_UI> ().enabled = false;
	}

	public void SetOutlineMaterial (GameObject game_object, Material new_outline_material)
	{
		Material [] materials_array = game_object.GetComponent<MeshRenderer>().materials;
		for (int i = 0; i < materials_array.Length; i++)
		{
			if (materials_array[i].name == "Outline OffPointer (Instance)" || materials_array[i].name == "Outline OnPointer (Instance)")
			{
				materials_array[i] = new_outline_material;
				break;
			}
		}
		game_object.GetComponent<MeshRenderer>().materials = materials_array;
	}

	public void SetInspectorWindow (bool state)
	{
		inspector_window_open = state;
	}

	public GameObject GetObjectUnderCursor ()
	{
		return object_under_cursor;
	}

	public void AddCoreToCoresList (CoreObject core)
	{
		cores_list.Add (core);
	}

	#region TImer

	public class Timer
	{
		public static GameObject Create (Action action, float time, string timer_name, GameObject parent)
		{
			GameObject timer;
			new Timer (action, time, timer_name, parent, out timer);
			return timer;
		}

		public static GameObject Create (Action action, float time, string timer_name)
		{
			GameObject timer;
			new Timer (action, time, timer_name, out timer);
			return timer;
		}

		public static GameObject Create (float time, string timer_name, GameObject parent)
		{
			GameObject timer;
			new Timer (time, timer_name, parent, out timer);
			return timer;
		}

		public static GameObject Create (float time, string timer_name)
		{
			GameObject timer;
			new Timer (time, timer_name, out timer);
			return timer;
		}

		private Timer (Action action, float time, string timer_name, GameObject parent, out GameObject timer)
		{
			timer = new GameObject ("timer - " + timer_name);
			timer.transform.SetParent (parent.transform);
			timer.AddComponent<TimerComponent>().SetVariables (time);
			timer.GetComponent<TimerComponent>().SetAction (action);
		}

		private Timer (Action action, float time, string timer_name, out GameObject timer)
		{
			timer = new GameObject ("timer - " + timer_name);
			timer.AddComponent<TimerComponent>().SetVariables (time);
			timer.GetComponent<TimerComponent>().SetAction (action);
		}

		private Timer (float time, string timer_name, GameObject parent, out GameObject timer)
		{
			timer = new GameObject ("timer - " + timer_name);
			timer.transform.SetParent (parent.transform);
			timer.AddComponent<TimerComponent>().SetVariables (time);
		}

		private Timer (float time, string timer_name, out GameObject timer)
		{
			timer = new GameObject ("timer - " + timer_name);
			timer.AddComponent<TimerComponent>().SetVariables (time);
		}

		public class TimerComponent : MonoBehaviour
		{
			#region

			private float time;
			private Action action;

			#endregion

			public void SetVariables (float time)
			{
				this.time = time;
			}

			public void SetAction (Action action)
			{
				this.action = action;
			}

			private void FixedUpdate()
			{
				time -= Time.deltaTime;
				if (time < 0)
				{
					action ();
					Destroy (gameObject);
				}
			}
		}
	}

	#endregion
}
