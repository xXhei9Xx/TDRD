using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static Enemy;
using static GameGrid;

public class Tower
{
	#region enum declarations

	public enum tower_id
	{
		none,
		test,
		wall,
		mana,
		blizzard,
		oil,
		lightning,
		flamethrower
	}

	public GameObject GetTowerTemplate (tower_id type)
	{
		switch (type)
		{
			default: return null;

			case tower_id.test:
			return GameObject.Find ("Tower Test");

			case tower_id.wall:
			return GameObject.Find ("Tower Wall");

			case tower_id.mana:
			return GameObject.Find("Tower Mana Generator");

			case tower_id.blizzard:
			return GameObject.Find("Tower Blizzard");

			case tower_id.oil:
			return GameObject.Find("Tower Oil");

			case tower_id.lightning:
			return GameObject.Find("Tower Lightning");

			case tower_id.flamethrower:
			return GameObject.Find("Tower FlameThrower");
		}
	}

	public enum targeting_priority
	{
		closest,
		furthest,
		lowest_health,
		highest_health,
		most_progress,
		least_progress
	}

	public enum damage_types
	{
		physical,
		electric,
		frost,
		fire,
		poison,
		magic
	}

	#endregion

	public Tower (tower_id type, Vector3 position, GameHandler caller, List<CardHandler.upgrade_id> card_upgrades, out GameObject tower)
	{
		tower = new GameObject(caller.GetTowerCounter () + " - " + type.ToString());
		tower.transform.parent = GameObject.Find("Tower Initialized").transform;
		tower.transform.position = position;
		switch (type)
		{
			case tower_id.test:
			tower.AddComponent<TowerTest>().cooldown_time = caller.tower_options.test.cooldown_time;
			tower.GetComponent<TowerTest>().type = type;
			tower.GetComponent<TowerTest>().caller = caller;
			break;

			case tower_id.wall:
			break;

			case tower_id.mana:
			tower.AddComponent<ManaGenerator>().SetVariables (caller);
			break;

			case tower_id.blizzard:
			tower.AddComponent<TowerBlizzard>().SetVariables (caller);
			break;

			case tower_id.oil:
			tower.AddComponent<TowerOil>().SetVariables (caller);
			break;

			case tower_id.lightning:
			tower.AddComponent<TowerLightning>().SetVariables (caller);
			break;

			case tower_id.flamethrower:
			tower.AddComponent<TowerFlameThrower>().SetVariables (caller);
			break;
		}
		tower.AddComponent<BaseTower>().SetVariables (caller, GetTowerTemplate (type), type, card_upgrades);
	}

	public class BaseTower : MonoBehaviour
	{
		#region variable declarations

		[SerializeField] private bool connected_to_mana = false, cooldown = false, set_xz = false, moving = false;
		public int x, z;
		private GameHandler caller;
		private GameObject this_tower_object, tower_template;
		public tower_id tower_id;
		public targeting_priority this_tower_targeting_priority = targeting_priority.most_progress;
		[SerializeField] private int target_moving_distance = 0;
		[SerializeField] private grid_direction moving_direction;
		[SerializeField] private float movement_timer = 0;


		#region stats

		private int max_health;
		private int health;
		private int mana_requirement;
		private int physical_damage = 0;
		private int fire_damage = 0;
		private int frost_damage = 0;
		private int electric_damage = 0;
		private int poison_damage = 0;
		private int magic_damage = 0;
		private int range;
		private float cooldown_time;

		private int base_max_health;
		private int base_physical_damage = 0;
		private int base_fire_damage = 0;
		private int base_frost_damage = 0;
		private int base_electric_damage = 0;
		private int base_poison_damage = 0;
		private int base_magic_damage = 0;
		private int base_range;
		private float base_cooldown_time;

		#endregion

