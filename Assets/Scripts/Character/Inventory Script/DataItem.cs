using System.Collections.Generic;
using UnityEngine;

public class DataItem : MonoBehaviour
{
	public List<Items> items;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		items.Add(new Items("Bo� Slot"     , 0, 0, 0, 0, Items.ItemType.Bos));
		items.Add(new Items("Can �ksiri"   , 1, 1, 1, 0, Items.ItemType.�ksir));
		items.Add(new Items("Enerji �ksiri", 2, 1, 1, 0, Items.ItemType.�ksir));
		items.Add(new Items("H�z �ksiri"   , 3, 1, 1, 0, Items.ItemType.�ksir));
	}
}
