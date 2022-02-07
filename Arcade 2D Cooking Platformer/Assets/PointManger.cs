using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManger : MonoBehaviour
{
    public static PointManger instance; //A static reference of the class

    [SerializeField] private int _tolatPoints = 0;

    [SerializeField] private PointData pointData;

    public int GetTolatPoints() { return _tolatPoints; }
    public void SetTolatPoints(int tolatPoints) { this._tolatPoints = tolatPoints; }
    public PointData GetPointData() { return pointData; }

    private void Awake()
    {
        //---Make sure there is only one instance of this class for each Scene.

        //If there is no instance of the object
        if (instance == null)
        {
            //Set an instance of it
            instance = this;
        }

        //Else, if there's already an instance
        else
        {
            //Destroy it
            Destroy(gameObject);
            return;
        }


        //This won't get destroy when you switch scene
        DontDestroyOnLoad(gameObject);
    }
}