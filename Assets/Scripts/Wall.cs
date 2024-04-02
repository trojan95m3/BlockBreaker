using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum Side
    {  Left, Right, Top, Bottom }

    public Side _Side;

    void Start()
    {
        float aspect = (float)Screen.width / Screen.height;
        float height = Camera.main.orthographicSize * 2;
        float width = height * aspect;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        switch (_Side)
        {
            case Side.Top:
                transform.position = new Vector2(0, height * 0.5f + sprite.size.y * 0.5f);
                transform.localScale = new Vector3(width, 1, 1);
                break;

            case Side.Left:
                transform.position = new Vector2(-width * 0.5f - sprite.size.x * 0.5f, 0);
                transform.localScale = new Vector3(1, height, 1);
                break;

            case Side.Bottom:
                transform.position = new Vector2(0, -height * 0.5f - sprite.size.y);
                transform.localScale = new Vector3(width, 1, 1);
                break;

            case Side.Right:
                transform.position = new Vector2(width * 0.5f + sprite.size.x * 0.5f, 0);
                transform.localScale = new Vector3(1, height, 1);
                break;
        }
    }
}
