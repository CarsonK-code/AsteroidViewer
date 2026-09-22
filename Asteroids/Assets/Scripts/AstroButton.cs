using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class AstroButton : MonoBehaviour, IPointerClickHandler
{
    public static GameObject asteroidPrefab;
    //static = shared

    private static GameObject currentAsteroid;



    public GameObject mainCamera;

    public NearEarthObject jsonData;

    public GameObject burjKahlifa;
    //public GameObject mtEverest;

    public bool clickedButton = false;

    



    public void OnPointerClick(PointerEventData eventData)
    {

        if (currentAsteroid != null)
        {
            Destroy(currentAsteroid);
        }

        float diameter = (float)jsonData.estimated_diameter.kilometers.estimated_diameter_min;


        burjKahlifa.SetActive(diameter > 2);


        Debug.Log($"Clicked {jsonData.name}");

        Vector3 SpawnPoint = new Vector3(diameter * 5f, diameter * 65f, diameter * -60f);
        Vector3 asteroidScale = new Vector3(diameter * 100f, diameter * 100f, diameter * 100f);
        Vector3 targetPos = new Vector3(diameter * -150f, diameter * 70f, diameter * -65f);



        currentAsteroid = Instantiate(asteroidPrefab, SpawnPoint, Quaternion.identity);
        currentAsteroid.transform.localScale = asteroidScale;
        
        InfoManager infoManager = UnityEngine.Object.FindObjectOfType<InfoManager>();
        if (infoManager != null)
        {
            infoManager.ReceiveData(jsonData);
        }
        else
        {
            Debug.LogError("Could not find InfoManager script in the scene!");
        }



        mainCamera.transform.DOKill();
        mainCamera.transform.DOMove(targetPos, 1.5f).SetEase(Ease.InOutSine);







    }
}