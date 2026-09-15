using UnityEngine;

public class AsteroidGenerate : MonoBehaviour
{
    private Vector3 rotationSpeed = new Vector3(0, 100, 0);

    [SerializeField] private float baseDisplacement = 0.5f;
    [SerializeField] private int octaves = 4;
    [SerializeField] private float lacunarity = 2f; // frequency multiplier per octave
    [SerializeField] private float persistence = 0.5f; // amplitude multiplier per octave
    [SerializeField] private float baseNoiseScale = 1.5f;

    void Start()
    {
        GenerateAsteroid();
    }

    private void GenerateAsteroid()
    {
        
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;

       
        Vector3 seedOffset = new Vector3(
            Random.Range(0f, 1000f),
            Random.Range(0f, 1000f),
            Random.Range(0f, 1000f)
        );

        
        for (int i = 0; i < vertices.Length; i++)
        {
            
            Vector3 dir = vertices[i].normalized;

            
            float displacement = FractalNoise(dir + seedOffset, octaves, baseNoiseScale, lacunarity, persistence);

            
            vertices[i] = dir * (vertices[i].magnitude + displacement * baseDisplacement);
        }

        
        mesh.vertices = vertices;

        
        mesh.RecalculateNormals();

       
        mesh.RecalculateBounds();
    }

    float FractalNoise(Vector3 p, int octaves, float scale, float lacunarity, float persistence)
    {
        
        float total = 0f;
        float amplitude = 1f;
        float frequency = scale;
        float maxAmplitude = 0f; 

        
        for (int o = 0; o < octaves; o++)
        {
            total += Perlin3D(p.x * frequency, p.y * frequency, p.z * frequency) * amplitude;

            
            maxAmplitude += amplitude;

            //halve amplitude for octave
            amplitude *= persistence;

            // increase frequency for octave
            frequency *= lacunarity;
        }

        return (total / maxAmplitude) * 2f - 1f;
    }

    float Perlin3D(float x, float y, float z)
    {
        float xy = Mathf.PerlinNoise(x, y); 
        float yz = Mathf.PerlinNoise(y, z); 
        float xz = Mathf.PerlinNoise(x, z); 

       
        return (xy + yz + xz) / 3f;
    }


    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);

    }
}
