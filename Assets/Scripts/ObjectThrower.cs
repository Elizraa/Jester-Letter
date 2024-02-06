using UnityEngine;
using System.Collections;

public class ObjectThrower : MonoBehaviour
{
    public Transform throwPoint;
    public Transform targetPoint;
    public float throwDuration = 1.5f;

    private bool isThrown = false;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && !isThrown)
    //    {
    //        StartCoroutine(ThrowTomato());
    //    }
    //}

    private void Start()
    {
        StartCoroutine(ThrowTomato());
    }

    private IEnumerator ThrowTomato()
    {
        isThrown = true;

        float elapsedTime = 0f;

        Vector3 startPos = throwPoint.position;
        Vector3 endPos = targetPoint.position;
        Vector3 controlPoint = CalculateControlPoint(startPos, endPos);

        while (elapsedTime < throwDuration)
        {
            float t = elapsedTime / throwDuration;
            Vector3 newPosition = CalculateBezierPoint(t, startPos, controlPoint, endPos);

            transform.position = newPosition;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the tomato reaches the final position precisely
        transform.position = endPos;
        Destroy(gameObject);
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * targetPoint.position;

        return p;
    }

    private Vector3 CalculateControlPoint(Vector3 start, Vector3 end)
    {
        // Adjust the Y value or add more complexity based on your specific requirements
        float xOffset = Random.Range(-3, 3);

        Vector3 midPoint = (start + end) / 2f;
        midPoint.x += xOffset;

        return midPoint;
    }
}