		public void SetVariables (GameHandler caller, GameObject tower_template, tower_id id, List<CardHandler.upgrade_id> card_upgrades)
		{
			this.caller = caller;
			this.tower_template = tower_template;
			tower_id = id;

			switch (id)
			{
				case tower_id.wall:
				base_max_health = caller.tower_options.wall.health;
				mana_requirement = caller.tower_options.wall.mana_requirement;
				break;

				case tower_id.mana:
				base_max_health = caller.tower_options.mana_generator.health;
				mana_requirement = - caller.tower_options.mana_generator.mana_generated;
				base_range = caller.tower_options.mana_generator.range;
				break;

				case tower_id.test:
				base_max_health = caller.tower_options.test.health;
				mana_requirement = caller.tower_options.test.mana_requirement;
				base_physical_damage = caller.tower_options.test.physical_damage;
				base_fire_damage = caller.tower_options.test.fire_damage;
				base_frost_damage = caller.tower_options.test.frost_damage;
				base_electric_damage = caller.tower_options.test.electric_damage;
				base_poison_damage = caller.tower_options.test.poison_damage;
				base_magic_damage = caller.tower_options.test.magic_damage;
				base_range = caller.tower_options.test.range;
				base_cooldown_time = caller.tower_options.test.cooldown_time;
				break;

				case tower_id.blizzard:
				base_max_health = caller.tower_options.blizzard.health;
				mana_requirement = caller.tower_options.blizzard.mana_requirement;
				base_physical_damage = caller.tower_options.blizzard.physical_damage;
				base_fire_damage = caller.tower_options.blizzard.fire_damage;
				base_frost_damage = caller.tower_options.blizzard.frost_damage;
				base_electric_damage = caller.tower_options.blizzard.electric_damage;
				base_poison_damage = caller.tower_options.blizzard.poison_damage;
				base_magic_damage = caller.tower_options.blizzard.magic_damage;
				base_range = caller.tower_options.blizzard.range;
				base_cooldown_time = caller.tower_options.blizzard.cooldown_time;
				break;

				case tower_id.oil:
				base_max_health = caller.tower_options.oil.health;
				mana_requirement = caller.tower_options.oil.mana_requirement;
				base_physical_damage = caller.tower_options.oil.physical_damage;
				base_fire_damage = caller.tower_options.oil.fire_damage;
				base_frost_damage = caller.tower_options.oil.frost_damage;
				base_electric_damage = caller.tower_options.oil.electric_damage;
				base_poison_damage = caller.tower_options.oil.poison_damage;
				base_magic_damage = caller.tower_options.oil.magic_damage;
				base_range = caller.tower_options.oil.range;
				base_cooldown_time = caller.tower_options.oil.cooldown_time;
				break;

				case tower_id.lightning:
				base_max_health = caller.tower_options.lightning.health;
				mana_requirement = caller.tower_options.lightning.mana_requirement;
				base_physical_damage = caller.tower_options.lightning.physical_damage;
				base_fire_damage = caller.tower_options.lightning.fire_damage;
				base_frost_damage = caller.tower_options.lightning.frost_damage;
				base_electric_damage = caller.tower_options.lightning.electric_damage;
				base_poison_damage = caller.tower_options.lightning.poison_damage;
				base_magic_damage = caller.tower_options.lightning.magic_damage;
				base_range = caller.tower_options.lightning.range;
				base_cooldown_time = caller.tower_options.lightning.cooldown_time;
				break;

				case tower_id.flamethrower:
				base_max_health = caller.tower_options.flamethrower.health;
				mana_requirement = caller.tower_options.flamethrower.mana_requirement;
				base_physical_damage = caller.tower_options.flamethrower.physical_damage;
				base_fire_damage = caller.tower_options.flamethrower.fire_damage;
				base_frost_damage = caller.tower_options.flamethrower.frost_damage;
				base_electric_damage = caller.tower_options.flamethrower.electric_damage;
				base_poison_damage = caller.tower_options.flamethrower.poison_damage;
				base_magic_damage = caller.tower_options.flamethrower.magic_damage;
				base_range = caller.tower_options.flamethrower.range;
				base_cooldown_time = caller.tower_options.flamethrower.cooldown_time;
				break;
			}

			if (card_upgrades != null)
			{
				foreach (CardHandler.upgrade_id upgrade in card_upgrades)
				{
					switch (upgrade)
					{
						case CardHandler.upgrade_id.tower_test_physical_damage:
						base_physical_damage += caller.deck_options.tower_upgrades.test.physical_damage;
						break;

						case CardHandler.upgrade_id.tower_test_fire_damage:
						base_fire_damage += caller.deck_options.tower_upgrades.test.fire_damage;
						break;

						case CardHandler.upgrade_id.tower_test_frost_damage:
						base_frost_damage += caller.deck_options.tower_upgrades.test.frost_damage;
						break;

						case CardHandler.upgrade_id.tower_test_electric_damage:
						base_electric_damage += caller.deck_options.tower_upgrades.test.electric_damage;
						break;

						case CardHandler.upgrade_id.tower_test_poison_damage:
						base_poison_damage += caller.deck_options.tower_upgrades.test.poison_damage;
						break;

						case CardHandler.upgrade_id.tower_test_magic_damage:
						base_magic_damage += caller.deck_options.tower_upgrades.test.magic_damage;
						break;

						case CardHandler.upgrade_id.tower_test_cooldown:
						base_cooldown_time *= caller.deck_options.tower_upgrades.test.cooldown;
						break;

						case CardHandler.upgrade_id.tower_test_mana_requirement:
						mana_requirement -= caller.deck_options.tower_upgrades.test.mana_requirement;
						break;

						case CardHandler.upgrade_id.tower_test_health:
						base_max_health = (int) ((float) base_max_health * caller.deck_options.tower_upgrades.test.health);
						break;
					}
				}
			}

			max_health = base_max_health;
			physical_damage = base_physical_damage;
			fire_damage = base_fire_damage;
			frost_damage = base_frost_damage;
			electric_damage = base_electric_damage;
			poison_damage = base_poison_damage;
			magic_damage = base_magic_damage;
			range = base_range;
			cooldown_time = base_cooldown_time;

			health = max_health;
		}
		#endregion

