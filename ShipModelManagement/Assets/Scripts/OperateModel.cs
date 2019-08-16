using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateModel : MonoBehaviour
{
    Vector3 scale;
    float offset = 0.2f;
    float maxSize = 2.0f;
    float minSize = 1.0f;
    public float speed = 200f;
    Vector3 bAngel;
    // Use this for initialization
    void Start()
    {
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
            if (scale.x <= maxSize)
            {
                scale.x += offset;
                scale.y += offset;
                scale.z += offset;
                this.transform.localScale = scale;
            }

        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (scale.x > minSize)
            {
                scale.x -= offset;
                scale.y -= offset;
                scale.z -= offset;
                this.transform.localScale = scale;
            }
        }
        //鼠标左键旋转物体
        if (Input.GetMouseButton(0))
        {
            float fMouseX = Input.GetAxis("Mouse X");
            float fMouseY = Input.GetAxis("Mouse Y");
            this.transform.Rotate(Vector3.up * Time.deltaTime * speed * fMouseX);
            this.transform.Rotate(Vector3.right * Time.deltaTime * speed * fMouseY);
        }
        if(Input.GetMouseButtonDown(1))
        {
            transform.localEulerAngles = bAngel;
            transform.localScale = new Vector3(1,1,1);
         //   this.transform.Rotate(Vector3.up , Time.deltaTime * speed,Space.World);
        }
    }
}
