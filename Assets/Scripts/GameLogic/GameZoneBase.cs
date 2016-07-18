using UnityEngine;using System.Collections;using System.Collections.Generic;


public class GameZoneBaze : MonoBehaviour
{
 
    public SpawnPointPlayer PlayerSpawn;

    protected List<Agent> _Enemies = new List<Agent>();
    protected List<GameObject> DeadBodies = new List<GameObject>();

    protected GameObject GameObject;

    public List<Agent> Enemies { get { return _Enemies; } }


    public void AddEnemy(Agent enemy)
    {
//        State = E_State.E_IN_PROGRESS;

        _Enemies.Add(enemy);
        //Debug.Log(Time.timeSinceLevelLoad + " add enemy " + enemy.gameObject.name);
    }

    public virtual void Enable()
    {
        //   Debug.Log(GameObject.name + " Enable");
        GameObject.SetActiveRecursively(true);
    }

    public virtual void SetInProgress()
    {

    }

    public void Disable()
    {
        //   Debug.Log(GameObject.name + " Disable");

        GameObject.SetActiveRecursively(false);
    }

    public void Restart()
    {
        Reset();
    }

    public virtual void Reset()
    {
        StopAllCoroutines();

        for (int i = _Enemies.Count - 1; i >= 0; i--)
            Mission.Instance.ReturnHuman(_Enemies[i].GameObject);

        _Enemies.Clear();

        for (int i = DeadBodies.Count - 1; i >= 0; i--)
            Mission.Instance.ReturnDeadBody(DeadBodies[i]);

        DeadBodies.Clear();
    }

    public virtual InteractionGameObject GetNearestInteractionObject(Vector3 center, float maxLen)
    {
        return null;
    }

    public virtual bool IsInteractionObjectInRange(Vector3 center, float maxLen)
    {
        return false;
    }

    public virtual void BreakBreakableObjects(Agent attacker)
    {
    }


    public void EnemiesRecvImpuls(Agent attacker, float angle, float range, float impuls)
    {
        Vector3 dirToEnemy;
        Vector3 center = attacker.Position + attacker.Forward * attacker.CharacterController.radius;
        Vector3 attackerDir = attacker.Forward;
        Agent enemy;
        bool hit = false;

        for (int i = 0; i < _Enemies.Count; i++)
        {
            enemy = _Enemies[i];

            if (enemy == attacker || enemy.IsAlive == false || enemy.enabled == false || enemy.BlackBoard.MotionType == E_MotionType.Knockdown)
                continue;

            dirToEnemy = enemy.Position - center;
            float len = dirToEnemy.sqrMagnitude;
            if (len > range * range)
                continue; //too far

            dirToEnemy.Normalize();

            if (len > 0.5f * 0.5f && Vector3.Angle(attackerDir, dirToEnemy) > angle)
                continue;

            hit = true;

            enemy.ReceiveImpuls(attacker, dirToEnemy * impuls);
        }

        if (hit)
            attacker.SoundPlayHit();
    }

    public void DoMeleeDamage(Agent attacker, Agent mainTarget, E_WeaponType byWeapon, AnimAttackData data, bool critical, bool knockdown)
    {
        if (attacker == Player.Instance.Agent)
        {
            StartCoroutine(EnemiesRecvDamage(attacker, mainTarget, byWeapon, data, critical, knockdown));
        }
        else
        {
            bool hit = false;
            Vector3 dirToEnemy;
            Vector3 attackerDir = attacker.Forward;

            if (mainTarget == null)
                mainTarget = Player.Instance.Agent;

            if (mainTarget.IsInvulnerable == false && mainTarget.BlackBoard.MotionType != E_MotionType.Roll)
            {
                dirToEnemy = mainTarget.Position - attacker.Position;

                float len = dirToEnemy.sqrMagnitude;

                if (len < attacker.BlackBoard.sqrWeaponRange)
                {
                    dirToEnemy.Normalize();

                    if (len < 0.5f * 0.5f || data.HitAngle == -1 || Vector3.Angle(attackerDir, dirToEnemy) < data.HitAngle)
                    {
                        if(Game.Instance.GameDifficulty == E_GameDifficulty.Hard)
                            mainTarget.ReceiveDamage(attacker, byWeapon, data.HitDamage * 1.2f, data);
                        else 
                            mainTarget.ReceiveDamage(attacker, byWeapon, data.HitDamage, data);
                        hit = true;
                    }
                }
            }

            if (hit)
                attacker.SoundPlayHit();
            else
                attacker.SoundPlayMiss();
        }
    }

