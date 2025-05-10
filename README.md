# ObjectPoolingSystem

**A lightweight, extensible object pooling solution for Unity.**

This system allows you to reuse GameObjects efficiently, improving performance by minimizing runtime instantiation and destruction. It handles pooling logic automatically, so you don’t need to manually attach components to your prefabs.

---

## 🚀 Features

* 🔁 Reuses GameObjects via pooling to reduce GC pressure and improve runtime performance.
* 🧠 Automatically attaches pooling logic — no setup required on prefabs.
* 🔌 Supports multiple independent pool managers.
* ⚙️ Extensible base class (`ObjectPoolManager`) for custom pooling logic.

---

## 🛠 Setup

### 1. Define Your Pools

In the Unity Editor:

* Add the `ObjectPoolManager` component to a GameObject.
* In the inspector, add elements to the `Object Pools` list:

  * `Key`: A unique string identifier.
  * `Prefab`: The GameObject to pool.
  * `Size`: Number of instances to preload.

### 2. Spawn Objects

To use the pool, either use `ObjectPoolManager` directly or subclass it for multiple poolers. Example:

```csharp
public class ProjectilePooler : ObjectPoolManager
{
    public void FireProjectile(Vector3 position, Quaternion rotation)
    {
        SpawnFromPool("projectile", position, rotation);
    }
}
```

> 🎯 You can also use `SpawnFromPool<T>()` to get a specific component directly.

### 3. Returning Objects

When a pooled object is disabled (e.g., via `SetActive(false)`), it automatically returns to its pool:

```csharp
gameObject.SetActive(false); // Triggers return to pool via ObjectPoolController
```

No need to manually manage return logic or attach `ObjectPoolController` — it's handled at runtime.

---

## 📁 Folder Structure

```
ObjectPoolingSystem/
├── ObjectPool.cs
├── ObjectPoolManager.cs
├── ObjectPoolController.cs
└── README.md
```

---

## ✅ Benefits

* Eliminates runtime `Instantiate()`/`Destroy()` calls for pooled objects.
* Auto-returns disabled objects to their pool.
* Debug mode to track pool behavior in the Editor.
* Simple integration — drop in and use.

---

## 🧪 Example Use Cases

* Projectile systems (bullets, arrows, etc.)
* Particle effects (explosions, dust clouds)
* NPCs or AI units with repeated spawning
* Interactive objects (collectibles, powerups)

---

## 🧩 Customization

If you want multiple independent pools:

* Subclass `ObjectPoolManager`
* Use custom logic on top of `SpawnFromPool()` and `ReturnToPool()`
