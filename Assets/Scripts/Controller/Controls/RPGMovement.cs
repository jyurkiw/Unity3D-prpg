using UnityEngine;
using System.Collections;

public partial class RPGController : MonoBehaviour {
	public float movementActionDuration;
	
	private enum MovementDirection { NORTH, SOUTH, EAST, WEST };
	private enum MovementStatus { MOVING, STOPPED, COLLIDED };
	
	private CharacterController controller;
	private MovementDirection movementDirection;
	private Vector3 movementVector;
	private MovementStatus movementStatus;
	
	private float tileSize;
	
	private void MovementStart() {
		controller = GetComponent<CharacterController>();
		DrawManager drawManager = GameObject.Find("Cornerstone").GetComponent<DrawManager>();
		tileSize = drawManager.tileSize;
		movementStatus = MovementStatus.STOPPED;
	}
	
	partial void HandleW() {
		if(movementStatus == MovementStatus.STOPPED) {
			movementStatus = MovementStatus.MOVING;
			movementDirection = MovementDirection.NORTH;
			movementVector = new Vector3(0, tileSize, 0);
			StartCoroutine(MovePlayer());
		}
	}
	
	partial void HandleA() {
		if(movementStatus == MovementStatus.STOPPED) {
			movementStatus = MovementStatus.MOVING;
			movementDirection = MovementDirection.WEST;
			movementVector = new Vector3(-tileSize, 0, 0);
			StartCoroutine(MovePlayer());
		}
	}
	
	partial void HandleS() {
		if(movementStatus == MovementStatus.STOPPED) {
			movementStatus = MovementStatus.MOVING;
			movementDirection = MovementDirection.SOUTH;
			movementVector = new Vector3(0, -tileSize, 0);
			StartCoroutine(MovePlayer());
		}
	}
	
	partial void HandleD() {
		if(movementStatus == MovementStatus.STOPPED) {
			movementStatus = MovementStatus.MOVING;
			movementDirection = MovementDirection.EAST;
			movementVector = new Vector3(tileSize, 0, 0);
			StartCoroutine(MovePlayer());
		}
	}
	
	private IEnumerator MovePlayer() {
		float moveTime = 0;
		Vector3 position = transform.localPosition;
		Vector3 destination = position + movementVector;
		
		while(moveTime < movementActionDuration) {
			//if we've collided with something, abort the move and return to the start position
			if (movementStatus == MovementStatus.COLLIDED) {
				destination = position;
				position = transform.localPosition;
				movementStatus = MovementStatus.MOVING;
			}
			moveTime += Time.deltaTime;
			transform.localPosition = Vector3.Lerp(position, destination, moveTime / movementActionDuration);
			
			yield return new WaitForEndOfFrame();
		}
		
		transform.localPosition = destination;
		
		movementStatus = MovementStatus.STOPPED;
		yield break;
	}
	
	void OnCollisionEnter(Collision collision) {
		movementStatus = MovementStatus.COLLIDED;
	}
}
