using UnityEngine;

/// <summary>
/// Es una clase generica de una skin, es lo que se va a mostrar en la tienda (precio y foto)
/// </summary>
[System.Serializable]
public class Skin
{
    // Imagen de la skin para la tienda
    public Sprite skinImage;
    // Material de la skin
    public Material skinMaterial;
    // Costo de la skin en la tienda
    public int price;
}