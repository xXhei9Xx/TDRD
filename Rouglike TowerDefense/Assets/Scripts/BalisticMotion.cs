using UnityEngine;
using static GameGrid;
using System;
using System.Collections.Generic;

public static class BallisticMotion
{
    public static void LaunchAtStationaryTarget (GameHandler caller, float peak_height, GameObject projectile, Vector3 target_position)
	{
		Rigidbody enemy_rigid_body = projectile.GetComponent<Rigidbody> ();
		float horizontal_distance = (float) Math.Pow ((Math.Pow (target_position.z - projectile.transform.position.z , 2f) + 
		Math.Pow (target_position.x - projectile.transform.position.x , 2f)) , 0.5f);
		float sin_horizontal_angle = 4 * peak_height / (float) Math.Pow ((Math.Pow (peak_height, 2) + Math.Pow (horizontal_distance, 2)), 0.5f);
		float cos_horizontal_angle = horizontal_distance / (float) Math.Pow ((Math.Pow (peak_height, 2) + Math.Pow (horizontal_distance, 2)), 0.5f);
		float tan_vertical_angle = (target_position.z - projectile.transform.position.z) / (target_position.x - projectile.transform.position.x);
		float starting_speed = (float) Math.Pow ((2 * peak_height * 9.81f / Math.Pow (sin_horizontal_angle, 2)), 0.5f);
		enemy_rigid_body.AddRelativeForce (new Vector3 ((1 / tan_vertical_angle) * cos_horizontal_angle,
		sin_horizontal_angle, tan_vertical_angle * cos_horizontal_angle) * starting_speed, ForceMode.Impulse);
	}

	public static void LaunchAtStationaryTarget (GameHandler caller, float peak_height, GameObject projectile, (int x, int z) target_position)
	{
		Vector3 target_position_vector = caller.GetGameGrid().GetWorldTileCenter (target_position);
		Rigidbody enemy_rigid_body = projectile.GetComponent<Rigidbody> ();
		float horizontal_distance = (float) Math.Pow ((Math.Pow (target_position_vector.z - projectile.transform.position.z , 2f) + 
		Math.Pow (target_position_vector.x - projectile.transform.position.x , 2f)) , 0.5f);
		float sin_horizontal_angle = 4 * peak_height / (float) Math.Pow ((Math.Pow (peak_height, 2) + Math.Pow (horizontal_distance, 2)), 0.5f);
		float cos_horizontal_angle = horizontal_distance / (float) Math.Pow ((Math.Pow (peak_height, 2) + Math.Pow (horizontal_distance, 2)), 0.5f);
		float tan_vertical_angle = (target_position_vector.z - projectile.transform.position.z) / (target_position_vector.x - projectile.transform.position.x);
		float starting_speed = (float) Math.Pow ((2 * peak_height * 9.81f / Math.Pow (sin_horizontal_angle, 2)), 0.5f);
		enemy_rigid_body.AddRelativeForce (new Vector3 ((1 / tan_vertical_angle) * cos_horizontal_angle,
		sin_horizontal_angle, tan_vertical_angle * cos_horizontal_angle) * starting_speed, ForceMode.Impulse);
	}

	public static void LaunchAtMovingEnemy (GameHandler caller, GameObject projectile, GameObject target_enemy)
	{
		float peak_height = 0.5f;
		float time_of_flight = 2 * peak_height / 9.81f;
		float enemy_movement_speed = target_enemy.GetComponent<Enemy.BaseEnemy>().GetMovementSpeed ();
		Vector3 target_position = GetFutureEnemyPosition (caller, time_of_flight, target_enemy);
		List <Collider> objects_in_path = new List<Collider> ();
		foreach (RaycastHit collider_hit in Physics.RaycastAll (new Ray (projectile.transform.position, target_position), Vector3.Distance (projectile.transform.position, target_position)))
		{
			if (collider_hit.collider.gameObject != projectile && collider_hit.collider.gameObject != target_enemy)
			{
				objects_in_path.Add (collider_hit.collider);
			}
		}
		if (objects_in_path.Count > 0)
		{

		}

		Rigidbody projectile_rigid_body = projectile.GetComponent<Rigidbody>();
		float horizontal_distance = (float)Math.Pow((Math.Pow(target_position.z - projectile.transform.position.z, 2f) +
		Math.Pow(target_position.x - projectile.transform.position.x, 2f)), 0.5f);
		float sin_horizontal_angle = 4 * peak_height / (float)Math.Pow((Math.Pow(peak_height, 2) + Math.Pow(horizontal_distance, 2)), 0.5f);
		float cos_horizontal_angle = horizontal_distance / (float)Math.Pow((Math.Pow(peak_height, 2) + Math.Pow(horizontal_distance, 2)), 0.5f);
		float tan_vertical_angle = (target_position.z - projectile.transform.position.z) / (target_position.x - projectile.transform.position.x);
		float starting_speed = (float)Math.Pow((2 * peak_height * 9.81f / Math.Pow(sin_horizontal_angle, 2)), 0.5f);
		projectile_rigid_body.AddRelativeForce(new Vector3((1 / tan_vertical_angle) * cos_horizontal_angle,
		sin_horizontal_angle, tan_vertical_angle * cos_horizontal_angle) * starting_speed, ForceMode.Impulse);
	}

