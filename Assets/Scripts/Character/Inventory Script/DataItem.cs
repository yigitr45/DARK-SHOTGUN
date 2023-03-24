using System.Collections.Generic;
using UnityEngine;

public class DataItem : MonoBehaviour
{
	public List<Items> items;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		items.Add(new Items("Boþ Slot"     , 0, 0, 0, 0, Items.ItemType.Bos));
		items.Add(new Items("Can Ýksiri"   , 1, 1, 1, 0, Items.ItemType.Ýksir));
		items.Add(new Items("Enerji Ýksiri", 2, 1, 1, 0, Items.ItemType.Ýksir));
		items.Add(new Items("Hýz Ýksiri"   , 3, 1, 1, 0, Items.ItemType.Ýksir));
	}
}
