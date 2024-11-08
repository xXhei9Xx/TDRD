using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Tower;
using UnityEngine.UIElements;
using UnityEditor;
using Unity.VisualScripting;
using TMPro;


public class CardHandler : MonoBehaviour
{
	#region variable declarations
	
	private float timer = 0;
	private int target_amount_of_cards, max_hand_size, deck_size, cards_in_deck;
	public bool drawing = true, cant_draw = false, tower_discarding = false, ability_discarding = false, finished_discarding = false, after_picking = false, picking = false, choosing_upgrade = false;
	private int cards_in_hand = 0;
	private GameHandler caller;
	private GameObject game_handler, main_camera;
	private GameObject upgrade_template;
	private GameObject tower_test_template;
	private GameObject tower_wall_template;
	private GameObject tower_mana_template;
	private GameObject blizzard_template;
	private GameObject oil_template;
	private GameObject lightning_template;
	private GameObject tower_flamethrower_template;
	private GameObject ability_repair_template;
	private List<GameObject> card_objects_list = new List<GameObject>();
	public int mouse_on_card_number;
	private Vector3 card_draw_starting_position = new Vector3 (-1200, 0, 0), card_discard_target_position = new Vector3 (2400, -800, 0);
	public List <card_id> cards_in_deck_list;
	public List <card_id> cards_in_graveyard_list = new List<card_id> ();

	#endregion

	#region enum declarations
	
	#region card id

	public enum card_id
	{
		tower_wall,
		tower_test,
		tower_mana,
		blizzard,
		oil,
		lightning,
		tower_flamethrower,
		ability_repair
	}

	public GameObject GetCardTemplate (card_id card_id)
	{
		switch (card_id)
		{
			default: return null;

			case card_id.ability_repair: return ability_repair_template;

			case card_id.tower_test: return tower_test_template;

			case card_id.tower_wall: return tower_wall_template;

			case card_id.tower_mana: return tower_mana_template;

			case card_id.blizzard: return blizzard_template;

			case card_id.oil: return oil_template;

			case card_id.lightning: return lightning_template;

			case card_id.tower_flamethrower: return tower_flamethrower_template;
		}
	}

	private static tower_id GetTowerTypeFromCardId (card_id card_id)
	{
		switch (card_id)
		{
			case card_id.tower_test: return tower_id.test;

			case card_id.tower_wall: return tower_id.wall;

			case card_id.tower_mana: return tower_id.mana;

			case card_id.blizzard: return tower_id.blizzard;

			case card_id.oil: return tower_id.oil;

			case card_id.lightning: return tower_id.lightning;

			case card_id.tower_flamethrower: return tower_id.flamethrower;

			default: return tower_id.none;
		}
	}

	#endregion
	#region upgrade id

	public enum upgrade_id
	{
		tower_test_physical_damage,
		tower_test_fire_damage,
		tower_test_frost_damage,
		tower_test_electric_damage,
		tower_test_poison_damage,
		tower_test_magic_damage,
		tower_test_cooldown,
		tower_test_mana_requirement,
		tower_test_health
	}

	public List<upgrade_id> GetCardUpgrades (card_id card, List<upgrade_id> card_upgrades)
	{
		List<upgrade_id> upgrades_list = new List<upgrade_id>();
		switch (card)
		{
			case card_id.tower_test:
			upgrades_list.Add (upgrade_id.tower_test_physical_damage);
			upgrades_list.Add (upgrade_id.tower_test_fire_damage);
			upgrades_list.Add (upgrade_id.tower_test_frost_damage);
			upgrades_list.Add (upgrade_id.tower_test_electric_damage);
			upgrades_list.Add (upgrade_id.tower_test_poison_damage);
			upgrades_list.Add (upgrade_id.tower_test_magic_damage);
			upgrades_list.Add (upgrade_id.tower_test_cooldown);
			upgrades_list.Add (upgrade_id.tower_test_mana_requirement);
			upgrades_list.Add (upgrade_id.tower_test_health);
			break;
		}
		foreach (upgrade_id upgrade in card_upgrades)
		{
			upgrades_list.Remove (upgrade);
		}
		return upgrades_list;
	}

	#endregion

	#endregion

