using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject player0Won, player1Won, draw;

	public void DisplayGameOver(int winnerID)
	{
		gameObject.SetActive(true);

		switch (winnerID)
		{ 
			case 0:
				player0Won.SetActive(true);
				break;
			case 1:
				player1Won.SetActive(true);
				break;
			default:
				draw.SetActive(true);
				break;
		}
	}

	public void RevertGameOverDisplay()
	{
		gameObject.SetActive(false);
		player0Won.SetActive(true);
		player1Won.SetActive(true);
		draw.SetActive(true);
	}
}
