/*         INFINITY CODE         */
/*   https://infinity-code.com   */

using UnityEngine;

namespace InfinityCode.OnlineMapsExamples
{
    /// <summary>
    /// Example of how to limit the position and zoom the map.
    /// </summary>
    [AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/LockPositionAndZoomExample")]
    public class LockPositionAndZoomExample : MonoBehaviour
    {
        private void Start()
        {
            // Lock map zoom range
            OnlineMaps.instance.zoomRange = new OnlineMapsRange(15f , 21);

            // Lock map coordinates range
            OnlineMaps.instance.positionRange = new OnlineMapsPositionRange(52.13214f, -106.63443f, 52.1354f, -106.63001f);

            // Initializes the position and zoom
            OnlineMaps.instance.zoom = 10;
            OnlineMaps.instance.position = OnlineMaps.instance.positionRange.center;
        }
    }
}