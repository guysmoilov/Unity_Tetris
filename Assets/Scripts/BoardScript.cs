using UnityEngine;
using System.Collections;

public class BoardScript : MonoBehaviour {

	public Transform[,] board;
	public int boardWidth = 12;
	public int boardHeight = 20;
	public int xOffset;
	public int yOffset;

	// Use this for initialization
	void Start () 
	{
		board = new Transform[boardWidth + 2,boardHeight + 2];

		// Fill edges
		for (int i = 0; i < boardHeight; i++) 
		{
			board[0,i + 2] = this.transform;
			board[1,i + 2] = this.transform;
			board[boardWidth,i + 2] = this.transform;
			board[boardWidth + 1,i + 2] = this.transform;
		}

		for (int i = 0; i < boardWidth + 2; i++) 
		{
			board[i,0] = this.transform;
			board[i,1] = this.transform;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check completed rows
	}
}
