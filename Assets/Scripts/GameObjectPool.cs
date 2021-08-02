using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    //ENCAPSULATION
    public static GameObjectPool Instance { get; private set; }

    [SerializeField] private GameObject objectPrefab;

    private Queue<GameObject> gameObjectAvailable = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        AddObjects(30);
    }

    public GameObject Get()
    {
        if (gameObjectAvailable.Count == 0)
        {
            AddObjects(1);
        }

        return gameObjectAvailable.Dequeue();
    }


    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var _newObject = GameObject.Instantiate(objectPrefab);
            _newObject.gameObject.SetActive(false);
            gameObjectAvailable.Enqueue(_newObject);
            #region
            _newObject.GetComponent<IGameObjectPooled>().Pool = this;
            #endregion
        }
        //var _bullet = Instantiate(objectPrefab);
        //return _bullet;

    }

    public void ReturnObjectsToPool(GameObject _object)
    {
        gameObjectAvailable.Enqueue(_object);
    }
}