    void Start()
    {
		game_handler = GameObject.Find("GameHandler");
		main_camera = GameObject.Find("Main Camera");
		caller = game_handler.GetComponent<GameHandler>();
		cards_in_deck_list = caller.deck_options.cards_in_deck;
        
		target_amount_of_cards = caller.deck_options.starting_card_amount_in_hand;
		upgrade_template = GameObject.Find ("Upgrade Template");
		tower_test_template = GameObject.Find ("Tower Test Template");
		tower_wall_template = GameObject.Find ("Tower Wall Template");
		ability_repair_template = GameObject.Find ("Ability Repair Template");
		tower_mana_template = GameObject.Find ("Tower Mana Template");
		blizzard_template = GameObject.Find ("Tower Blizzard Template");
		oil_template = GameObject.Find ("Tower Oil Template");
		lightning_template = GameObject.Find ("Tower Lightning Template");
		tower_flamethrower_template = GameObject.Find ("Tower FlameThrower Template");
		deck_size = cards_in_deck_list.Count;
		max_hand_size = caller.deck_options.max_hand_size;
		target_amount_of_cards = caller.deck_options.starting_card_amount_in_hand;
    }

    void Update()
    {
		//draw phase of the turn
		if ((cards_in_hand >= target_amount_of_cards && drawing == true && timer > (caller.gameplay_options.ui.card_movement_time + 0.1f)) ||
		cant_draw == true && timer > (caller.gameplay_options.ui.card_movement_time + 0.1f))
		{
			drawing = false;
		}
        if (drawing == true && cant_draw == false && timer > (caller.gameplay_options.ui.card_movement_time + 0.1f))
		{
			cards_in_hand++;
			DrawCard ();
			timer = 0;
		}
		if (caller.CheckIfWaveActive() == true && tower_discarding == false && finished_discarding == false)
		{
			cant_draw = false;
			tower_discarding = true;
		}
		if (tower_discarding == true && timer > (caller.gameplay_options.ui.card_movement_time + 0.1f))
		{
			for (int i = cards_in_hand; i > 0; i--)
			{
				if (card_objects_list [i - 1].GetComponent<Card>().tower_id != tower_id.none)
				{
					cards_in_hand--;
					DiscardCard (i);
					card_objects_list.Remove (card_objects_list [i - 1]);
					RecenterCards (i - 1);
					timer = 0;
					break;
				}
			}
			if (timer != 0)
			{
				finished_discarding = true;
			}
		}
		if (ability_discarding == true && timer > (caller.gameplay_options.ui.card_movement_time + 0.1f))
		{
			for (int i = cards_in_hand; i > 0;)
			{
				cards_in_hand--;
				DiscardCard (i);
				card_objects_list.Remove (card_objects_list [i - 1]);
				RecenterCards (i - 1);
				timer = 0;
				break;
			}
			if (timer != 0)
			{
				finished_discarding = true;
			}
		}
		timer += Time.deltaTime;
    }

	public void DrawCard ()
	{
		//checking if drawing is possible
		if (cards_in_deck_list.Count == 0 && cards_in_graveyard_list.Count == 0)
		{
			cant_draw = true;
		}
		else
		{
			// shuffling graveyard back into the deck
			if (cards_in_deck_list.Count == 0)
			{
				for (int i = cards_in_graveyard_list.Count - 1; i > 0; i--)
				{
					cards_in_deck_list.Add (cards_in_graveyard_list [i]);
					cards_in_graveyard_list.Remove (cards_in_graveyard_list [i]);
				}
			}
			//picking a random card from the deck
			int card_index = caller.RandomInt (0, cards_in_deck_list.Count - 1);
			card_id id = cards_in_deck_list [card_index];
			cards_in_deck_list.RemoveAt (card_index);
			//card constructor
			GameObject current_card = Instantiate (GetCardTemplate (id));
			current_card.AddComponent<Card>().target_position = new Vector3 (0, -1500, 0);
			current_card.GetComponent<Card>().card_handler = this;
			current_card.GetComponent<Card>().caller = caller;
			current_card.GetComponent<Card>().card_movement_time = caller.gameplay_options.ui.card_movement_time;
			current_card.GetComponent<Card>().starting_position = new Vector3 (-1200, 0, 0);
			current_card.GetComponent<Card>().card_id = id;
			card_objects_list.Add(current_card);
			current_card.GetComponent<Card>().card_number = card_objects_list.Count;
			current_card.name = "card " + card_objects_list.Count;
			current_card.transform.SetParent(transform);
			//adjusting position of cards in hand
			current_card.transform.localPosition = card_draw_starting_position;
			current_card.transform.Rotate (0, 0, (cards_in_hand * caller.gameplay_options.ui.space_between_cards));
			RecenterCards (0);
		}
	}

