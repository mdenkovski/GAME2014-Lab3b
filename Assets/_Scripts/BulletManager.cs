using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BulletManager : MonoBehaviour
{
    public GameObject bullet;
    public int maxBullets;
    private Queue<GameObject> m_bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }


    private void _BuildBulletPool()
    {
        //create empty queue structure
        m_bulletPool = new Queue<GameObject>();
        for (int count = 0; count < maxBullets; count++)
        {
            var tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            tempBullet.transform.parent = transform;
            m_bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject GetBullet(Vector3 position)
    {
        var newBullet = m_bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        newBullet.transform.parent = transform;
        return newBullet;
    }

    public void ReturnBullet(GameObject returnedBullet)
    {
        returnedBullet.SetActive(false);
        m_bulletPool.Enqueue(returnedBullet);
    }


}
