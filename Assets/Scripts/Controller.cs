using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	private enum Move { RIGHT, LEFT, UP, DOWN };

	private AudioSource Shift;
	void Start()
	{
		Shift = GetComponent<AudioSource>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!Logic.Instance.IsGameOver)
		{
			if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
			{
				if (eventData.delta.x > 0)
					Moving(Move.RIGHT);
				else
					Moving(Move.LEFT);
			}
			else
			{
				if (eventData.delta.y > 0)
					Moving(Move.UP);
				else
					Moving(Move.DOWN);
			}
		}
	}

	public void OnDrag(PointerEventData eventData) {}

	void Moving(Move move)
	{
		Vector2Int EmptyCell = FindEmptyCell();
		switch (move)
		{
			case Move.RIGHT:
				if (EmptyCell.x - 1 > -1) 
					Swap(ref Logic.Instance.Cells[EmptyCell.y, EmptyCell.x - 1],
						ref Logic.Instance.Cells[EmptyCell.y, EmptyCell.x]);
				break;
			case Move.LEFT:
				if (EmptyCell.x + 1 < Logic.MAX_CELL)
					Swap(ref Logic.Instance.Cells[EmptyCell.y, EmptyCell.x + 1],
						ref Logic.Instance.Cells[EmptyCell.y, EmptyCell.x]);
				break;
			case Move.DOWN:
				if (EmptyCell.y - 1 > -1)
					Swap(ref Logic.Instance.Cells[EmptyCell.y - 1, EmptyCell.x],
						ref Logic.Instance.Cells[EmptyCell.y, EmptyCell.x]);
				break;
			case Move.UP:
				if (EmptyCell.y + 1 < Logic.MAX_CELL)
					Swap(ref Logic.Instance.Cells[EmptyCell.y + 1, EmptyCell.x],
						ref Logic.Instance.Cells[EmptyCell.y, EmptyCell.x]);
				break;
		}
	}

	Vector2Int FindEmptyCell()
	{
		Vector2Int PositionEmptyCell = new Vector2Int(0, 0);
		for (int i = 0; i < Logic.MAX_CELL; ++i)
		{
			for (int j = 0; j < Logic.MAX_CELL; ++j)
			{
				if (Logic.Instance.Cells[i, j].name == "EmptyCell")
				{
					PositionEmptyCell = new Vector2Int(j, i);
					return PositionEmptyCell;
				}
			}
		}
		return PositionEmptyCell; // it's impossible
	}

	void Swap(ref GameObject cell, ref GameObject empty_cell)
	{
		Sprite Temp = cell.GetComponent<Image>().sprite;
		cell.GetComponent<Image>().sprite = empty_cell.GetComponent<Image>().sprite;
		empty_cell.GetComponent<Image>().sprite = Temp;

		string Temp2 = cell.GetComponentInChildren<Text>().text;
		cell.GetComponentInChildren<Text>().text = empty_cell.GetComponentInChildren<Text>().text;
		empty_cell.GetComponentInChildren<Text>().text = Temp2;

		string Temp3 = cell.name;
		cell.name = empty_cell.name;
		empty_cell.name = Temp3;

		AfterMoving(); // If can move
	}

	void AfterMoving()
	{
		++Logic.Instance.CountAttempts;
		Shift.Play();
		if (Logic.Instance.IsWin())
		{
			Logic.Instance.ButtonReset.SetActive(true);
			if (Logic.Instance.RecordAttempts == 0) // if the player plays for the first time 
				Logic.Instance.RecordAttempts = int.MaxValue;
			if (Logic.Instance.CountAttempts < Logic.Instance.RecordAttempts)
			{
				PlayerPrefs.SetInt("Record", Logic.Instance.CountAttempts);
				PlayerPrefs.Save();
				Logic.Instance.RecordAttempts = Logic.Instance.CountAttempts;
			}
			Logic.Instance.PrintAttempt.text = "Score: " + Logic.Instance.CountAttempts
												+ "\nRecord: " + Logic.Instance.RecordAttempts;
			Logic.Instance.IsGameOver = true;
		}
		else
			Logic.Instance.PrintAttempt.text = "Attepmts: " + Logic.Instance.CountAttempts;
	}
}
