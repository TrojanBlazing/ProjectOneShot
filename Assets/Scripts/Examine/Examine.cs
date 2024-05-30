using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour
{
    public GameObject offset;
    private Transform examineGb;
    private Vector3 lastMousePosition;
    private Canvas can;
    private bool isExamining = false;
    private GameObject targetOb;

    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    void Start()
    {
        can = GetComponent<Canvas>();
        can.enabled = false;
        targetOb = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Object"))
                {
                    ToggleExam();

                    if (isExamining)
                    {
                        examineGb = hit.transform;
                        if (!originalPositions.ContainsKey(examineGb))
                            originalPositions[examineGb] = examineGb.position;
                        if (!originalRotations.ContainsKey(examineGb))
                            originalRotations[examineGb] = examineGb.rotation;
                    }
                }
            }
        }

        if (CheckUserClose())
        {
            if (isExamining)
            {
                can.enabled = false;
                Exam();
                StartExamine();
            }
            else
            {
                can.enabled = true;
                NonExamine();
                StopExamine();
            }
        }
        else
        {
            can.enabled = false;
        }
    }

    public void ToggleExam()
    {
        isExamining = !isExamining;
    }

    private void StartExamine()
    {
        lastMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    private void StopExamine()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void Exam()
    {
        if (examineGb != null)
        {
            examineGb.position = Vector3.Lerp(examineGb.position, offset.transform.position, 0.2f);
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationSpeed = 1.0f;
            examineGb.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examineGb.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    private void NonExamine()
    {
        if (examineGb != null)
        {
            if (originalPositions.ContainsKey(examineGb))
                examineGb.position = Vector3.Lerp(examineGb.position, originalPositions[examineGb], 0.2f);

            if (originalRotations.ContainsKey(examineGb))
                examineGb.rotation = Quaternion.Slerp(examineGb.rotation, originalRotations[examineGb], 0.2f);
        }
    }

    private bool CheckUserClose()
    {
        float distance = Vector3.Distance(targetOb.transform.position, transform.position);
        return (distance < 2);
    }
}
