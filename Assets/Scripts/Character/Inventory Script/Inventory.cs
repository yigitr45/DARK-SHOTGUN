using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> itemler = new List<Items>();

    public DataItem dataItem;

	public GameObject itemObject;

    public GameObject[] slots = new GameObject[5];

	public bool[] isFull;

	//public GameObject[] items = new GameObject[5];

	void Start()
	{
		dataItem = GameObject.Find("Data Item").GetComponent<DataItem>();
	}

	public bool ItemEkle(int id, int miktar)
	{
		for (int i = 0; i < dataItem.items.Count; i++)
		{
			if (dataItem.items[i].itemID == id)
			{
				Items yeniItem = new Items(dataItem.items[i].itemName, dataItem.items[i].itemID, miktar, dataItem.items[i].itemdepoMiktar, dataItem.items[i].itemHasar, dataItem.items[i].itemTipi);
				bool isAdd = EmptySlotAddItem(yeniItem);
				if (isAdd)
					return true;
			}
		}
		return false;
	}

	public bool EmptySlotAddItem(Items item)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (isFull[i] == false)
			{
				isFull[i] = true;

				itemler.Insert(i, item);
				
                GameObject Instaniated = Instantiate(item.itemButtonModel, slots[i].transform, false);

				Instaniated.GetComponent<Button>().onClick.AddListener(FindObjectOfType<Karakter>().GetComponent<CodeLink>().HealPotionButton);

				return true;
			}
		}
		return false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
			Potion potion = collision.gameObject.GetComponent<Potion>();

			bool isAdd = ItemEkle(potion.itemID, potion.itemMiktar);

			if (isAdd) 
			{
				Destroy(collision.gameObject);
			}
        }

		/*
        if (collision.gameObject.CompareTag("Weapon"))
        {
			Silah Almak
        }
		*/
    }
}
