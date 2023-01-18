using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public sealed class WindowStatisticsGameplay : Window
    {
        [SerializeField] private TextMeshProUGUI _textCoordinateSheep;
        [SerializeField] private TextMeshProUGUI _textAngleRotationSheep;
        [SerializeField] private TextMeshProUGUI _textVelocitySheep;
        [SerializeField] private TextMeshProUGUI _textAmmoLazer;
        [SerializeField] private TextMeshProUGUI _textTimeRefillAmmoLazer;

        public void UpdateCoordinateSheep(Vector3 position)
        {
            _textCoordinateSheep.text =
                $"x: {Mathf.Round(position.x)} y: {Mathf.Round(position.y)} z: {Mathf.Round(position.z)}";
        }

        public void UpdateAngleRotationSheep(Vector3 angle)
        {
            _textAngleRotationSheep.text =
                $"x: {Mathf.Round(angle.x)} y: {Mathf.Round(angle.y)} z: {Mathf.Round(angle.z)}";
        }

        public void UpdateVelocitySheep(float velocity)
        {
            _textVelocitySheep.text = $"{Math.Round(velocity, 2)}";
        }

        public void UpdateAmmoLazer(int ammo)
        {
            _textAmmoLazer.text = $"{ammo}";
        }

        public void UpdateCooldownLazer(float cooldown)
        {
            _textTimeRefillAmmoLazer.text = $"{Math.Round(cooldown, 2)}";
        }
    }
}
