using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GetJSON : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform contentParent;

    public GameObject thisCamera;

    //public GameObject mtEverest;
    public GameObject burjKahlifa;

    public GameObject originalAsteroid;

    private string baseURL = "https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=mSEHpISlDuw83Gpi3SZVFmFMmh0TvCPyKjZhZSSV";

    
    private int pagesToFetch = 5;


    void Start()
    {
        StartCoroutine(FetchMultiplePages());
    }

    IEnumerator FetchMultiplePages()
    {
        // Loop through 5 pages
        for (int pageIndex = 0; pageIndex < pagesToFetch; pageIndex++)
        {
           
            string pagedURL = $"{baseURL}&page={pageIndex}";
            yield return StartCoroutine(FetchData(pagedURL));
        }
    }

    IEnumerator FetchData(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // send request and wait for response
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string json = webRequest.downloadHandler.text;
                    NeoBrowseResponse data = JsonUtility.FromJson<NeoBrowseResponse>(json);

                    foreach (var neo in data.near_earth_objects)
                    {
                        string message =
                            $"{neo.name}\n" +
                            $"hazardous: {neo.is_potentially_hazardous_asteroid}\n" +
                            $"diameter: {neo.estimated_diameter.kilometers.estimated_diameter_min:F2}-" +
                            $"{neo.estimated_diameter.kilometers.estimated_diameter_max:F2} km";

                        Debug.Log(message);

                        GameObject newButton = Instantiate(buttonPrefab, contentParent);
                        newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = neo.name;
                        var button = newButton.AddComponent<AstroButton>();
                        AstroButton.asteroidPrefab = originalAsteroid;
                        
                        button.jsonData = neo;

                        button.mainCamera = thisCamera;


                        button.burjKahlifa = burjKahlifa;
                        //button.mtEverest = mtEverest;


                        

                    }


                    break;
            }
        }
    }
}


[Serializable]
public class NeoBrowseResponse
{
    public NearEarthObject[] near_earth_objects;
}

[Serializable]
public class NearEarthObject
{
    public string id;
    public string name;
    public bool is_potentially_hazardous_asteroid;
    public EstimatedDiameter estimated_diameter;
    public CloseApproachData[] close_approach_data;
}

[Serializable]
public class EstimatedDiameter
{
    public Kilometers kilometers;
}

[Serializable]
public class Kilometers
{
    public double estimated_diameter_min;
    public double estimated_diameter_max;
}

[Serializable]
public class CloseApproachData
{
    public string close_approach_date;
    public RelativeVelocity relative_velocity;
    public MissDistance miss_distance;
}

[Serializable]
public class RelativeVelocity
{
    public string kilometers_per_second;
}

[Serializable]
public class MissDistance
{
    public string kilometers;
}

[Serializable]
public class PageInfo
{
    public int size;
    public int total_elements;
    public int total_pages;
    public int number;
}