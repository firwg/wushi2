using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LiveHumanCache
{
    const int cache_inuse = 20;
    const int cache_free = 21;

    private GameObject Prefab;
    public E_EnemyType EnemyType = E_EnemyType.None;
    public int MaxCount;

    private List<GameObject> Cache = new List<GameObject>();

    public void Init()
    {
        GameObject g;

        switch (EnemyType)
        {
            case E_EnemyType.SwordsMan:
                Prefab = Resources.Load("Enemies/EnemySwordsMan") as GameObject;
                break;
            case E_EnemyType.Peasant:
                Prefab = Resources.Load("Enemies/EnemyPeasant") as GameObject;
                break;
            case E_EnemyType.TwoSwordsMan:
                Prefab = Resources.Load("Enemies/EnemyDoubleSwordsman") as GameObject;
                break;
            case E_EnemyType.Bowman:
                Prefab = Resources.Load("Enemies/EnemyBowman") as GameObject;
                break;
            case E_EnemyType.PeasantLow:
                Prefab = Resources.Load("Enemies/EnemyPeasantEasy") as GameObject;
                break;
            case E_EnemyType.MiniBoss01:
                Prefab = Resources.Load("Enemies/EnemyMiniBoss") as GameObject;
                break;
            case E_EnemyType.SwordsManLow:
                Prefab = Resources.Load("Enemies/EnemySwordsManEasy") as GameObject;
                break;
            /* case E_EnemyType.Boss01:
                Prefab = Resources.Load("Enemies/EnemyMiniBoss") as GameObject;
                break;
            /*case E_EnemyType.Boss02:
                Prefab = Resources.Load("Enemies/EnemyMiniBoss") as GameObject;
                break;
            case E_EnemyType.Boss03:
                Prefab = Resources.Load("Enemies/EnemyMiniBoss") as GameObject;
                break;*/
            case E_EnemyType.BossOrochi:
                Prefab = Resources.Load("Enemies/EnemyBossOrochi") as GameObject;
                break;
            default:
                Debug.LogError("HumanCache::Init -  Unknow enemy type");
                break;
        }

        if (Application.isEditor && Prefab == null)
            Debug.LogError(this.ToString() + " Prefab " + EnemyType + " is null");

        for (int i = 0; i < MaxCount; i++)
        {
            g = GameObject.Instantiate(Prefab) as GameObject;

            g.GetComponent<Agent>().EnemyType = EnemyType;

            g.name = g.name + i.ToString();
            g.SetActiveRecursively(false);
            g.layer = cache_free; 
            Cache.Add(g);
        }

       // GameObject.Destroy(Prefab);
       // Prefab = null;
        //Resources.UnloadUnusedAssets();
    }

    public GameObject Get()
    {
        int len = Cache.Count;
        GameObject obj;
        for (int i = 0; i < len; i++)
        {
            obj = Cache[i];
            if (obj.layer == cache_free)
            {
                obj.SetActiveRecursively(true);
                obj.transform.position = new Vector3(0, 0, 10000);
                obj.layer = cache_inuse;
                return obj;
            }
        }
        return null;
    }

    public bool Return(GameObject enemy)
    {
        int len = Cache.Count;
        GameObject obj;
        for (int i = 0; i < len; i++)
        {
            obj = (Cache[i] as GameObject);
            if (obj == enemy)
            {
                Mission.Instance.StartCoroutine(Hide(enemy));
                return true;
            }
        }
        return false;
    }

    IEnumerator Hide(GameObject enemy)
    {
        enemy.SetActiveRecursively(false);
        yield return new WaitForSeconds(0.5f);
        enemy.layer = cache_free;
    }
}       


[System.Serializable]
public class DeadHumanCache
{
    const int cache_inuse = 20;
    const int cache_free = 21;

    private GameObject Prefab;
    public E_EnemyType EnemyType = E_EnemyType.None;
    public int MaxCount;

    private List<GameObject>[] Cache = new List<GameObject>[(int)E_DeadBodyType.Max];

