using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractFactoryMethod : MonoBehaviour
{
    private static AbstractFactoryMethod _instance;
    public static AbstractFactoryMethod Instance
    {
        get { return _instance; }
    }
    
    [System.Serializable]
    public struct ColoredTree
    {
        public Tree.TreeColor color;
        public List<GameObject> treePrefabs;
    }
    public List<ColoredTree> coloredTreeList;

    public GameObject GetAppropriatePrefab(Tree tree)
    {
        foreach (ColoredTree s in coloredTreeList)
        {
            if(s.color == tree.Color)
            {
                return s.treePrefabs[tree.TreeType];
            }
        }
        Debug.Log("No match");
        return null;
    }
    void Awake()
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
    void Start()
    {
        TreeFactory[] coloredTreeFactories = new TreeFactory[3];
        coloredTreeFactories[0] = new GreenTreeFactory();
        coloredTreeFactories[1] = new BlueTreeFactory();
        coloredTreeFactories[2] = new OrangeTreeFactory();

        foreach(TreeFactory colorFactory in coloredTreeFactories)
        {
            foreach(Tree t in colorFactory.TreeList)
            {
                Instantiate(t.TreePrefab, t.Position, Quaternion.identity);
            }
        }
    }
}
abstract class TreeFactory
{
    private List<Tree> treeList = new List<Tree>();
    public List<Tree> TreeList
    {
        get { return treeList; }
    }
    public TreeFactory()
    {
        this.CreatePineTree();
        this.CreateTallpineTree();
        this.CreateTallTree();
    }
    public abstract void CreatePineTree();
    public abstract void CreateTallpineTree();
    public abstract void CreateTallTree();
}
class GreenTreeFactory : TreeFactory
{
    public override void CreatePineTree()
    {
        TreeList.Add(new PineTree(Tree.TreeColor.Green, new Vector3(0, 0, 0)));
    }
    public override void CreateTallpineTree()
    {
        TreeList.Add(new TallpineTree(Tree.TreeColor.Green, new Vector3(4, 0, 0)));
    }
    public override void CreateTallTree()
    {
        TreeList.Add(new TallTree(Tree.TreeColor.Green, new Vector3(8, 0, 0)));
    }
}
class BlueTreeFactory : TreeFactory
{
    public override void CreatePineTree()
    {
        TreeList.Add(new PineTree(Tree.TreeColor.Blue, new Vector3(0, 0, 4)));
    }
    public override void CreateTallpineTree()
    {
        TreeList.Add(new TallpineTree(Tree.TreeColor.Blue, new Vector3(4, 0, 4)));
    }
    public override void CreateTallTree()
    {
        TreeList.Add(new TallTree(Tree.TreeColor.Blue, new Vector3(8, 0, 4)));
    }
}

class OrangeTreeFactory : TreeFactory
{
    public override void CreatePineTree()
    {
        TreeList.Add(new PineTree(Tree.TreeColor.Orange,new Vector3(0,0,8)));
    }
    public override void CreateTallpineTree()
    {
        TreeList.Add(new TallpineTree(Tree.TreeColor.Orange, new Vector3(4, 0, 8)));
    }
    public override void CreateTallTree()
    {
        TreeList.Add(new TallTree(Tree.TreeColor.Orange, new Vector3(8, 0,8)));
    }
}
public abstract class Tree
{
    private Vector3 position;
    public Vector3 Position
    {
        get { return position; }
    }
    public enum TreeColor { Green, Blue, Orange };
    private TreeColor color;
    public TreeColor Color
    {
        get { return color; }
    }
    internal int treeType;
    public int TreeType
    {
        get { return treeType; }
    }
    private GameObject treePrefab;
    public GameObject TreePrefab
    {
        get { return treePrefab; }
    }
    public Tree(TreeColor _treeColor, Vector3 _position, int _treeType)
    {
        color = _treeColor;
        position = _position;
        treeType = _treeType;
        MatchColorWithPrefab();
        
    }
    protected void MatchColorWithPrefab()
    {
        treePrefab = AbstractFactoryMethod.Instance.GetAppropriatePrefab(this);
    }
}
class PineTree : Tree
{
    public PineTree(TreeColor _color, Vector3 _position) : base(_color, _position, 0)
    {

    }
}
class TallpineTree : Tree
{
    public TallpineTree(TreeColor _color, Vector3 _position) : base(_color,_position, 1)
    {

    }
}
class TallTree : Tree
{
    public TallTree(TreeColor _color, Vector3 _position) : base(_color, _position, 2)
    {

    }
}

