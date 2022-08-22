using UnityEngine;

[CreateAssetMenu(fileName = "ReferencesData", menuName = "ScriptableObject/ReferencesData", order = 0)]
public class ReferencesData : ScriptableObject
{
    [SerializeField] private Material _material;
    [SerializeField] private PhysicMaterial _physicMaterial ;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Sprite _sprite;
}