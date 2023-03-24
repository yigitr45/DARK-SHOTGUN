using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Karakter : MonoBehaviour
{
	#region Veriler
	private static Karakter karakter;
	public static Karakter KarakterKod
	{
		get
		{
			if (karakter == null)
			{
				karakter = GameObject.FindObjectOfType<Karakter>();
			}
			return karakter;
		}
	}

	private Manager ManagerScript;

	private Inventory InventoryScript;

	private float yatay;

	public int CharacterHealt, Coin;

	public GameObject UI, Cinemachine;

	[Header("Dialogue")]

	public GameObject dialogueBox;

	public bool dialogue;

	[Header("Raycast")]

	public LayerMask HitLayer;

	public RaycastHit2D Hit;

	public float Distance;

	public int Range;

	[Header("Chest")]

	public bool Contact;

	public Animator ChestAnimator;

	public GameObject ChestObject;

	[Header("Dash")]

	public bool Dash;

	public float DashRange;

	[Header("Reload")]

	public float totalBullet;
	public float magazineBullet;
	public float currentBullet;

	public float reloadMaxTime;
	public float reloadTime;

	public bool reload;

	[Header("Zıplama")]

	public float TemasCapi;
	public float ZiplamaKuvveti;

	public bool Zipla;
	public bool Fall;

	public bool zeminde;
	public bool HavadaKontrol;

	[Header("Geri Tepme")]

	public float recoilSpeed;
	public float recoilTime;

	public bool GeriTepme;

	[Header("Ateşleme")]

	public bool AtesSerbest;
	public bool Fire;

	[Header("Hareket")]

	public bool lookingRight;

	public float Speed;

	public Vector3 rotation;

	[Header("Sesler")]

	public AudioSource KarakterAudioSource;

	public AudioClip ShotgunFireSound, ShotgunReloadSound, ShotgunSound;
	public AudioClip CoinSound;
	public AudioClip DashSound;
	public AudioClip StepSound;
	public AudioClip JumpSound;

	[Header("UI")]

	public TextMeshProUGUI totalBulletText;
	public TextMeshProUGUI currentBulletText;
	public TextMeshProUGUI coinText;

	public Slider HealtBar;
	public Slider StaminaBar;

	public Color Blue;
	public Color Red;

	public GameObject Mermi;

	public bool StaminaActive;

	public float Stamina;

	public float StaminaCooldown;

	[Header("Konumlar")]

	public Transform[] Temas_Noktalari;

	public Transform Namlu;

	[Header("Katmanlar")]

	public LayerMask HangiZemin;

	public Rigidbody2D KarakterRigidbody { get; set; }
	public Animator KarakterAnimator { get; set; }

	public Vector3 karakterYon;

	public Vector3 recoilDirection;

	Scene mevcutSahne;

	public int sceneNumber;
	#endregion

	private void Awake()
    {
		ManagerScript = FindObjectOfType<Manager>();

		InventoryScript = FindObjectOfType<Inventory>();

		magazineBullet = 5;

		KarakterAudioSource = GameObject.Find("Sound Effect").GetComponent<AudioSource>();

		KarakterRigidbody = GetComponent<Rigidbody2D>();
		KarakterAnimator = GetComponent<Animator>();

		StaminaBar = GameObject.Find("Stamina Bar").GetComponent<Slider>();
		HealtBar = GameObject.Find("Can Bar").GetComponent<Slider>();

		Cinemachine = GameObject.Find("Cinemachine");
		UI = GameObject.Find("Canvas");
	}

	private void Start()
	{
		Coin = ManagerScript.CoinSave;
		CharacterHealt = ManagerScript.HealSave;
		totalBullet = ManagerScript.TotalBulletSave;
		currentBullet = ManagerScript.CurrentBulletSave;
		//InventoryScript.items = ManagerScript.ItemSave;

		#region UI İşlemleri

		currentBulletText.text = currentBullet.ToString();

		if (totalBullet >= 10)
		{
			totalBulletText.text = totalBullet.ToString();
		}
		else
		{
			totalBulletText.text = "0" + totalBullet.ToString();
		}

		/*
		if (currentBullet >= 10)
		{
			currentBulletText.text = currentBullet.ToString();
		}
		else
		{
			currentBulletText.text = "0" + currentBullet.ToString();
		}

		/*
		if (totalBullet >= 100)
		{
			totalBulletText.text = totalBullet.ToString();
		}
		else if (totalBullet >= 10)
		{
			totalBulletText.text = "0" + totalBullet.ToString();
		}
		else
		{
			totalBulletText.text = "00" + totalBullet.ToString();
		}
		*/

		coinText.text = Coin.ToString();
		#endregion

		for (int i = 0; i < InventoryScript.slots.Length; i++)
		{
			if (ManagerScript.ItemSave[i] != null)
			{
				Instantiate(ManagerScript.ItemSave[i], InventoryScript.slots[i].transform, false);
			}
		}

		mevcutSahne = SceneManager.GetActiveScene();

		sceneNumber = mevcutSahne.buildIndex;

		lookingRight = true;
		AtesSerbest = true;
		StaminaActive = true;

		Fire = false;
		GeriTepme = false;

		Speed = 7.5f;
		Stamina = 100;

		StaminaCooldown = 0;

		recoilTime = 0;

		HealtBar.value = CharacterHealt;
	}

	private void Update()
	{
		Raycast();

        //Kontroller();

		Hareket_Katmanlari();

		StaminaValue();
	}

	private void FixedUpdate()
	{
		//Yatay = Input.GetAxisRaw("Horizontal");

		zeminde = Zeminde();

		Temel_Hareketler(yatay);

		Yon_Cevir(yatay);

		Reload();
	}

	private void Temel_Hareketler(float Yatay)
	{
		if (KarakterRigidbody.velocity.y < -3)
		{
            if (!GeriTepme)
            {
				KarakterAnimator.SetBool("Fall", true);
			}
		}

		if (!Fire && !GeriTepme && !Dash)
		{
			if (!reload)
			{
				KarakterRigidbody.velocity = new Vector2(Yatay * Speed, KarakterRigidbody.velocity.y);
			}
			else
			{
				KarakterRigidbody.velocity = new Vector2(Yatay * (Speed / 3), KarakterRigidbody.velocity.y);
			}
		}

		if (Zipla && zeminde)
		{
			zeminde = false;

			//KarakterRigidbody.velocity = new Vector2(0, ZiplamaKuvveti);
			KarakterRigidbody.AddForce(new Vector2(0, ZiplamaKuvveti) * 100);
		}

		if (recoilTime < 0.5f && GeriTepme)
		{
			recoilTime += Time.deltaTime;
		}
		else
		{
			GeriTepme = false;

			recoilTime = 0;
		}

		if (GeriTepme)
		{
			//KarakterRigidbody.velocity = new Vector2(recoilDirection.x * recoilSpeed, KarakterRigidbody.velocity.y);
			//KarakterRigidbody.AddForce(new Vector2(recoilDirection.x * recoilSpeed * 100, 0));

			transform.position += new Vector3(recoilDirection.x * recoilSpeed * Time.fixedDeltaTime, 0 ,0);
		}

		if (zeminde)
		{
		    KarakterAnimator.SetFloat("Hız", Mathf.Abs(Yatay));
		}
		else
		{
			KarakterAnimator.SetFloat("Hız", 0);
		}

		/*
        Ayak Sesleri
        if (!reload)
        {
			if (zeminde && Yatay != 0)
			{
				if (!KarakterAudiosource.isPlaying)
				{
					KarakterAudiosource.clip = FootStepSound;
					KarakterAudiosource.Play();
				}
			}
        }
		*/

		//Hareket Kısıtlayıcı
		if (Dash || GeriTepme)
        {
			KarakterRigidbody.velocity = Vector2.zero;
        }

		/*
        if (KarakterRigidbody.velocity.y > 0 && Fire)
        {
			KarakterAnimator.ResetTrigger("Jump");
        }
		*/
	}

	private void Raycast()
    {
		Hit = Physics2D.Raycast(transform.position, transform.right, Range, HitLayer);

		Distance = Hit.distance;
	}

	public void Ates_Et()
	{
		GeriTepme = true;

		AtesSerbest = false;

		Instantiate(Mermi, Namlu.position, Quaternion.identity);

		KarakterAudioSource.PlayOneShot(ShotgunFireSound);

		currentBullet--;

		currentBulletText.text = currentBullet.ToString();

		/*
		if (currentBullet >= 10)
		{
			currentBulletText.text = currentBullet.ToString();
		}
		else
		{
			currentBulletText.text = "0" + currentBullet.ToString();
		}
		*/
	}

	private void Yon_Cevir(float yatay)
	{
		if (yatay > 0 && !lookingRight || yatay < 0 && lookingRight)
		{
			if (!Fire && !Dash && !GeriTepme)
			{
				/*
				sagaBak = !sagaBak;

				karakterYon = transform.localScale;

				karakterYon.x *= -1;

				transform.localScale = karakterYon;
				*/

				lookingRight = !lookingRight;

				rotation = transform.eulerAngles;

				if (lookingRight)
                {
					rotation.y = 0;

					recoilDirection.x = -1;
				}
                else
                {
					rotation.y = 180;

					recoilDirection.x = 1;
				}

				transform.eulerAngles = rotation;
			}
		}
	}

	private bool Zeminde()
	{
		if (KarakterRigidbody.velocity.y <= 0)
		{
			foreach (Transform nokta in Temas_Noktalari)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(nokta.position, TemasCapi, HangiZemin);
				for (int i = 0; i < colliders.Length; i++)
				{
					if (colliders[i].gameObject != gameObject)
					{
						KarakterAnimator.ResetTrigger("Jump");
						KarakterAnimator.SetBool("Fall", false);
						return true;
					}
				}
			}
		}
		return false;
	}

	/*
    #region PC Controller
	private void Kontroller()
	{
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (!Zipla && !GeriTepme && !Dash && !reload && zeminde)
			{
				KarakterAnimator.SetTrigger("Jump");

				KarakterAudioSource.PlayOneShot(JumpSound);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
            if (AtesSerbest)
            {
				if (!HavadaKontrol)
				{
					KarakterAnimator.SetTrigger("Shot");
				}
				else
				{
					KarakterAnimator.SetTrigger("JumpShot");
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			if (totalBullet > 0 && currentBullet != magazineBullet && !reload && zeminde)
			{
				reload = true;

				AtesSerbest = false;

				KarakterAnimator.SetBool("Charge", true);

				if (magazineBullet - currentBullet < totalBullet)
				{
					reloadMaxTime = magazineBullet - currentBullet;
				}
				else
				{
					reloadMaxTime = totalBullet;
				}

				StartCoroutine(ReloadSound());
			}
		}

        if (!GeriTepme && !Dash && !Fire && !reload)
        {
            if (Stamina >= 100)
            {
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (Distance > 0)
					{
						DashRange = Distance - 1f;
					}
					else
					{
						DashRange = Range;
					}

					if (lookingRight)
					{
						transform.position += new Vector3(DashRange, 0, 0);

						lookingRight = false;
					}
					else
					{
						transform.position -= new Vector3(DashRange, 0, 0);

						lookingRight = true;
					}

					StaminaActive = false;

					KarakterAnimator.SetTrigger("Dash");
						
					KarakterAudioSource.PlayOneShot(DashSound);

					StartCoroutine(StaminaReset());
				}
			}
		}

        if (Input.GetKeyDown(KeyCode.F))
        {
			if (Contact && !reload)
			{
				ChestAnimator.SetBool("Open", true);

				Contact = false;
			}

            if (dialogue)
            {
				dialogueBox.SetActive(true);

				dialogue = false;
            }
		}
	}
    #endregion
	*/

    private void OnCollisionEnter2D(Collision2D collision)
	{
        if (!collision.gameObject.CompareTag("Zemin"))
        {
			GeriTepme = false;
		}	
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Jeton"))
		{
			KarakterAudioSource.PlayOneShot(CoinSound);

			Coin ++;

			coinText.text = Coin.ToString();

			Destroy(collision.gameObject);
		}

		if (collision.gameObject.CompareTag("Chest"))
		{
			ChestObject = collision.gameObject;
			ChestAnimator = collision.gameObject.GetComponent<Animator>();

			Contact = true;
		}

		if (collision.gameObject.CompareTag("Bearded"))
		{
			dialogue = true;
		}

        if (collision.gameObject.CompareTag("Damaged"))
        {
			CharacterHealt -= Random.Range(5, 10);

			//Düzenlenecek
			if (lookingRight)
			{
				KarakterRigidbody.velocity += new Vector2(-10, 5);
			}
			else
			{
				KarakterRigidbody.velocity += new Vector2(10, 5);
			}

			if (CharacterHealt <= 0)
            {
				KarakterAnimator.SetTrigger("Death");
            }
            else
            {
				KarakterAnimator.SetTrigger("Damaged");
			}
        }

		if (collision.gameObject.CompareTag("Finish"))
        {
			ManagerScript.CoinSave = Coin;
			ManagerScript.HealSave = CharacterHealt;
			ManagerScript.TotalBulletSave = totalBullet;
			ManagerScript.CurrentBulletSave = currentBullet;
			//ManagerScript.ItemSave = InventoryScript.items;

			if (sceneNumber == 2)
            {
				ManagerScript.level = 3;
			}

            if (sceneNumber == 3)
            {
				ManagerScript.level = 4;
            }

			SceneManager.LoadScene(1);
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
			ChestObject = null;
			ChestAnimator = null;

			Contact = false;
        }

		if (collision.gameObject.CompareTag("Bearded"))
		{
			dialogue = false;

			dialogueBox.SetActive(false);
		}
	}

	/*
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Chest"))
		{
			ChestObject = collision.gameObject;
			ChestAnimator = collision.gameObject.GetComponent<Animator>();

			if (Input.GetKeyDown(KeyCode.F))
			{
				ChestAnimator.SetBool("Open", true);
			}
		}
	}
	*/

    private void Reload()
	{
		if (reload)
		{
			if (reloadTime > reloadMaxTime)
			{
				float requiredBullet = magazineBullet - currentBullet;

				if (requiredBullet > totalBullet)
				{
					currentBullet += totalBullet;
					totalBullet = 0;
				}
				else
				{
					currentBullet = magazineBullet;
					totalBullet -= requiredBullet;
				}

				currentBulletText.text = currentBullet.ToString();

				if (totalBullet >= 10)
				{
					totalBulletText.text = totalBullet.ToString();
				}
				else
				{
					totalBulletText.text = "0" + totalBullet.ToString();
				}

				/*
				if (totalBullet >= 100)
				{
					totalBulletText.text = totalBullet.ToString();
				}
				else if (totalBullet >= 10)
				{
					totalBulletText.text = "0" + totalBullet.ToString();
				}
				else
				{
					totalBulletText.text = "00" + totalBullet.ToString();
				}

				if (currentBullet >= 10)
				{
					currentBulletText.text = currentBullet.ToString();
				}
				else
				{
					currentBulletText.text = "0" + currentBullet.ToString();
				}
				*/

				reload = false;

				AtesSerbest = true;

				KarakterAnimator.SetBool("Charge", false);

				reloadTime = 0;
			}
			else
			{
				reloadTime += Time.deltaTime;
			}
		}
	}

	private void Hareket_Katmanlari()
	{
		if (zeminde)
		{
			KarakterAnimator.SetLayerWeight(1, 1);
			HavadaKontrol = false;
		}
		else
		{
			KarakterAnimator.SetLayerWeight(1, 1);
			HavadaKontrol = true;
		}
	}

	public void ShotgunSes()
	{
		KarakterAudioSource.PlayOneShot(ShotgunSound);
	}

	private IEnumerator ReloadSound()
	{
		for (int i = 0; i < reloadMaxTime; i++)
		{
			Debug.Log(i);

			KarakterAudioSource.PlayOneShot(ShotgunReloadSound);

			yield return new WaitForSeconds(1f);
		}
	}

	public void Dash_Move()
    {

    }

	public void RightButton()
    {
		yatay = 1;
	}

	public void LeftButton()
    {
        yatay = -1;
	}

	public void WaitButton()
    {
		yatay = 0;
	}

	public void JumpButton()
    {
		if (!Zipla && !GeriTepme && !Dash && !reload && zeminde)
		{
			KarakterAnimator.SetTrigger("Jump");

			KarakterAudioSource.PlayOneShot(JumpSound);
		}
	}

	public void AttackButton()
    {
		if (AtesSerbest)
		{
            if (zeminde)
            {
				KarakterAnimator.SetTrigger("Shot");
			}
            else
            {
				KarakterAnimator.SetTrigger("JumpShot");
			}			
		}
	}

	public void DashButton()
    {
		if (!GeriTepme && !Dash && !Fire && !reload)
		{
			if (Stamina >= 100)
			{
				if (Distance > 0)
				{
					DashRange = Distance - 1;
				}
				else
				{
					DashRange = Range;
				}

				if (lookingRight)
			    {
				    transform.position += new Vector3(DashRange, 0, 0);

				    lookingRight = false;
				}
				else
				{
					transform.position -= new Vector3(DashRange, 0, 0);

					lookingRight = true;
				}

				StaminaActive = false;

				KarakterAnimator.SetTrigger("Dash");

				KarakterAudioSource.PlayOneShot(DashSound);

				StartCoroutine(StaminaReset());
			}
		}
	}

	public void ReloadButton()
    {
		if (totalBullet > 0 && currentBullet != magazineBullet && !reload && zeminde)
		{
			reload = true;

			AtesSerbest = false;

			KarakterAnimator.SetBool("Charge", true);

			if (magazineBullet - currentBullet < totalBullet)
			{
				reloadMaxTime = magazineBullet - currentBullet;
			}
			else
			{
				reloadMaxTime = totalBullet;
			}

			StartCoroutine(ReloadSound());
		}
	}

	public void ContactButton()
    {
        if (Contact && !reload)
        {
			ChestAnimator.SetBool("Open", true);

			Contact = false;
		}

        if (dialogue)
        {
			dialogueBox.SetActive(true);

			dialogue = false;
        }
	}

	private void StaminaValue()
	{
		if (!Dash)
		{
			if (StaminaCooldown <= 0)
			{
				if (Stamina < 100)
				{
					StaminaActive = true;

					Stamina += 10 * Time.deltaTime;

					StaminaCooldown = 0;
				}
				else
				{
					Stamina = 100;
				}
			}
			else
			{
				StaminaCooldown -= Time.deltaTime;
			}
		}

		StaminaBar.value = Stamina;
		StaminaBar.fillRect.GetComponent<Image>().color = new Color(0, 0, Stamina / 100);
	}

	private IEnumerator StaminaReset()
	{
		if (Stamina > 0)
		{
			Stamina -= 5;
			StaminaCooldown += 0.05f;

			yield return new WaitForSeconds(0.01f);

			StartCoroutine(StaminaReset());
		}
	}

	public IEnumerator Die()
    {
		yield return new WaitForSeconds(1);

		Time.timeScale = 0;
    }
}