		private void Start()
		{
			this_tower_object = Instantiate (tower_template);
			this_tower_object.transform.parent = transform;
			this_tower_object.transform.name = tower_template.name.ToString() + " Object";
			this_tower_object.transform.position = transform.position;
		}

		private void Update()
		{
			if (moving == true)
			{
				movement_timer += Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
				if (target_moving_distance > 0)
				{
					switch (moving_direction)
					{
						case grid_direction.up:
						transform.position += new Vector3 (0, 0, 1) * Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
						break;

						case grid_direction.down:
						transform.position += new Vector3 (0, 0, -1) * Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
						break;

						case grid_direction.left:
						transform.position += new Vector3 (-1, 0, 0) * Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
						break;

						case grid_direction.right:
						transform.position += new Vector3 (1, 0, 0) * Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
						break;

						case grid_direction.top_left:
						transform.position += new Vector3 (-1, 0, 1) * Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
						break;

						case grid_direction.top_right:
						transform.position += new Vector3 (1, 0, 1) * Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
						break;

						case grid_direction.bottom_left:
						transform.position += new Vector3 (-1, 0, -1) * Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
						break;

						case grid_direction.bottom_right:
						transform.position += new Vector3 (1, 0, -1) * Time.deltaTime * caller.gameplay_options.ui.tower_pushing_speed;
						break;
					}
				}
				if (movement_timer > 1)
				{
					transform.position = caller.GetGameGrid().GetWorldTileCenter (caller.GetGameGrid().GetXZ (transform.position), transform.localScale.y);
					target_moving_distance--;
					movement_timer = 0;
					switch (moving_direction)
					{
						case grid_direction.up:
						if (!(caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x, caller.GetGameGrid().GetXZ
						(transform.position).z + 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty)))
						{
							moving = false;
						}
						break;

						case grid_direction.down:
						if (!(caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x, caller.GetGameGrid().GetXZ
						(transform.position).z - 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty)))
						{
							moving = false;
						}
						break;

						case grid_direction.left:
						if (!(caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x - 1, caller.GetGameGrid().GetXZ
						(transform.position).z), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty)))
						{
							moving = false;
						}
						break;

