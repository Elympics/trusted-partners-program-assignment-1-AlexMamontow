using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
public class PlayerHandler : ElympicsMonoBehaviour, IInputHandler, IUpdatable
{
	[SerializeField]
	private InputHandler inputHandler;
	[SerializeField]
	private MovementHandler movementHandler;
	[SerializeField]
	private ActionHandler actionHandler;
	[SerializeField]
	private GameManager gameManager;
	[SerializeField]
	private ElympicsBool playerActive = new ElympicsBool(true);

	public void TurnOffPlayer()
	{
		playerActive.Value = false;
	}

	private void Update()
	{
		if (Elympics.Player != PredictableFor)
		{
			return;
		}

		inputHandler.UpdateInput();
	}
	public void ElympicsUpdate()
	{
		if (!gameManager.PlayersReady)
		{
			return;
		}

		if (!playerActive.Value)
		{
			return;
		}
		GatheredInput currentInput;
		currentInput.movementInput = Vector2.zero;
		currentInput.mousePosition = transform.position + transform.forward;
		currentInput.attack = false;

		if (ElympicsBehaviour.TryGetInput(PredictableFor, out var inputReader))
		{
			float x1, y1, x2, y2, z2;
			inputReader.Read(out x1);
			inputReader.Read(out y1);
			currentInput.movementInput = new Vector2(x1, y1);
			inputReader.Read(out x2);
			inputReader.Read(out y2);
			inputReader.Read(out z2);
			currentInput.mousePosition = new Vector3(x2, y2, z2);

			inputReader.Read(out currentInput.attack);
		}

		//Debug.Log($"Movement: {currentInput.movementInput}, mouse : {currentInput.mousePosition}");
		movementHandler.HandleMovement(currentInput.movementInput, currentInput.mousePosition);
		actionHandler.HandleActions(currentInput.attack, Elympics.Tick);
	}

	public void OnInputForBot(IInputWriter inputSerializer)
	{
		//throw new System.NotImplementedException();
	}

	public void OnInputForClient(IInputWriter inputSerializer)
	{
		GatheredInput currentInput = inputHandler.GetInput();
		inputSerializer.Write(currentInput.movementInput.x);
		inputSerializer.Write(currentInput.movementInput.y);
		inputSerializer.Write(currentInput.mousePosition.x);
		inputSerializer.Write(currentInput.mousePosition.y);
		inputSerializer.Write(currentInput.mousePosition.z);

		inputSerializer.Write(currentInput.attack);
	}

	
}
