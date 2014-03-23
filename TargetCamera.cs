using UnityEngine;
using System.Collections;

public class TargetCamera : MonoBehaviour {

	public Transform target1;//target: Player
	public Transform target2;//target: 
	public Transform target3;//target: 
	public Transform target4;//target: 
	int activeTarget;

	//BorderValues
	public float CameraVerticalMin = -1.0f;
	public float CameraVerticalMax = 1.0f;
	public float CameraHorizontalMin = -1.0f;
	public float CameraHorizontalMax = 1.0f;

	//Position coordinates for movement detection
	float newPosX;
	float newPosY;
	float oldPosX;
	float oldPosY;

	Vector3 camTargetPos;
	Vector3 playerPos;
	public float camTargetSpeedAmplifier = 1.4f;
	public float camTargetSpeedCounter;

	int delayCounter = 0;

	void Start () 
	{
	
	}

	void LateUpdate () 
	{
		playerPos = target1.transform.position;
		camTargetPos = new Vector3(playerPos.x+(newPosX-oldPosX)*camTargetSpeedAmplifier*camTargetSpeedCounter, playerPos.y+(newPosY-oldPosY)*camTargetSpeedAmplifier*camTargetSpeedCounter,-30);

		//set camera to follow player
		SetCameraTarget(1);

		//handle oldPos / newPos
		//save positions
		playerPosHandle(4);

		//**********************
		//**TAG Movement
		//**********************
		//is player NOT moving?
		if(isMovingX() || isMovingY() )
		{
			//player staying still
			if(camTargetSpeedCounter<8)
				camTargetSpeedCounter = camTargetSpeedCounter +0.05f;

		}
		else
		{
			//player is moving
			camTargetSpeedCounter=1.0f;

			//Move tag towards player
		}

		//**********************
		//**Camera Movement
		//**********************
		MoveCam();


	}
	void playerPosHandle(int d)
	{
		//create delay for positionsavings with delaycounter
		delayCounter++;
		if(delayCounter>d)
		{
			//Process savings on parameters
			saveTargetPosition(activeTarget);

			delayCounter = 0;
		}
	}
	void SetCameraTarget(int l)
	{
		//save activeTarget on parameter for use in this class. 
		activeTarget = l;
	}
	void saveTargetPosition (int l) 
	{
		//Save oldPos / newPos
		oldPosX = newPosX;
		oldPosY = newPosY;

		//target player
		newPosX = target1.position.x;
		newPosY = target1.position.y;
	}
	bool isMovingX() 
	{
		if(oldPosX == newPosX)
		{
			//player X is the same ----> horizontally still
			return false;
		}
		else
		{
			//player moving horizontally
			return true;
		}
	}
	bool isMovingY() 
	{
		if(oldPosY == newPosY)
		{
			//player Y still -----> inactive
			return false;
		}
		else
		{
			//player is moving vertically
			return true;
		}
	}
	void MoveCam () 
	{
		if(target1)
		{
			transform.position = Vector3.Lerp(transform.position,camTargetPos,Time.deltaTime) ;
		}
	}
}
