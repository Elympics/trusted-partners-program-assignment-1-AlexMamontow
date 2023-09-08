using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasView : MonoBehaviour
{
	public Button ReadyButton => readyButton;
	public TextMeshProUGUI WaitingForPlayersText => waitingForPlayersText;

	[SerializeField]
	private Button readyButton;
	[SerializeField]
	private TextMeshProUGUI waitingForPlayersText;
}
