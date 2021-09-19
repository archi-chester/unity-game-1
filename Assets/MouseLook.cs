using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Управление мышью
public class MouseLook : MonoBehaviour
{

    public enum RotationAxes {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXandY;

    //  скорость вращения
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    //  пределы вращения по вертикали
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;
    
    void Start() {
        //  отрубаем воздействие на компонент мышки
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) //    Проверяем, существует ли этот компонент.
            body.freezeRotation = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX) {
            //  horisontal
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

        } else if (axes == RotationAxes.MouseY) {
            //  vertical
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float rotationY = this.transform.localEulerAngles.y;

            this.transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        } else {
            //  combination
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;
            
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        
    } 
}