	public void DiscardCard (int position)
	{
		card_objects_list [position - 1].GetComponent<Card>().discarding = true;
		cards_in_graveyard_list.Add (card_objects_list [position - 1].GetComponent<Card>().card_id);
	}

	public void RecenterCards (int position)
	{
		//cards to the right of selected position
		for (int i = 0; i < position; i++)
		{
			card_objects_list [i].GetComponent<Card>().single_rotation_increment_z = caller.gameplay_options.ui.space_between_cards;
			card_objects_list [i].GetComponent<Card>().rotating = true;
		}
		//cards to the left of selected position
		for (int i = position; i < card_objects_list.Count; i++)
		{
			card_objects_list [i].GetComponent<Card>().single_rotation_increment_z = -caller.gameplay_options.ui.space_between_cards;
			card_objects_list [i].GetComponent<Card>().rotating = true;
		}
	}

	public void RenameCards ()
	{
		//readjusting naming scheme after using or removing a card
		for (int i = 1; i <= card_objects_list.Count; i++)
		{
			card_objects_list [i - 1].name = "card " + i;
			card_objects_list [i - 1].GetComponent<Card>().card_number = i;
		}
	}

	public void ToggleHandCardsVisibility ()
	{
		//make map visible while placing towers
		for (int i = 0; i < card_objects_list.Count; i++)
		{
			if (card_objects_list [i].GetComponent<UnityEngine.UI.Image>().enabled == true)
			{
				card_objects_list [i].GetComponent<UnityEngine.UI.Image>().enabled = false;
				card_objects_list [i].transform.GetChild (0).transform.gameObject.SetActive(false);
			}
			else
			{
				card_objects_list [i].GetComponent<UnityEngine.UI.Image>().enabled = true;
				card_objects_list [i].transform.GetChild (0).transform.gameObject.SetActive(true);
			}
		}
	}

	public void AddingNewCardToDeck ()
	{
		picking = true;
		List<GameObject> cards_to_pick = new List<GameObject> ();
		List<int> cards_to_pick_indexes = new List<int> ();
		var cards_to_pick_indexes_var = Enum.GetValues(typeof(card_id));
		foreach (int i in cards_to_pick_indexes_var)
		{
			cards_to_pick_indexes.Add (i);
		}
		for (int i = 0; i < caller.deck_options.amount_of_new_cards_to_choose; i++)
		{
			int number = caller.RandomInt (0, cards_to_pick_indexes.Count - 1);
			int index = cards_to_pick_indexes [number];
			GameObject card = Instantiate (GetCardTemplate ((card_id)index));
			card.transform.SetParent (GameObject.Find ("Camera Screen").transform);
			card.AddComponent<CardToPick>().SetVariables ((card_id)index, caller, this, cards_to_pick);
			cards_to_pick_indexes.Remove (index);
			cards_to_pick.Add(card);
		}
		foreach (GameObject card in cards_to_pick)
		{
			card.transform.localPosition = new Vector3 ((-500 + (cards_to_pick.IndexOf(card) * 500)), -1000, 0);
		}
	}

	public void AddCardToDeck (card_id card_Id)
	{
		cards_in_deck_list.Add (card_Id);
	}

	public void ChoosingCardUpgrades (Card card)
	{
		picking = true;
		List<GameObject> upgrades_to_pick = new List<GameObject> ();
		var upgrades_to_pick_enum_var = GetCardUpgrades (card.card_id, card.card_upgrades);
		for (int i = 0; i < caller.deck_options.amount_of_upgrades_to_choose; i++)
		{
			int number = caller.RandomInt (0, upgrades_to_pick_enum_var.Count - 1);
			var upgrade_enum = (upgrade_id)number;
			GameObject upgrade = Instantiate (upgrade_template);
			upgrade.GetComponentInChildren<TextMeshProUGUI>().text = upgrade_enum.ToString();
			card.transform.SetParent (GameObject.Find ("Camera Screen").transform);
			card.AddComponent<UpgradeToPick>().SetVariables (upgrade_enum, card, caller, this, upgrades_to_pick);
			upgrades_to_pick_enum_var.Remove (upgrade_enum);
			upgrades_to_pick.Add (upgrade);
		}
		foreach (GameObject upgrade in upgrades_to_pick)
		{
			card.transform.localPosition = new Vector3 ((-500 + (upgrades_to_pick.IndexOf (upgrade) * 500)), -1000, 0);
		}
	}