    public void DoDamageToPlayer(Agent attacker, AnimAttackData data, float distance)
    {
        Agent mainTarget = Player.Instance.Agent;

        if (mainTarget.BlackBoard.MotionType != E_MotionType.Roll)
        {
            if ((mainTarget.Position - attacker.Position).sqrMagnitude < distance * distance)
            {
                mainTarget.ReceiveDamage(attacker, E_WeaponType.Body, data.HitDamage, data);
                attacker.SoundPlayHit();
            }
        }
    }

    public void DoDamageFatality(Agent attacker, Agent mainTarget, E_WeaponType byWeapon, AnimAttackData data)
    {
        if (mainTarget.IsAlive == false || mainTarget.enabled == false)
            return;

        mainTarget.ReceiveDamage(attacker, byWeapon, 1000, data);
    }


    IEnumerator EnemiesRecvDamage(Agent attacker, Agent mainTarget, E_WeaponType byWeapon, AnimAttackData data, bool critical, bool knockdown)
    {
        bool hit = false;
        bool block = false;
        bool knock = false;

        Vector3 dirToEnemy;
        Vector3 center = attacker.Position;
        Vector3 attackerDir = attacker.Forward;
        Agent enemy;

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < _Enemies.Count; i++)
        {
            enemy = _Enemies[i];

            if (enemy.IsAlive == false || enemy.enabled == false || enemy.IsKnockedDown == true)
                continue;

            dirToEnemy = enemy.Position - center;
            float len = dirToEnemy.magnitude;
            dirToEnemy.Normalize();

            if (enemy.IsInvulnerable || (enemy.BlackBoard.DamageOnlyFromBack && Vector3.Angle(attackerDir, enemy.Forward) > 80))
            {
                // Debug.Log(enemy.name + " high angle " + Vector3.Angle(attackerDir, enemy.Forward));
                enemy.ReceiveHitCompletelyBlocked(attacker);
                block = true;
                continue;
            }

            if (len > attacker.BlackBoard.WeaponRange)
            {
                if (data.HitAreaKnockdown == true && knockdown && len < attacker.BlackBoard.WeaponRange * 1.2f)
                {
                    knock = true;
                    enemy.ReceiveKnockDown(attacker, dirToEnemy * data.HitMomentum);
                }
                else if (data.UseImpuls && len < attacker.BlackBoard.WeaponRange * 1.4f)
                {
                    enemy.ReceiveImpuls(attacker, dirToEnemy * data.HitMomentum);
                }
                continue; //too far
            }

            if (enemy.IsInvulnerable || (enemy.BlackBoard.DamageOnlyFromBack && Vector3.Angle(attackerDir, enemy.Forward) > 80))
            {
                // Debug.Log(enemy.name + " high angle " + Vector3.Angle(attackerDir, enemy.Forward));
                enemy.ReceiveHitCompletelyBlocked(attacker);
                block = true;
                continue;
            }

            if (len > 0.5f && Vector3.Angle(attackerDir, dirToEnemy) > data.HitAngle)
            {

                if (data.UseImpuls)
                {
                    //Debug.Log(enemy.name + " impuls");
                    enemy.ReceiveImpuls(attacker, dirToEnemy * data.HitMomentum);
                }
                continue;
            }

            if (enemy.BlackBoard.CriticalAllowed && data.HitCriticalType != E_CriticalHitType.None && Vector3.Angle(attackerDir, enemy.Forward) < 45) // from behind
            {
                // Debug.Log(enemy.name + " critical from behind");
                enemy.ReceiveCriticalHit(attacker, data.HitCriticalType);
                hit = true;
            }
            else if (enemy.IsBlocking)
            {
                //Debug.Log(enemy.name + " block ");
                enemy.ReceiveBlockedHit(attacker, byWeapon, data.HitDamage, data);
                block = true;
            }
            else if (enemy.BlackBoard.CriticalAllowed && critical && (mainTarget == enemy || (data.HitCriticalType == E_CriticalHitType.Horizontal && Random.Range(0, 100) < 30)))
            {
                //   Debug.Log(enemy.name + " critical by chance");
                enemy.ReceiveCriticalHit(attacker, data.HitCriticalType);
                hit = true;
            }
            else if (data.HitAreaKnockdown == true && knockdown)
            {
                //Debug.Log(Time.timeSinceLevelLoad + " " + enemy.name + " knockdown");
                enemy.ReceiveKnockDown(attacker, dirToEnemy * (1 - (len / attacker.BlackBoard.WeaponRange) + data.HitMomentum));
                knock = true;
            }
            else
            {
                //  Debug.Log(enemy.name + " damage");
                enemy.ReceiveDamage(attacker, byWeapon, data.HitDamage, data);
                hit = true;
            }

            

            yield return new WaitForEndOfFrame();
        }

