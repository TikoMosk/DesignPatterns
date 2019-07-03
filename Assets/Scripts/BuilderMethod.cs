using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderMethod : MonoBehaviour
{
    private static BuilderMethod _instance;
    public static BuilderMethod Instance
    {
        get { return _instance; }
    }
    [System.Serializable]
    public struct Parts
    {
        public string name;
        public GameObject partPrefab;
    }
    public List<Parts> bodyParts;

    Director director = new Director();
    CharacterBuilder humanBuilder = new HumanoidBuilder();
    CharacterBuilder womanBuilder = new WomanoidBuilder();
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        director.ConstructCharacter(humanBuilder);
        director.ConstructCharacter(womanBuilder);
        Character humanoid = humanBuilder.GetCharacter();
        Character womanoid = womanBuilder.GetCharacter();

        Debug.Log("Build");
        BuildCharacterGraphics(womanoid);
    }

    public GameObject GetGameobjectOfPart(string partname)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if(partname == bodyParts[i].name)
            {
                return bodyParts[i].partPrefab;
            }
        }
        Debug.Log("No corresponding part in the inspector");
        return null; 
    }
    public void BuildCharacterGraphics(Character c)
    {
        for (int i = 0; i < c.CharObjects.Count; i++)
        {
            Instantiate(c.CharObjects[i], c.CharObjects[i].transform.position, Quaternion.identity);
        }
    }
}
class Director
{
    public void ConstructCharacter(CharacterBuilder builder)
    {
        builder.BuildArms();
        builder.BuildBody();
        builder.BuildHead();
        builder.BuildLegs();
        builder.BuildHair();
        builder.BuildBreasts();
    }
}
abstract class CharacterBuilder
{

    protected List<GameObject> bodyParts;
    public abstract void BuildHead();
    public abstract void BuildBody();
    public abstract void BuildLegs();
    public abstract void BuildArms();
    public abstract void BuildHair();
    public abstract void BuildBreasts();

    public abstract Character GetCharacter();
}
class HumanoidBuilder : CharacterBuilder
{
    Character humanoid = new Character();
    public override void BuildArms()
    {
        humanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Arm1"));
        humanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Arm2"));
    }

    public override void BuildBody()
    {
        humanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Body"));
    }

    public override void BuildBreasts()
    {

    }

    public override void BuildHair()
    {

    }

    public override void BuildHead()
    {
        humanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Head"));
    }

    public override void BuildLegs()
    {
        humanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Leg1"));
        humanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Leg2"));
    }
    public override Character GetCharacter()
    {
        return humanoid;
    }
}
class WomanoidBuilder : CharacterBuilder
{
    Character womanoid = new Character();
    public override void BuildArms()
    {
        womanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Arm1"));
        womanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Arm2"));
    }

    public override void BuildBody()
    {
        womanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Body"));
    }

    public override void BuildBreasts()
    {
        womanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Breasts"));
    }

    public override void BuildHair()
    {
        womanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Hair"));
    }

    public override void BuildHead()
    {
        womanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Head"));
    }

    public override void BuildLegs()
    {
        womanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Leg1"));
        womanoid.Add(BuilderMethod.Instance.GetGameobjectOfPart("Leg2"));
    }
    public override Character GetCharacter()
    {
        return womanoid;
    }
}
public class Character
{
    private List<GameObject> charObjects = new List<GameObject>();
    public List<GameObject> CharObjects
    {
        get { return charObjects; }
    }
    public void Add(GameObject part)
    {
        charObjects.Add(part);
    }
}
