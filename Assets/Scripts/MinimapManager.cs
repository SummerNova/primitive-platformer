using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class MinimapManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RawImage Target;

    static MinimapObject Player;
    static List<MinimapObject> EnvironmentObjects = new();
    static List<MinimapObject> Hazards = new();
    static List<MinimapObject> Collectibles = new();
    private static int _id = 0;
    private static int ID 
    { 
        get 
        {
            _id++;
            return _id;
        } 
    }

    private int MidX = 0;
    private int MidY = 0;

    [Header("Texture")]
    [SerializeField] Texture2D _BaseTexture;
    [SerializeField] Texture2D _TargetTexture;
    [SerializeField] float Scale = 1;
    [SerializeField] Color _EnvironmentColor = Color.white;
    [SerializeField] Color _PlayerColor = Color.green;
    [SerializeField] Color _HazardColor = Color.red;
    [SerializeField] Color _CollectibleColor = Color.yellow;
    [SerializeField] Color _BackgroundColor = Color.black;

    public static int LoadObject(Transform Object, RenderType type)
    {
        MinimapObject temp = new(ID, Object);
        switch (type) {
            case RenderType.Player:
                Player = temp;
                break;
            case RenderType.Environment:
                EnvironmentObjects.Add(temp);
                break;
            case RenderType.Collectible:
                Collectibles.Add(temp);
                break;
            case RenderType.Hazard:
                Hazards.Add(temp);
                break;
        }
       
        return temp.ID;
    }

    public static void UnloadObject(int id, RenderType type)
    {
        switch (type)
        {
            case RenderType.Player: throw new UnassignedReferenceException("Tried Unloading player"); break;
            case RenderType.Environment: 
                for (int i = 0; i < EnvironmentObjects.Count; i++)
                {
                    if (EnvironmentObjects[i].ID == id) EnvironmentObjects.Remove(EnvironmentObjects[i]);
                }
                break;
            case RenderType.Collectible:
                for (int i = 0; i< Collectibles.Count; i++)
                {
                    if (Collectibles[i].ID == id) Collectibles.Remove(Collectibles[i]);
                }
                break;
            case RenderType.Hazard:
                for (int i = 0;i< Hazards.Count; i++)
                {
                    if (Hazards[i].ID == id) Hazards.Remove(Hazards[i]);
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetFrame();

        foreach (MinimapObject hazard in  Hazards)
        {
            AddObjectToMap(hazard, RenderType.Hazard);
        }

        foreach (MinimapObject Collectible in Collectibles)
        {
            AddObjectToMap(Collectible, RenderType.Collectible);
        }

        AddObjectToMap(Player,RenderType.Player);

        foreach (MinimapObject Piece in EnvironmentObjects)
        {
            AddObjectToMap(Piece, RenderType.Environment);
        }

        _TargetTexture.Apply();

        Target.texture = _TargetTexture;
    }

    private void AddObjectToMap(MinimapObject obj, RenderType type)
    {
        float PosX = MidX + Scale * (Player.Ref.position.x - obj.Ref.position.x);
        float PosY = MidY + Scale * (Player.Ref.position.y - obj.Ref.position.y);
        float SizeX = Scale * obj.Ref.lossyScale.x / 2;
        float SizeY = Scale * obj.Ref.lossyScale.y / 2;
        Color TargetColor;
        switch (type) 
        {
            case RenderType.Player: TargetColor = _PlayerColor; break;  
            case RenderType.Hazard: TargetColor = _HazardColor; break;
            case RenderType.Collectible: TargetColor = _CollectibleColor; break;
            case RenderType.Environment: TargetColor = _EnvironmentColor; break;
            default: TargetColor = _BackgroundColor; break;
        }


        for (int x = (int)Mathf.Max(0,PosX - SizeX); x < Mathf.Min(_BaseTexture.width, PosX + SizeX); x++)
        {
            for (int y = (int)Mathf.Max(0, PosY - SizeY); y < Mathf.Min(_BaseTexture.width, PosY + SizeY); y++)
            {
                _TargetTexture.SetPixel(x, y, new(TargetColor.r, TargetColor.g, TargetColor.b, _BaseTexture.GetPixel(x, y).a));

                
            }
        }

    }

    private void ResetFrame()
    {
        _TargetTexture = new Texture2D(_BaseTexture.width, _BaseTexture.height);
        MidX = _BaseTexture.width/2;
        MidY = _BaseTexture.height/2;
        for (int x = 0; x < _BaseTexture.width; x++)
        {
            for (int y = 0; y < _BaseTexture.height; y++)
            {
                _TargetTexture.SetPixel(x, y, new(_BackgroundColor.r, _BackgroundColor.g, _BackgroundColor.b, _BaseTexture.GetPixel(x, y).a));
            }
        }
    }
}

public struct MinimapObject
{
    public int ID { get; private set; }
    public Transform Ref { get; private set; }

    public MinimapObject(int id, Transform _Ref)
    {
        ID = id;
        Ref = _Ref;
    }
}

public enum RenderType { Player, Environment, Hazard, Collectible}
