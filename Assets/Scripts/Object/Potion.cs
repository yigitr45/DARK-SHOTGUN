using UnityEngine;

public class Potion : MonoBehaviour
{
    public Inventory InventoryScript { get; set; }
    public DataItem DataItemScript { get; set; }

    [Header("Item Özellikleri")]
    public int itemID;
    public int itemMiktar;

    public SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        InventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        DataItemScript = GameObject.Find("Data Item").GetComponent<DataItem>();

        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        itemID = Random.Range(1, 3);

        itemMiktar = 1;

        SpriteRenderer.sprite = DataItemScript.items[itemID].itemIcon;

        /*
        if (itemName[itemID] == "Heal")
        {
            SpriteRenderer.sprite = itemSprite[0];
        }

        if (itemName[itemID] == "Stamina")
        {
            SpriteRenderer.sprite = itemSprite[1];
        }

        if (itemName[itemID] == "Speed")
        {
            SpriteRenderer.sprite = itemSprite[2];
        }
        */
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            for (int i = 0; i < InventoryScript.slots.Length; i++)
            {
                if (InventoryScript.isFull[i] == false)
                {
                    InventoryScript.isFull[i] = true;

                    InventoryScript.items[i] = itemButton[itemID];

                    GameObject Instaniated = Instantiate(itemButton[itemID], InventoryScript.slots[i].transform, false);

                    CodeLink codeLink = FindObjectOfType<CodeLink>();

                    if (itemName[hangiPotion] == "Heal")
                    {
                        Instaniated.GetComponent<Button>().onClick.AddListener(FindObjectOfType<Karakter>().GetComponent<CodeLink>().HealPotionButton);
                    }
                    else if (itemName[hangiPotion] == "Stamina")
                    {
                        Instaniated.GetComponent<Button>().onClick.AddListener(FindObjectOfType<Karakter>().GetComponent<CodeLink>().StaminaPotionButton);
                    }
                    else if (itemName[hangiPotion] == "Speed")
                    {
                        Instaniated.GetComponent<Button>().onClick.AddListener(FindObjectOfType<Karakter>().GetComponent<CodeLink>().SpeedPotionButton);
                    }

                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
    */
}
