using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Test : MonoBehaviour
{
    public GameObject test;
    public Image testImg;
    public Vector3 angle;

    private bool flag = true;
    private int flag2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        angle = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            test.transform.Rotate(Vector3.forward, 3000 * Time.deltaTime);
            flag2 = (flag2 + 1) % 5;
            switch (flag2)
            {
                case 0:
                    testImg.sprite = Resources.Load<Sprite>(Resource.FieldElementSoil) as Sprite;
                    break;
                case 1:
                    testImg.sprite = Resources.Load<Sprite>(Resource.FieldElementWater) as Sprite;
                    break;
                case 2:
                    testImg.sprite = Resources.Load<Sprite>(Resource.FieldElementFire) as Sprite;
                    break;
                case 3:
                    testImg.sprite = Resources.Load<Sprite>(Resource.FieldElementSteal) as Sprite;
                    break;
                case 4:
                    testImg.sprite = Resources.Load<Sprite>(Resource.FieldElementWood) as Sprite;
                    break;

            }
        }


    }

    public void OnClickButton()
    {
        flag = !flag;
        Debug.Log(test.transform.rotation.eulerAngles);
    }
}