    public void Init()
    { //     Legs = 0,   Beheaded,    HalfBody,    SliceFrontBack,    SliceLeftRight,
        //   SwordsMan = 0,    Peasant = 1,    TwoSwordsMan = 2,     Bowman = 3,   
        string[][] resources = { new string[] { "Enemies/DeadSwordsmanHLegs", "Enemies/DeadSwordsmanHHead", "Enemies/DeadSwordsmanHBody", "Enemies/DeadSwordsmanVFront", "Enemies/DeadSwordsmanVSide" }, //E_EnemyType.SwordsMan
                                   new string[] { "Enemies/DeadPeasantHLegs", "Enemies/DeadPeasantHHead", "Enemies/DeadPeasantHBody", "Enemies/DeadPeasantVFront", "Enemies/DeadPeasantVSide" }, //E_EnemyType.Peasant
                                   new string[] { "Enemies/DeadTwoSwordsmanHLegs", "Enemies/DeadTwoSwordsmanHHead", "Enemies/DeadTwoSwordsmanHBody", "Enemies/DeadTwoSwordsmanVFront", "Enemies/DeadTwoSwordsmanVSide" }, //E_EnemyType.TwoSwordsMan
                                   new string[] { "Enemies/DeadBowmanH", "Enemies/DeadBowmanH", "Enemies/DeadBowmanH", "Enemies/DeadBowmanV", "Enemies/DeadBowmanV" }, //E_EnemyType.Bowman
                                   new string[] { "Enemies/DeadPeasantLowHLegs", "Enemies/DeadPeasantLowHHead", "Enemies/DeadPeasantLowHBody", "Enemies/DeadPeasantLowVFront", "Enemies/DeadPeasantLowVSide" }, //E_EnemyType.PeasantLow
                                   new string[] {}, // MiniBoss01
                                   new string[] { "Enemies/DeadSwordsmanLowHLegs", "Enemies/DeadSwordsmanLowHHead", "Enemies/DeadSwordsmanLowHBody", "Enemies/DeadSwordsmanLowVFront", "Enemies/DeadSwordsmanLowVSide" }, //E_EnemyType.SwordsManLow
                               };

       // Debug.Log("IOnit dead cache");

        GameObject g;


        for (int i = 0; i < (int)E_DeadBodyType.Max; i++)
        {
            Cache[i] = new List<GameObject>();

           // Debug.Log("Loading prefab " + resources[(int)EnemyType][i]);


            if (EnemyType != E_EnemyType.Bowman && EnemyType != E_EnemyType.SwordsMan && EnemyType != E_EnemyType.TwoSwordsMan && EnemyType != E_EnemyType.Peasant && EnemyType != E_EnemyType.PeasantLow && EnemyType != E_EnemyType.SwordsManLow)
            {
                Debug.LogError("DeadHumanCache Enemytype (" + EnemyType + ") is not allowed");
                continue;
            }

            Prefab = Resources.Load(resources[(int)EnemyType][i]) as GameObject;
            for (int ii = 0; ii < MaxCount; ii++)
            {
                g = GameObject.Instantiate(Prefab) as GameObject;

               // g.GetComponent<Agent>().EnemyType = EnemyType;

                g.name = g.name + i.ToString();
                g.SetActiveRecursively(false);
                g.layer = cache_free;
                Cache[i].Add(g);
            }

          //  GameObject.Destroy(Prefab);
           // Prefab = null;
            //Resources.UnloadUnusedAssets();
        }
    }

    public GameObject Get(E_DeadBodyType type)
    {
        List<GameObject> cache = Cache[(int)type];
        GameObject obj;
        for (int i = 0; i < cache.Count; i++)
        {
            obj = cache[i];
            if (obj.layer == cache_free)
            {
                obj.SetActiveRecursively(true);
                obj.transform.position = new Vector3(0, 0, 10000);
                obj.layer = cache_inuse;
                return obj;
            }
        }
        return null;
    }

    public bool Return(GameObject enemy)
    {
        for(int i = 0; i < Cache.Length;i++)
        {
            for (int ii = 0; ii < Cache[i].Count; ii++)
            {
                if (Cache[i][ii] == enemy)
                {
                    Mission.Instance.StartCoroutine(Hide(enemy));
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator Hide(GameObject enemy)
    {
        enemy.SetActiveRecursively(false);
        yield return new WaitForSeconds(0.5f);
        enemy.layer = cache_free;
    }
}

class LevelCache
{
}
