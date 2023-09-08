using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using MatchTcpClients.Synchronizer;
using System;
using Cinemachine;

public class GameManager : ElympicsMonoBehaviour, IClientHandlerGuid
{
	[SerializeField]
	private CinemachineVirtualCamera playerFollowCamera;
	[SerializeField]
	private ElympicsBehaviour player0, player1;
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
		Debug.Log($"OnSynchronized");
	}
}
