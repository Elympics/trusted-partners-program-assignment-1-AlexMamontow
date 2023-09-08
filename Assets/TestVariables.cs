using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class TestVariables : MonoBehaviour, IObservable, IInitializable
{
    public ElympicsBool elympicsBool = new ElympicsBool();
    public ElympicsInt elympicsInt = new ElympicsInt();
    public ElympicsFloat elympicsFloat = new ElympicsFloat();

	public void Initialize()
	{
		elympicsBool.Value = false;
		elympicsInt.Value = 0;
		elympicsFloat.Value = 0f;
	}

	public void UpdateVariables()
	{
		elympicsBool.Value = true;
		elympicsInt.Value = 1;
		elympicsFloat.Value = 1f;
	}

}
