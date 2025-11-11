using System.Collections;
using UnityEngine;

public class CharacterRespawner
{
    private readonly Transform _respawnerTransform;
    private readonly Transform _respawnTransform;
    private readonly float _respawnTime;

    public CharacterRespawner(Transform respawnerTransform, Transform respawnTransform, float respawnTime)
    {
        _respawnerTransform = respawnerTransform;
        _respawnTransform = respawnTransform;
        _respawnTime = respawnTime;
    }

    public IEnumerator Respawn(System.Action onRespawnComplete)
    {
        yield return new WaitForSeconds(_respawnTime);
        _respawnerTransform.position = _respawnTransform.position;
        yield return new WaitForSeconds(_respawnTime);
        onRespawnComplete?.Invoke();
    }
}