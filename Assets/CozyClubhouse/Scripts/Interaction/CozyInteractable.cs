using CozyClubhouse.Player;
using UnityEngine;

namespace CozyClubhouse.Interaction
{
    public class CozyInteractable : MonoBehaviour
    {
        [SerializeField] private string activity = "Hanging out";
        [SerializeField] private Transform snapPoint;

        private void OnMouseDown()
        {
            var player = FindFirstObjectByType<CozyPlayerController>();
            if (!player) return;

            if (snapPoint) player.SnapTo(snapPoint);
            player.SetActivity(activity);
        }
    }
}
