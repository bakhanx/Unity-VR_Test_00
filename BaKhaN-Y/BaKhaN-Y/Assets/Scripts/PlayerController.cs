using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Text moveText;
    [SerializeField] private Text itemText;
    [SerializeField] private float range;
    [SerializeField] private GameObject player;

    private Rigidbody myRigid;
    private RaycastHit hitInfo;
    private float distance;
    private Transform target;
    private bool isMove;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePointCheck();
        TryMove();
    }

    private void MovePointCheck()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range))
        {
            if (hitInfo.transform.tag == "MovePoint")
            {
                MoveTextOn();
                target = hitInfo.transform;
            }
            else
            {
                MoveTextOff();
            }
        }
    }

    private void MoveTextOn()
    {
        moveText.gameObject.SetActive(true);
        moveText.text = "Press it, and you move there";
        if (Input.GetMouseButtonDown(0))
        {
            isMove = true;
        }

    }

    private void MoveTextOff()
    {
        moveText.gameObject.SetActive(false);
    }

    private void TryMove()
    {
        if (isMove)
        {
            targetPos = new Vector3(target.position.x, player.transform.position.y, target.position.z);
            player.transform.position = Vector3.Lerp(player.transform.position, targetPos, 0.01f);
            if (Vector3.Distance(player.transform.position, targetPos) < 0.1)
            {
                isMove = false;
            }


        }
    }

    private void ItemTextOn()
    {
        itemText.gameObject.SetActive(true);
        itemText.text = "Drink this liquid";
    }

    private void ItemTextOff()
    {
        itemText.gameObject.SetActive(false);
    }
}
