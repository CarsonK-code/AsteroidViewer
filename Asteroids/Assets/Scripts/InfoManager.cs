using TMPro;
using UnityEngine;

public class InfoManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI ID;
    [SerializeField] private TextMeshProUGUI EstimatedDiameterMin;
    [SerializeField] private TextMeshProUGUI EstimatedDiameterMax;
    [SerializeField] private TextMeshProUGUI PotentiallyHazardous;
    

    public NearEarthObject data;

    public void ReceiveData(NearEarthObject incomingData)
    {
        data = incomingData;
        Debug.Log($"InfoManager received data for: {data.name}");

    }

    private void Update()
    {
        Name.text = $"Name: {data.name}";
        ID.text = $"ID: {data.id}";
        string MinText = data.estimated_diameter.kilometers.estimated_diameter_min.ToString("F2");
        EstimatedDiameterMin.text = $"Estimated Diameter (minimum): {MinText}" + " kilometers";
        string MaxText = data.estimated_diameter.kilometers.estimated_diameter_max.ToString("F2");
        EstimatedDiameterMax.text = $"Estimated Diameter (maximum): {MaxText}" + " kilometers";
        PotentiallyHazardous.text = $"Potentially hazardous: {data.is_potentially_hazardous_asteroid}";
        
    }







}


