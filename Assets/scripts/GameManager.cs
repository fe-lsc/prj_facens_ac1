
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    public GameObject hazardPrefab;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHazards());   
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
}
