using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeMethod : MonoBehaviour
{
    public int paperSizeX;
    public int paperSizeY;
    public GameObject paperSquareGameObject;
    List<GameObject> paperSquares;
    List<Shade> baseShades;
    Shade toolShade;
    private void Start()
    {
        paperSquares = new List<GameObject>();
        for (int x = 0; x < paperSizeX; x++)
        {
            for (int y = 0; y < paperSizeY; y++)
            {
                Vector3 spawnPos = new Vector3(-paperSizeX / 2 + x, -paperSizeY /2 + y , 1);
                GameObject square = Instantiate(paperSquareGameObject, spawnPos, Quaternion.identity) as GameObject;

            }
        }
        ShadePalette palette = new ShadePalette();
        List<ShadePrototype> shades = new List<ShadePrototype>();
        shades.Add(palette["black"] = new Shade(0, 0, 0));
        shades.Add(palette["white"] = new Shade(255, 255, 255));
        shades.Add(palette["grey"] = new Shade(128, 128, 128));
        shades.Add(palette["red"] = new Shade(255, 0, 0));
        shades.Add(palette["green"] = new Shade(0, 255, 0));
        shades.Add(palette["blue"] = new Shade(0, 0, 255));
        shades.Add(palette["orange"] = new Shade(255, 128, 0));
        shades.Add(palette["yellow"] = new Shade(255, 255, 0));
        shades.Add(palette["purple"] = new Shade(128, 0, 255));
        shades.Add(palette["magenta"] = new Shade(255, 0, 128));
        shades.Add(palette["pink"] = new Shade(255, 0, 255));
        shades.Add(palette["lightblue"] = new Shade(0, 128, 255));
        shades.Add(palette["skyblue"] = new Shade(0, 255, 255));
        shades.Add(palette["greenblue"] = new Shade(0, 255, 128));
        shades.Add(palette["greenyellow"] = new Shade(128, 255, 0));
        shades.Add(palette["brown"] = new Shade(100, 50, 0));

        int count = 0;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                Vector3 spawnPos = new Vector3( -paperSizeX /2 + x, paperSizeY/2 + 0.5f  + y, 1);
                GameObject square = Instantiate(paperSquareGameObject, spawnPos, Quaternion.identity) as GameObject;
                square.tag = "Palette";
                if (count < shades.Count)
                {
                    Shade shade = shades[count] as Shade;
                    square.GetComponent<MeshRenderer>().material.color = new Color32(shade.Red, shade.Green, shade.Blue,1);
                    square.GetComponent<ShadeInfo>().shade = shade;
                    count++;
                }
                
            }
            
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButton(0))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.tag == "Palette")
            {
                toolShade = hit.transform.gameObject.GetComponent<ShadeInfo>().shade.Clone() as Shade;
            }
            else if(!Input.GetKey(KeyCode.LeftAlt))
            {
                hitObject.GetComponent<MeshRenderer>().material.color = new Color32(toolShade.Red, toolShade.Green, toolShade.Blue, 1);
                hitObject.GetComponent<ShadeInfo>().shade = toolShade;
            }
            else
            {
                toolShade = hitObject.GetComponent<ShadeInfo>().shade;
            }

        }
    }
}
public abstract class ShadePrototype
{
    public abstract ShadePrototype Clone();
}
public class Shade : ShadePrototype
{
    private byte red;
    private byte green;
    private byte blue;
    public byte Red
    {
        get { return red; }
    }
    public byte Green
    {
        get { return green; }
    }
    public byte Blue
    {
        get { return blue; }
    }

    public Shade(byte _red, byte _green, byte _blue)
    {
        red = _red;
        green = _green;
        blue = _blue;
    }
    public override ShadePrototype Clone()
    {
        return MemberwiseClone() as ShadePrototype;
    }

}
class ShadePalette
{
    private SortedList<string, ShadePrototype> shades = new SortedList<string, ShadePrototype>();

    public ShadePrototype this[string key]
    {
        get { return Shades[key]; }
        set { Shades.Add(key, value); }
    }

    public SortedList<string, ShadePrototype> Shades { get => shades; set => shades = value; }
}