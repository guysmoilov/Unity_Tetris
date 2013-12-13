using UnityEngine;
using System.Linq;
using System.Collections;

public class BoardScript : MonoBehaviour {

	public Transform[,] board;
	public int boardWidth = 14;
	public int boardHeight = 18;
	public int xOffset = 7;
	public int yOffset = 9;

	// Use this for initialization
	void Start () 
	{
		board = new Transform[boardHeight + 2,boardWidth + 4];

		// Fill edges
		for (int i = 2; i < boardHeight + 2; i++) 
		{
			board[i,0] = this.transform;
			board[i,1] = this.transform;
			board[i,boardWidth + 2] = this.transform;
			board[i,boardWidth + 3] = this.transform;
		}

		for (int i = 0; i < boardWidth + 4; i++) 
		{
			board[0,i] = this.transform;
			board[1,i] = this.transform;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (board != null)
		{
			// Check completed rows
			for (int row = 2; row < boardHeight + 2; row++) 
			{
				bool bCompleted = true;

				for (int col = 2; col < boardWidth + 2 && bCompleted; col++)
				{
					Debug.Log("row " + row + " col " + col + " : " + board[row,col]);
					if (board[row,col] == null)
					{
						bCompleted = false;
					}
				}

				if (bCompleted)
				{
					Debug.Log("Completed row " + row);
					// Effects
					// Drop row
				}
			}
		}
	}
}
