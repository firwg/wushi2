using UnityEngine;
using System.Collections;

public class SpawnPointEnemy : SpawnPoint
{
    public E_EnemyType EnemyType = E_EnemyType.None;
	public E_GameDifficulty Difficulty =  E_GameDifficulty.Normal;

    // Use this for initialization
    void Start()
    {
        enabled = false;
    }

    // We'll draw a gizmo in the scene view, so it can be found....
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.4f);

        Gizmos.DrawIcon(transform.position, "SpawnPoint.tif");
    }

}
