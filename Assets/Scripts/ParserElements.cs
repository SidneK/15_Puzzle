using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ParserElements : MonoBehaviour
{
	public List<GameObject> Elements;

	private const int MAX_CELL = 4;
	
	void Start()
	{
		InitializeElements(); // random generator name and text a object
	}

	void InitializeElements()
	{
		int x = 0;
		for (int i = 0; i < MAX_CELL; ++i)
		{
			for (int j = 0; j < MAX_CELL; ++j)
			{
				if (x < 15)
					Elements[x].GetComponentInChildren<Text>().text = Elements[x].name = GenerateNumber();
				Logic.Instance.Cells[i, j] = Elements[x];
				++x;
			}
		}
	}

	string GenerateNumber()
	{
		int number_for_initialize = Random.Range(1, 16);
		while (!IsOriginal(number_for_initialize.ToString()))
			number_for_initialize = Random.Range(1, 16);
		return number_for_initialize.ToString();
	}

	bool IsOriginal(string number)
	{
		foreach (var it in Elements)
			if (number == it.name)
				return false;
		return true;
	}
}
