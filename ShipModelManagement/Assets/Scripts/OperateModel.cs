using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateModel : MonoBehaviour
{
    Vector3 scale;
    float offset = 0.2f;
    float maxSize = 5.0f;
    float minSize = 1.0f;
    public float speed = 15f;
    Vector3 bAngel;
    // Use this for initialization
    void Start()
    {
        this.transform.localScale *= 2;
        scale = this.transform.localScale;
        bAngel = this.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //鼠标滚轮的效果
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
           
            if (scale.x > minSize)
            {
                scale.x -= offset;
                scale.y -= offset;
                scale.z -= offset;
                this.transform.localScale = scale;
            }
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (scale.x <= maxSize)
            {
                scale.x += offset;
                scale.y += offset;
                scale.z += offset;
                this.transform.localScale = scale;
            }
        }
        //鼠标左键旋转物体
        if (Input.GetMouseButton(0))
        {
            float fMouseX = Input.GetAxis("Mouse X");
            float fMouseY = Input.GetAxis("Mouse Y");
            this.transform.Rotate(new Vector3(fMouseY,-fMouseX,0) * speed,Space.World);
            //this.transform.Rotate(Vector3.right * Time.deltaTime * speed * fMouseX);
        }
        if(Input.GetMouseButtonDown(1))
        {
            transform.localEulerAngles = bAngel;
            transform.localScale = new Vector3(2,2,2);
         //   this.transform.Rotate(Vector3.up , Time.deltaTime * speed,Space.World);
        }
    }
}
