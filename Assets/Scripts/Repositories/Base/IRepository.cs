using System;
using UnityEngine;

public interface IRepository<T>
{
    T Load();

    void Save(T data);

    bool Exists();

    void Delete();
}
