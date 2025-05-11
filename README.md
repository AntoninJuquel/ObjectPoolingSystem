# ObjectPoolingSystem

**A dead-simple, no-setup object pooling system for Unity.**

This lightweight system lets you reuse GameObjects efficiently without ever touching the Inspector. It’s entirely code-driven: no configuration, no asset creation, and no prefab modification required.

---

## ✨ Features

* ⚡ **No setup required** — Just call `SpawnFromPool()` and you're done.
* 🌀 **Automatic pooling** — Objects are pooled on first use.
* 🧠 **Self-managed** — Automatically returned to the pool on `SetActive(false)`.
* 🧰 **Generic support** — Use `SpawnFromPool<T>()` to get components directly.
* 🧼 **Minimalist** — One static class, one helper component. That’s it.

---

## 🚀 Usage

### 🔄 Spawn a GameObject

```csharp
GameObject obj = ObjectPoolManager.SpawnFromPool(ballPrefab, position, rotation);
```

### 🔍 Spawn and get a component

```csharp
BallController ball = ObjectPoolManager.SpawnFromPool<BallController>(ballPrefab, position, Quaternion.identity);
```

### 🔁 Return an object manually (optional)

Usually this happens automatically on `SetActive(false)`:

```csharp
ObjectPoolManager.ReturnToPool(ballPrefab, ball.gameObject);
```

---

## ⚙️ How It Works

* The system keeps a queue for each prefab.
* If an object is available, it’s reused.
* If not, a new one is instantiated and tracked.
* A `ObjectPoolController` is auto-attached if the prefab doesn't have one.
* When the object is disabled, it returns to the pool via `OnDisable`.

---

## 📁 Structure

```
ObjectPoolingSystem/
├── ObjectPoolManager.cs
├── ObjectPoolController.cs
└── README.md
```

---

## ✅ Why Use This?

* Replace `Instantiate` / `Destroy` for frequently reused objects (e.g. bullets, particles, enemies).
* Greatly reduce memory allocation and GC overhead.
* Avoid runtime lag caused by frequent instantiation.

---

## 🧪 Example

```csharp
public void Fire()
{
    var projectile = ObjectPoolManager.SpawnFromPool<Projectile>(projectilePrefab, transform.position, transform.rotation);
    projectile.Launch();
}
```