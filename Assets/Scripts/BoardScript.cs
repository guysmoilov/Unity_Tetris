using UnityEngine;
using System.Linq;
using System.Collections;

public class BoardScript : MonoBehaviour {

	public Transform[,] board;
	public int boardWidth = 14;
	public int boardHeight = 18;
	public int xOffset = 7;
	public int yOffset = 9;
	public long score = 0; 
	public int rowCompletionScore = 100;

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
			for (int row = boardHeight + 1; row > 1; row--) 
			{
				bool bCompleted = true;

				for (int col = 2; col < boardWidth + 2 && bCompleted; col++)
				{
					//Debug.Log("row " + row + " col " + col + " : " + board[row,col]);
					if (board[row,col] == null)
					{
						bCompleted = false;
					}
				}

				if (bCompleted)
				{
					Debug.Log("Completed row " + row + " at " + Time.time);
					score += rowCompletionScore;

					// Drop row
					for (int col = 2; col < boardWidth + 2; col++)
					{
						// Effects
						var block = board[row,col].gameObject;

						block.transform.Translate(Vector3.back, Space.World);
						block.collider.enabled = true;
						block.rigidbody.isKinematic = false;
						block.rigidbody.useGravity = true;
						block.rigidbody.AddForceAtPosition(new Vector3(Random.Range(-3,3), Random.Range(-1,1), Random.Range(0,0)),
						                                   new Vector3(0, -0.5f * block.transform.localScale.y, -0.5f * block.transform.localScale.z),
						                         ForceMode.Impulse);
						block.particleSystem.Play();

						// Check if need to destroy the block's parent
						var parent = block.transform.parent.gameObject;
						StartCoroutine("BlockParentDestroyer", parent);

						// Delayed destruction
						Destroy(block, 2f);

						for (int row2 = row; row2 < boardHeight + 1; row2++)
						{
							board[row2,col] = board[row2 + 1,col];
							if (board[row2,col] != null)
							{
								board[row2,col].Translate(Vector3.down, Space.World);
							}
						}
					}

					// Repeat row
					row++;
				}
			}
		}
	}

	IEnumerator BlockParentDestroyer(GameObject parent)
	{
		yield return new WaitForSeconds(3f);

		var remainingBlocks = parent.GetComponent<BlockControllerV2>().blocks;
		
		Debug.Log((from blk in remainingBlocks where (blk != null) select blk).Count().ToString() + " blocks in " + parent.name);
		if ((from blk in remainingBlocks where (blk != null) select blk).Count() <= 0)
		{
			Destroy(parent.gameObject);
        }
    }
}