	public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		#region variable delcarations

		public int card_number;
		private float timer = 0;
		private GameObject tower_object;
		public GameHandler caller;
		public CardHandler card_handler;
		public bool auto_moving = true, rotating = true, mouse_tracking = false, mouse_on_card = false, discarding = false, shifting_card = false;
		public float card_movement_time;
		public Vector3 target_position, starting_position;
		public float single_rotation_increment_z;
		public float movement_speed_multiplier = 1;
		public float rotation_speed_multiplier = 1;
		public Vector3 target_rotation;
		public card_id card_id;
		public tower_id tower_id;
		public List<upgrade_id> card_upgrades = null;

		#endregion

		private void Start()
		{
			//assigning tower type
			tower_id = GetTowerTypeFromCardId (card_id);
		}

		private void Update()
		{
			//rotating and movement timer
			if (auto_moving == true || rotating == true || discarding == true)
			{
				timer += Time.deltaTime / card_movement_time;
			}
			//moving from starting position to the card hand position
			if (auto_moving == true)
			{
				MovingToTargetPosition (timer, movement_speed_multiplier);
			}
			if (rotating == true)
			{
				RotateOneIncrement (timer, rotation_speed_multiplier);
			}
			//resetting the movement and rotation timer and shifting boolean
			if (rotating == false && auto_moving == false)
			{
				if (timer != 0)
				{
					timer = 0;
				}
				if (shifting_card == true)
				{
					rotation_speed_multiplier = 1;
					shifting_card = false;
				}
			}
			if (discarding == true)
			{
				transform.localPosition = target_position + ((card_handler.card_discard_target_position - target_position) * timer);
				if (timer >= 1)
				{
					Destroy (gameObject);
				}
			}
			//Choosing this card to be upgraded
			if (Input.GetMouseButtonDown(caller.gameplay_options.controls.MouseButtonTranslator(caller.gameplay_options.controls.drag_card)) &&
			mouse_on_card == true && mouse_tracking == false && card_handler.drawing == false && rotating == false && card_handler.picking == true)
			{
				
			}
			//initializing moving the card along with the mouse
			if (Input.GetMouseButtonDown(caller.gameplay_options.controls.MouseButtonTranslator(caller.gameplay_options.controls.drag_card)) &&
			mouse_on_card == true && mouse_tracking == false && card_handler.drawing == false && rotating == false)
			{
				target_rotation = transform.rotation.eulerAngles;
				target_position = transform.localPosition;
				gameObject.transform.Rotate(0, 0, -target_rotation.z);
				mouse_tracking = true;
			}
			if (mouse_tracking == true)
			{
				transform.position = Input.mousePosition - new Vector3 (0, 900, 0);
				//shifting card positions between each other
				if (card_handler.mouse_on_card_number != card_number && card_handler.mouse_on_card_number != 0)
				{
					if (card_handler.card_objects_list [card_handler.mouse_on_card_number - 1].GetComponent<Card>().shifting_card == false)
					{
						target_rotation = card_handler.card_objects_list [card_handler.mouse_on_card_number - 1].transform.rotation.eulerAngles;
						if (card_handler.mouse_on_card_number > card_number)
						{
							for (int i = card_handler.mouse_on_card_number; i > card_number; i--)
							{
								if (card_handler.card_objects_list [i - 1].GetComponent<Card>().shifting_card == false)
								{
									card_handler.card_objects_list [i - 1].GetComponent<Card>().single_rotation_increment_z = - 2 * caller.gameplay_options.ui.space_between_cards;
									card_handler.card_objects_list [i - 1].GetComponent<Card>().rotation_speed_multiplier = 2;
									card_handler.card_objects_list [i - 1].GetComponent<Card>().rotating = true;
									card_handler.card_objects_list [i - 1].GetComponent<Card>().shifting_card = true;
								}
							}
						}
						else
						{
							for (int i = card_handler.mouse_on_card_number; i < card_number; i++)
							{
								if (card_handler.card_objects_list [i - 1].GetComponent<Card>().shifting_card == false)
								{
									card_handler.card_objects_list [i - 1].GetComponent<Card>().single_rotation_increment_z = 2 * caller.gameplay_options.ui.space_between_cards;
									card_handler.card_objects_list [i - 1].GetComponent<Card>().rotation_speed_multiplier = 2;
									card_handler.card_objects_list [i - 1].GetComponent<Card>().rotating = true;
									card_handler.card_objects_list [i - 1].GetComponent<Card>().shifting_card = true;
								}
							}
						}
						card_handler.card_objects_list.RemoveAt (card_number - 1);
						card_handler.card_objects_list.Insert (card_handler.mouse_on_card_number - 1, gameObject);
						card_handler.RenameCards();
						transform.SetSiblingIndex (card_number);
						card_handler.mouse_on_card_number = 0;
					}
				}
				if (GetComponent<UnityEngine.UI.Image>().enabled == false)
				{
					Ray mouse_world_ray = card_handler.main_camera.GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);
					Physics.Raycast (mouse_world_ray, out RaycastHit rayCastHit);
					var mouse_xz = caller.GetGameGrid().GetXZ (rayCastHit.point);
					if ((mouse_xz.x >= 0 && mouse_xz.x < caller.GetGameGrid().length_x && mouse_xz.z >= 0 && mouse_xz.z < caller.GetGameGrid().width_z) &&
					caller.GetGameGrid().GetValue (mouse_xz.x, mouse_xz.z, GameGrid.grid_parameter.object_type) == 0)
					{
						SetTowerPositionXZ (mouse_xz.x, mouse_xz.z);
					}
				}
				//transforming the card into tower
				if(Input.mousePosition.y > 200f && GetComponent<UnityEngine.UI.Image>().enabled == true)
				{
					card_handler.ToggleHandCardsVisibility ();
					transform.GetChild (0).transform.gameObject.SetActive(false);
					Ray mouse_world_ray = card_handler.main_camera.GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);
					Physics.Raycast (mouse_world_ray, out RaycastHit rayCastHit);
					var mouse_xz = caller.GetGameGrid().GetXZ (rayCastHit.point);
					if ((mouse_xz.x < 0 || mouse_xz.x >= caller.GetGameGrid().length_x || mouse_xz.z < 0 || mouse_xz.z >= caller.GetGameGrid().width_z) ||
					caller.GetGameGrid().GetValue (mouse_xz.x, mouse_xz.z, GameGrid.grid_parameter.object_type) != 0)
					{
						mouse_xz = caller.GetGameGrid().RandomTile (GameGrid.object_type.empty);
					}
					Tower tower = new Tower (tower_id, 
					caller.GetGameGrid().GetWorldTileCenter(mouse_xz.x, mouse_xz.z, 1), caller, card_upgrades, out tower_object);
					
				}
				if (!Input.GetMouseButton(caller.gameplay_options.controls.MouseButtonTranslator(caller.gameplay_options.controls.drag_card)))
				{
					//final tower placement
					if (GetComponent<UnityEngine.UI.Image>().enabled == false)
					{
						Ray mouse_world_ray = card_handler.main_camera.GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);
						Physics.Raycast (mouse_world_ray, out RaycastHit rayCastHit);
						var mouse_xz = caller.GetGameGrid().GetXZ (rayCastHit.point);
						SetTowerPositionXZ (mouse_xz.x, mouse_xz.z);
						if (card_id == card_id.tower_mana)
						{
							tower_object.GetComponent<ManaGenerator>().ConnectTilesToMana ();
						}
						tower_object.GetComponent<BaseTower>().UpdateConnectedToMana ();
						caller.UpdateMana();
						card_handler.cards_in_hand -= 1;
						card_handler.ToggleHandCardsVisibility ();
						card_handler.card_objects_list.RemoveAt (card_number - 1);
						card_handler.RenameCards ();
						card_handler.RecenterCards (card_number - 1);
						Destroy(gameObject);
					}
					//returning the card to initial position
					else
					{
						caller.SetAllSpawnerPathfinding ();
						gameObject.transform.Rotate(0, 0, target_rotation.z);
						transform.localPosition = target_position;
						mouse_tracking = false;
					}
				}
				//cancel tower placement
				if (Input.GetMouseButton(caller.gameplay_options.controls.MouseButtonTranslator(caller.gameplay_options.controls.cancel)))
				{
					if (tower_object != null)
					{
						tower_object.GetComponent<BaseTower>().DestroyThisTower ();
						caller.SetAllSpawnerPathfinding ();
					}
					if (GetComponent<UnityEngine.UI.Image>().enabled == false)
					{
						card_handler.ToggleHandCardsVisibility ();
					}
					gameObject.transform.Rotate(0, 0, target_rotation.z);
					transform.localPosition = target_position;
					mouse_tracking = false;
				}
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			mouse_on_card = true;
			card_handler.mouse_on_card_number = card_number;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			mouse_on_card = false;
		}

		public void Add_Upgrade (upgrade_id upgrade_id)
		{
			card_upgrades.Add (upgrade_id);
		}

		public void MovingToTargetPosition (float timer, float moving_speed_multiplier)
		{
			transform.localPosition = starting_position + ((target_position - starting_position) * timer * moving_speed_multiplier);
			if (timer >= 1 / moving_speed_multiplier)
			{
				if (auto_moving == true)
				{
					auto_moving = false;
					transform.localPosition = target_position;
				}
			}
		}

		public void RotateOneIncrement (float timer, float rotating_speed_multiplier)
		{
			gameObject.transform.Rotate(0, 0, single_rotation_increment_z * Time.deltaTime * rotating_speed_multiplier / card_movement_time);
			if (timer >= 1 / rotating_speed_multiplier)
			{
				rotating = false;
				target_rotation = transform.rotation.eulerAngles;
				Vector3 rotation_gap = new Vector3 (0, 0, target_rotation.z - (float) Math.Ceiling (target_rotation.z));
				gameObject.transform.Rotate (-rotation_gap);
			}
		}

		private void SetTowerPositionXZ (int x, int z)
		{
			tower_object.transform.position = caller.GetGameGrid().GetWorldTileCenter(x, z, 1);
			tower_object.GetComponent<BaseTower>().SetXZ ();
		}

		public void DestroyCardComponent ()
		{
			Destroy (this);
		}
	}

	public class CardToPick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		#region variable declarations

		private bool mouse_on_card = false;
		private GameHandler caller;
		private CardHandler card_handler;
		private card_id card_id;
		private List<GameObject> cards_to_pick;

		#endregion

		private void Update()
		{
			if (mouse_on_card == true && Input.GetMouseButtonDown (caller.gameplay_options.controls.MouseButtonTranslator (caller.gameplay_options.controls.drag_card)))
			{
				card_handler.AddCardToDeck (card_id);
				card_handler.picking = false;
				card_handler.after_picking = true;
				foreach (GameObject card in cards_to_pick)
				{
					Destroy (card);
				}
			}
		}

		public void SetVariables (card_id card_id, GameHandler caller, CardHandler card_handler, List<GameObject> cards_to_pick)
		{
			this.card_id = card_id;
			this.caller = caller;
			this.card_handler = card_handler;
			this.cards_to_pick = cards_to_pick;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			mouse_on_card = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			mouse_on_card = false;
		}
	}

	public class UpgradeToPick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		#region variable declarations

		private bool mouse_on_card = false;
		private GameHandler caller;
		private CardHandler card_handler;
		private List<GameObject> upgrades_to_pick;
		private upgrade_id upgrade_id;
		private Card card;

		#endregion

		private void Update()
		{
			if (mouse_on_card == true && Input.GetMouseButtonDown (caller.gameplay_options.controls.MouseButtonTranslator (caller.gameplay_options.controls.drag_card)))
			{
				card.card_upgrades.Add (upgrade_id);
				card_handler.picking = false;
				card_handler.after_picking = true;
				foreach (GameObject card in upgrades_to_pick)
				{
					Destroy (card);
				}
			}
		}

		public void SetVariables (upgrade_id upgrade_id, Card card, GameHandler caller, CardHandler card_handler, List<GameObject> upgrades_to_pick)
		{
			this.upgrade_id = upgrade_id;
			this.card = card;
			this.caller = caller;
			this.card_handler = card_handler;
			this.upgrades_to_pick = upgrades_to_pick;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			mouse_on_card = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			mouse_on_card = false;
		}
	}
}