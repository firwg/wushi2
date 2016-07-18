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
		G = 0;
		H = 0;
		F = float.MaxValue;

		Flag = E_AStarFlags.Unchecked;
	}

	public short NodeID;
	public float G;
	public float H;
	public float F;
	public AStarNode Next;
	public AStarNode Previous;
	public AStarNode Parent;
	public E_AStarFlags Flag;


};
