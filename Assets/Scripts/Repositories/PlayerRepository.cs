using System.IO;
using UnityEngine;

public class PlayerRepository : IRepository<PlayerModel>
{
    private readonly string path =
        Application.persistentDataPath + "/player.json";

    public void Save(PlayerModel player)
    {
        var json = JsonUtility.ToJson(player, true);
        File.WriteAllText(path, json);
    }

    public PlayerModel Load()
    {
        if (!Exists())
            return new PlayerModel("Harkyl", 100);

        var json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerModel>(json);
    }

    public bool Exists()
    {
        return File.Exists(path);
    }

    public void Delete()
    {
        if (Exists())
            File.Delete(path);
    }
}
