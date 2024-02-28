using UnityEngine;

public class LineCollision : MonoBehaviour
{
    public LineRenderer laser1;

    void Update()
    {
        Vector3 startPoint = laser1.GetPosition(0);
        Vector3 endPoint = laser1.GetPosition(1);

        RaycastHit hit;
        if (Physics.Raycast(startPoint, endPoint - startPoint, out hit, Vector3.Distance(startPoint, endPoint)))
        {
            
            laser1.GetComponent<KillPlayer>().killPlayer(hit);
        }
    }
}
