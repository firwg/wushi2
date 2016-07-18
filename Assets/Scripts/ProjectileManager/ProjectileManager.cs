using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ProjectileManager : MonoBehaviour 
{

    public GameObject Arrow;
    private ArrowProjectileCache Cache = new ArrowProjectileCache();

    private List<ArrowProjectile> ArrowsInAir = new List<ArrowProjectile>();

    public static ProjectileManager Instance;

	// Use this for initialization
	void Awake () 
    {
        Instance = this;
        Cache.Init(Arrow, 10);
	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < ArrowsInAir.Count; i++)
        {
            ArrowsInAir[i].Update();
           // Debug.Log("Update arrow !!");
        }
	}

    void FixedUpdate()
    {
        for (int i = 0; i < ArrowsInAir.Count; i++)
        {
            if (ArrowsInAir[i].IsFinished() == false)
                continue;

            Cache.Return(ArrowsInAir[i]);
            ArrowsInAir.RemoveAt(i);
            break;
        }
    }

    public void SpawnArrow(Agent attacker, Vector3 pos, Vector3 dir, float speed, float damage )
    {
        //Debug.Log("Spawning arrow !! " + pos.ToString() + " " + dir.ToString() + " " + speed  );
        ArrowProjectile a =  Cache.Get();
        a.Init(attacker, pos, dir, speed, damage);
        ArrowsInAir.Add(a);
    }
}
