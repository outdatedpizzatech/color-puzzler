﻿using UnityEngine;
using System.Collections;

public class Matrix : MonoBehaviour {
	public GameObject[,] matrix;
	private GFRectGrid grid;
	private int xMax;
	private int yMax;

	// Use this for initialization
	void Start () {
		grid = GetComponent<GFRectGrid>();
		xMax = Mathf.CeilToInt((grid.renderTo.x * 2));
		yMax = Mathf.CeilToInt ((grid.renderTo.y * 2));
		matrix = new GameObject[xMax, yMax];
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public bool InsertIntoMatrix(GameObject objectToInsert){
		int xArrayPosition = CoordinateToPosition(objectToInsert.transform.position.x, grid.renderTo.x, grid.spacing.x);
		int yArrayPosition = CoordinateToPosition(objectToInsert.transform.position.y, grid.renderTo.y, grid.spacing.y);
		print ("trying to insert at... " + xArrayPosition +", " + yArrayPosition);
		
		if(CanInsertIntoMatrix(objectToInsert)){
			if(matrix[xArrayPosition, yArrayPosition]){
				Destroy (matrix[xArrayPosition, yArrayPosition]);
			}
			print ("inserting into " + xArrayPosition +", " + yArrayPosition);
			
			matrix[xArrayPosition, yArrayPosition] = objectToInsert;
			objectToInsert.GetComponent<GridElement>().SetPosition(xArrayPosition, yArrayPosition);
			return(true);
		}else{
			return(false);
		}
	}
	
	public bool CanInsertIntoMatrix(GameObject objectToInsert){
		int xArrayPosition = CoordinateToPosition(objectToInsert.transform.position.x, grid.renderTo.x, grid.spacing.x);
		int yArrayPosition = CoordinateToPosition(objectToInsert.transform.position.y, grid.renderTo.y, grid.spacing.y);
		if(xArrayPosition < xMax && yArrayPosition < yMax && xArrayPosition >= 0 && yArrayPosition >= 0){
			if(matrix[xArrayPosition, yArrayPosition] == null){
				return(true);
			}else{
				return(matrix[xArrayPosition, yArrayPosition].GetComponent<GridElement>().canBeReplaced);
			}
		}else{
			return(false);
		}
	}
	
	private int CoordinateToPosition(float coordinate, float renderTo, float spacing){
		float position = coordinate * (1/spacing) + (spacing/2) + renderTo - 1;
		return(Mathf.CeilToInt(position));
	}
	
	public GameObject ElementAtArrayPosition(int xPosition, int yPosition){
		if(xPosition >= 0 && xPosition < xMax && yPosition >= 0 && yPosition < yMax){
			return(matrix[xPosition, yPosition]);
		}else{
			return(null);
		}		
	}
	
	public GameObject ElementAtVectorPosition(Vector3 position){
		int xArrayPosition = CoordinateToPosition(position.x, grid.renderTo.x, grid.spacing.x);
		int yArrayPosition = CoordinateToPosition(position.y, grid.renderTo.y, grid.spacing.y);
		if(xArrayPosition < xMax && yArrayPosition < yMax && xArrayPosition >= 0 && yArrayPosition >= 0 && matrix[xArrayPosition, yArrayPosition]){
			return(matrix[xArrayPosition, yArrayPosition]);
		}else{
			return(null);
		}
	}
	
}