        if(knock)
            attacker.SoundPlayKnockdown();
        else if (block)
            attacker.SoundPlayBlockHit();
        else if (hit)
            attacker.SoundPlayHit();
        else
            attacker.SoundPlayMiss();

    }

    public bool IsEnemyInRange(Vector3 center, float maxLen)
    {
        float nearestLen = maxLen * maxLen;
        for (int i = _Enemies.Count - 1; i >= 0; i--)
        {
            Agent e = _Enemies[i];
            if (e.IsAlive == false)
                continue;

            if ((e.Position - center).sqrMagnitude < nearestLen)
                return true;
        }
        return false;
    }

    public Agent GetNearestAliveEnemy(Vector3 start, Vector3 end, float radius, Agent ignoreAgent)
    {
        float len;
        float nearestLen = radius;
        Agent best = null;

        //          Debug.Log("start " + start.ToString() + " end " + end.ToString());

        //if (ignoreAgent.debugGOAP) Debug.DrawLine(start, end);
        for (int i = 0; i < _Enemies.Count; i++)
        {
            Agent e = _Enemies[i];

            if (e == ignoreAgent)
                continue;

            // Debug.Log("testing enemy " + e.name + " : " + e.Position.ToString());
            if (e.IsAlive == false)
                continue;

            if (e.IsVisible == false)
                continue;

            len = Mathfx.DistanceFromPointToVector(start, end, e.Position);

            //  Debug.Log("Distance is " + len.ToString());

            if (len < nearestLen)
            {
                //if (ignoreAgent.debugGOAP) Debug.DrawLine(ignoreAgent.Position, e.Position);
                nearestLen = len;
                best = e;
            }
        }

        return best;
    }

    public Agent GetNearestAliveEnemy(Vector3 center, float radius, Agent ignoreAgent, E_EnemyType enemyType)
    {
        float len;
        float nearestLen = radius * radius;
        Agent best = null;
        for (int i = 0; i < _Enemies.Count; i++)
        {
            Agent e = _Enemies[i];

            if (e == ignoreAgent)
                continue;

            if (e.EnemyType != enemyType)
                continue;

            // Debug.Log("testing enemy " + e.name + " : " + e.Position.ToString());
            if (e.IsAlive == false)
                continue;

            if (e.IsVisible == false)
                continue;

            len = (center - e.Position).sqrMagnitude;

            //  Debug.Log("Distance is " + len.ToString());

            if (len < nearestLen)
            {
                //if (ignoreAgent.debugGOAP) Debug.DrawLine(ignoreAgent.Position, e.Position);
                nearestLen = len;
                best = e;
            }
        }

        return best;
    }
    public Agent GetNearestAliveEnemy(Agent agent, E_Direction direction, float maxRadius)
    {
        Vector3 dir;

        if (direction == E_Direction.Forward)
            dir = agent.Forward;
        else if (direction == E_Direction.Backward)
            dir = -agent.Forward;
        else if (direction == E_Direction.Right)
            dir = agent.Right;
        else if (direction == E_Direction.Left)
            dir = -agent.Right;
        else
            return null;

        return GetNearestAliveEnemy(agent.Position + dir, agent.Position + dir * 3, maxRadius, agent);
    }

}