using System.Collections;
using UnityEngine;

public class CamMovementHandler : MonoBehaviour
{
    public LayerMask teleportLayerMask;
    public float moveSpeed = 3f;
    public float movementDuration = 1f; 

    private bool isTeleporting = false;

    private Vector3 targetPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTeleporting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
            {
                if (hit.collider.CompareTag("TeleportationArea"))
                {
                    //Debug.Log("Teleporting to " + hit.point);
                    targetPosition = new Vector3(hit.point.x, hit.point.y + 2f, hit.point.z);
                    //targetPosition = hit.point;
                    StartCoroutine(TeleportPlayer(targetPosition));
                }
            }
        }
    }

    IEnumerator TeleportPlayer(Vector3 targetPosition)
    {
        GameObject player = GameObject.Find("Main Camera");

        if (player != null)
        {
            isTeleporting = true;

            Vector3 startPosition = player.transform.position;
            float startTime = Time.time;

            while (Time.time - startTime < movementDuration)
            {
                float journeyLength = Vector3.Distance(startPosition, targetPosition);
                float distanceCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distanceCovered / journeyLength;
                var newPos = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
                player.transform.position = new Vector3(newPos.x, newPos.y, newPos.z);

                yield return null;
            }

           
            player.transform.position = new Vector3(targetPosition.x, targetPosition.y , targetPosition.z);
            

            isTeleporting = false;
        }
    }
}
