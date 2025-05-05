# Pool Connection Using Bounded Blocking Queue

This is a simple C# console application that demonstrates how to create and manage a **custom SQL connection pool** using a **bounded blocking queue** via `BlockingCollection<T>`. This helps control the maximum number of open SQL connections and prevents resource exhaustion in concurrent environments.

## 📌 Features

- Implements a thread-safe SQL connection pool.
- Limits the number of concurrent SQL connections.
- Demonstrates connection reuse using multiple parallel tasks.
- Simulates heavy database usage with controlled concurrency.

---

## 📁 Project Structure

```
Pool_Connection_Using_Bounded_Blocking_Queue/
│
├── DB.cs               # Implements the custom SQL connection pool
├── Program.cs          # Main entry point with multithreading demo
├── Pool_Connection_Using_Bounded_Blocking_Queue.csproj
└── README.md
```

---

## 🚀 How It Works

- `DB` class:
  - Initializes a pool of open `SqlConnection` objects using `BlockingCollection<SqlConnection>`.
  - Restricts the number of active connections based on the `maxPoolSize` you define.
  - Provides methods to acquire and release connections in a thread-safe manner.

- `Program.cs`:
  - Spawns multiple tasks (simulating concurrent DB access).
  - Each task acquires a connection from the pool, simulates usage via `Thread.Sleep`, and releases it back to the pool.
  - Ensures that no more than `maxPoolSize` connections are used at a time.

---

## 🛠️ Usage

1. **Clone the repository** or copy the source files into your own project.

2. **Set your SQL connection string** in `Program.cs`:
   ```csharp
   var connectionString = "your-sql-connection-string";
   ```

3. **Build and run the project**:
   ```bash
   dotnet build
   dotnet run
   ```

4. **Observe the console output**, where threads acquire and release connections respecting the pool size limit.

---

## 📦 Example Output

```text
Thread 3 acquired connection.
Thread 4 acquired connection.
Thread 3 released connection.
Thread 5 acquired connection.
Thread 4 released connection.
...
All tasks completed.
```

---

## ⚠️ Notes

- This is a **manual connection pooling** implementation for educational purposes.
- In production, **ADO.NET already provides built-in connection pooling**, which is more efficient and optimized.
- Always close or dispose SQL connections properly to avoid leaks.

---

## 📄 License

This project is open for educational and demonstrative purposes. No license is attached.