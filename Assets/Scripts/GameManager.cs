using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using MatchTcpClients.Synchronizer;
using System;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : ElympicsMonoBehaviour, IClientHandlerGuid, IObservable, IInitializable, IUpdatable
{
	public bool PlayersReady => readyPlayersAmount.Value == 2;

	[SerializeField]
	private CinemachineVirtualCamera playerFollowCamera;
	[SerializeField]
	private CanvasView canvas;
	[SerializeField]
	private PlayerHandler player0, player1;
	[SerializeField]
	private PlayerInfo player0Info, player1Info;
	[SerializeField]
	private GameOverScreenHandler gameOverScreenHandler;
	[SerializeField]
	private PlayerHandler player0Handler, player1Handler;

	private bool gameEnded = false;

	private ElympicsInt readyPlayersAmount = new ElympicsInt();

	private bool IsReadyButtonClicked = false;

	public void OnAuthenticated(Guid userId)
	{
		Debug.Log($"OnAuthenticated {userId}");
	}

	public void OnAuthenticatedFailed(string errorMessage)
	{
		Debug.Log($"OnAuthenticatedFailed");
	}

	public void OnClientsOnServerInit(InitialMatchPlayerDatasGuid data)
	{
		Debug.Log($"OnClientsOnServerInit");
	}

	public void OnConnected(TimeSynchronizationData data)
	{
		Debug.Log($"OnConnected");
		SetupCameraToPlayer();
		readyPlayersAmount.ValueChanged += HandlePlayerReady;

		if (Elympics.IsServer)
		{
			return;
		}

		canvas.ReadyButton.onClick.AddListener(() =>
		{
			IsReadyButtonClicked = true;
			canvas.ReadyButton.gameObject.SetActive(false);
		});
	}

	private void HandlePlayerReady(int lastValue, int newValue)
	{
		canvas.WaitingForPlayersText.gameObject.SetActive(!PlayersReady && !canvas.ReadyButton.gameObject.activeSelf);
	}

	private void SetupCameraToPlayer()
	{
		var targetPlayer = player0.PredictableFor == Elympics.Player? player0 : player1;
		playerFollowCamera.Follow = targetPlayer.transform;
		playerFollowCamera.LookAt = targetPlayer.transform;
	}

	public void OnConnectingFailed()
	{
		Debug.Log($"OnConnectingFailed");
	}

	public void OnDisconnectedByClient()
	{
		Debug.Log($"OnDisconnectedByClient");
	}

	public void OnDisconnectedByServer()
	{
		Debug.Log($"OnDisconnectedByServer");
	}

	public void OnMatchEnded(Guid matchId)
	{
		Debug.Log($"OnDisconnectedByServer");
	}

	public void OnMatchJoined(Guid matchId)
	{
		Debug.Log($"OnMatchJoined");
	}

	public void OnMatchJoinedFailed(string errorMessage)
	{
		Debug.Log($"OnMatchJoinedFailed");
	}

	public void OnStandaloneClientInit(InitialMatchPlayerDataGuid data)
	{
		Debug.Log($"OnStandaloneClientInit");
	}

	public void OnSynchronized(TimeSynchronizationData data)
	{
		//Debug.Log($"OnSynchronized");
	}

	public void Initialize()
	{
		readyPlayersAmount.Value = 0;
	}

	[ElympicsRpc(ElympicsRpcDirection.PlayerToServer)]
	public void PlayerIsReady()
	{
		readyPlayersAmount.Value++;
	}

	public void ElympicsUpdate()
	{
		if (IsReadyButtonClicked)
		{
			PlayerIsReady();
			IsReadyButtonClicked = false;
		}

		bool player0Died = player0Info.GetHealthRatio() <= 0;
		bool player1Died = player1Info.GetHealthRatio() <= 0;

		if (player0Died || player1Died)
		{
			if (player0Died && player1Died)
			{
				gameOverScreenHandler.DisplayGameOver(-1);
			}
			else if (player0Died)
			{
				gameOverScreenHandler.DisplayGameOver(1);
			}
			else
			{
				gameOverScreenHandler.DisplayGameOver(0);
			}

			gameEnded = true;

			if (Elympics.IsServer)
			{
				player0Handler.TurnOffPlayer();
				player1Handler.TurnOffPlayer();
				Elympics.EndGame();
			}
		}
		else if (gameEnded)
		{
			gameEnded = false;
			gameOverScreenHandler.RevertGameOverDisplay();
		}		
	}

	public void ToMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
