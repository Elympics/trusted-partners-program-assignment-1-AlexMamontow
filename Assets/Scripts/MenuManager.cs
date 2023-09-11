using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class MenuManager : ElympicsMonoBehaviour
{
	private bool searchInProgress = false;

	public async void PlayOnline()
	{
		if (searchInProgress)
		{
			return;
		}

		var (Region, LatencyMs) = await ClosestRegionFinder.GetClosestRegion();
		ElympicsLobbyClient.Instance.PlayOnlineInRegion(Region, null, null, "Default");

		searchInProgress = true;

		Debug.Log($"Joined matchmaking in region {Region} with ping {LatencyMs}");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public static class ClosestRegionFinder
	{
		private static (string Region, float LatencyMs)? CachedClosestRegion;

		// Note that `ElympicsCloudPing.ChooseClosestRegion` is `async` and so it has to be handled with `await` in order to wait for its result.
		// The same goes for every method wrapping this one, as we see below and in the next example.
		public static async UniTask<(string Region, float LatencyMs)> GetClosestRegion()
		{
			if (!CachedClosestRegion.HasValue)
			{
				CachedClosestRegion = await ElympicsCloudPing.ChooseClosestRegion(ElympicsRegions.AllAvailableRegions);
			}

			return CachedClosestRegion.Value;
		}
	}
}
