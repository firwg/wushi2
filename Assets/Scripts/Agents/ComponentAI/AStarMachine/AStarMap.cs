using System;


abstract class AStarMap : System.Object
{
	public abstract int GetNumAStarNeighbours(AStarNode pAStarNode);
	public abstract short GetAStarNeighbour(AStarNode pAStarNode, short iNeighbor);
	public abstract AStarNode CreateANode(short id);
	public abstract AStarNode.E_AStarFlags GetAStarFlags(short NodeID);
	public virtual void SetAStarFlags(short NodeID, AStarNode.E_AStarFlags flag){}
	public abstract bool CompareNodes(AStarNode node1, AStarNode node2);
	public abstract void Cleanup();
}