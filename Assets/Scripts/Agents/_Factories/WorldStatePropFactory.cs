using UnityEngine;
using System.Collections.Generic;

public static class WorldStatePropFactory
{
	private static Queue<WorldStateProp> m_UnusedProps = new Queue<WorldStateProp>();


    public static WorldStateProp Create(E_PropKey key, object t)
    {
        WorldStateProp p = null;

        if (m_UnusedProps.Count > 0)
        {
            p = m_UnusedProps.Dequeue();
            p.PropValue = t;//new Value(t);//new ValueBool(state);
            if (t == null)
            {
                p.PropType = E_PropType.E_INVALID;
                return p;
            } 

            if (t.GetType() == typeof(int)) p.PropType = E_PropType.E_INT;
            else if (t.GetType() == typeof(float)) p.PropType = E_PropType.E_FLOAT;
            else if (t.GetType() == typeof(Agent)) p.PropType = E_PropType.E_AGENT;
            else if (t.GetType() == typeof(bool)) p.PropType = E_PropType.E_BOOL;
            else if (t.GetType() == typeof(E_EventTypes)) p.PropType = E_PropType.E_EVENT;
            else if (t.GetType() == typeof(AgentOrder)) p.PropType = E_PropType.E_ORDER;
            else if (t.GetType() == typeof(Vector3)) p.PropType = E_PropType.E_VECTOR;
            else p.PropType = E_PropType.E_INVALID;
        }
        else
            p = new WorldStateProp(t);


        return p;
    }


    //static public WorldStateProp Create(E_PropKey key, bool state)
    //{
    //    WorldStateProp p = null;

    //    if (m_UnusedProps.Count > 0)
    //    {
    //        p = m_UnusedProps.Dequeue();
    //        p.PropValue = new ValueBool(state);
    //        p.PropType = E_PropType.E_BOOL;
    //    }
    //    else
    //        p = new WorldStateProp(state);

    //    p.Time = UnityEngine.Time.timeSinceLevelLoad;
    //    p.PropKey = key;
    //    return p;
    //}

    //static public WorldStateProp Create(E_PropKey key, int state)
    //{
    //    WorldStateProp p;

    //    if (m_UnusedProps.Count > 0)
    //    {
    //        p = m_UnusedProps.Dequeue();
    //        p.PropValue = new ValueInt(state);
    //        p.PropType = E_PropType.E_INT;
    //    }
    //    else
    //        p = new WorldStateProp(state);

    //    p.Time = UnityEngine.Time.timeSinceLevelLoad;
    //    p.PropKey = key;
    //    return p;
    //}

    //static public WorldStateProp Create(E_PropKey key, float state)
    //{
    //    WorldStateProp p;

    //    if (m_UnusedProps.Count > 0)
    //    {
    //        p = m_UnusedProps.Dequeue();
    //        p.PropKey = key;
    //        p.PropValue = new ValueFloat(state);
    //    }
    //    else
    //        p = new WorldStateProp(state);

    //    p.Time = UnityEngine.Time.timeSinceLevelLoad;
    //    p.PropType = E_PropType.E_FLOAT;
    //    return p;
    //}

    //static public WorldStateProp Create(E_PropKey key, Agent state)
    //{
    //    WorldStateProp p = null;

    //    if (m_UnusedProps.Count > 0)
    //    {
    //        p = m_UnusedProps.Dequeue();
    //        p.PropValue = new ValueAgent(state);
    //        p.PropType = E_PropType.E_AGENT;
    //    }
    //    else
    //        p = new WorldStateProp(state);

    //    p.Time = UnityEngine.Time.timeSinceLevelLoad;
    //    p.PropKey = key;
    //    return p;
    //}

    //static public WorldStateProp Create(E_PropKey key, UnityEngine.Vector3 vector)
    //{
    //    WorldStateProp p = null;

    //    if (m_UnusedProps.Count > 0)
    //    {
    //        p = m_UnusedProps.Dequeue();
    //        p.PropValue = new ValueVector(vector);
    //        p.PropType = E_PropType.E_VECTOR;
    //    }
    //    else
    //        p = new WorldStateProp(vector);

    //    p.Time = UnityEngine.Time.timeSinceLevelLoad;
    //    p.PropKey = key;
    //    return p;
    //}

    //static public WorldStateProp Create(E_PropKey key, E_EventTypes eventType)
    //{
    //    WorldStateProp p = null;

    //    if (m_UnusedProps.Count > 0)
    //    {
    //        p = m_UnusedProps.Dequeue();
    //        p.PropValue = new ValueEvent(eventType);
    //        p.PropType = E_PropType.E_EVENT;
    //    }
    //    else
    //        p = new WorldStateProp(eventType);

    //    p.Time = UnityEngine.Time.timeSinceLevelLoad;
    //    p.PropKey = key;
    //    return p;
    //}

    //static public WorldStateProp Create(E_PropKey key, AgentOrder.E_OrderType orderType)
    //{
    //    WorldStateProp p = null;

    //    if (m_UnusedProps.Count > 0)
    //    {
    //        p = m_UnusedProps.Dequeue();
    //        p.PropValue = new ValueOrder(orderType);
    //        p.PropType = E_PropType.E_EVENT;
    //    }
    //    else
    //        p = new WorldStateProp(orderType);

    //    p.Time = UnityEngine.Time.timeSinceLevelLoad;
    //    p.PropKey = key;
    //    return p;
    //}

	static public void Return(WorldStateProp prop) 
    {
		prop.PropKey = E_PropKey.E_INVALID;
		m_UnusedProps.Enqueue(prop); 
	}
}
