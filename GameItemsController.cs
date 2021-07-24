using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameItemsController : MonoBehaviour
{
	public Text gameOverText;
	public Text youWinText;
	public Text instructionsText; 

	public int randomNumber;

    AudioSource damageAS, scoreAS;

    public AudioClip damageSound;
    public AudioClip scoreSound;

	public GameObject HandSanitizer;
    public GameObject News;
    public GameObject FaceShield;
    public GameObject SurgicalMask;
    public GameObject HomemadeClothMask;
    public GameObject PFF2FaceMask;
    public GameObject FakeNews;
    public GameObject Virus;
    public GameObject Vaccine;

    public GameObject Player;

    public int xp = 0;

	int countHandSanitizer = 0;
	int countNews = 0;
	int countFaceShield = 0;
	int countSurgicalMask = 0;
	int countHomemadeClothMask = 0;
	int countPFF2FaceMask = 0;
	int countFakeNews = 0;
	int countVirus = 0;
	int countVaccine = 0;

    GameObject[] instanceHandSanitizer, instanceNews, instanceFaceShield, instanceSurgicalMask, instanceHomemadeClothMask, instancePFF2FaceMask, instanceFakeNews, instanceVirus, instanceVaccine;

	float time = 0;
	float vaccineTime = 0;

    // Start is called before the first frame update
    void Start()
    {
    	Time.timeScale = 1;
        instanceHandSanitizer = new GameObject[5];
        instanceNews = new GameObject[5];
        instanceFaceShield = new GameObject[5];
        instanceSurgicalMask = new GameObject[5];
        instanceHomemadeClothMask = new GameObject[5];
        instancePFF2FaceMask = new GameObject[5];
        instanceFakeNews = new GameObject[5];
        instanceVirus = new GameObject[5];
        instanceVaccine = new GameObject[1];

        damageAS = gameObject.AddComponent<AudioSource>();
        scoreAS = gameObject.AddComponent<AudioSource>();

        damageAS.clip = damageSound;
        scoreAS.clip = scoreSound;
    }

    // Update is called once per frame
    void Update()
    {
    	time = time + Time.deltaTime;
    	vaccineTime += Time.deltaTime;

    	if(vaccineTime >= 30)
	    {
	    	instanceVaccine[0] = Instantiate<GameObject>(Vaccine);
	    	countVaccine ++;

	    	time = -1;
	    }

    	else if(time >= 2)
    	{
    		randomNumber = Random.Range(1,7);

    		if(randomNumber == 0)
    		{
    			if(countHandSanitizer < 5){
    				instanceHandSanitizer[countHandSanitizer] = Instantiate<GameObject>(HandSanitizer);
		        	countHandSanitizer ++;
    			}
		        
    		}

    		else if (randomNumber == 1)
    		{
    			if(countNews < 5)
    			{
    				instanceNews[countNews] = Instantiate<GameObject>(News);
		        	countNews ++;
    			}
    		}

    		else if (randomNumber == 2)
    		{
    			if(countFaceShield < 5)
    			{
    				instanceFaceShield[countFaceShield] = Instantiate<GameObject>(FaceShield);
		        	countFaceShield ++;
    			}
    		}

    		else if (randomNumber == 3)
    		{
    			if(countSurgicalMask < 5)
    			{
    				instanceSurgicalMask[countSurgicalMask] = Instantiate<GameObject>(SurgicalMask);
		        	countSurgicalMask ++;
    			}
    		}

    		else if (randomNumber == 4){
    			if(countHomemadeClothMask < 5)
    			{
    				instanceHomemadeClothMask[countHomemadeClothMask] = Instantiate<GameObject>(HomemadeClothMask);
		        	countHomemadeClothMask ++;
    			}
    		}

    		else if (randomNumber == 5){
    			if(countPFF2FaceMask < 5)
    			{
    				instancePFF2FaceMask[countPFF2FaceMask] = Instantiate<GameObject>(PFF2FaceMask);
		        	countPFF2FaceMask ++;
    			}
    		}

    		else if (randomNumber == 6){
    			if(countFakeNews < 5)
    			{
    				instanceFakeNews[countFakeNews] = Instantiate<GameObject>(FakeNews);
		        	countFakeNews ++;
    			}
    		}

    		else{
    			if(countVirus < 5)
    			{
    				instanceVirus[countVirus] = Instantiate<GameObject>(Virus);
		        	countVirus ++;
    			}
    		}

    		time = 0;
    	}

    	countHandSanitizer = MoveItem(instanceHandSanitizer, countHandSanitizer, 1);
    	countNews = MoveItem(instanceNews, countNews, 1);
    	countFaceShield = MoveItem(instanceFaceShield, countFaceShield, 1);
    	countSurgicalMask = MoveItem(instanceSurgicalMask, countSurgicalMask, 1);
    	countHomemadeClothMask = MoveItem(instanceHomemadeClothMask, countHomemadeClothMask, 1);
    	countPFF2FaceMask = MoveItem(instancePFF2FaceMask, countPFF2FaceMask, 1);
    	countFakeNews = MoveItem(instanceFakeNews, countFakeNews, 0);
    	countVirus = MoveItem(instanceVirus, countVirus, 1);
    	countVaccine = MoveItem(instanceVaccine, countVaccine, 2);

    	if(vaccineTime >= 30)
    	{
    		vaccineTime = 0;
    	}
    }

	private int MoveItem(GameObject[] instance, int count, int score)
	{
		for (int i = 0; i < count; i++)
		{
			float x = instance[i].transform.localPosition.x;
			x -= Time.deltaTime * GameManager.Instance.ScrollSpeed;

			Vector3 curPos = instance[i].transform.localPosition;
			instance[i].transform.localPosition = new Vector3(x, curPos.y, curPos.z);

			float Distance = Vector3.Distance(instance[i].transform.position, Player.transform.position);

			if(x < -958)
			{
				count = DestroyItem(instance, count, x, i);
			}
			else if(score == 0 && Distance < 100)
			{
                damageAS.Play();

				Time.timeScale = 0;
    			gameOverText.gameObject.SetActive(true);
    			instructionsText.gameObject.SetActive(true);

    			if (Input.GetButtonDown("Jump")){
		        	SceneManager.LoadScene("Game");
		        }
		        else if(Input.GetButtonDown("Fire1")){
		        	SceneManager.LoadScene("Menu");
		        }
			}
			else if(score == 1 && Distance < 130)
			{
                scoreAS.Play();

				count = DestroyItem(instance, count, x, i);
				xp += 100;
			}
			else if(score == 2 && Distance < 100){
                scoreAS.Play();

				Time.timeScale = 0;
				youWinText.gameObject.SetActive(true);
				instructionsText.gameObject.SetActive(true);

				if (Input.GetButtonDown("Jump")){
		        	SceneManager.LoadScene("Game");
		        }
		        else if(Input.GetButtonDown("Fire1")){
		        	SceneManager.LoadScene("Menu");
		        }
			}
			
		}

		return count;
	}

	private int DestroyItem(GameObject[] instance, int count, float x, int i)
	{
        DestroyImmediate(instance[i]);

        for(int j = i; j < count - 1; j++)
        {
            instance[j] = instance[j+1];
        }

        count --;

        return count;
	}
}