	public static Vector3 GetFutureEnemyPosition (GameHandler caller, float time, GameObject target_enemy)
	{
		Vector3 target_position = Vector3.zero;
		float enemy_movement_speed = target_enemy.GetComponent<Enemy.BaseEnemy>().GetMovementSpeed ();
		float distance_from_next_position = Vector3.Distance (target_enemy.transform.position, caller.GetGameGrid().GetWorldTileCenter (target_enemy.GetComponent<Enemy.BaseEnemy>().GetNextTile ()));
		if (time > (distance_from_next_position / enemy_movement_speed))
		{
			float enemy_simulation_time = time - (distance_from_next_position / enemy_movement_speed);
			(int x, int z) [] enemy_path_array = target_enemy.GetComponent<Enemy.BaseEnemy>().GetPathTupleArray ();
			int current_path_counter = target_enemy.GetComponent<Enemy.BaseEnemy>().GetDistanceFromCore () - 1;
			
		}
		else
		{
			switch (target_enemy.GetComponent<Enemy.BaseEnemy>().GetMovementDirection ())
			{
				case grid_direction.up:
				target_position = target_enemy.transform.position + new Vector3 (0, 0, (enemy_movement_speed * time));
				break;

				case grid_direction.down:
				target_position = target_enemy.transform.position + new Vector3 (0, 0, - (enemy_movement_speed * time));
				break;

				case grid_direction.left:
				target_position = target_enemy.transform.position + new Vector3 ((enemy_movement_speed * time), 0, 0);
				break;

				case grid_direction.right:
				target_position = target_enemy.transform.position + new Vector3 (- (enemy_movement_speed * time), 0, 0);
				break;
			}
		}
		return target_position;
	}

	//public Vector3 PredictFallingObjectFuturePosition (GameObject projectile)
	//{
	//	Vector3 next_starting_point = projectile.transform.position;
	//	float time_increment = 0.05f;
	//	float velocity_x = projectile.GetComponent<Rigidbody>().linearVelocity.x;
	//	float velocity_y = projectile.GetComponent<Rigidbody>().linearVelocity.y;
	//	float velocity_z = projectile.GetComponent<Rigidbody>().linearVelocity.z;
	//	float time_passed = 0;
	//	bool hit_collider = false;
	//	float [] hit_points_array = new float [] {};
	//	RaycastHit 
	//	while (hit_collider == false)
	//	{
	//		Vector3 next_ending_point = next_starting_point + new Vector3 (velocity_z * time_increment,
	//		(velocity_y + (velocity_y - (time_increment * 9.81f)) / 2) * time_increment, velocity_z * time_increment);
	//		velocity_y -= (time_increment * 9.81f);
	//		bool hit_col = false;
	//		RaycastHit [] colliders_hit = Physics.RaycastAll (next_starting_point, next_ending_point, Vector3.Distance (next_starting_point, next_ending_point));
	//		foreach (RaycastHit collider_hit in colliders_hit)
	//		{
	//			if (collider_hit.collider.gameObject != projectile)
	//			{
	//				hit_col = true;
	//				break;
	//			}
	//		}
	//		if (hit_col == false)
	//		{
	//			next_starting_point = next_ending_point;
	//			time_passed += time_increment;
	//		}
	//		else
	//		{
	//			foreach (RaycastHit collider_hit in colliders_hit)
	//			{
	//				hit_points_array.Append (Vector3.Distance (projectile.transform.position, collider_hit.point));
	//			}
	//			float closest_point_index = Array.IndexOf (hit_points_array, hit_points_array.Min ());
	//			colliders_hit [closest_point_index];
	//			hit_collider = true;
	//		}
	//	}
	//}

	//public Vector3 PredictFallingObjectFuturePosition (GameObject projectile, float time)
	//{
		
	//}
}
