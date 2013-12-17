using UnityEngine;
using System.Collections;

public class BlockControllerV2 : MonoBehaviour {

	public Transform[] blocks;
	public GameObject[] blockPrefabs;
	public GameObject nextBlock;
	public BoardScript board;
	public Vector3 spawnPoint = new Vector3(0, 8, 0);
	public Vector3 previewPoint = new Vector3(13, 5, 0);
	public float moveAmount = 1f;
	public float dropRate = 1f;
    public bool canRotate = true;

	// Use this for initialization
	void Start () 
	{
		Debug.Log(this.name + " activated on: " + Time.time);
		// Stop rotating
		this.GetComponent<Rotator>().enabled = false;
		transform.rotation = Quaternion.identity;

		StartCoroutine(Drop());
	}
	
	// Update is called once per frame
	void Update () {
	}

	Vector2 BlockToBoardCoords(Transform block, BoardScript board)
	{
		var blockPos = block.position;
		Debug.Log(this.name + " " + blockPos.ToString());
		blockPos.x += board.xOffset;
		blockPos.y += board.yOffset;
		Debug.Log(this.name + " " + blockPos.ToString());
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

				if (board.board[(int)boardCoords.y, (int)boardCoords.x + 1] != null) 
				{
					bCanMove = false;
					break;
				}
			}

			// Move
            if (bCanMove)
            {
                transform.Translate(Vector3.right * moveAmount, Space.World); 
            }
		}
		else if (Input.GetButtonDown("Left"))
		{
			// Check no collision on all blocks...
			bool bCanMove = true;
			
			foreach (var block in blocks) 
			{
				Vector2 boardCoords = BlockToBoardCoords(block, board);
				
				if (board.board[(int)boardCoords.y, (int)boardCoords.x - 1] != null) 
				{
					bCanMove = false;
					break;
				}
			}

			// Move
            if (bCanMove)
            {
                transform.Translate(Vector3.left * moveAmount, Space.World); 
            }
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
		
		if (Input.GetButtonDown("Fire1") && canRotate)
		{
			// Check no collision on all blocks...
			bool bCanMove = true;

			// Rotate left
			transform.Rotate(new Vector3(0,0,90));

			foreach (var block in blocks) 
			{
				Vector2 boardCoords = BlockToBoardCoords(block, board);
				
				if (board.board[(int)boardCoords.y, (int)boardCoords.x] != null) 
				{
					bCanMove = false;
					break;
				}
			}

			// Rotate back if it failed
            if (!bCanMove)
            {
                transform.Rotate(new Vector3(0, 0, -90)); 
            }
		}
        else if (Input.GetButtonDown("Fire2") && canRotate)
		{
			// Check no collision on all blocks...
			bool bCanMove = true;
			
			// Rotate right
			transform.Rotate(new Vector3(0,0,-90));
			
			foreach (var block in blocks) 
			{
				Vector2 boardCoords = BlockToBoardCoords(block, board);
				
				if (board.board[(int)boardCoords.y, (int)boardCoords.x] != null) 
				{
					bCanMove = false;
					break;
				}
			}
			
			// Rotate back if it failed
            if (!bCanMove)
            {
                transform.Rotate(new Vector3(0, 0, 90)); 
            }
		}
	}

	IEnumerator Drop()
	{
		// Check if collided..
		bool bHitBottom = false;

		while (!bHitBottom && this.enabled)
		{
			if (board != null && board.board != null)
			{
				foreach (var block in blocks) 
				{
					Vector2 boardCoords = BlockToBoardCoords(block, board);
					if (board.board[(int)boardCoords.y - 1, (int)boardCoords.x] != null)
					{
						bHitBottom = true;
						break;
					}
				}
				
				// If collided, stop the block, write it on the board and create a new one
				if (bHitBottom) 
				{
					//Debug.Log("collide");
					foreach (var block in blocks) 
					{
						Vector2 boardCoords = BlockToBoardCoords(block, board);
						
						board.board[(int)boardCoords.y, (int)boardCoords.x] = block;
					}

					// Create new block
					var newBlock = GameObject.Instantiate(
						blockPrefabs[Mathf.FloorToInt(Random.Range(0, blockPrefabs.Length - float.Epsilon))],
						previewPoint, Quaternion.identity) as GameObject;

					// For some wierd reason, need to reset components...
					newBlock.GetComponent<Rotator>().enabled = true;
					newBlock.GetComponent<BlockControllerV2>().enabled = false;

					// Move existing block to board
					nextBlock.transform.position = spawnPoint;
					var nextController = nextBlock.GetComponent<BlockControllerV2>();
					nextController.nextBlock = newBlock;
					nextController.board = this.board;
					nextBlock.GetComponent<BlockControllerV2>().enabled = true;

					this.enabled = false;
				}
				else
				{
					//Debug.Log("drop!");
					transform.Translate(Vector3.down * moveAmount, Space.World);
				}
			}

			if (!bHitBottom)
			{
            	yield return new WaitForSeconds(dropRate);
			}
		}
	}
}
