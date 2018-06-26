using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
	public GameObject ButtonReset;
	public Text PrintAttempt;
	[HideInInspector]
	public int CountAttempts;
	[HideInInspector]
	public int RecordAttempts;
	public bool IsGameOver;

	public const int MAX_CELL = 4;
	public GameObject[,] Cells; // reference on the cells from outside
	
	static public Logic Instance { get; private set; }
	void Awake()
	{
		IsGameOver = false;
		ButtonReset.SetActive(false);
		CountAttempts = 0;
		Instance = this;
		Cells = new GameObject[4, 4]; // total: 16 cell where 15 from their not empty a cell
		RecordAttempts = PlayerPrefs.GetInt("Record");
	}

	public bool IsWin()
	{
		int x = 0; // checking with each a iteration on equal
		for (int i = 0; i < MAX_CELL; ++i)
		{
			for (int j = 0; j < MAX_CELL; ++j)
			{
				++x;
				if (x == 16)
					break;
				if (Cells[i, j].name != x.ToString())
					return false;
			}
		}
		return true;
	}
}
