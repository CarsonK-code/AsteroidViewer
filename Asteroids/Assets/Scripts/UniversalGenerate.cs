using UnityEngine;

public class UniversalGenerate : MonoBehaviour
{
    private Vector3 rotationSpeed = new Vector3(0, 100, 0);

    private void Start()
    {
        float randomScaleX = Random.Range(0.5f, 2f);
        float randomScaleY = Random.Range(0.5f, 2f);
        float randomScaleZ = Random.Range(0.5f, 2f);
        transform.localScale = new Vector3(randomScaleX, randomScaleY, randomScaleZ);
     


    }


    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);

    }
}
