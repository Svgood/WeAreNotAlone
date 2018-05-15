using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    public static Vector2Int mousePosInt() => new Vector2Int((int)mouseCoords().x, (int)mouseCoords().y);

    public static Vector2 mousePos() => new Vector2(mouseCoords().x, mouseCoords().y);

    public static Vector3 mouseCoords() => Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
}
