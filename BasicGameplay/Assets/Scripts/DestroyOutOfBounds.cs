using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float topBound = 30f;
    public float bottomBound = -10f;

    void Update()
    {
        CheckBounds(topBound, bottomBound);
    }

    void CheckBounds(float top, float bottom)
    {
        if (transform.position.z > top || transform.position.z < bottom)
        {
            Destroy(gameObject);
        }
    }
}

