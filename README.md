# Algorithm Benchmarks – Unsorted vs Sorted Arrays

This project is a simple benchmark harness in C# that compares two array-based strategies for handling a collection of strings. It measures the time cost of **insert**, **search**, and **delete** operations for:

- **Option A (unsorted array)**  
  - Insert: Θ(1) – append at end  
  - Search: Θ(n) – linear scan  
  - Delete: Θ(n) – find and shift left  

- **Option B (sorted array)**  
  - Insert: Θ(n) – find position + shift right  
  - Search: Θ(log n) – binary search  
  - Delete: Θ(n) – binary search + shift left  

The trade-off: Option A is faster at building the list, while Option B is much faster at searches. Deletes are costly in both.

---

## How it works

- Generates `N` names (`"Name0000000" … "Name0099999"`).  
- Inserts them into either an unsorted or sorted array.  
- Performs a large number of random searches, then random deletions.  
- Uses a fixed random seed for reproducibility (`Random(42)`).  
- Times each operation with `Stopwatch` and prints results.

---

## Running the benchmark

Clone this repo, then:

```bash
dotnet new console -n AlgoBench        # if you want a fresh project
cd AlgoBench
# replace Program.cs with the one in this repo
dotnet run -c Release
