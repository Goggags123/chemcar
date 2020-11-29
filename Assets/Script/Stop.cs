using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    public Transform Animal;
    public Animator Animal_Animation;
    private bool isCollied = false;
    public Transform StartNode;
    public float speed;
    //private bool enter = false;
    public Transform EndNode;

    // Start is called before the first frame update
    void Start()
    {
        Animal_Animation = Animal.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        isCollied = true;
        collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Animal_Animation.SetTrigger("Walk");
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.GetComponent<Rigidbody>().isKinematic+" " +isCollied);
        if (collision.gameObject.tag == "Player")
        {            
            if (Vector3.Distance(Animal.position, EndNode.position) < 1f)
            {
                Animal_Animation.SetTrigger("Idle");
                Vector3 relativeVector = transform.InverseTransformPoint(EndNode.position);
                transform.Rotate(new Vector3(0, 180, 0));
                Transform temp = EndNode;
                EndNode = StartNode;
                StartNode = EndNode;
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                isCollied = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if(isCollied)
        Animal.position = Vector3.MoveTowards(Animal.position,EndNode.position,speed*Time.deltaTime);
    }
}
