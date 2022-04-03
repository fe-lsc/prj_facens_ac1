
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    public GameObject hazardPrefab;
    public GameObject player;
    public TMPro.TextMeshProUGUI scoreText;
    public Image backgroundMenu;

    private int             score;
    private float           timer;
    private bool            gameOver;
    private Coroutine hazardsCoroutine;

    public GameObject mainVCam;
    public GameObject zoomVCam;
    public GameObject gameOverMenu;

    private static GameManager instance;
    public static GameManager Instance => instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
    }

    private void OnEnable(){
        player.SetActive(true);

        mainVCam.SetActive(true);
        zoomVCam.SetActive(false);

        gameOver = false;
        score = 0;
        timer = 0;
        scoreText.text = "0";
        hazardsCoroutine = StartCoroutine(SpawnHazards());  
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                StartCoroutine(ScaleTime(0, 1, 0.75f));
                backgroundMenu.gameObject.SetActive(false);
            }
            if(Time.timeScale == 1)
            {
                StartCoroutine(ScaleTime(1, 0, 0.75f));
                backgroundMenu.gameObject.SetActive(true);
            }
        }

        if (gameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score++;
            scoreText.text = score.ToString();

            timer = 0;
        }
    }

    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while(timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * end;
    }

    private IEnumerator SpawnHazards()
    {

        var hazardToSpawn = Random.Range(1, 4);

        for(int i  = 0; i < hazardToSpawn; i++)
        {
            var x = Random.Range(-7, 7);
            var drag = Random.Range(0f, 2f);
            var hazard = Instantiate(hazardPrefab, new Vector3(x, 11, 0), Quaternion.identity);

            hazard.GetComponent<Rigidbody>().drag = drag;

        }

        var timeToWait = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(1f);

        yield return SpawnHazards();
    }

    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);
        gameOver = true;
        mainVCam.SetActive(false);
        zoomVCam.SetActive(true);

        gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
