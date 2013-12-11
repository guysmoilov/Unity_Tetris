using UnityEngine;
using System.Collections;

public class BlockControllerV2 : MonoBehaviour {

	public Transform[] blocks;
	public BoardScript board;
	public float moveAmount = 1f;
	public float dropRate = 1f;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(Drop());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Vector2 BlockToBoardCoords(Transform block, BoardScript board)
	{
		var blockPos = block.position;
		Debug.Log(blockPos);
		blockPos.x += board.xOffset;
		blockPos.y += board.yOffset;
		Debug.Log(blockPos);
		return blockPos;
	}

	void FixedUpdate()
	{
		if (Input.GetButtonDown("Right"))
		{
			// Check no collision on all blocks...
			bool bCanMove = true;

			foreach (var block in blocks) 
			{
				Vector2 boardCoords = BlockToBoardCoords(block, board);

				if (board.board[(int)boardCoords.x + 1, (int)boardCoords.y] != null) 
				{
					bCanMove = false;
					break;
				}
			}

			// Move
			transform.Translate(Vector3.right * moveAmount, Space.World);
		}
		else if (Input.GetButtonDown("Left"))
		{
			// Check no collision on all blocks...
			bool bCanMove = true;
			
			foreach (var block in blocks) 
			{
				Vector2 boardCoords = BlockToBoardCoords(block, board);
				
				if (board.board[(int)boardCoords.x - 1, (int)boardCoords.y] != null) 
				{
					bCanMove = false;
					break;
				}
			}

			// Move
			transform.Translate(Vector3.left * moveAmount, Space.World);
		}
		
		// Snap down
//		if (Input.GetButton("Jump"))
//		{
//			realFallSpeed = snapDownSpeed;
//		}
//		else 
//		{
//			realFallSpeed = fallSpeed;
//		}
		
		if (Input.GetButtonDown("Fire1"))
		{
			// Check no collision on all blocks...
			bool bCanMove = true;

			// Rotate left
			transform.Rotate(new Vector3(0,0,90));

			foreach (var block in blocks) 
			{
				Vector2 boardCoords = BlockToBoardCoords(block, board);
				
				if (board.board[(int)boardCoords.x, (int)boardCoords.y] != null) 
				{
					bCanMove = false;
					break;
				}
			}

			// Rotate back if it failed
			transform.Rotate(new Vector3(0,0,-90));
		}
		else if (Input.GetButtonDown("Fire2"))
		{
			// Check no collision on all blocks...
			bool bCanMove = true;
			
			// Rotate right
			transform.Rotate(new Vector3(0,0,-90));
			
			foreach (var block in blocks) 
			{
				Vector2 boardCoords = BlockToBoardCoords(block, board);
				
				if (board.board[(int)boardCoords.x, (int)boardCoords.y] != null) 
				{
					bCanMove = false;
					break;
				}
			}
			
			// Rotate back if it failed
			transform.Rotate(new Vector3(0,0,90));
		}
	}

	IEnumerator Drop()
	{
		// Check if collided..
		bool bHitBottom = false;

		while (!bHitBottom)
		{
			if (board != null && board.board != null)
			{
				foreach (var block in blocks) 
				{
					Vector2 boardCoords = BlockToBoardCoords(block, board);
					
					if (board.board[(int)boardCoords.x, (int)boardCoords.y - 1] != null)
					{
						bHitBottom = true;
						break;
					}
				}
				
				// If collided, stop the block, write it on the board and create a new one
				if (bHitBottom) 
				{
					Debug.Log("collide");
					foreach (var block in blocks) 
					{
						Vector2 boardCoords = BlockToBoardCoords(block, board);
						
						board.board[(int)boardCoords.x, (int)boardCoords.y] = block;
					}
					
					this.enabled = false;
				}
				else
				{
					Debug.Log("drop!");
					transform.Translate(Vector3.down * moveAmount);
				}
			}

            yield return new WaitForSeconds(dropRate);
		}
	}
}
