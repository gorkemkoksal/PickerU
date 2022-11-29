using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Collectables : MonoBehaviour
{
    [SerializeField] Material _transformMaterial;
    [SerializeField] Mesh _transformMesh;
    [SerializeField] Mesh _defaultMesh;
    [SerializeField] Material _defaulfMaterial;

    private Rigidbody rb;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private float _transformDelay = 0.5f;
    private bool _isGrounded;
    private void OnEnable()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnDisable()
    {
        _meshRenderer.material = _defaulfMaterial;
        _meshFilter.mesh = _defaultMesh;
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    private void Update()
    {
        UpperBorderForCollectables();
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    private void UpperBorderForCollectables()
    {
        if (!_isGrounded) return;
        if (gameObject.transform.position.y < 1) return;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
    }
    private void TransformingObjects()
    {
        _meshFilter.mesh = _transformMesh;
        _meshRenderer.material = _transformMaterial;
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, .3f);
        rb.AddForce(Vector3.up * Random.Range(50f, 100f), ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            _isGrounded = true;
        }
        if (collision.gameObject.tag != "Collector") { return; }
        Invoke("TransformingObjects", _transformDelay);
    }
}
