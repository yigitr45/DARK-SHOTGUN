using UnityEngine;

[System.Serializable]

public class Items
{
    public string itemName;

    public int itemID, itemMiktar, itemdepoMiktar, itemHasar;

    public Sprite itemIcon;

    public GameObject itemModel;
    public GameObject itemButtonModel;

    public AudioClip itemAudio;

    public ItemType itemTipi;

    public enum ItemType
    {
        Silah,
        Ýksir,
        Bos
    }

    public Items(string isim, int id, int miktar, int depo, int hasar, ItemType tip)
    {
        itemName = isim;
        itemID = id;
        itemMiktar = miktar;
        itemdepoMiktar = depo;
        itemHasar = hasar;
        itemTipi = tip;

        itemIcon = Resources.Load<Sprite>(id.ToString());

        if (tip == ItemType.Ýksir)
        {
            itemModel = Resources.Load<GameObject>("Potion");
            itemAudio = Resources.Load<AudioClip>("Potion");

            itemButtonModel = Resources.Load<GameObject>(id.ToString());
        }

        if (tip == ItemType.Silah)
        {
            itemModel = Resources.Load<GameObject>("Weapon");
            itemAudio = Resources.Load<AudioClip>("Weapon");
        }
    }
}
