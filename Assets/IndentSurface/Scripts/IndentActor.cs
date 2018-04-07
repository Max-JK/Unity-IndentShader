using UnityEngine;
namespace Wacki.IndentSurface {
    /// <summary>
    /// Simple control script for our sphere that leaves a track in the snow.
    /// </summary>
    public class IndentActor : MonoBehaviour {
        [Range(0.0f, 0.2f)]
        [SerializeField] private float drawDelta = 0.01f;
        private Vector3 _prevDrawPos;

        private void FixedUpdate() {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            GetComponent<Rigidbody>().AddTorque(v, 0, -h);
        }
        
        private void OnCollisionStay(Collision collider) {
            if (Vector3.Distance(_prevDrawPos, transform.position) < drawDelta) return;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit) && collider.collider == hit.collider) {
                var texDraw = collider.gameObject.GetComponent<IndentDraw>();

                if (texDraw == null) return;
                Debug.DrawLine(transform.position, transform.position + Vector3.down);

                texDraw.IndentAt(hit);
            }
            _prevDrawPos = transform.position;
        }

    }

}
