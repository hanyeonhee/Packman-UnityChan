using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f;

    CharacterController charCtrl;
    Animator anim;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (dir.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward, dir, rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, dir));
            transform.LookAt(transform.position + forward);
        }

        charCtrl.Move(dir * moveSpeed * Time.deltaTime);
        anim.SetFloat("Speed", charCtrl.velocity.magnitude);

        if (GameObject.FindGameObjectsWithTag("Dot").Length < 1)
        {
            SceneManager.LoadScene("Win");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Dot":
                Destroy(other.gameObject);
                break;
            case "Enemy":
                SceneManager.LoadScene("Lose");
                break;
        }
    }
}
