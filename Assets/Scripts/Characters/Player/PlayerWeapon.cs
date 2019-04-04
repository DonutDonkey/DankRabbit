using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint = null;

    [SerializeField]
    private GameObject bullet = null;

    private List<GameObject> bulletList;

    private int pullAmount = 5;

    public AudioClip sound;

    private void Start()
    {
        bulletList = new List<GameObject>();

        for(int i = 0; i < pullAmount; i++)
        {
            GameObject obj = Instantiate(bullet, firePoint.position, firePoint.rotation) as GameObject;
            obj.SetActive(false);
            bulletList.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(CfgVariables.GetFireKey()))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Debug.Log("Fire");

        AudioSource.PlayClipAtPoint(sound, transform.position);

        for (int i = 0; i < bulletList.Count; i++)
        {
            if(!bulletList[i].activeInHierarchy)
            {
                bulletList[i].transform.position = firePoint.position;
                bulletList[i].transform.rotation = firePoint.rotation;
                bulletList[i].SetActive(true);
                break;
            }
        }
    }

}
