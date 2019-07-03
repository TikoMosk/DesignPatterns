using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMethod : MonoBehaviour
{
    Environment[] environments = new Environment[2];
    public GameObject treePrefab;
    public GameObject rockPrefab;

    void Start()
    {
        environments[0] = new ForestEnvironment();
        environments[1] = new RockyEnvironment();

        foreach(Environment env in environments)
        {
            GameObject environmentGO = new GameObject(env.ToString());
            foreach(EnvObject envObj in env.EnvironmentObjects)
            {
                GameObject envGameObject = treePrefab;
                if(envObj is EnvTree)
                {
                    envGameObject = Instantiate(treePrefab, envObj.Position, Quaternion.identity);
                }
                else if(envObj is EnvRock)
                {
                    envGameObject = Instantiate(rockPrefab, envObj.Position, Quaternion.identity);
                }
                envGameObject.transform.parent = environmentGO.transform;
                Debug.Log("Spawn " + envObj.ToString() + " at the coordinates" + envObj.Position + " In the " + env.ToString() + " environment");
            }
        }
    }
    
}
abstract class Environment
{
    private List<EnvObject> _environmentObjects = new List<EnvObject>();
    private Vector3 position;
    public Vector3 Position
    {
        get { return position; }
    }
    public Environment()
    {
        this.CreateEnvironment();
    }
    public List<EnvObject> EnvironmentObjects
    {
        get { return _environmentObjects; }
    }
    public abstract void CreateEnvironment();
}
class ForestEnvironment : Environment
{
    public override void CreateEnvironment()
    {
        EnvironmentObjects.Add(new EnvTree(new Vector3(0,0,0)));
    }
}
class RockyEnvironment : Environment
{
    public override void CreateEnvironment()
    {
        EnvironmentObjects.Add(new EnvRock(new Vector3(0, 0, 10)));
        EnvironmentObjects.Add(new EnvRock(new Vector3(0, 0, 8)));
        EnvironmentObjects.Add(new EnvRock(new Vector3(2, 0, 7)));
        EnvironmentObjects.Add(new EnvRock(new Vector3(0, 0, 5)));
    }
}
abstract class EnvObject
{
    protected Vector3 position = Vector3.zero;
    public Vector3 Position
    {
        get { return position; }
    }
}
class EnvTree : EnvObject
{
    public EnvTree(Vector3 _position)
    {
        
        position = _position;
    }
}
class EnvRock : EnvObject
{
    public EnvRock(Vector3 _position)
    {
        
        position = _position;
    }
}
