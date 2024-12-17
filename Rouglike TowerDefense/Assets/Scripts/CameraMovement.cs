using UnityEngine;
using static GameHandler;

public class CameraMovement : MonoBehaviour
{
	#region variable declarations

	GameGrid grid;
	GameHandler caller;
	Camera camera;
	bool grid_text;
	private camera_directions camera_direction = camera_directions.up;

	#region enum declarations

	private enum camera_directions
	{
		up,
		down,
		left,
		right
	}

	#endregion

	public void SetVariables (GameHandler caller)
	{
		this.caller = caller;
	}

	public void SetVariables (GameGrid grid, bool grid_text)
	{
		this.grid = grid;
		this.grid_text = grid_text;
	}

	#endregion

	private void Start()
	{
		camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}

	private void Update()
	{
		if (caller != null)
		{
			if (Input.GetKey (caller.gameplay_options.controls.move_down) == true && camera.transform.position.z > 0)
			{
				MoveCamera (camera_directions.up);
			}
			if (Input.GetKey (caller.gameplay_options.controls.move_up) == true && camera.transform.position.z < caller.gameplay_options.map.width_z)
			{
				MoveCamera (camera_directions.down);
			}
			if (Input.GetKey (caller.gameplay_options.controls.move_left) == true && camera.transform.position.x > 0)
			{
				MoveCamera (camera_directions.left);
			}
			if (Input.GetKey (caller.gameplay_options.controls.move_right) == true && camera.transform.position.x < caller.gameplay_options.map.length_x)
			{
				MoveCamera (camera_directions.right);
			}
			if (Input.GetKeyDown (caller.gameplay_options.controls.rotate_left) == true)
			{
				RotateCamera (90);
				if (caller.testing_options.display_grid_text == true)
				{
					RotateGridText (90);
				}
			}
			if (Input.GetKeyDown (caller.gameplay_options.controls.rotate_right) == true)
			{
				RotateCamera (-90);
				if (caller.testing_options.display_grid_text == true)
				{
					RotateGridText (-90);
				}
			}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0 && camera.transform.position.y > caller.gameplay_options.map.cell_length_x * 2)
			{
				camera.transform.position += new Vector3 (0, -1, 0);
			}
			if (Input.GetAxis ("Mouse ScrollWheel") < 0 && camera.transform.position.y < caller.gameplay_options.map.length_x + (caller.gameplay_options.map.cell_length_x * 2) )
			{
				camera.transform.position += new Vector3 (0, 1, 0);
			}
		}
		else
		{
			if (Input.GetKey (KeyCode.S) == true && camera.transform.position.z > 0)
			{
				MoveCamera (camera_directions.up);
			}
			if (Input.GetKey (KeyCode.W) == true && camera.transform.position.z < grid.width_z)
			{
				MoveCamera (camera_directions.down);
			}
			if (Input.GetKey (KeyCode.A) == true && camera.transform.position.x > 0)
			{
				MoveCamera (camera_directions.left);
			}
			if (Input.GetKey (KeyCode.D) == true && camera.transform.position.x < grid.length_x)
			{
				MoveCamera (camera_directions.right);
			}
			if (Input.GetKeyDown (KeyCode.Q) == true)
			{
				RotateCamera (90);
				if (grid_text == true)
				{
					RotateGridText (90);
				}
			}
			if (Input.GetKeyDown (KeyCode.E) == true)
			{
				RotateCamera (-90);
				if (grid_text == true)
				{
					RotateGridText (-90);
				}
			}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0 && camera.transform.position.y > grid.cell_length_x * 2)
			{
				camera.transform.position += new Vector3 (0, -1, 0);
			}
			if (Input.GetAxis ("Mouse ScrollWheel") < 0 && camera.transform.position.y < grid.length_x + (grid.cell_length_x * 2) )
			{
				camera.transform.position += new Vector3 (0, 1, 0);
			}
		}
	}

	#region camera: rotate, move, rotate grid text

	private void RotateCamera (float angle_z)
	{
		camera.transform.Rotate (0, 0, angle_z);
		if (angle_z < 0)
		{
			switch (camera_direction)
			{
				case camera_directions.up:
				camera_direction = camera_directions.left;
				break;

				case camera_directions.down:
				camera_direction = camera_directions.right;
				break;

				case camera_directions.left:
				camera_direction = camera_directions.down;
				break;

				case camera_directions.right:
				camera_direction = camera_directions.up;
				break;
			}
		}
		if (angle_z > 0)
		{
			switch (camera_direction)
			{
				case camera_directions.up:
				camera_direction = camera_directions.right;
				break;

				case camera_directions.down:
				camera_direction = camera_directions.left;
				break;

				case camera_directions.left:
				camera_direction = camera_directions.up;
				break;

				case camera_directions.right:
				camera_direction = camera_directions.down;
				break;
			}
		}
	}

	private void MoveCamera (camera_directions movement_direction)
	{
		if (caller != null)
		{
			switch (camera_direction)
			{
				case camera_directions.up:
				switch (movement_direction)
				{
					case camera_directions.up:
					camera.transform.position += new Vector3 (0, 0, -Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed);
					break;

					case camera_directions.down:
					camera.transform.position += new Vector3 (0, 0, Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed);
					break;

					case camera_directions.left:
					camera.transform.position += new Vector3 (-Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed, 0, 0);
					break;

					case camera_directions.right:
					camera.transform.position += new Vector3 (Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed, 0, 0);
					break;
				}
				break;

				case camera_directions.down:
				switch (movement_direction)
				{
					case camera_directions.up:
					camera.transform.position += new Vector3 (0, 0, Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed);
					break;

					case camera_directions.down:
					camera.transform.position += new Vector3 (0, 0, -Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed);
					break;

					case camera_directions.left:
					camera.transform.position += new Vector3 (Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed, 0, 0);
					break;

					case camera_directions.right:
					camera.transform.position += new Vector3 (-Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed, 0, 0);
					break;
				}
				break;

				case camera_directions.left:
				switch (movement_direction)
				{
					case camera_directions.up:
					camera.transform.position += new Vector3 (-Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed, 0, 0);
					break;

					case camera_directions.down:
					camera.transform.position += new Vector3 (Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed, 0, 0);
					break;

					case camera_directions.left:
					camera.transform.position += new Vector3 (0, 0, Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed);
					break;

					case camera_directions.right:
					camera.transform.position += new Vector3 (0, 0, -Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed);
					break;
				}
				break;

				case camera_directions.right:
				switch (movement_direction)
				{
					case camera_directions.up:
					camera.transform.position += new Vector3 (Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed, 0, 0);
					break;

					case camera_directions.down:
					camera.transform.position += new Vector3 (-Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed, 0, 0);
					break;

					case camera_directions.left:
					camera.transform.position += new Vector3 (0, 0, -Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed);
					break;

					case camera_directions.right:
					camera.transform.position += new Vector3 (0, 0, Time.unscaledDeltaTime * caller.gameplay_options.ui.camera_movement_speed);
					break;
				}
				break;
			}
		}
		else
		{
			switch (camera_direction)
			{
				case camera_directions.up:
				switch (movement_direction)
				{
					case camera_directions.up:
					camera.transform.position += new Vector3 (0, 0, -Time.unscaledDeltaTime * 3);
					break;

					case camera_directions.down:
					camera.transform.position += new Vector3 (0, 0, Time.unscaledDeltaTime * 3);
					break;

					case camera_directions.left:
					camera.transform.position += new Vector3 (-Time.unscaledDeltaTime * 3, 0, 0);
					break;

					case camera_directions.right:
					camera.transform.position += new Vector3 (Time.unscaledDeltaTime * 3, 0, 0);
					break;
				}
				break;

				case camera_directions.down:
				switch (movement_direction)
				{
					case camera_directions.up:
					camera.transform.position += new Vector3 (0, 0, Time.unscaledDeltaTime * 3);
					break;

					case camera_directions.down:
					camera.transform.position += new Vector3 (0, 0, -Time.unscaledDeltaTime * 3);
					break;

					case camera_directions.left:
					camera.transform.position += new Vector3 (Time.unscaledDeltaTime * 3, 0, 0);
					break;

					case camera_directions.right:
					camera.transform.position += new Vector3 (-Time.unscaledDeltaTime * 3, 0, 0);
					break;
				}
				break;

				case camera_directions.left:
				switch (movement_direction)
				{
					case camera_directions.up:
					camera.transform.position += new Vector3 (-Time.unscaledDeltaTime * 3, 0, 0);
					break;

					case camera_directions.down:
					camera.transform.position += new Vector3 (Time.unscaledDeltaTime * 3, 0, 0);
					break;

					case camera_directions.left:
					camera.transform.position += new Vector3 (0, 0, Time.unscaledDeltaTime * 3);
					break;

					case camera_directions.right:
					camera.transform.position += new Vector3 (0, 0, -Time.unscaledDeltaTime * 3);
					break;
				}
				break;

				case camera_directions.right:
				switch (movement_direction)
				{
					case camera_directions.up:
					camera.transform.position += new Vector3 (Time.unscaledDeltaTime * 3, 0, 0);
					break;

					case camera_directions.down:
					camera.transform.position += new Vector3 (-Time.unscaledDeltaTime * 3, 0, 0);
					break;

					case camera_directions.left:
					camera.transform.position += new Vector3 (0, 0, -Time.unscaledDeltaTime * 3);
					break;

					case camera_directions.right:
					camera.transform.position += new Vector3 (0, 0, Time.unscaledDeltaTime * 3);
					break;
				}
				break;
			}
		}
	}

	private void RotateGridText (float angle_z)
	{
		RectTransform [] grid_text_transform_array = GameObject.Find("Text Collection").GetComponentsInChildren<RectTransform>();
		for (int i = 1; i < grid_text_transform_array.Length; i++)
		{
			grid_text_transform_array[i].Rotate (0, 0, angle_z);
		}
	}

	#endregion
}
