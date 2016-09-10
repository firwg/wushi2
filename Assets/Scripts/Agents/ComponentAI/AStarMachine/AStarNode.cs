using System;

////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////	A STAR NODE
///////////////////////////////////////////////////////////////////////////////////////////

class AStarNode : System.Object
{
	public enum E_AStarFlags
	{
		Unchecked = 0,
		Open = 1,
		Closed = 2,
		NotPassable = 3,
	}

	public AStarNode()
	{
		NodeID = -1;
		G = 0;//水平距离
		H = 0;//垂直距离
		F = float.MaxValue;

		Flag = E_AStarFlags.Unchecked;
	}

	public short NodeID;
    /// <summary>
    /// //水平距离
    /// </summary>
	public float G;

    /// <summary>
    /// 垂直距离
    /// </summary>
	public float H;
	public float F;

    //串联
	public AStarNode Next;
	public AStarNode Previous;
	public AStarNode Parent;
    /// <summary>
    /// 标记
    /// </summary>
	public E_AStarFlags Flag;


};