						case grid_direction.right:
						if (!(caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x + 1, caller.GetGameGrid().GetXZ
						(transform.position).z), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty)))
						{
							moving = false;
						}
						break;

						case grid_direction.top_left:
						if (!(caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x - 1, caller.GetGameGrid().GetXZ
						(transform.position).z + 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
						caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x, caller.GetGameGrid().GetXZ
						(transform.position).z + 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
						caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x - 1, caller.GetGameGrid().GetXZ
						(transform.position).z), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty)))
						{
							moving = false;
						}
						break;

						case grid_direction.top_right:
						if (!(caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x + 1, caller.GetGameGrid().GetXZ
						(transform.position).z + 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
						caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x, caller.GetGameGrid().GetXZ
						(transform.position).z + 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
						caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x + 1, caller.GetGameGrid().GetXZ
						(transform.position).z), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty)))
						{
							moving = false;
						}
						break;

						case grid_direction.bottom_left:
						if (!(caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x - 1, caller.GetGameGrid().GetXZ
						(transform.position).z - 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
						caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x, caller.GetGameGrid().GetXZ
						(transform.position).z - 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
						caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x - 1, caller.GetGameGrid().GetXZ
						(transform.position).z), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty)))
						{
							moving = false;
						}
						break;

						case grid_direction.bottom_right:
						if (!(caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x + 1, caller.GetGameGrid().GetXZ
						(transform.position).z - 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
						caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x, caller.GetGameGrid().GetXZ
						(transform.position).z - 1), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty) &&
						caller.GetGameGrid().GetValue ((caller.GetGameGrid().GetXZ (transform.position).x + 1, caller.GetGameGrid().GetXZ
						(transform.position).z), grid_parameter.object_type) == caller.GetGameGrid().EnumTranslator (object_type.empty)))
						{
							moving = false;
						}
						break;
					}
					if (target_moving_distance == 0)
					{
						moving = false;
					}
				}
			}
		}

		public void SetMovingTower (GameObject enemy, int distance, grid_direction push_direction)
		{
			moving_direction = push_direction;
			target_moving_distance = distance;
			moving = true;
		}

		#region stat modification

		public void HealthAddition (int health_increase)
		{
			health += health_increase;
		}

		#endregion

		public (int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage) GetDamageDealt ()
		{
			return (physical_damage, fire_damage, frost_damage, electric_damage, poison_damage, magic_damage);
		}

		public bool CheckCooldownAndManaAndWaveActive ()
		{
			if (cooldown == false && connected_to_mana == true && caller.CheckIfWaveActive () == true)
			{
				return true;
			}
			return false;
		}

		public void UpdateConnectedToMana ()
		{
			if (caller.GetGameGrid().GetValue (x, z, grid_parameter.mana) == caller.GetGameGrid().EnumTranslator (mana.connected) )
			{
				connected_to_mana = true;
			}
			else
			{
				connected_to_mana = false;
			}
		}

		public int GetManaRequirement ()
		{
			if (connected_to_mana == true)
			{
				return mana_requirement;
			}
			return 0;
		}

		public void SetXZ ()
		{
			if (set_xz == true)
			{
				caller.GetGameGrid().SetValue (x, z, grid_parameter.object_type, object_type.empty);
			}
			set_xz = true;
			var xz = caller.GetGameGrid().GetXZ(transform.position);
			x = xz.x; z = xz.z;
			caller.GetGameGrid().SetValue (xz.x, xz.z, grid_parameter.object_type, object_type.tower);
			caller.SetAllSpawnerPathfinding ();
		}

		public void DestroyThisTower ()
		{
			caller.GetGameGrid().SetValue (x, z, grid_parameter.object_type, object_type.empty);
			connected_to_mana = false;
			caller.UpdateMana();
			Destroy (gameObject);
		}

		public void SetCooldown (float cooldown_time)
		{
			cooldown = true;
			GameHandler.Timer.Create (()=>{
			cooldown = false;
			}, cooldown_time, "cooldown", gameObject);
		}

		public (int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage) GetTowerDamage ()
		{
			return (physical_damage, fire_damage, frost_damage, electric_damage, poison_damage, magic_damage);
		}

		public (int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage, int range, float cooldown) GetTowerBaseStats ()
		{
			return (base_physical_damage, base_fire_damage, base_frost_damage, base_electric_damage, base_poison_damage, base_magic_damage, base_range, base_cooldown_time);
		}

		public (int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage, int range, float cooldown) GetTowerStats ()
		{
			return (physical_damage, fire_damage, frost_damage, electric_damage, poison_damage, magic_damage, range, cooldown_time);
		}
	}

	public class ManaGenerator : MonoBehaviour
	{
		#region variable declarations

		private GameHandler caller;

		#endregion

		private void Start()
		{
			
		}

		public void ConnectTilesToMana ()
		{
			for (int x = GetComponent<BaseTower>().x - caller.tower_options.mana_generator.range;
			x <= GetComponent<BaseTower>().x + caller.tower_options.mana_generator.range; x++)
			{
				for (int z = GetComponent<BaseTower>().z - caller.tower_options.mana_generator.range;
				z <= GetComponent<BaseTower>().z + caller.tower_options.mana_generator.range; z++)
				{
					if (caller.GetGameGrid().CheckIfInsideGrid (x, z))
					{
						caller.GetGameGrid().SetValue (x, z, grid_parameter.mana, caller.GetGameGrid().EnumTranslator (mana.connected));
					}
				}
			}
			Collider [] tower_collider_in_range_array = Physics.OverlapBox (caller.GetGameGrid().GetWorldTileCenter (GetComponent<BaseTower>().x, GetComponent<BaseTower>().z),
			new Vector3 (caller.tower_options.mana_generator.range + (transform.localScale.x / 2), 0, caller.tower_options.mana_generator.range + (transform.localScale.x / 2)) );
			foreach (Collider collider in tower_collider_in_range_array)
			{
				if (collider.tag == "tower")
				{
					collider.GetComponentInParent<BaseTower>().UpdateConnectedToMana ();
				}	
			}
			GetComponent<BaseTower>().UpdateConnectedToMana();
			caller.UpdateMana();
			caller.GetManaOutline ();
		}

		public void DisconnectTilesFromMana ()
		{
			for (int x = GetComponent<BaseTower>().x - caller.tower_options.mana_generator.range;
			x <= GetComponent<BaseTower>().x + caller.tower_options.mana_generator.range; x++)
			{
				for (int z = GetComponent<BaseTower>().z - caller.tower_options.mana_generator.range;
				z <= GetComponent<BaseTower>().z + caller.tower_options.mana_generator.range; z++)
				{
					caller.GetGameGrid().SetValue (x, z, grid_parameter.mana, caller.GetGameGrid().EnumTranslator (mana.unconnected));
				}
			}
			Collider [] tower_collider_in_range_array = Physics.OverlapBox (caller.GetGameGrid().GetWorldTileCenter (GetComponent<BaseTower>().x, GetComponent<BaseTower>().z),
			new Vector3 (caller.tower_options.mana_generator.range, 0, caller.tower_options.mana_generator.range) );
			foreach (Collider collider in tower_collider_in_range_array)
			{
				if (collider.tag == "tower" && collider.gameObject != gameObject)
				{
					collider.GetComponentInParent<BaseTower>().UpdateConnectedToMana ();
				}	
			}
			caller.GetManaOutline ();
		}

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}
	}

	public class TowerTest : MonoBehaviour
	{
		#region variable declarations

		public float cooldown_time;
		public tower_id type;
		public GameHandler caller;

		#endregion

		private void Update()
		{
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive () == true)
			{
				GameObject target_enemy = GetTargetEnemy(gameObject.transform.position, caller.tower_options.test.range, targeting_priority.most_progress);
				if (target_enemy != null)
				{
					GameObject projectile = Instantiate(GameObject.Find("Shot Projectile"));
					projectile.name = transform.name + " projectile object";
					projectile.transform.position = transform.position;
					projectile.AddComponent<ShotProjectile>().targeted_enemy = target_enemy;
					projectile.GetComponent<ShotProjectile>().caller = caller;
					projectile.GetComponent<ShotProjectile>().damage_tuple = GetComponent<BaseTower>().GetDamageDealt();
					GetComponent<BaseTower>().SetCooldown (caller.tower_options.test.cooldown_time);
				}
			}
		}
	}

	public class TowerBlizzard : MonoBehaviour
	{
		#region variable declarations

		private GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}

		#endregion

		private void Update()
		{
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive () == true)
			{
				foreach (GameObject enemy in GetListOfEnemiesInRange (transform.position, caller.tower_options.blizzard.range))
				{
					enemy.GetComponent<BaseEnemy>().MovementSpeedMultiplier (caller.tower_options.blizzard.slow_amount);
					GameHandler.Timer.Create (() => {enemy.GetComponent<BaseEnemy>().MovementSpeedMultiplier (1 / caller.tower_options.blizzard.slow_amount);},
					caller.tower_options.blizzard.slow_duration, "blizzard slow", enemy);
					Debug.DrawLine (transform.position, enemy.transform.position, Color.cyan, 0.25f);
				}
			}
		}
	}

	public class TowerTeleport : MonoBehaviour
	{

	}

	public class TowerWaterHose : MonoBehaviour
	{
		#region variable declarations

		private GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}

		#endregion

		private void Update()
		{
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive () == true)
			{
				GameObject target_enemy = GetTargetEnemy (transform.position, caller.tower_options.water_hose.range, GetComponent<BaseTower>().this_tower_targeting_priority);
				//target_enemy
			}
		}
	}

	public class TowerLightning : MonoBehaviour
	{
		#region variable declarations

		private GameHandler caller;
		List <GameObject> enemies_already_hit;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}

		#endregion

		private void Update()
		{
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive() == true)
			{
				GameObject target_enemy = GetTargetEnemy (transform.position, caller.tower_options.lightning.range, GetComponent<BaseTower>().this_tower_targeting_priority);
				if (target_enemy != null)
				{
					//target_enemy.GetComponent<BaseEnemy>().HealthAddition (- caller.tower_options.lightning.damage);
					enemies_already_hit = new List<GameObject> {target_enemy};
					for (int i = 0; i < caller.tower_options.lightning.amount_of_bounces; i++)
					{
						GameObject new_target_enemy = GetBounceTarget (enemies_already_hit [enemies_already_hit.Count - 1].transform.position);
						if (new_target_enemy != null)
						{
							new_target_enemy.GetComponent<BaseEnemy>().HealthAddition (- caller.tower_options.lightning.damage);
						}
						else
						{
							break;
						}
					}
					//drawing lightning effect lines
					Debug.DrawLine (transform.position, enemies_already_hit [0].transform.position, Color.yellow, 0.25f);
					for (int i = 0; i < enemies_already_hit.Count - 1; i++)
					{
						Debug.DrawLine (enemies_already_hit [i].transform.position, enemies_already_hit [i + 1].transform.position, Color.yellow, 0.25f);
					}
				}
			}
		}

		private GameObject GetBounceTarget (Vector3 previous_enemy_position)
		{
			Collider[] colliders_in_range = Physics.OverlapSphere(previous_enemy_position, caller.tower_options.lightning.bounce_range);
			List<Collider> enemies_in_range = new List<Collider>();
			foreach (Collider collider in colliders_in_range)
			{
				if (collider.gameObject.tag == "enemy" && enemies_already_hit.Contains (collider.gameObject) == false)
				{
					enemies_in_range.Add (collider);
				}
			}
			GameObject target_enemy = null;
			if (enemies_in_range.Count > 0)
			{
				List<float> distance_from_enemy_list = new List<float>();
				foreach (Collider enemy in enemies_in_range)
				{
					distance_from_enemy_list.Add (Vector3.Distance (previous_enemy_position, enemy.transform.position));
				}
				target_enemy = enemies_in_range [distance_from_enemy_list.IndexOf (distance_from_enemy_list.Min ())].gameObject;
				enemies_already_hit.Add (target_enemy);
			}
			return target_enemy;
		}
	}

	public class TowerOil : MonoBehaviour
	{
		#region variable declarations

		private GameHandler caller;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}

		#endregion

		private void Update()
		{
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive () == true)
			{
				GameObject target_enemy = GetTargetEnemy (transform.position, caller.tower_options.oil.range, GetComponent<BaseTower>().this_tower_targeting_priority);
				if (target_enemy != null)
				{
					OilTileConstructor (target_enemy.transform.position);
					GetComponent<BaseTower>().SetCooldown (caller.tower_options.oil.cooldown);
				}
			}
		}

		private void OilTileConstructor (Vector3 enemy_position)
		{
			GameObject oil_object = Instantiate (GameObject.Find ("Oil Template") );
			oil_object.transform.position = caller.GetGameGrid().GetWorldTileCenter (caller.GetGameGrid().GetXZ (enemy_position), gameObject.transform.localScale.y);
			GameHandler.Timer.Create (() => {Destroy (oil_object);}, caller.tower_options.oil.oil_on_tile_duration, "destroy this tile", oil_object);
		}
	}

	public class TowerFlameThrower : MonoBehaviour
	{
		#region variable declarations

		private GameHandler caller;
		private GameObject targeted_enemy = null;
		private float overheat_timer = 0;
		private bool overheated = false;
		List<GameObject> hit_list = new List<GameObject>();
		private GameObject flame_object;

		public void SetVariables (GameHandler caller)
		{
			this.caller = caller;
		}

		#endregion

		private void Start()
		{
			flame_object = Instantiate (GameObject.Find ("FlameThrower Collider Template") );
			flame_object.transform.position = transform.position;
			flame_object.transform.parent = transform;
			flame_object.AddComponent<FlameCollider> ().SetVariables (this);
		}

		private void Update()
		{
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive() == true)
			{
				targeted_enemy = GetTargetEnemy (transform.position, caller.tower_options.flamethrower.range, GetComponent<BaseTower>().this_tower_targeting_priority);
				if (targeted_enemy != null)
				{
					flame_object.GetComponent<MeshRenderer>().enabled = true;
					flame_object.transform.LookAt (targeted_enemy.transform.position);
					flame_object.transform.Rotate (180, 0, 0);
				}
				else
				{
					flame_object.GetComponent<MeshRenderer>().enabled = false;
				}
			}
			else
			{
				overheat_timer = 0;
			}
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive() == true && overheated == false && targeted_enemy != null)
			{
				foreach (GameObject enemy in hit_list)
				{
					enemy.GetComponent<BaseEnemy>().CalculateIncomingDamage (GetComponent<BaseTower>().GetDamageDealt ());
				}
				GetComponent<BaseTower>().SetCooldown (caller.tower_options.flamethrower.cooldown);
			}
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive () == true && overheat_timer > 0 || overheated == true)
			{
				overheat_timer -= Time.deltaTime;
				if (overheat_timer < 0)
				{
					overheated = false;
					overheat_timer = 0;
				}
			}
			if (GetComponent<BaseTower>().CheckCooldownAndManaAndWaveActive () == false)
			{
				overheat_timer += Time.deltaTime;
				if (overheat_timer >= caller.tower_options.flamethrower.overheat_threshold)
				{
					overheated = true;
				}
			}
		}

		public void AddEnemyToHitList (GameObject enemy)
		{
			hit_list.Add (enemy);
		}

		public void RemoveEnemyToHitList (GameObject enemy)
		{
			hit_list.Remove (enemy);
		}

		private class FlameCollider : MonoBehaviour
		{
			#region

			private TowerFlameThrower parent;

			public void SetVariables (TowerFlameThrower parent)
			{
				this.parent = parent;
			}

			#endregion

			private void OnTriggerEnter (Collider collider)
			{
				if (collider.tag == "enemy")
				{
					parent.AddEnemyToHitList (collider.gameObject);
				}
			}

			private void OnTriggerExit (Collider collider)
			{
				if (collider.tag == "enemy")
				{
					parent.RemoveEnemyToHitList (collider.gameObject);
				}
			}
		}
	}

	//public class Tower : MonoBehaviour
	//{
	//	#region variable declarations

	//	private GameHandler caller;

	//	public void SetVariables (GameHandler caller)
	//	{
	//		this.caller = caller;
	//	}

	//	#endregion

	//	private void Update()
	//	{
	//		if (CheckWaveIsActiveAndCooldown (GetComponent<BaseTower>() ) )
	//		{
	//			GetComponent<BaseTower>().cooldown = true;
	//			GameHandler.Timer.Create (() => {GetComponent<BaseTower>().cooldown = false;}, caller.tower_options.blizzard.cooldown, "cooldown", gameObject);
	//		}
	//	}
	//}

	public class ShotProjectile : MonoBehaviour
	{
		#region variable declarations

		public GameObject targeted_enemy;
		public GameHandler caller;
		public (int physical_damage, int fire_damage, int frost_damage, int electric_damage, int poison_damage, int magic_damage) damage_tuple;
		private Vector3 starting_position;

		#endregion

		private void Start()
		{
			starting_position = transform.position;
		}

		private void Update()
		{
			if (targeted_enemy == null)
			{
				Destroy(gameObject);
			}
			else
			{
				transform.LookAt(targeted_enemy.transform);
				transform.position += new Vector3 (targeted_enemy.transform.position.x - starting_position.x,
				targeted_enemy.transform.position.y - starting_position.y,
				targeted_enemy.transform.position.z - starting_position.z) * Time.deltaTime * caller.tower_options.test.projectile_speed;
			}
		}

		private void OnTriggerEnter (Collider other)
		{
			if (other.gameObject.tag == "enemy" || other.gameObject.tag == "enemy blocking")
			{
				Destroy (gameObject.transform.GetChild(0).gameObject);
				Destroy (gameObject);
			}
		}
	}

	public static GameObject GetTargetEnemy (Vector3 tower_position, float tower_range, targeting_priority priority)
	{
		Collider[] colliders_in_range = Physics.OverlapSphere(tower_position, tower_range);
		List<Collider> enemies_in_range = new List<Collider>();
		foreach (Collider collider in colliders_in_range)
		{
			if (collider.gameObject.tag == "enemy")
			{
				enemies_in_range.Add (collider);
			}
		}
		GameObject target_enemy = null;
		List<float> distance_from_enemy_list = new List<float>();
		List<int> enemy_health_list = new List<int>();
		List<int> enemy_distance_from_core_list = new List<int>();

		if (enemies_in_range.Count != 0)
		{
			switch (priority)
			{
				case targeting_priority.closest:
				foreach (Collider enemy in enemies_in_range)
				{
					distance_from_enemy_list.Add (Vector3.Distance (tower_position, enemy.transform.position));
				}
				target_enemy = enemies_in_range [distance_from_enemy_list.IndexOf (distance_from_enemy_list.Min ())].gameObject;
				break;

				case targeting_priority.furthest:
				foreach (Collider enemy in enemies_in_range)
				{
					distance_from_enemy_list.Add (Vector3.Distance (tower_position, enemy.transform.position));
				}
				target_enemy = enemies_in_range [distance_from_enemy_list.IndexOf (distance_from_enemy_list.Max ())].gameObject;
				break;

				case targeting_priority.lowest_health:
				foreach (Collider enemy in enemies_in_range)
				{
					enemy_health_list.Add (enemy.GetComponent<BaseEnemy>().GetCurrentHealth ());
				}
				target_enemy = enemies_in_range [enemy_health_list.IndexOf (enemy_health_list.Min ())].gameObject;
				break;

				case targeting_priority.highest_health:
				foreach (Collider enemy in enemies_in_range)
				{
					enemy_health_list.Add (enemy.GetComponent<BaseEnemy>().GetCurrentHealth ());
				}
				target_enemy = enemies_in_range [enemy_health_list.IndexOf (enemy_health_list.Max ())].gameObject;
				break;

				case targeting_priority.most_progress:
				foreach (Collider enemy in enemies_in_range)
				{
					enemy_distance_from_core_list.Add (enemy.GetComponent<BaseEnemy>().GetCurrentHealth ());
				}
				target_enemy = enemies_in_range [enemy_distance_from_core_list.IndexOf (enemy_distance_from_core_list.Min ())].gameObject;
				break;

				case targeting_priority.least_progress:
				foreach (Collider enemy in enemies_in_range)
				{
					enemy_distance_from_core_list.Add (enemy.GetComponent<BaseEnemy>().GetCurrentHealth ());
				}
				target_enemy = enemies_in_range [enemy_distance_from_core_list.IndexOf (enemy_distance_from_core_list.Max ())].gameObject;
				break;
			}
		}

		return target_enemy;
	}

	private static List<GameObject> GetListOfEnemiesInRange (Vector3 tower_position, float tower_range)
	{
		Collider[] colliders_in_range = Physics.OverlapSphere(tower_position, tower_range);
		List<GameObject> enemies_in_range = new List<GameObject>();
		foreach (Collider collider in colliders_in_range)
		{
			if (collider.gameObject.tag == "enemy")
			{
				enemies_in_range.Add (collider.gameObject);
			}
		}
		return enemies_in_range;
	}
}
