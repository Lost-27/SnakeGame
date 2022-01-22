using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    #region Variables

    public Transform SegmentPrefab;

    private Vector2 _direction;
    public List<Transform> _segments;

    #endregion


    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        var position = transform.position;
        position = new Vector3(Mathf.Round(position.x + _direction.x), Mathf.Round(position.y + _direction.y), 0.0f);
        transform.position = position;
    }

    private void Grow()
    {
        Transform segment = Instantiate(SegmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        }
    }
}